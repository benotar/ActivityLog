using ActivityLog.SharedKernel.Domain;

namespace ActivityLog.Chassis.EF;

public static class QueryableExtensions
{
    public static IQueryable<TEntity> ByName<TEntity>(this IQueryable<TEntity> query, string name)
    where TEntity : class, IName
    {
        return query.Where(e => e.Name == name);
    }
}
