using System.IO.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RetroEmu.Devices.DMG;
using RetroEmu.Gui;
using RetroEmu.Gui.Widgets.Disassembler;
using RetroEmu.Gui.Widgets.FileDialogue;
using RetroEmu.Gui.Widgets.MainMenu;
using RetroEmu.Gui.Widgets.MemoryDebugger;
using RetroEmu.Gui.Widgets.ProcessorInfo;
using RetroEmu.State;
using RetroEmu.Wrapper;

namespace RetroEmu;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGame(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddSingleton<IWrapper<GameWindow>>(sp =>
                new GameWindowWrapper(sp.GetRequiredService<IGame>().Window))
            .AddSingleton<IWrapper<GraphicsDevice>>(sp =>
                new GraphicsDeviceWrapper(sp.GetRequiredService<IGame>().GraphicsDevice))
            .AddSingleton<IWrapper<ContentManager>>(sp =>
                new ContentManagerWrapper(sp.GetRequiredService<IGame>().Content))
            .AddSingleton<IFileSystem, FileSystem>()
            .AddSingleton<IApplication, Application>()
            .AddSingleton<IApplicationStateContext, ApplicationStateContext>()
            .AddSingleton<IImGuiRenderer, ImGuiRenderer>()
            .AddSingleton<IGui, Gui.Gui>()
            .AddSingleton<IGuiWidget, MainMenuWidget>()
            .AddSingleton<IGuiWidget, FileDialogueWidget>()
            .AddSingleton<IGuiWidget, ProcessorInfoWidget>()
            .AddSingleton<IGuiWidget, MemoryEditorWidget>()
            .AddSingleton<IGuiWidget, DisassemblerWidget>()
            .AddSingleton<IFileDialogueState, FileDialogueState>()
            .AddDotMatrixGameBoy()
            .AddSingleton<IGame, GameInstance>();
    }
}