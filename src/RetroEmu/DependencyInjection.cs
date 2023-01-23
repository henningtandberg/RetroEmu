using System.IO.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RetroEmu.Devices.DMG;
using RetroEmu.Gui;
using RetroEmu.Wrapper;

namespace RetroEmu;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGame(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddSingleton<IWrapper<GameWindow>>(sp => new GameWindowWrapper(sp.GetRequiredService<IGame>().Window))
            .AddSingleton<IWrapper<GraphicsDevice>>(sp => new GraphicsDeviceWrapper(sp.GetRequiredService<IGame>().GraphicsDevice))
            .AddSingleton<IWrapper<ContentManager>>(sp => new ContentManagerWrapper(sp.GetRequiredService<IGame>().Content))
            .AddSingleton<IFileSystem, FileSystem>()
            .AddSingleton<IApplication, Application>()
            .AddSingleton<IImGuiRenderer, ImGuiRenderer>()
            .AddSingleton<IGui, Gui.Gui>()
            .AddDotMatrixGameBoy()
            .AddSingleton<IGame, GameInstance>();
    }
}