using ActivityLog.Chassis.EF;
using ActivityLog.Constants.Aspire;
using ActivityLog.IdentityService.Infrastructure;
using ActivityLog.SharedKernel.Extensions;
using Microsoft.AspNetCore.Identity;

namespace ActivityLog.IdentityService;

public static class DependencyInjection
{
    public static void AddPersistenceLayer(this IHostApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString(Components.Postgres.Identity);

        if (connectionString!.IsNullOrEmpty())
        {
            throw new InvalidOperationException($"The {Components.Postgres.Identity} connection string is empty");
        }

        //builder.Services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(connectionString));
        
        builder.AddPostgresDbContext<DatabaseContext>(
            connectionString!,
            app => app.Services.AddMigration<IDbContext, DatabaseContextSeed>());

        builder.Services.AddIdentity<IdentityUser<Guid>, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<DatabaseContext>();
        
        builder.Services.AddScoped<IDbContext>(sp => sp.GetRequiredService<DatabaseContext>());
    }
}
