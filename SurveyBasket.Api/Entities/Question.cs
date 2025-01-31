namespace SurveyBasket.Api.Entities;

public class Question : AuditableEntity
{
    public int Id { get; set; }
    public string Content { get; set; } = null!;
    public bool IsActive { get; set; } = true;

    public int PollId { get; set; }
    public virtual Poll Poll { get; set; } = null!;
    public virtual ICollection<Answer> Answers { get; set; } = [];
}
