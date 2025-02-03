namespace SurveyBasket.Api.Context.EntitiesConfiguration;

public class VoteAnswerConfiguration : IEntityTypeConfiguration<VoteAnswer>
{
    public void Configure(EntityTypeBuilder<VoteAnswer> builder)
    {
        builder.HasIndex(i => new { i.AnswerId, i.VoteId }).IsUnique();
    }
}