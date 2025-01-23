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
        return Ok(_pollServices.GetAll());
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var poll = _pollServices.Get(id);

        return poll is null ? NotFound() : Ok(poll);

    }

    [HttpPost]
    public IActionResult Add(Poll poll)
    {
        var result = _pollServices.Add(poll);

        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Poll poll)
    {
        var isUpdated = _pollServices.Update(id, poll);

        return isUpdated ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var IsDeleted = _pollServices.Delete(id);

        return IsDeleted ? NoContent() : NotFound();
    }
}
