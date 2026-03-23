using RetroEmu.Abstractions;
using RetroEmu.Devices.GameBoy;

namespace RetroEmu.Runtime.State;

public interface IEmulatorState
{
    public void HandleStart();
    
    public void HandlePause();

    public void HandleLoad(byte[] cartridgeData);

    public void HandleStep();

    void Update(IReadOnlyFrameCounter frameCounter, IGameBoy gameBoy);
}