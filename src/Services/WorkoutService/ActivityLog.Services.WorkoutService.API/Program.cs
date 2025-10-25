using ActivityLog.Chassis.Exceptions;
using ActivityLog.ServiceDefaults;
using ActivityLog.ServiceDefaults.ApiSpecification.OpenApi;
using ActivityLog.ServiceDefaults.Kestrel;
using ActivityLog.Services.WorkoutService.Application;
using ActivityLog.Services.WorkoutService.Application.Interfaces.Services;
using ActivityLog.Services.WorkoutService.Application.Models;
using ActivityLog.Services.WorkoutService.Infrastructure;
using ActivityLog.SharedKernel.Extensions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddDefaultCors();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddProblemDetails();

builder.Services.AddOpenApi();

builder.AddApplicationLayer();

builder.AddPersistenceLayer();

var app = builder.Build();

app.MapDefaultEndpoints();

app.UseDefaultOpenApi();

app.MapPost("/api/exercise/create", async (CreateExerciseRequest request, IExerciseService exerciseService, IValidator<CreateExerciseRequest> validator, CancellationToken cancellationToken) =>
{
    var validationResult = validator.Validate(request);

    if (!validationResult.IsValid)
    {
        return Results.BadRequest(new { errors = validationResult.Errors.Select(e => e.ErrorMessage).ToArray() });
    }

    var createExerciseResult = await exerciseService.CreateAsync(request, cancellationToken);

    return !createExerciseResult.IsSuccess ? Results.BadRequest(createExerciseResult.ErrorCode) : Results.Ok(createExerciseResult.Data);
});

app.MapGet("/api/exercise/get", async (IExerciseService exerciseService, CancellationToken cancellationToken) =>
{
    var getResult = await exerciseService.GetAllAsync(cancellationToken);

    return getResult.IsSuccess ? Results.Ok(getResult.Data) : Results.Empty;
});

app.MapGet("/api/exercise/get-by-name",
    async ([FromQuery]string name, IExerciseService exerciseService, CancellationToken cancellationToken) =>
    {
        var getResult = await exerciseService.GetByNameAsync(name, cancellationToken);

        return !getResult.IsSuccess ? Results.BadRequest(getResult.ErrorCode.ToMessage()) : Results.Ok(getResult.Data);
    });

app.Run();
