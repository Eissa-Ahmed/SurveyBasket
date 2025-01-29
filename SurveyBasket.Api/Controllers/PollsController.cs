using Mapster;
using SurveyBasket.Api.Contracts.Poll.Requests;
using SurveyBasket.Api.Contracts.Poll.Responses;
using SurveyBasket.Api.Services;

namespace SurveyBasket.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PollsController(IPollServices pollServices) : ControllerBase
{
    private readonly IPollServices _pollServices = pollServices;

    [HttpGet]
    public IActionResult GetAll()
    {
        var polls = _pollServices.GetAll();
        return Ok(polls.Adapt<List<PollResponse>>());
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var poll = _pollServices.Get(id);

        return poll is null ? NotFound() : Ok(poll.Adapt<PollResponse>());

    }

    [HttpPost]
    public IActionResult Add(CreatePollRequest poll)
    {
        var result = _pollServices.Add(poll.Adapt<Poll>());

        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public IActionResult Update(UpdatePollRequest poll)
    {
        var isUpdated = _pollServices.Update(poll.Id, poll.Adapt<Poll>());

        return isUpdated ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var IsDeleted = _pollServices.Delete(id);

        return IsDeleted ? NoContent() : NotFound();
    }
}
