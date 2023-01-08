using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RetroEmu.Gui;

public interface IImGuiRenderer
{
    void RebuildFontAtlas();
    IntPtr BindTexture(Texture2D xnaTexture);
    void BeforeLayout(GameTime gameTime);
    void AfterLayout();
}