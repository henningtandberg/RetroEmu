namespace RetroEmu;

public class ApplicationStateProviderProvider : IApplicationStateProvider
{
    public ApplicationState ApplicationState { get; set; } = ApplicationState.Running;

    public void SetApplicationState(ApplicationState applicationState)
    {
        ApplicationState = applicationState;
    }
}