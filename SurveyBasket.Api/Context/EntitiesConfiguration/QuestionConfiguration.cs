namespace SurveyBasket.Api.Context.EntitiesConfiguration;

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.Property(i => i.Content).HasMaxLength(1000).IsRequired();
        builder.HasIndex(i => new { i.PollId, i.Content }).IsUnique();
    }
}
