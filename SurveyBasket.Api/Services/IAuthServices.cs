
using SurveyBasket.Api.Contracts.Authentication;
using SurveyBasket.Api.ResponseManager;

namespace SurveyBasket.Api.Services;

public interface IAuthServices
{
    Task<AppResponse<AuthenticationResponse>> LoginAsync(string email, string password, CancellationToken cancellationToken = default);
    Task<AppResponse<AuthenticationResponse>> RefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default);
}
