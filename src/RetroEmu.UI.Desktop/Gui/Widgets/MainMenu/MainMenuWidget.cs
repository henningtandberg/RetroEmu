using ImGuiNET;
using Microsoft.Xna.Framework;
using RetroEmu.UI.Desktop.Gui.Widgets.FileDialogue;
using RetroEmu.Runtime.State;

namespace RetroEmu.UI.Desktop.Gui.Widgets.MainMenu;

public class MainMenuWidget(
    IFileDialogueState fileDialogueState,
    IEmulatorStateContext emulatorStateContext)
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
            if (emulatorStateContext.IsInitialState())
            {
                ImGui.MenuItem("Load a cartridge using \"File > Open\" to get started!", "", false, false);
            }
            
            if (emulatorStateContext.IsRunningState())
            {
                if (ImGui.MenuItem("Pause", "", false, true))
                {
                    emulatorStateContext.Pause();
                }
            }
            
            if (emulatorStateContext.IsPaused())
            {
                if (ImGui.MenuItem("Start", "", false, true))
                {
                    emulatorStateContext.Start();
                }
            }

            if (emulatorStateContext.IsPaused())
            {
                if (ImGui.MenuItem("Step", "", false, true))
                {
                    emulatorStateContext.Step();
                }
            }
            
            ImGui.EndMenu();
            return;
        }
        
        #endregion
        
        ImGui.EndMainMenuBar();
    }
}
