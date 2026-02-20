using ActivityLog.Chassis.EF;
using ActivityLog.Constants.Aspire;
using ActivityLog.SharedKernel.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ActivityLog.IdentityService.Infrastructure;

public class DatabaseContext(DbContextOptions<DatabaseContext> options)
    : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>(options), IDbContext
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasDefaultSchema(Components.Postgres.IdentitySchema);
    }
}

public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
{
    public DatabaseContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true)
            .Build();

        var connectionStringBase = configuration.GetConnectionString("DefaultConnection");

        if (connectionStringBase!.IsNullOrWhiteSpace())
        {
            connectionStringBase = "Host=localhost;Database={0};Username={1};Password={2}";
        }

        connectionStringBase = string.Format(connectionStringBase!,
            Components.Postgres.Identity,
            Components.Postgres.User,
            Components.Postgres.Password);

        return new DatabaseContext(new DbContextOptionsBuilder<DatabaseContext>()
            .UseNpgsql(connectionStringBase)
            .Options);
    }
}
