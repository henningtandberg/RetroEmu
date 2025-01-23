using System;
using ImGuiNET;
using MediatR;
using Microsoft.Xna.Framework;
using RetroEmu.Gui.Widgets.Debug;
using RetroEmu.Gui.Widgets.FilePicker;

namespace RetroEmu.Gui.Widgets.MainMenu;

public class MainMenuWidget(
    IMediator mediator,
    IApplicationStateProvider applicationStateProvider,
    IDebugState debugState,
    IFilePickerState filePickerState)
    : IGuiWidget
{
    public void Initialize() { }
    
    public void LoadContent() { }

    public void Draw(GameTime gameTime)
    {
        if (!ImGui.BeginMainMenuBar())
        {
            return;
        }

        FileMenu();
        DebugMenu();
        
        ImGui.EndMainMenuBar();
    }

    private void DebugMenu()
    {
        if (!ImGui.BeginMenu("Debug"))
        {
            return;
        }

        if (ImGui.MenuItem("Memory Editor", "", false, true))
        {
            debugState.DisplayMemoryEditor = true;
        }

        ImGui.EndMenu();
    }

    private void FileMenu()
    {
        if (!ImGui.BeginMenu("File"))
        {
            return;
        }

        if (ImGui.MenuItem("Open", "Ctrl+O", false))
        {
            filePickerState.OpenFile = true;
        }
        if (ImGui.MenuItem("Save", "Ctrl+S", false, true))
        {
            Console.WriteLine("Save");
        }
        if (ImGui.MenuItem("Save As..", "Ctrl+Shift+S", false, true))
        {
            Console.WriteLine("Save As..");
        }

        if (applicationStateProvider.ApplicationState == ApplicationState.Running)
        {
            if (ImGui.MenuItem("Pause", "", false, true))
            {
                mediator.Send(new ApplicationStateRequest { State = ApplicationState.Paused });
            }
        }
        else
        {
            if (ImGui.MenuItem("Resume", "", false, true))
            {
                mediator.Send(new ApplicationStateRequest { State = ApplicationState.Running });
            }
        }
        
        ImGui.EndMenu();
    }
}
