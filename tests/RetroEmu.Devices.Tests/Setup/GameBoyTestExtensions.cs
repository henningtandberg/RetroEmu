using System;
using RetroEmu.Devices.DMG;

namespace RetroEmu.Devices.Tests.Setup;

public static class GameBoyTestExtensions
{
    public static void RunFor(this IGameBoy gameBoy, int cycles)
    {
        for (var i = 0; i < cycles; i++)
        {
            gameBoy.Update();
        }
    }
    
    public static void RunWhile(this IGameBoy gameBoy, Func<bool> predicate, int maxCycles = int.MaxValue)
    {
        for (var i = 0; i < maxCycles && predicate(); i++)
        {
            gameBoy.Update();
        }
    }
}
