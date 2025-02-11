using System;
using ImGuiNET;
using MediatR;
using Microsoft.Xna.Framework;
using RetroEmu.Gui.Widgets.FileDialogue;

namespace RetroEmu.Gui.Widgets.MainMenu;

public class MainMenuWidget(IFileDialogueState fileDialogueState, IApplicationStateProvider applicationStateProvider)
    : IGuiWidget
{
    public void Draw(GameTime gameTime)
    {
        if (!ImGui.BeginMainMenuBar())
        {
            return;
        }

        #region FileDialogue
        if (!ImGui.BeginMenu("File"))
        {
            return;
        }

        fileDialogueState.OpenFileIsVisible = ImGui.MenuItem("Open", "Ctrl+O", false);
        fileDialogueState.SaveFileIsVisible = ImGui.MenuItem("Save", "Ctrl+S", false, true);
        fileDialogueState.SaveFileAsIsVisible = ImGui.MenuItem("Save As..", "Ctrl+Shift+S", false, true);
        
        ImGui.EndMenu();
        #endregion

        #region ApplicationState
        if (!ImGui.BeginMenu("Application State"))
        {
            return;
        }
        if (applicationStateProvider.ApplicationState == ApplicationState.Running)
        {
            applicationStateProvider.ApplicationState = ImGui.MenuItem("Pause", "", false, true)
                ? ApplicationState.Paused
                : ApplicationState.Running;
        }
        else if (applicationStateProvider.ApplicationState == ApplicationState.Paused)
        {
            applicationStateProvider.ApplicationState = ImGui.MenuItem("Resume", "", false, true)
                ? ApplicationState.Running
                : ApplicationState.Paused;
        }
        ImGui.EndMenu();
        #endregion
        
        ImGui.EndMainMenuBar();
    }
}
