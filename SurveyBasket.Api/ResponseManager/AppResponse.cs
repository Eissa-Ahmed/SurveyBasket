namespace SurveyBasket.Api.ResponseManager;

public class AppResponse
{
    public AppResponse(bool isSuccess, string error = "")
    {
        IsSuccess = isSuccess;
        Error = error;
    }
    public bool IsSuccess { get; set; }
    public string Error { get; set; } = string.Empty;

    public static AppResponse Success(string message = "") => new(true, message);

    public static AppResponse Failure(string error = "") => new(false, error: error);

    public static AppResponse<T> Success<T>(T data, string message = "") => new(data, true, message);

    public static AppResponse<T> Failure<T>(string error = "") => new(default, false, error: error);
}

public class AppResponse<T>(T? data, bool isSuccess, string message = "", string error = "") : AppResponse(isSuccess, error)
{
    public string Message { get; set; } = message;
    public T? Data => data;
}
