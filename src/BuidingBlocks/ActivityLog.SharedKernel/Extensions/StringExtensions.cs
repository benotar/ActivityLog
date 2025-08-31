namespace ActivityLog.SharedKernel.Extensions;

public static class StringExtensions
{
    public static bool IsNullOrEmpty(this string value)
    {
        return string.IsNullOrEmpty(value);
    }
    
    public static bool IsNullOrWhiteSpace(this string value)
    {
        return string.IsNullOrWhiteSpace(value);
    }

    public static bool IsEmpty(this Guid value)
    {
        return value == Guid.Empty;
    }
}
