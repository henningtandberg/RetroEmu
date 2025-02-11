namespace RetroEmu.Gui.Widgets.FileDialogue;

public interface IFileDialogueState
{
    public bool OpenFileIsVisible { get; set; }
    public bool SaveFileIsVisible { get; set; }
    public bool SaveFileAsIsVisible { get; set; }
}