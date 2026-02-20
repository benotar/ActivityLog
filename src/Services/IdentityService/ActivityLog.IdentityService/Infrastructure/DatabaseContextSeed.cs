using ActivityLog.Chassis.EF;
using ActivityLog.IdentityService.Domain;
using Microsoft.AspNetCore.Identity;

namespace ActivityLog.IdentityService.Infrastructure;

public class DatabaseContextSeed : IDbSeeder<IDbContext>
{
    private readonly ILogger<DatabaseContextSeed> _logger;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;

    public DatabaseContextSeed(ILogger<DatabaseContextSeed> logger, RoleManager<IdentityRole<Guid>> roleManager)
    {
        _logger = logger;
        _roleManager = roleManager;
    }

    public async Task SeedAsync(IDbContext dbContext)
    {
        if (!await _roleManager.RoleExistsAsync(Roles.Admin))
        {
            _logger.LogInformation("<--- Seeding {AdminRole} role ---->", Roles.Admin);
            await _roleManager.CreateAsync(new IdentityRole<Guid>(Roles.Admin));
        }

        if (!await _roleManager.RoleExistsAsync(Roles.Member))
        {
            _logger.LogInformation("<--- Seeding {MemberRole} role ---->", Roles.Member);
            await _roleManager.CreateAsync(new IdentityRole<Guid>(Roles.Member));
        }
    }
}
