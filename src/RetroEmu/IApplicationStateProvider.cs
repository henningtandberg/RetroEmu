namespace RetroEmu;

public interface IApplicationStateProvider
{
    public ApplicationState ApplicationState { get; set; }
    public void SetApplicationState(ApplicationState applicationState);
}