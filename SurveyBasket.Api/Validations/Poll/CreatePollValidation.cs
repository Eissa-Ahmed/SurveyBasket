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

        RuleFor(i => i.Summary)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .Length(3, 500)
            .WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters");

        RuleFor(i => i.StartsAt)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
            .WithMessage("{PropertyName} must be greater than or equal to today");

        RuleFor(i => i.EndsAt)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .Must((model, key, CancellationToken) => model.StartsAt < model.EndsAt)
            .WithMessage("{PropertyName} must be greater than starts at");

    }
}
