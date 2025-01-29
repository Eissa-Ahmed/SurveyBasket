using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

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
        return Ok(polls.Adapt<IEnumerable<PollResponse>>());

    }

    //[DisableCors]
    [EnableCors("AllowAll2")]
    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id, CancellationToken cancellationToken = default)
    {
        var poll = await _pollServices.GetAsync(id, cancellationToken);

        return poll is null ? NotFound() : Ok(poll.Adapt<PollResponse>());
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreatePollRequest poll, CancellationToken cancellationToken)
    {
        var result = await _pollServices.AddAsync(poll.Adapt<Poll>(), cancellationToken);

        return CreatedAtAction(nameof(Get), new { id = result.Id }, result.Adapt<PollResponse>());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePollRequest poll, CancellationToken cancellationToken)
    {
        var isUpdated = await _pollServices.UpdateAsync(id, poll.Adapt<Poll>(), cancellationToken);

        return isUpdated ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
    {
        var IsDeleted = await _pollServices.DeleteAsync(id, cancellationToken);

        return IsDeleted ? NoContent() : NotFound();
    }
}
