using RetroEmu.Devices.GameBoy;
using RetroEmu.Runtime.Input;
using RetroEmu.Runtime.State;

namespace RetroEmu.Runtime;

public class EmulatorOrchestrator(
    IGameBoy gameBoy,
    IEmulatorStateContext stateContext,
    IInputManager inputManager)
    : IEmulatorOrchestrator
{
    private readonly FrameCounter _frameCounter = new();

    public void Initialize() { }

    public void Update(TimeSpan deltaTime)
    {
        _frameCounter.Update(deltaTime);
        inputManager.ProcessInput(gameBoy);
        stateContext.Update(_frameCounter, gameBoy);
    }
}