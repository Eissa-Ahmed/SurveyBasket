using SurveyBasket.Api.Contracts.Poll.Responses;

namespace SurveyBasket.Api.Mapping;

public class MappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Poll, PollResponse>()
            .Map(dest => dest.Id, src => src.Id);
    }
}
