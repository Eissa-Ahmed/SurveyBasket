using SurveyBasket.Api.Contracts.Authentication;
using System.Security.Cryptography;

namespace SurveyBasket.Api.Services;

public class AuthServices(UserManager<ApplicationUser> userManager, IJwtProvider jwtProvider) : IAuthServices
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IJwtProvider _jwtProvider = jwtProvider;
    private readonly int _refreshTokenExpiration = 30;
    public async Task<AuthenticationResponse?> LoginAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null) return null;

        var result = await _userManager.CheckPasswordAsync(user, password);
        if (!result) return null;

        var (token, expires) = _jwtProvider.GenerateJwtToken(user);

        var refreshToken = GenerateRefreshToken();
        var refreshTokenExpires = DateTime.UtcNow.AddDays(_refreshTokenExpiration);

        user.RefreshTokens.Add(new RefreshToken() { Token = refreshToken, ExpiresOn = refreshTokenExpires });
        await _userManager.UpdateAsync(user);

        return new AuthenticationResponse(user.Id, user.UserName!, user.Email!, token, expires, refreshToken, refreshTokenExpires);
    }

    public async Task<AuthenticationResponse?> RefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default)
    {
        var userId = _jwtProvider.ValidateToken(token);
        if (userId is null)
            return null;

        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
            return null;

        var refreshTokenFromUser = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken && x.IsActive);
        if (refreshTokenFromUser is null)
            return null;

        refreshTokenFromUser.RevokedOn = DateTime.UtcNow;

        var newRefreshToken = GenerateRefreshToken();
        var newRefreshTokenExpires = DateTime.UtcNow.AddDays(_refreshTokenExpiration);

        var (newToken, newExpires) = _jwtProvider.GenerateJwtToken(user);
        await _userManager.UpdateAsync(user);

        return new AuthenticationResponse(user.Id, user.UserName!, user.Email!, newToken, newExpires, newRefreshToken, newRefreshTokenExpires);
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}
