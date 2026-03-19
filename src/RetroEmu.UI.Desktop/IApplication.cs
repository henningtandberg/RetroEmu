using Microsoft.Xna.Framework;

namespace RetroEmu.UI.Desktop;

public interface IApplication
{
    public void Initialize();
    public void LoadContent();
    public void Update(GameTime gameTime);
    public void Draw(GameTime gameTime);
}