using System.Diagnostics;
using ActivityLog.ServiceDefaults.Logging;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

namespace ActivityLog.ServiceDefaults;

// Adds common .NET Aspire services: service discovery, resilience, health checks, and OpenTelemetry.
// This project should be referenced by each service project in your solution.
// To learn more about using this project, see https://aka.ms/dotnet/aspire/service-defaults
public static class Extensions
{
    private const string HealthChecks = nameof(HealthChecks);
    private const string HealthEndpointPath = "/health";
    private const string AlivenessEndpointPath = "/alive";

    public static TBuilder AddServiceDefaults<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
    {
        builder.ConfigureOpenTelemetry();

        builder.AddDefaultHealthChecks();

        builder.Services.AddServiceDiscovery();

        builder.Services.ConfigureHttpClientDefaults(http =>
        {
            // Turn on resilience by default
            http.AddStandardResilienceHandler();

            // Turn on service discovery by default
            http.AddServiceDiscovery();
        });

        // Uncomment the following to restrict the allowed schemes for service discovery.
        // builder.Services.Configure<ServiceDiscoveryOptions>(options =>
        // {
        //     options.AllowedSchemes = ["https"];
        // });

        return builder;
    }

    private static void ConfigureOpenTelemetry<TBuilder>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
    {
        var services = builder.Services;

        services.AddHttpContextAccessor();
        
        builder.AddLogging();
        
        services.AddOpenTelemetry(builder);

        builder.AddOpenTelemetryExporters();
    }

    private static void AddLogging(this IHostApplicationBuilder builder)
    {
        var logger = builder.Logging;

        logger.EnableEnrichment();
        builder.Services.AddLogEnricher<ApplicationEnricher>();
        
        logger.AddOpenTelemetry(logging =>
        {
            logging.IncludeFormattedMessage = true;
            logging.IncludeScopes = true;
        });

        if (builder.Environment.IsDevelopment())
        {
            logger.AddTraceBasedSampler();
        }
    }
    
    private static void AddOpenTelemetry(this IServiceCollection services, IHostApplicationBuilder builder)
    {
        Activity.DefaultIdFormat = ActivityIdFormat.W3C;

        services
            .AddOpenTelemetry()
            .WithMetrics(metrics =>
            {
                metrics.AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddRuntimeInstrumentation();
            })
            .WithTracing(tracing =>
            {
                tracing
                    .AddSource(builder.Environment.ApplicationName)
                    .AddAspNetCoreInstrumentation(options =>
                        options.Filter = httpContext =>
                            !(httpContext.Request.Path.StartsWithSegments(HealthEndpointPath)
                              || httpContext.Request.Path.StartsWithSegments(AlivenessEndpointPath)
                                )
                    )
                    .AddGrpcClientInstrumentation()
                    .AddHttpClientInstrumentation();
            });
    }
    
    private static void AddOpenTelemetryExporters<TBuilder>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
    {
        var useOtlpExporter = !string.IsNullOrWhiteSpace(builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"]);

        if (useOtlpExporter)
        {
            builder.Services.AddOpenTelemetry().UseOtlpExporter();
        }
    }

    private static void AddDefaultHealthChecks(this IHostApplicationBuilder builder)
    {
        var healthChecksConfiguration = builder.Configuration.GetSection(HealthChecks);

        // All health checks endpoints must return within the configured timeout value (defaults to 5 seconds)
        var healthChecksRequestTimeout = healthChecksConfiguration.GetValue<TimeSpan?>("RequestTimeout")
                                         ?? TimeSpan.FromSeconds(5);

        builder.Services.AddRequestTimeouts(timeouts => timeouts.AddPolicy(HealthChecks, healthChecksRequestTimeout));
        
        // Cache health checks responses for the configured duration (defaults to 10 seconds)
        var healthChecksExpireAfter =
            healthChecksConfiguration.GetValue<TimeSpan?>("ExpireAfter")
            ?? TimeSpan.FromSeconds(10);
        
        builder.Services.AddOutputCache(caching =>
            caching.AddPolicy(HealthChecks, policy => policy.Expire(healthChecksExpireAfter))
        );
        
        builder.Services.AddHealthChecks()
            // Add a default liveness check to ensure app is responsive
            .AddCheck("self", () => HealthCheckResult.Healthy(), ["live"]);

    }

    public static WebApplication MapDefaultEndpoints(this WebApplication app)
    {
        // Adding health checks endpoints to applications in non-development environments has security implications.
        // See https://aka.ms/dotnet/aspire/healthchecks for details before enabling these endpoints in non-development environments.
        if (app.Environment.IsDevelopment())
        {
            // All health checks must pass for app to be considered ready to accept traffic after starting
            app.MapHealthChecks(HealthEndpointPath);

            // Only health checks tagged with the "live" tag must pass for app to be considered alive
            app.MapHealthChecks(AlivenessEndpointPath,
                new HealthCheckOptions { Predicate = r => r.Tags.Contains("live") });
        }

        return app;
    }
}
