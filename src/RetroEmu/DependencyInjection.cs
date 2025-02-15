using System.IO.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RetroEmu.Devices.DMG;
using RetroEmu.Gui;
using RetroEmu.Gui.Widgets.FileDialogue;
using RetroEmu.Gui.Widgets.MainMenu;
using RetroEmu.Wrapper;

namespace RetroEmu;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGame(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddSingleton<IWrapper<GameWindow>>(sp =>
                new GameWindowWrapper(sp.GetRequiredService<GameInstance>().Window))
            .AddSingleton<IWrapper<GraphicsDevice>>(sp =>
                new GraphicsDeviceWrapper(sp.GetRequiredService<GameInstance>().GraphicsDevice))
            .AddSingleton<IWrapper<ContentManager>>(sp =>
                new ContentManagerWrapper(sp.GetRequiredService<GameInstance>().Content))
            .AddSingleton<IGame, GameInstance>()
            .AddSingleton<IFileSystem, FileSystem>()
            .AddSingleton<IApplication, Application>()
            .AddSingleton<IImGuiRenderer, ImGuiRenderer>()
            .AddSingleton<IApplicationStateProvider, ApplicationStateProviderProvider>()
            .AddSingleton<IGui, Gui.Gui>()
            .AddSingleton<IGuiWidget, MainMenuWidget>()
            .AddSingleton<IGuiWidget, FileDialogueWidget>()
            .AddSingleton<IFileDialogueState, FileDialogueState>()
            .AddDotMatrixGameBoy();
    }
}