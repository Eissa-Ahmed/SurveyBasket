namespace SurveyBasket.Api.Context.EntitiesConfiguration;

public class PollConfiguration : IEntityTypeConfiguration<Poll>
{
    public void Configure(EntityTypeBuilder<Poll> builder)
    {
        builder.HasKey(i => i.Id);

        builder.HasIndex(i => i.Title)
            .IsUnique();

        builder.Property(i => i.Title)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(i => i.Summary)
            .HasMaxLength(500);
    }
}
