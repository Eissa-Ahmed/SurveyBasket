using SurveyBasket.Api.Contracts.Authentication;

namespace SurveyBasket.Api.Validations.Auth;

public class LoginValidation : AbstractValidator<LoginRequestModel>
{
    public LoginValidation()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
    }
}

