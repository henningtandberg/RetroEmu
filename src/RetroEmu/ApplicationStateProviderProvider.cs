namespace RetroEmu;

public class ApplicationStateProviderProvider : IApplicationStateProvider
{
    private string _selectedFile = string.Empty; // This is a temporary solution to store the selected file path
    private bool _step;

    public ApplicationState ApplicationState { get; set; } = ApplicationState.Initial;

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

    public void Step() => _step = true;

    public bool ShouldStep()
    {
        var shouldStep = _step;
        _step = false;
        
        return shouldStep;
    }
}