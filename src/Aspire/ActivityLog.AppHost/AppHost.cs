var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.ActivityLog_Services_WorkoutService_API>("workout-service-api");

builder.Build().Run();
