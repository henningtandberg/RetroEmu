namespace RetroEmu;

public class ApplicationStateProviderProvider : IApplicationStateProvider
{
    public ApplicationState ApplicationState { get; private set; } = ApplicationState.Running;

    public void SetApplicationState(ApplicationState applicationState)
    {
        ApplicationState = applicationState;
    }
}