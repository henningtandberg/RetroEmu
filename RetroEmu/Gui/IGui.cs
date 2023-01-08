using Microsoft.Xna.Framework;

namespace RetroEmu.Gui;

public interface IGui
{
    public void Initialize();
    public void LoadContent();
    public void Draw(GameTime gameTime);
}