using System.Security.Claims;
using ActivityLog.Constants.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.Enrichment;

namespace ActivityLog.ServiceDefaults.Logging;

public class ApplicationEnricher(IHttpContextAccessor httpContextAccessor) : ILogEnricher
{
    public void Enrich(IEnrichmentTagCollector collector)
    {
        collector.Add(SharedConstants.MachineName, Environment.MachineName);
        
        var httpContext = httpContextAccessor.HttpContext;
        
        if (httpContext is not null)
        {
            collector.Add(
                SharedConstants.UserId,
                httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty);
        }
    }
}
