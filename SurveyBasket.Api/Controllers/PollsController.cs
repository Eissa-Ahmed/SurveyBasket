using Microsoft.AspNetCore.Authorization;

namespace SurveyBasket.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PollsController(IPollServices pollServices) : ControllerBase
{
    private readonly IPollServices _pollServices = pollServices;

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var polls = await _pollServices.GetAllAsync(cancellationToken);
        return polls.IsSuccess ? Ok(polls) : BadRequest(polls);

    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id, CancellationToken cancellationToken = default)
    {
        var poll = await _pollServices.GetAsync(id, cancellationToken);

        return poll.IsSuccess ? Ok(poll) : NotFound(poll);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreatePollRequest poll, CancellationToken cancellationToken)
    {
        var response = await _pollServices.AddAsync(poll.Adapt<Poll>(), cancellationToken);
        return response.IsSuccess
            ? CreatedAtAction(nameof(Get), new { id = response.Data!.Id }, response)
            : BadRequest(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePollRequest poll, CancellationToken cancellationToken)
    {
        var response = await _pollServices.UpdateAsync(id, poll.Adapt<Poll>(), cancellationToken);

        return response.IsSuccess ? Ok(response) : NotFound(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
    {
        var response = await _pollServices.DeleteAsync(id, cancellationToken);

        return response.IsSuccess ? Ok(response) : NotFound(response);
    }
}
