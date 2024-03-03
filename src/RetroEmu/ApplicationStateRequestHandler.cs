using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace RetroEmu;

public class ApplicationStateRequestHandler : IRequestHandler<ApplicationStateRequest>
{
    private readonly IApplicationStateProvider _applicationStaterProvider;

    public ApplicationStateRequestHandler(IApplicationStateProvider applicationStateProvider)
    {
        _applicationStaterProvider = applicationStateProvider;
    }

    public Task Handle(ApplicationStateRequest request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Got request for state change. Transitioning to: {request.State} from {_applicationStaterProvider.ApplicationState}");
        _applicationStaterProvider.SetApplicationState(request.State);
        return Task.CompletedTask;
    }
}