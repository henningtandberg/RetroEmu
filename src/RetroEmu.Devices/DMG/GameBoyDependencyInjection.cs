using Microsoft.Extensions.DependencyInjection;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.Devices.DMG.ROM;

namespace RetroEmu.Devices.DMG
{
	public static class GameBoyDependencyInjection
	{
		public static IServiceCollection AddDotMatrixGameBoy(this IServiceCollection services)
		{
			return services
				.AddSingleton<IMemory, Memory>()
				.AddSingleton<IProcessor, Processor>()
				.AddSingleton<ICartridge, Cartridge>()
				.AddSingleton<IGameBoy,GameBoy>();
		}
	}
}