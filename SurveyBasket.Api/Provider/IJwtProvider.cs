namespace SurveyBasket.Api.Provider;

public interface IJwtProvider
{
    (string token, int expires) GenerateJwtToken(ApplicationUser user);
    string? ValidateToken(string token);
}
