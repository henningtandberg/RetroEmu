using System;
using System.IO.Abstractions;
using Microsoft.Extensions.Configuration;
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
    public static IApplication BuildApplication(this IServiceProvider serviceProvider, IGameInstance gameInstance)
    {
        return new ServiceCollection()
            .AddSingleton<IWrapper<GameWindow>>(new GameWindowWrapper(gameInstance.Window))
            .AddSingleton<IWrapper<GraphicsDevice>>(new GraphicsDeviceWrapper(gameInstance.GraphicsDevice))
            .AddSingleton<IWrapper<ContentManager>>(new ContentManagerWrapper(gameInstance.Content))
            .AddSingleton(serviceProvider.GetRequiredService<IConfiguration>())
            .AddSingleton<IFileSystem, FileSystem>()
            .AddSingleton<IApplication, Application>()
            .AddSingleton<IImGuiRenderer, ImGuiRenderer>()
            .AddSingleton<IGui, Gui.Gui>()
            .AddDotMatrixGameBoy()
            .BuildServiceProvider()
            .GetRequiredService<IApplication>();
    }
}