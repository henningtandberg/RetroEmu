using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RetroEmu;

public interface IGameInstance: IDisposable
{
    // include all the Properties/Methods that you'd want to use on your Game class below.
    public ContentManager Content { get; }
    GameWindow Window { get; }
    GraphicsDevice GraphicsDevice { get; }
    
    event EventHandler<EventArgs> Exiting;
    void Run();
    void Exit();
}