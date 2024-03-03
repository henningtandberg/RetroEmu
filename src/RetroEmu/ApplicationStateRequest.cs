using MediatR;

namespace RetroEmu;

public record ApplicationStateRequest : IRequest
{
    public ApplicationState State { get; init; }
}
