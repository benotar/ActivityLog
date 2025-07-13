using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Scalar.AspNetCore;


namespace ActivityLog.ServiceDefaults.ApiSpecification.OpenApi;

public static class OpenApiExtensions
{
    public static void UseDefaultOpenApi(this WebApplication app)
    {
        app.MapOpenApi();

        if (!app.Environment.IsDevelopment())
        {
            return;
        }


        app.MapScalarApiReference(options =>
        {
            options
                .WithTitle("Workout Service API")
                .WithTheme(ScalarTheme.Mars)
                .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
        });

        app.MapGet("/", () => Results.Redirect("/scalar/v1")).ExcludeFromDescription();
    }
}
