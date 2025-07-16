using ActivityLog.ServiceDefaults;
using ActivityLog.ServiceDefaults.ApiSpecification.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddControllers();

builder.Services.AddOpenApi();

var app = builder.Build();

app.MapDefaultEndpoints();

//app.MapGet("/", () => $"Welcome to the Workout Service homepage!\nUtc Time: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}");

app.MapControllers();

app.UseDefaultOpenApi();

app.Run();
