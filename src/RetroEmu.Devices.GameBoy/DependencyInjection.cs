using Microsoft.Extensions.DependencyInjection;
using RetroEmu.Devices.GameBoy.CPU;
using RetroEmu.Devices.GameBoy.CPU.Interrupts;
using RetroEmu.Devices.GameBoy.CPU.Link;
using RetroEmu.Devices.GameBoy.CPU.PPU;
using RetroEmu.Devices.GameBoy.CPU.Timing;
using RetroEmu.Devices.GameBoy.Disassembly;
using RetroEmu.Devices.GameBoy.ROM;

namespace RetroEmu.Devices.GameBoy;

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
            .AddSingleton<ISerial, Serial>()
            .AddSingleton<IWire, Wire>()
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