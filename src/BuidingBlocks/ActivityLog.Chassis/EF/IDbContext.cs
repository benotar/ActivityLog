using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ActivityLog.Chassis.EF;

public interface IDbContext : IAsyncDisposable
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    DatabaseFacade Database { get; }
}
