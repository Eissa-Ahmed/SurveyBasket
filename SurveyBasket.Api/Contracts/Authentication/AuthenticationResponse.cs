namespace SurveyBasket.Api.Contracts.Authentication;

public record AuthenticationResponse(
    int Id,
    string UserName,
    string Email,
    string Token,
    int ExpiresIn,
    string RefreshToken,
    DateTime RefreshTokenExpiresIn
    );
