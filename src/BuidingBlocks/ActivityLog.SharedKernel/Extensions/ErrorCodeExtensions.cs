using ActivityLog.SharedKernel.Domain;

namespace ActivityLog.SharedKernel.Extensions;

public static class ErrorCodeExtensions
{
    public static string ToMessage(this ErrorCode? errorCode)
    {
        return errorCode switch
        {
            ErrorCode.NothingToDelete => "There is nothing to delete",
            ErrorCode.AlreadyExists => "The record already exist in the database",
            ErrorCode.NotFound => "The record doesn't exist in the database",
            _ => "An unexpected error occurred"
        };
    }
}
