using SurveyBasket.Api.Contracts.Authentication;

namespace SurveyBasket.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthenticationController(IAuthServices authServices) : ControllerBase
{
    private readonly IAuthServices _authServices = authServices;

    [HttpPost]
    public async Task<IActionResult> LoginAsync(LoginRequestModel request, CancellationToken cancellationToken)
    {
        var response = await _authServices.LoginAsync(request.Email, request.Password, cancellationToken);
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshTokenAsync(RefreshTokenRequestModel request, CancellationToken cancellationToken)
    {
        var response = await _authServices.RefreshTokenAsync(request.Token, request.RefreshToken, cancellationToken);
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }
}
