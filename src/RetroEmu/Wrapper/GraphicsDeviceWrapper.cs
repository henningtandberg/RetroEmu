using Microsoft.Xna.Framework.Graphics;

namespace RetroEmu.Wrapper;

public class GraphicsDeviceWrapper : IWrapper<GraphicsDevice>
{
    private GraphicsDevice _graphicsDevice;

    public GraphicsDeviceWrapper(GraphicsDevice graphicsDevice)
    {
        _graphicsDevice = graphicsDevice;
    }

    public ref GraphicsDevice Value => ref _graphicsDevice;
}