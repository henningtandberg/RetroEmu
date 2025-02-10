using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace RetroEmu;

public class ApplicationStateRequestHandler(IApplicationStateProvider applicationStateProvider)
    : IRequestHandler<ApplicationStateRequest>
{
    public Task Handle(ApplicationStateRequest request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Got request for state change. Transitioning to: {request.State} from {applicationStateProvider.ApplicationState}");
        applicationStateProvider.SetApplicationState(request.State);
        return Task.CompletedTask;
    }
}