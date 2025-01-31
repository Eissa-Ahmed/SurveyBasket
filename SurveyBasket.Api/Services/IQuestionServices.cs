using SurveyBasket.Api.Contracts.Question;

namespace SurveyBasket.Api.Services;

public interface IQuestionServices
{
    Task<AppResponse<List<QuestionResponse>>> GetAllAsync(int pollId, CancellationToken cancellationToken = default);
    Task<AppResponse<QuestionResponse>> GetAsync(int id, CancellationToken cancellationToken = default);
    Task<AppResponse<QuestionResponse>> AddAsync(int pollId, QuestionRequest questionRequest, CancellationToken cancellationToken = default);
    Task<AppResponse> UpdateAsync(int id, QuestionRequest questionRequest, CancellationToken cancellationToken = default);
}
