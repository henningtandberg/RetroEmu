using Microsoft.Extensions.DependencyInjection;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.Devices.DMG.CPU.Interrupts;
using RetroEmu.Devices.DMG.CPU.Timing;
using RetroEmu.Devices.DMG.ROM;

namespace RetroEmu.Devices.DMG
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddDotMatrixGameBoy(this IServiceCollection services)
		{
			return services
				.AddSingleton<IMemory, Memory>()
				.AddSingleton<ITimer, Timer>()
				.AddSingleton<IInterruptState, InterruptState>()
				.AddSingleton<IProcessor, Processor>()
				.AddSingleton<ICartridge, Cartridge>()
				.AddSingleton<IGameBoy, GameBoy>();
		}
	}
}