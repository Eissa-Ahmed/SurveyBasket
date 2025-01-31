using Microsoft.AspNetCore.Authorization;
using SurveyBasket.Api.Contracts.Question;

namespace SurveyBasket.Api.Controllers;

[Route("api/polls/{pollId}/[controller]")]
[ApiController]
[Authorize]
public class QuestionController(IQuestionServices _services) : ControllerBase
{
    [HttpGet("")]
    public async Task<IActionResult> GetAllQuestions([FromRoute] int pollId, CancellationToken cancellationToken = default)
    {
        var poll = await _services.GetAllAsync(pollId, cancellationToken);
        return poll.IsSuccess ? Ok(poll) : NotFound(poll);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetQuestion([FromRoute] int id, CancellationToken cancellationToken = default)
    {
        var poll = await _services.GetAsync(id, cancellationToken);
        return poll.IsSuccess ? Ok(poll) : NotFound(poll);
    }

    [HttpPost("")]
    public async Task<IActionResult> AddQuestion([FromRoute] int pollId, [FromBody] QuestionRequest question, CancellationToken cancellationToken)
    {
        var response = await _services.AddAsync(question.PollId, question, cancellationToken);
        return response.IsSuccess ? CreatedAtAction(nameof(GetQuestion), new { pollId = question.PollId, id = response.Data!.Id }, response) : BadRequest(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateQuestion([FromRoute] int id, [FromBody] QuestionRequest question, CancellationToken cancellationToken)
    {
        var response = await _services.UpdateAsync(id, question, cancellationToken);
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }
}
