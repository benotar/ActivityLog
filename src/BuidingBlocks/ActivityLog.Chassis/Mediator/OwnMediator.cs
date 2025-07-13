using System.Reflection;
using ActivityLog.SharedKernel.Mediator.Request;
using ActivityLog.SharedKernel.Mediator.Sender;
using Microsoft.Extensions.DependencyInjection;

namespace ActivityLog.Chassis.Mediator;

public static class OwnMediator
{
    public static IServiceCollection AddOwnMediator(this IServiceCollection services, Assembly? assembly = null)
    {
        assembly ??= Assembly.GetCallingAssembly();

        services.AddScoped<ISender, Sender>();

        var handlerInterfaceType = typeof(IRequestHandler<,>);

        var handlerTypes = assembly
            .GetTypes()
            .Where(type => type is { IsAbstract: false, IsInterface: false })
            .SelectMany(type => type.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterfaceType)
                .Select(i => new { Interface = i, Implementation = type }));

        foreach (var handler in handlerTypes)
        {
            services.AddScoped(handler.Interface, handler.Implementation);
        }

        return services;
    }
}
