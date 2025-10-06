using ActivityLog.Chassis.Exceptions;
using ActivityLog.ServiceDefaults;
using ActivityLog.ServiceDefaults.ApiSpecification.OpenApi;
using ActivityLog.ServiceDefaults.Kestrel;
using ActivityLog.Services.WorkoutService.Application;
using ActivityLog.Services.WorkoutService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddDefaultCors();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddProblemDetails();

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.AddApplication();

builder.AddPersistence();

var app = builder.Build();

app.MapDefaultEndpoints();

app.MapControllers();

app.UseDefaultOpenApi();

app.Run();
