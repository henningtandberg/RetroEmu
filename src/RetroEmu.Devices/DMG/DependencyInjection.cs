using Microsoft.Extensions.DependencyInjection;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.Devices.DMG.CPU.Interrupts;
using RetroEmu.Devices.DMG.CPU.PPU;
using RetroEmu.Devices.DMG.CPU.Timing;
using RetroEmu.Devices.DMG.ROM;

namespace RetroEmu.Devices.DMG
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddDotMatrixGameBoy(this IServiceCollection services)
		{
			return services
				.AddSingleton<IAddressBus, AddressBus>()
				.AddSingleton<ITimer, Timer>()
				.AddSingleton<IPixelProcessingUnit, PixelProcessingUnit>()
				.AddSingleton<IInterruptState, InterruptState>()
				.AddSingleton<IJoypad, Joypad>()
				.AddSingleton<IProcessor, Processor>()
				.AddSingleton<IDebugProcessor>(serviceProvider =>
					(Processor)serviceProvider.GetRequiredService<IProcessor>())
				.AddSingleton<ICartridge, CartridgeStrategy>()
				.AddSingleton<IInternalRam>(new InternalRam())
				.AddSingleton<IGameBoy, GameBoy>();
		}
	}
}