namespace SurveyBasket.Api.Context.EntitiesConfiguration;

public class VoteConfiguration : IEntityTypeConfiguration<Vote>
{
    public void Configure(EntityTypeBuilder<Vote> builder)
    {
        builder.HasIndex(i => new { i.PollId, i.UserId }).IsUnique();
    }
}
