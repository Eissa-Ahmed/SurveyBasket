using SurveyBasket.Api.Contracts.Question;

namespace SurveyBasket.Api.Validations.Question;

public class QuestionRequestValidation : AbstractValidator<QuestionRequest>
{
    public QuestionRequestValidation()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Content)
            .NotEmpty();

        RuleFor(x => x.PollId)
            .GreaterThan(0)
            .NotEmpty();

        RuleFor(x => x.Answers)
            .NotEmpty();

        RuleFor(x => x.Answers)
            .Must(x => x != null && x.Count > 1)
            .WithMessage("Question must have at least 2 answers")
            .When(x => x.Answers != null);

        RuleFor(x => x.Answers)
            .Must(x => x.Distinct().Count() == x.Count)
            .WithMessage("Answers must be unique")
            .When(x => x.Answers != null);
    }
}
