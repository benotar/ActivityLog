using ActivityLog.Constants.Aspire;

namespace ActivityLog.AppHost.Extensions.Infrastructure;

public static class AppHostExtensions
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

    public static IResourceBuilder<PostgresServerResource> AddConfiguredPostgres(this IDistributedApplicationBuilder builder)
    {
        var pgUser = builder.AddParameterFromConfiguration("pgUser", Components.Postgres.User, true);
        var pgPassword = builder.AddParameterFromConfiguration("pgPassword", Components.Postgres.Password, true);

        var postgres = builder
            .AddPostgres(Components.Postgres.Name, pgUser, pgPassword, port: Components.Postgres.Port)
            .WithPgWeb()
            .WithLifetime(ContainerLifetime.Session)
            .WithDataVolume()
            .WithIconName("HomeDatabase");

        pgUser.WithParentRelationship(postgres);
        pgPassword.WithParentRelationship(postgres);

        return postgres;
    }

    public static IResourceBuilder<RabbitMQServerResource> AddConfiguredRabbitMq(this IDistributedApplicationBuilder builder)
    {
        var rmqUser = builder.AddParameterFromConfiguration("rmqUser", Components.RabbitMq.User, true);
        var rmqPassword = builder.AddParameterFromConfiguration("rmqPassword", Components.RabbitMq.Password, true);

        var rmq = builder
            .AddRabbitMQ(Components.RabbitMq.Name, rmqUser, rmqPassword)
            .WithManagementPlugin();

        rmqUser.WithParentRelationship(rmq);
        rmqPassword.WithParentRelationship(rmq);

        return rmq;
    }
}
