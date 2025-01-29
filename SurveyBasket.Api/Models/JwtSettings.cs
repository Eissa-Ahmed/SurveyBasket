using System.ComponentModel.DataAnnotations;

namespace SurveyBasket.Api.Models;

public class JwtSettings
{
    [Required]
    public string Key { get; init; } = null!;
    [Required]
    public string Issuer { get; init; } = null!;
    [Required]
    public string Audience { get; init; } = null!;
    [Range(1, int.MaxValue)]
    public int ExpiresIn { get; init; }
}

