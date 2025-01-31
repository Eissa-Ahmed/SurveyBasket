namespace SurveyBasket.Api.Contracts.Question;

public record QuestionRequest(
    string Content,
    int PollId,
    List<string> Answers);

