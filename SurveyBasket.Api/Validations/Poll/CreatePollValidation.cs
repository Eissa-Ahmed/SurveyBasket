using FluentValidation;
using SurveyBasket.Api.Contracts.Poll.Requests;

namespace SurveyBasket.Api.Validations.Poll;

public class CreatePollValidation : AbstractValidator<CreatePollRequest>
{
    public CreatePollValidation()
    {
        RuleFor(i => i.Title)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .Length(3, 100)
            .WithMessage("Title must be between {MinLength} and {MaxLength} characters");

        RuleFor(i => i.Description)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .Length(3, 500)
            .WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters");
    }
}
