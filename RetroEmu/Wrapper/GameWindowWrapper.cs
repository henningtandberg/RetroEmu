using Microsoft.Xna.Framework;

namespace RetroEmu.Wrapper;

public class GameWindowWrapper : IWrapper<GameWindow>
{
    private GameWindow _gameWindow;

    public GameWindowWrapper(GameWindow gameWindow)
    {
        _gameWindow = gameWindow;
    }

    public ref GameWindow Value => ref _gameWindow;
}