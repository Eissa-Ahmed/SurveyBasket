

namespace SurveyBasket.Api.Services;

public class PollServices(ApplicationDbContext context) : IPollServices
{
    private readonly ApplicationDbContext _context = context;

    public async Task<List<Poll>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Polls.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<Poll?> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Polls.FindAsync(id, cancellationToken);
    }

    public async Task<Poll> AddAsync(Poll poll, CancellationToken cancellationToken = default)
    {
        await _context.Polls.AddAsync(poll, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return poll;
    }

    public async Task<bool> UpdateAsync(int id, Poll poll, CancellationToken cancellationToken)
    {
        var existing = await GetAsync(id, cancellationToken);
        if (existing == null) return false;

        existing.Title = poll.Title;
        existing.Summary = poll.Summary;
        existing.IsPublished = poll.IsPublished;
        existing.StartsAt = poll.StartsAt;
        existing.EndsAt = poll.EndsAt;

        _context.Polls.Update(existing);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var poll = await _context.Polls.FindAsync(id, cancellationToken);
        if (poll == null) return false;

        _context.Polls.Remove(poll);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
