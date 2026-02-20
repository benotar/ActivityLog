namespace ActivityLog.SharedKernel.Domain;

public enum ErrorCode
{
    Unknown = 0,
    NothingToDelete = 1,
    AlreadyExists = 2,
    NotFound = 3,
}
