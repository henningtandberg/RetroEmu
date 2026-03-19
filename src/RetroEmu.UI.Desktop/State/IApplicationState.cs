using RetroEmu.Devices.DMG;

namespace RetroEmu.UI.Desktop.State;

public interface IApplicationState
{
    public void HandleStart();
    
    public void HandlePause();

    public void HandleLoad(byte[] cartridgeData);

    public void HandleStep();

    void Update(IFrameCounter frameCounter, IGameBoy gameBoy);
}