using RetroEmu.Devices.Disassembly;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.Devices.DMG.ROM;

namespace RetroEmu.Devices.DMG;

public class GameBoy(
    IDisassembler disassembler,
    ICartridge cartridge,
    IAddressBus addressBus,
    IProcessor processor,
    IJoypad joypad)
    : IGameBoy
{
    public string GetOutput() => addressBus.GetOutput();

    public int GetCurrentClockSpeed() => processor.GetCurrentClockSpeed();

    public void Reset()
    {
        addressBus.Reset();
        processor.Reset();
    }

    public void Load(byte[] cartridgeMemory)
    {
        cartridge.Load(cartridgeMemory);
        processor.Reset();
    }

    public void ButtonPressed(Button button) =>
        joypad.PressButton((byte)button);

    public void ButtonReleased(Button button) =>
        joypad.ReleaseButton((byte)button);

    public void DPadPressed(DPad direction) =>
        joypad.PressDPad((byte)direction);

    public void DPadReleased(DPad direction) =>
        joypad.ReleaseDPad((byte)direction);

    public CartridgeHeader GetCartridgeInfo() =>
        cartridge.GetCartridgeInfo();

    public int Update()
    {
        disassembler.DisassembleNextInstruction();
        return processor.Update();
    }
    
    public bool VBlankTriggered() =>
        processor.VBlankTriggered();

    // This can be replaced by getting/keeping a reference to IProcessor outside IGameBoy
    public IProcessor GetProcessor() => processor;

    
    // This can be replaced by getting/keeping a reference to IAddressBus outside IGameBoy
    public IAddressBus GetMemory() => addressBus;
}