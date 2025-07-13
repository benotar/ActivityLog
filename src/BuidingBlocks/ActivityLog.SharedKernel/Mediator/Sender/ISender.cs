using ActivityLog.SharedKernel.Mediator.Request;

namespace ActivityLog.SharedKernel.Mediator.Sender;

public interface ISender
{
    Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);
}
