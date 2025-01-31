namespace SurveyBasket.Api.Middlewares;

public class ValidationExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ValidationExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            // إذا حدث خطأ تحقق، أرجع رد مخصص
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";

            var response = AppResponse.Failure(
                error: string.Join("; ", ex.Errors.Select(e => e.ErrorMessage))
            );

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
