
namespace SurveyBasket.Api.Services;

public class PollServices : IPollServices
{
    private static readonly List<Poll> _polls =
        [
            new Poll {Id = 1,Title = "What is your name?" , Description = "Enter Your Name."},
            new Poll {Id = 2,Title = "What is your age?" , Description = "Enter Your Age."},
            new Poll {Id = 3,Title = "What is your anything?" , Description = "Enter Your Anything."},
        ];

    public List<Poll> GetAll() => _polls;

    public Poll? Get(int id) => _polls.SingleOrDefault(i => i.Id == id);

    public Poll Add(Poll poll)
    {
        poll.Id = _polls.Count + 1;
        _polls.Add(poll);
        return poll;
    }

    public bool Update(int id, Poll poll)
    {
        var currentPoll = _polls.SingleOrDefault(i => i.Id == id);
        if (currentPoll is null)
            return false;

        currentPoll.Title = poll.Title;
        currentPoll.Description = poll.Description;

        return true;
    }

    public bool Delete(int id)
    {
        var currentPoll = _polls.SingleOrDefault(i => i.Id == id);
        if (currentPoll is null)
            return false;

        _polls.Remove(currentPoll);
        return true;
    }
}
