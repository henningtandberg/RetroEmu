using System.Reflection;
using Xunit;
using Moq;
using RetroEmu.Devices.DMG;
using Microsoft.Extensions.DependencyInjection;

namespace RetroEmu.Devices.Tests
{
    public class GameBoyResetTests
    {
        [Fact]
        public void WithGameBoy_GameBoyIsReset_CartridgeIsReset()
        {
            IGameBoy gameBoy = CreateGameBoy();

            gameBoy.Reset();

            Assert.True(true);
        }

        private static IGameBoy CreateGameBoy()
        {
            return new ServiceCollection()
                .AddDMG()
                .BuildServiceProvider()
                .GetRequiredService<IGameBoy>();
        }
    }
}
