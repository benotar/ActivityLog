using ActivityLog.Constants.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ActivityLog.ServiceDefaults.Kestrel;

public static class CorsExtensions
{
    private const string AllowAllPolicy = "AllowAll";

    public static void AddDefaultCors(this IHostApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
            {
                options.AddPolicy(
                    AllowAllPolicy,
                    policyBuilder =>
                    {
                        policyBuilder.SetIsOriginAllowed(origin =>
                            {
                                var host = new Uri(origin).Host;
                                return host == Restful.Host.Localhost;
                            }
                        );

                        policyBuilder
                            .WithMethods(
                                Restful.Methods.Get,
                                Restful.Methods.Post,
                                Restful.Methods.Put,
                                Restful.Methods.Delete
                            )
                            .AllowAnyHeader()
                            .AllowCredentials();
                    }
                );
            }
        );
    }

    public static void UseDefaultCors(this WebApplication app)
    {
        app.UseCors(AllowAllPolicy);
    }
}
