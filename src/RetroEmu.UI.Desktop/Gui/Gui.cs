using System.Collections.Generic;
using ImGuiNET;
using Microsoft.Xna.Framework;
using RetroEmu.UI.Desktop.Gui.Rendering;
using RetroEmu.UI.Desktop.Gui.Widgets;

namespace RetroEmu.UI.Desktop.Gui;

public class Gui(IImGuiRenderer imGuiRenderer, IEnumerable<IGuiWidget> guiWidgets) : IGui
{
    private const string DockSpaceId = "MainDockSpace";
    private uint MainDockSpaceId { get; set; }

    public void Initialize()
    {
        imGuiRenderer.RebuildFontAtlas();
    }

    public void LoadContent()
    {
    }

    public void Draw(GameTime gameTime)
    {
        imGuiRenderer.BeforeLayout(gameTime);
        
        SetUpDockSpace();

        foreach (var widget in guiWidgets)
        {
            widget.Draw(gameTime);
        }

        imGuiRenderer.AfterLayout();
    }

    private void SetUpDockSpace()
    {
        var viewport = ImGui.GetMainViewport();

        // Draw fullscreen background
        var backgroundColor = new System.Numerics.Vector4(0.1f, 0.1f, 0.1f, 1.0f);
        var drawList = ImGui.GetBackgroundDrawList();
        drawList.AddRectFilled(viewport.Pos, viewport.Pos + viewport.Size,
            ImGui.ColorConvertFloat4ToU32(backgroundColor));

        ImGui.SetNextWindowPos(viewport.WorkPos);
        ImGui.SetNextWindowSize(viewport.WorkSize);
        ImGui.SetNextWindowViewport(viewport.ID);

        ImGui.PushStyleVar(ImGuiStyleVar.WindowRounding, 0.0f);
        ImGui.PushStyleVar(ImGuiStyleVar.WindowBorderSize, 0.0f);
        ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, System.Numerics.Vector2.Zero);

        const ImGuiWindowFlags windowFlags = ImGuiWindowFlags.NoTitleBar |
                                             ImGuiWindowFlags.NoCollapse |
                                             ImGuiWindowFlags.NoResize |
                                             ImGuiWindowFlags.NoMove |
                                             ImGuiWindowFlags.NoBringToFrontOnFocus |
                                             ImGuiWindowFlags.NoNavFocus |
                                             ImGuiWindowFlags.NoBackground |
                                             ImGuiWindowFlags.MenuBar;

        ImGui.Begin("DockSpaceWindow", windowFlags);
        ImGui.PopStyleVar(3);


        MainDockSpaceId = ImGui.GetID(DockSpaceId);
        ImGui.DockSpace(MainDockSpaceId, System.Numerics.Vector2.Zero, ImGuiDockNodeFlags.PassthruCentralNode);

        ImGui.End();
    }
}