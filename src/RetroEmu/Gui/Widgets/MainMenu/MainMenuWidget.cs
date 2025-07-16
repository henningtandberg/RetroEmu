using System;
using ImGuiNET;
using MediatR;
using Microsoft.Xna.Framework;
using RetroEmu.Gui.Widgets.FileDialogue;
using static RetroEmu.ApplicationState;

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
        if (ImGui.BeginMenu("File"))
        {
            fileDialogueState.OpenFileIsVisible = ImGui.MenuItem("Open", "Ctrl+O", false);
            fileDialogueState.SaveFileIsVisible = ImGui.MenuItem("Save", "Ctrl+S", false, true);
            fileDialogueState.SaveFileAsIsVisible = ImGui.MenuItem("Save As..", "Ctrl+Shift+S", false, true);
            
            ImGui.EndMenu();
        }

        #endregion

        #region ApplicationState
        if (ImGui.BeginMenu("Application State"))
        {
            if (applicationStateProvider.ApplicationState == Initial)
            {
                ImGui.MenuItem("Load a cartridge using \"File > Open\" to get started!", "", false, false);
            }
            
            applicationStateProvider.ApplicationState = applicationStateProvider.ApplicationState switch
            {
                Running => ImGui.MenuItem("Pause", "", false, true)
                    ? Paused
                    : Running,
                Paused => ImGui.MenuItem("Resume", "", false, true)
                    ? Running
                    : Paused,
                _ => applicationStateProvider.ApplicationState
            };

            if (applicationStateProvider.ApplicationState == Paused)
            {
                if (ImGui.MenuItem("Step", "", false, true))
                {
                    applicationStateProvider.Step();
                }
            }
            
            ImGui.EndMenu();
            return;
        }
        #endregion
        
        ImGui.EndMainMenuBar();
    }
}
