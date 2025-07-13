using ActivityLog.SharedKernel.Mediator.Request;
using ActivityLog.SharedKernel.Mediator.Sender;
using Microsoft.Extensions.DependencyInjection;

namespace ActivityLog.Chassis.Mediator;

public class Sender : ISender
{
    private readonly IServiceProvider _serviceProvider;

    public Sender(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

    public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request,
        CancellationToken cancellationToken = default)
    {
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
        
        var handler = _serviceProvider.GetRequiredService(handlerType);

        var handleMethod = handlerType.GetMethod("Handle")
                           ?? throw new InvalidOperationException(
                               $"Handle method not found on handler type {handlerType.FullName}");
        
        return await (Task<TResponse>)handleMethod.Invoke(handler, [request, cancellationToken])!;
    }
}
