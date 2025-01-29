namespace SurveyBasket.Api.Contracts.Poll.Requests;

public record UpdatePollRequest(int Id, string Title, string Description);
