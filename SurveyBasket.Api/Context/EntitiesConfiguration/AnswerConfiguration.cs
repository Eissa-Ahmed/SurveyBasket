namespace SurveyBasket.Api.Context.EntitiesConfiguration;

public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
{
    public void Configure(EntityTypeBuilder<Answer> builder)
    {
        builder.Property(i => i.Content).HasMaxLength(1000).IsRequired();
        builder.HasIndex(i => new { i.QuestionId, i.Content }).IsUnique();
    }
}
