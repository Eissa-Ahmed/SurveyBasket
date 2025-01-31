namespace SurveyBasket.Api.Services;

public interface IPollServices
{
    Task<AppResponse<List<PollResponse>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<AppResponse<PollResponse>> GetAsync(int id, CancellationToken cancellationToken = default);
    Task<AppResponse<PollResponse>> AddAsync(Poll poll, CancellationToken cancellationToken = default);
    Task<AppResponse> UpdateAsync(int id, Poll poll, CancellationToken cancellationToken = default);
    Task<AppResponse> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
