namespace SurveyBasket.Api.Services;

public class AuthServices(UserManager<ApplicationUser> userManager, IJwtProvider jwtProvider) : IAuthServices
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IJwtProvider _jwtProvider = jwtProvider;
    private readonly int _refreshTokenExpiration = 30;

    public async Task<AppResponse<AuthenticationResponse>> LoginAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            return AppResponse.Failure<AuthenticationResponse>(error: AuthenticationError.USER_NOT_FOUND);

        var result = await _userManager.CheckPasswordAsync(user, password);
        if (!result)
            return AppResponse.Failure<AuthenticationResponse>(error: AuthenticationError.Credentials_INCORRECT);

        var (token, expires) = _jwtProvider.GenerateJwtToken(user);

        var refreshToken = GenerateRefreshToken();
        var refreshTokenExpires = DateTime.UtcNow.AddDays(_refreshTokenExpiration);

        user.RefreshTokens.Add(new RefreshToken() { Token = refreshToken, ExpiresOn = refreshTokenExpires });
        await _userManager.UpdateAsync(user);

        var respoonse = new AuthenticationResponse(user.Id, user.UserName!, user.Email!, token, expires, refreshToken, refreshTokenExpires);
        return AppResponse.Success(respoonse);
    }

    public async Task<AppResponse<AuthenticationResponse>> RefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default)
    {
        var userId = _jwtProvider.ValidateToken(token);
        if (userId is null)
            return AppResponse.Failure<AuthenticationResponse>(error: AuthenticationError.INVALID_TOKEN);

        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
            return AppResponse.Failure<AuthenticationResponse>(error: AuthenticationError.USER_NOT_FOUND);

        var refreshTokenFromUser = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken && x.IsActive);
        if (refreshTokenFromUser is null)
            return AppResponse.Failure<AuthenticationResponse>(error: AuthenticationError.INVALID_TOKEN);

        refreshTokenFromUser.RevokedOn = DateTime.UtcNow;

        var newRefreshToken = GenerateRefreshToken();
        var newRefreshTokenExpires = DateTime.UtcNow.AddDays(_refreshTokenExpiration);

        var (newToken, newExpires) = _jwtProvider.GenerateJwtToken(user);
        await _userManager.UpdateAsync(user);

        var response = new AuthenticationResponse(user.Id, user.UserName!, user.Email!, newToken, newExpires, newRefreshToken, newRefreshTokenExpires);
        return AppResponse.Success(response);
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}
