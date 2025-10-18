using ActivityLog.Chassis.Exceptions;
using ActivityLog.ServiceDefaults;
using ActivityLog.ServiceDefaults.ApiSpecification.OpenApi;
using ActivityLog.ServiceDefaults.Kestrel;
using ActivityLog.Services.WorkoutService.Application;
using ActivityLog.Services.WorkoutService.Application.Interfaces.Services;
using ActivityLog.Services.WorkoutService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddDefaultCors();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddProblemDetails();

//builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.AddApplicationLayer();

builder.AddPersistenceLayer();

var app = builder.Build();

app.MapDefaultEndpoints();

//app.MapControllers();

app.UseDefaultOpenApi();

app.MapGet("/api/Customer", async (IExerciseService exerciseService) => await exerciseService.GetAllAsync());

app.Run();
