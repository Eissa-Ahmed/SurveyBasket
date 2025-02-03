namespace SurveyBasket.Api.Entities;

public class Vote
{
    public int Id { get; set; }
    public int PollId { get; set; }
    public virtual Poll Poll { get; set; } = null!;
    public int UserId { get; set; }
    public virtual ApplicationUser User { get; set; } = null!;
    public DateTime SubmitedOn { get; set; } = DateTime.UtcNow;

    public ICollection<VoteAnswer> VoteAnswers { get; set; } = [];
}
public class VoteAnswer
{
    public int Id { get; set; }
    public int VoteId { get; set; }
    public virtual Vote Vote { get; set; } = null!;
    public int AnswerId { get; set; }
    public virtual Answer Answer { get; set; } = null!;
    public int QuestionId { get; set; }
    public virtual Question Question { get; set; }
}
