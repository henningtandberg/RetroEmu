using System;
using Microsoft.Xna.Framework;

namespace RetroEmu.Gui.Widgets.FileDialogue;

public class FileDialogueWidget(IFileDialogueState fileDialogueState) : IGuiWidget
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
        
        var pickedFilePath = "/";
        
        // Might want to reset the file picker after use
        switch (_filePicker.Draw(ref pickedFilePath))
        {
            case FilePickerResult.Cancel:
                fileDialogueState.OpenFileIsVisible = false;
                break;
            case FilePickerResult.Selected:
                Console.WriteLine("Picked file: " + _filePicker.SelectedFile);
                fileDialogueState.OpenFileIsVisible = false;
                break;
            case FilePickerResult.NotSelected:
                Console.WriteLine("No file selected");
                break;
            default:
                throw new AggregateException("WHAT THE HELL?!?!");
        }
    }
}