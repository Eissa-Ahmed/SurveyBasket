namespace SurveyBasket.Api.Entities;

public class Answer
{
    public int Id { get; set; }
    public string Content { get; set; } = null!;
    public bool IsActive { get; set; } = true;

    public int QuestionId { get; set; }
    public virtual Question Question { get; set; } = null!;
}
