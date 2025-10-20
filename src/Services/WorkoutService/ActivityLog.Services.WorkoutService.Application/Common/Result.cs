namespace ActivityLog.Services.WorkoutService.Application.Common;

public class Result<T>
{
    public T? Data { get; set; }

    public string? ErrorMessage { get; set; }
    
    public bool IsSuccess { get; init; }

    private Result(T? data)
    {
        IsSuccess = true;
        Data = data;
        ErrorMessage = null;
    }

    private Result(string? errorMessage)
    {
        IsSuccess = false;
        Data = default;
        ErrorMessage = errorMessage;
    }
    
    public static implicit operator Result<T>(T? data) => new Result<T>(data);

    public static implicit operator Result<T>(string error) => new Result<T>(error);
}
