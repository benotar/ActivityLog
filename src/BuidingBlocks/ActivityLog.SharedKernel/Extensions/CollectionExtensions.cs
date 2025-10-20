namespace ActivityLog.SharedKernel.Extensions;

public static class CollectionExtensions
{
    public static bool IsEmpty<TCollection>(this IEnumerable<TCollection>? collection)
    {
        return collection is null || !collection.Any();
    }
    
    public static bool IsNotEmpty<TCollection>(this IEnumerable<TCollection>? collection)
    {
        return !collection.IsEmpty();
    }
}
