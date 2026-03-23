using RetroEmu.Devices.GameBoy.Cartridge;
using RetroEmu.Devices.GameBoy.Input;

namespace RetroEmu.Devices.GameBoy;

public interface IGameBoy
{
    public void Reset();
    void Load(byte[] rom);
    int Update();
    void RunAt(double frameRate);

    void ButtonPressed(Button button);
    void ButtonReleased(Button button);
    void DPadPressed(DPad direction);
    void DPadReleased(DPad direction);

    CartridgeHeader GetCartridgeInfo();
}
