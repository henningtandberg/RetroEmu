using Microsoft.Xna.Framework;

namespace RetroEmu;

public interface IApplication
{
    public void Initialize();
    public void LoadContent();
    public void Update(GameTime gameTime);
    public void Draw(GameTime gameTime);
}