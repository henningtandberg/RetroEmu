namespace RetroEmu;

public interface IApplicationStateProvider
{
    public ApplicationState ApplicationState { get; set; }
    public void SetApplicationState(ApplicationState applicationState);
    public void InitiateLoadRom(string selectedFile);
    public string GetSelectedFile();
}