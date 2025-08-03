using ActivityLog.Constants.Aspire;

namespace ActivityLog.AppHost.Extensions.Infrastructure;

public static class DashboardExtensions
{
    public static void AddDashboard(this IDistributedApplicationBuilder builder)
    {
        if (!builder.ExecutionContext.IsPublishMode)
        {
            return;
        }

        var dashboard = builder
            .AddContainer(DashboardConstants.Marker, DashboardConstants.ContainerImageName)
            .WithHttpEndpoint(port: DashboardConstants.ContainerPort, targetPort: DashboardConstants.ContainerPort)
            .WithHttpEndpoint(name: DashboardConstants.OtlpProtocol, port: DashboardConstants.OtlpPort,
                targetPort: DashboardConstants.OtlpPort);

        builder.Eventing.Subscribe<BeforeStartEvent>((e, _) =>
            {
                foreach (var resource in e.Model.Resources.OfType<IResourceWithEnvironment>())
                {
                    if (resource == dashboard.Resource)
                    {
                        continue;
                    }

                    builder
                        .CreateResourceBuilder(resource)
                        .WithEnvironment(callbackContext =>
                        {
                            callbackContext.EnvironmentVariables[DashboardConstants.OtelExporterOtlpEndpoint] =
                                dashboard.GetEndpoint(DashboardConstants.OtlpProtocol);
                            callbackContext.EnvironmentVariables[DashboardConstants.OtelExporterOtlpProtocol] =
                                DashboardConstants.GrpcProtocol;
                            callbackContext.EnvironmentVariables[DashboardConstants.OtelServiceName] = resource.Name;
                        });
                }

                return Task.CompletedTask;
            }
        );
    }
}
