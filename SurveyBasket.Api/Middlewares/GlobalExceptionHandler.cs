using Microsoft.AspNetCore.Diagnostics;

namespace SurveyBasket.Api.Middlewares;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> _logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
       // _logger.LogError("حدث خطا ما: {Message}", exception.Message);
        var response = AppResponse.Failure(
            error: exception.Message
        );

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await httpContext.Response.WriteAsJsonAsync(response);
        return true;
    }
}
