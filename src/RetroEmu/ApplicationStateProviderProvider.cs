namespace RetroEmu;

public class ApplicationStateProviderProvider : IApplicationStateProvider
{
    private string _selectedFile = string.Empty; // This is a temporary solution to store the selected file path
    
    public ApplicationState ApplicationState { get; set; } = ApplicationState.Paused;

    public void SetApplicationState(ApplicationState applicationState)
    {
        ApplicationState = applicationState;
    }

    public void InitiateLoadRom(string selectedFile)
    {
        ApplicationState = ApplicationState.LoadRom;
        _selectedFile = selectedFile;
    }
    
    public string GetSelectedFile()
    {
        return _selectedFile;
    }
}