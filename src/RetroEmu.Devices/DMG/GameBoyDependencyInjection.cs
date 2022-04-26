using Microsoft.Extensions.DependencyInjection;

namespace RetroEmu.Devices.DMG
{
	public static class GameBoyDependencyInjection
	{
		public static IServiceCollection AddDMG(this IServiceCollection services)
		{
			return services.AddSingleton<IGameBoy>(GameBoyFactory.CreateGameBoy());
		}
	}
}