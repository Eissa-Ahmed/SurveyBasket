using SurveyBasket.Api.Contracts.Question;

namespace SurveyBasket.Api.Mapping;

public class MappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Poll, PollResponse>()
            .Map(dest => dest.Id, src => src.Id);

        config.NewConfig<QuestionRequest, Question>()
            .Map(dest => dest.Answers, src => src.Answers.Select(a => new Answer { Content = a }).ToList());

        config.NewConfig<Question, QuestionRequest>()
            .Map(dest => dest.Answers, src => src.Answers.Select(a => a.Content).ToList());
    }
}
