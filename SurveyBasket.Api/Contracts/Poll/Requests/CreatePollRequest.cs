namespace SurveyBasket.Api.Contracts.Poll.Requests;

public record CreatePollRequest(
    string Title,
    bool IsPublished,
    DateOnly StartsAt,
    DateOnly EndsAt,
    string? Summary = null
    );
