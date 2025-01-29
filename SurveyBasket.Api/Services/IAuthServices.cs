
using SurveyBasket.Api.Contracts.Authentication;

namespace SurveyBasket.Api.Services;

public interface IAuthServices
{
    Task<AuthenticationResponse?> LoginAsync(string email, string password, CancellationToken cancellationToken = default);
    Task<AuthenticationResponse?> RefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default);
}
