using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace RetroEmu.Gui;

public class Gui(IImGuiRenderer imGuiRenderer, IEnumerable<IGuiWidget> guiWidgets) : IGui
{
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
        
        foreach (var widget in guiWidgets)
        {
            widget.Draw(gameTime);
        }
        
        imGuiRenderer.AfterLayout();
    }
}