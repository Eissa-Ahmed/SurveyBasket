

namespace SurveyBasket.Api.Services;

public class PollServices(ApplicationDbContext _context) : IPollServices
{

    public async Task<AppResponse<List<PollResponse>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var polls = await _context.Polls.AsNoTracking().ToListAsync(cancellationToken);
        return AppResponse.Success(polls.Adapt<List<PollResponse>>().ToList(), SharedMessage.Success);
    }

    public async Task<AppResponse<PollResponse>> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        var poll = await _context.Polls.FindAsync(id, cancellationToken);
        if (poll is null)
            return AppResponse.Failure<PollResponse>(error: PollError.POLL_NOT_FOUND);

        return AppResponse.Success(poll.Adapt<PollResponse>(), SharedMessage.Success);
    }

    public async Task<AppResponse<PollResponse>> AddAsync(Poll poll, CancellationToken cancellationToken = default)
    {
        bool isExists = await _context.Polls.AnyAsync(x => x.Title == poll.Title, cancellationToken);
        if (isExists)
            return AppResponse.Failure<PollResponse>(error: PollError.POLL_ALREADY_EXISTS);

        await _context.Polls.AddAsync(poll, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return AppResponse.Success(poll.Adapt<PollResponse>(), SharedMessage.Success);
    }

    public async Task<AppResponse> UpdateAsync(int id, Poll poll, CancellationToken cancellationToken)
    {
        var existing = await _context.Polls.FindAsync(id, cancellationToken);
        if (existing == null)
            return AppResponse.Failure(error: PollError.POLL_NOT_FOUND);

        existing.Title = poll.Title;
        existing.Summary = poll.Summary;
        existing.IsPublished = poll.IsPublished;
        existing.StartsAt = poll.StartsAt;
        existing.EndsAt = poll.EndsAt;

        _context.Polls.Update(existing);
        await _context.SaveChangesAsync(cancellationToken);
        return AppResponse.Success(SharedMessage.Success);
    }

    public async Task<AppResponse> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var poll = await _context.Polls.FindAsync(id, cancellationToken);
        if (poll == null)
            return AppResponse.Failure(error: PollError.POLL_NOT_FOUND);

        _context.Polls.Remove(poll);
        await _context.SaveChangesAsync(cancellationToken);
        return AppResponse.Success(SharedMessage.Success);
    }
}
