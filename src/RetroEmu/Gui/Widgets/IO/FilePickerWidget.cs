using System;
using Microsoft.Xna.Framework;
using RetroEmu.Gui.Widgets.FilePicker;

namespace RetroEmu.Gui.Widgets.IO;

public class FilePickerWidget(IFilePickerState filePickerState) : IGuiWidget
{
    private readonly FilePicker _filePicker = new();
    public void Initialize() { }

    public void LoadContent() { }

    public void Draw(GameTime gameTime)
    {
        if (!filePickerState.OpenFile)
        {
            return;
        }

        var pickedFilePath = "/";
        if (_filePicker.Draw(ref pickedFilePath))
        {
            Console.WriteLine("Picked file: " + pickedFilePath);
        }
    }
}

public class FilePickerState : IFilePickerState
{
    public bool OpenFile { get; set; }
}