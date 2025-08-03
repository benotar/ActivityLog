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
                foreach (var r in e.Model.Resources.OfType<IResourceWithEnvironment>())
                {
                    if (r == dashboard.Resource)
                    {
                        continue;
                    }

                    builder
                        .CreateResourceBuilder(r)
                        .WithEnvironment(c =>
                        {
                            c.EnvironmentVariables[DashboardConstants.OtelExporterOtlpEndpoint] =
                                dashboard.GetEndpoint(DashboardConstants.OtlpProtocol);
                            c.EnvironmentVariables[DashboardConstants.OtelExporterOtlpProtocol] =
                                DashboardConstants.GrpcProtocol;
                            c.EnvironmentVariables[DashboardConstants.OtelServiceName] = r.Name;
                        });
                }

                return Task.CompletedTask;
            }
        );
    }
}
