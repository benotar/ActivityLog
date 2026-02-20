namespace ActivityLog.SharedKernel.Domain;

public class Result<T>
{
    public T? Data { get; set; }

    public ErrorCode? ErrorCode { get; set; }
    
    public bool IsSuccess { get; init; }

    private Result(T? data)
    {
        IsSuccess = true;
        Data = data;
        ErrorCode = null;
    }

    private Result(ErrorCode? errorCode)
    {
        IsSuccess = false;
        Data = default;
        ErrorCode = errorCode;
    }
    
    public static Result<T> Success(T? data) => new Result<T>(data);
    
    public static implicit operator Result<T>(T? data) => new Result<T>(data);

    public static implicit operator Result<T>(ErrorCode error) => new Result<T>(error);
}

public record struct Unit;