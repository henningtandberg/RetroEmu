using Microsoft.Extensions.DependencyInjection;
using RetroEmu.Devices.Disassembly;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.Devices.DMG.CPU.Interrupts;
using RetroEmu.Devices.DMG.CPU.PPU;
using RetroEmu.Devices.DMG.CPU.Timing;
using RetroEmu.Devices.DMG.ROM;

namespace RetroEmu.Devices.DMG;

public static class DependencyInjection
{
    public static IServiceCollection AddDotMatrixGameBoy(this IServiceCollection services) =>
        services
            .AddSingleton<IAddressBus, AddressBus>()
            .AddSingleton<IReadOnlyAddressBus>(serviceProvider =>
                (AddressBus)serviceProvider.GetRequiredService<IAddressBus>())
            .AddSingleton<ITimer, Timer>()
            .AddSingleton<IPixelProcessingUnit, PixelProcessingUnit>()
            .AddSingleton<IInterruptState, InterruptState>()
            .AddSingleton<IJoypad, Joypad>()
            .AddSingleton<IProcessor, Processor>()
            .AddSingleton<ICartridge, CartridgeStrategy>()
            .AddSingleton<IInternalRam>(new InternalRam())
            .AddSingleton<IDisassembler, Disassembler>()
            .AddSingleton<IGameBoy, GameBoy>()
            .AddDebugInterfaces();

    private static IServiceCollection AddDebugInterfaces(this IServiceCollection services) =>
        services
            .AddSingleton<IDebugProcessor>(serviceProvider =>
                (Processor)serviceProvider.GetRequiredService<IProcessor>())
            .AddSingleton<IDebugCartridge>(serviceProvider =>
                (CartridgeStrategy)serviceProvider.GetRequiredService<ICartridge>())
            .AddSingleton<IDebugInternalRam>(serviceProvider =>
                (InternalRam)serviceProvider.GetRequiredService<IInternalRam>());
}