namespace RetroEmu;

public interface IApplicationStateProvider
{
    public ApplicationState ApplicationState { get; }
    public void SetApplicationState(ApplicationState applicationState);
}