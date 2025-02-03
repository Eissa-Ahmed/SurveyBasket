namespace SurveyBasket.Api.Entities;

public class Poll : AuditableEntity
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Summary { get; set; } = null;
    public bool IsPublished { get; set; }
    public DateOnly StartsAt { get; set; }
    public DateOnly EndsAt { get; set; }

    public virtual ICollection<Question> Questions { get; set; } = [];
    public virtual ICollection<Vote> Votes { get; set; } = [];
}
