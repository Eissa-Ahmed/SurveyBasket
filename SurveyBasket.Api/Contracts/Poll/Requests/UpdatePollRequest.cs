namespace SurveyBasket.Api.Contracts.Poll.Requests;

public record UpdatePollRequest(
    string Title,
    bool IsPublished,
    DateOnly StartsAt,
    DateOnly EndsAt,
    string? Summary = null
    );
