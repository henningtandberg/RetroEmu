using RetroEmu.Devices.DMG;

namespace RetroEmu.State;

internal sealed class RunningState(IApplicationStateContext applicationStateContext)
    : BaseApplicationState(applicationStateContext)
{
    public override void Update(IFrameCounter frameCounter, IGameBoy gameBoy)
    {
        // Iterate until VBlank, with backup in case LCD is turned of. do-while is necessary to jump gameboy out of VBlank on next update
        var currentClockSpeed = gameBoy.GetCurrentClockSpeed();
        var cyclesToRun = currentClockSpeed / frameCounter.CurrentFramesPerSecond;
        
        var i = 0;
        do
        {
            gameBoy.Update();
            i++;
            if (i >= 2 * cyclesToRun)
            {
                break;
            }
        } while (!gameBoy.VBlankTriggered());
    }
}