using System;
using ImGuiNET;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.Types;
using RetroEmu.Devices.DMG;
using RetroEmu.Wrapper;
using Vector2 = System.Numerics.Vector2;

namespace RetroEmu.Gui.Widgets.Screen;

public class ScreenWidget : IGuiWidget
{
    private int gbWidth = 160;
    private int gbHeight = 144;
    
    private readonly IGameBoy _gameBoy;
    private readonly Texture2D _displayTexture;
    private readonly IntPtr _displayTextureId;

    public ScreenWidget(IWrapper<GraphicsDevice> graphicsDevice, IImGuiRenderer imGuiRenderer, IGameBoy gameBoy)
    {
        _gameBoy = gameBoy;
        _displayTexture = new Texture2D(graphicsDevice.Value, gbWidth, gbHeight);
        _displayTextureId = imGuiRenderer.BindTexture(_displayTexture);
    }

    public void Draw(GameTime gameTime)
    {
        ImGui.SetNextWindowPos(new Vector2(0, 0), ImGuiCond.FirstUseEver);
        ImGui.SetNextWindowSize(new Vector2(gbWidth, gbHeight), ImGuiCond.FirstUseEver);

        if (!ImGui.Begin("Screen")) return;

        // Temp easy windowstuff
        var processor = _gameBoy.GetProcessor();
        var displayColors = new Color[gbWidth * gbHeight];
        
        for (var y = 0; y < gbHeight; y++)
        {
            for (var x = 0; x < gbWidth; x++)
            {
                var inColor = processor.GetDisplayColor(x, y);
                var index = y * gbWidth + x;

                var outColor = inColor switch
                {
                    1 => new Color(0.66f, 0.66f, 0.66f),
                    2 => new Color(0.33f, 0.33f, 0.33f),
                    3 => new Color(0.0f, 0.0f, 0.0f),
                    _ => new Color(1.0f, 1.0f, 1.0f)
                };
                
                displayColors[index] = outColor;
            }
        }
        _displayTexture.SetData(displayColors);

        var viewPortSize = ImGui.GetContentRegionAvail();
        var imageAspectRatio = (float)gbWidth / gbHeight;
        var contentRegionAspectRatio = viewPortSize.X / viewPortSize.Y;

        var scale = contentRegionAspectRatio > imageAspectRatio
            ? viewPortSize.Y / gbHeight
            : viewPortSize.X / gbWidth;

        var scaledImageSize = new Vector2(gbWidth * scale, gbHeight * scale);
        var xPadding = (viewPortSize.X - scaledImageSize.X) / 2.0f;
        var yPadding = (viewPortSize.Y - scaledImageSize.Y) / 2.0f;
        
        ImGui.SetCursorPosX(ImGui.GetCursorPosX() + xPadding);
        ImGui.SetCursorPosY(ImGui.GetCursorPosY() + yPadding);
        
        ImGui.Image(_displayTextureId, scaledImageSize);

        ImGui.End();
    }
}