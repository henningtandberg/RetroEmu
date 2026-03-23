using RetroEmu.Abstractions;
using RetroEmu.Devices.GameBoy;
using RetroEmu.Runtime.Input;
using RetroEmu.Runtime.State;

namespace RetroEmu.Runtime;

public class EmulatorOrchestrator(
    IGameBoy gameBoy,
    IEmulatorStateContext stateContext,
    IInputManager inputManager,
    IReadOnlyFrameCounter frameCounter)
    : IEmulatorOrchestrator
{

    public void Initialize() { }

    public void Update()
    {
        inputManager.ProcessInput(gameBoy);
        stateContext.Update(frameCounter, gameBoy);
    }
}