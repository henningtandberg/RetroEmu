using ImGuiNET;
using Microsoft.Xna.Framework;
using RetroEmu.UI.Desktop.Gui.Widgets.FileDialogue;
using RetroEmu.UI.Desktop.State;

namespace RetroEmu.UI.Desktop.Gui.Widgets.MainMenu;

public class MainMenuWidget(
    IFileDialogueState fileDialogueState,
    IApplicationStateContext applicationStateContext)
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
            if (applicationStateContext.IsInitialState())
            {
                ImGui.MenuItem("Load a cartridge using \"File > Open\" to get started!", "", false, false);
            }
            
            if (applicationStateContext.IsRunningState())
            {
                if (ImGui.MenuItem("Pause", "", false, true))
                {
                    applicationStateContext.Pause();
                }
            }
            
            if (applicationStateContext.IsPaused())
            {
                if (ImGui.MenuItem("Start", "", false, true))
                {
                    applicationStateContext.Start();
                }
            }

            if (applicationStateContext.IsPaused())
            {
                if (ImGui.MenuItem("Step", "", false, true))
                {
                    applicationStateContext.Step();
                }
            }
            
            ImGui.EndMenu();
            return;
        }
        
        #endregion
        
        ImGui.EndMainMenuBar();
    }
}
