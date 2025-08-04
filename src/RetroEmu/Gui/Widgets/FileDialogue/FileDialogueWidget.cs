using System;
using System.IO.Abstractions;
using Microsoft.Xna.Framework;
using RetroEmu.State;

namespace RetroEmu.Gui.Widgets.FileDialogue;

/// <summary>
/// This class is responsible for drawing and opening a file dialogue. It communicates directly with IApplicationStateProvider
/// which should be avoided in the future, but for now it's fine. A solution would be to create an event system.
/// </summary>
/// <param name="fileDialogueState"></param>
/// <param name="application"></param>
public class FileDialogueWidget(
    IFileSystem fileSystem,
    IFileDialogueState fileDialogueState,
    IApplicationStateContext applicationStateContext)
    : IGuiWidget
{
    private readonly FilePicker _filePicker = new("/");
    
    public void Draw(GameTime gameTime)
    {
        DrawOpenFileDialogue();
    }

    private void DrawOpenFileDialogue()
    {
        if (!fileDialogueState.OpenFileIsVisible)
        {
            return;
        }
        
        var selectedFile = SelectFile();
        if (string.IsNullOrEmpty(selectedFile))
        {
            return;
        }
        
        Console.WriteLine("HEre");
        
        if (!fileSystem.File.Exists(selectedFile))
        {
            return;
        }
        
        var cartridgeData = fileSystem.File.ReadAllBytes(selectedFile);
        applicationStateContext.Load(cartridgeData);
    }

    private string SelectFile()
    {
        switch (_filePicker.Draw())
        {
            case FilePickerResult.Cancel:
                fileDialogueState.OpenFileIsVisible = false;
                return string.Empty;
            case FilePickerResult.Selected:
                fileDialogueState.OpenFileIsVisible = false;
                return _filePicker.SelectedFile;
            case FilePickerResult.NotSelected:
                return string.Empty;
            default:
                throw new AggregateException("WHAT THE HELL?!?!");
        }
    }
}