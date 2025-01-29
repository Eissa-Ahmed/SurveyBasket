using SurveyBasket.Api.Contracts.Authentication;

namespace SurveyBasket.Api.Validations.Auth;

public class RefreshTokenRequestValidation : AbstractValidator<RefreshTokenRequestModel>
{
    public RefreshTokenRequestValidation()
    {
        RuleFor(x => x.Token).NotEmpty();
        RuleFor(x => x.RefreshToken).NotEmpty();
    }
}

