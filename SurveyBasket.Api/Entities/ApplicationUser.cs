namespace SurveyBasket.Api.Entities;

public class ApplicationUser : IdentityUser<int>
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public ICollection<RefreshToken> RefreshTokens { get; set; } = [];
}
