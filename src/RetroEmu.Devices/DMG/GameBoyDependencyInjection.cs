using Microsoft.Extensions.DependencyInjection;

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