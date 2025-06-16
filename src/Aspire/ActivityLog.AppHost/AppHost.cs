var builder = DistributedApplication.CreateBuilder(args);

var workoutApi = builder.AddProject<Projects.ActivityLog_Services_WorkoutService_API>("workout-service-api");

builder.Build().Run();
