using Microsoft.Xna.Framework;

namespace RetroEmu.UI.Desktop.Gui;

public interface IGui
{
    public void Initialize();
    public void LoadContent();
    public void Draw(GameTime gameTime);
}