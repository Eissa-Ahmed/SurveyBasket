namespace SurveyBasket.Api.Entities;

public class AuditableEntity
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int CreatedById { get; set; }
    public DateTime? UpdatedAt { get; set; } = null;
    public int? UpdatedById { get; set; } = null;
    public ApplicationUser CreatedBy { get; set; } = null!;
    public ApplicationUser? UpdatedBy { get; set; }
}
