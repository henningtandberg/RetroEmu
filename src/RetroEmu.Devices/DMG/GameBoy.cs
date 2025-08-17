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

    private const double DefaultFramesPerSecond = 59.7275;
    /// <summary>
    /// Update until VBlank, with backup in case LCD is turned off. do-while is necessary to break GameBoy out of
    /// VBlank on next update.
    /// </summary>
    /// <param name="framesPerSecond">If not provided, the GameBoy will run at a steady 59.7275 frame rate</param>
    public void RunAt(double framesPerSecond = DefaultFramesPerSecond)
    {
        var currentClockSpeed = processor.GetCurrentClockSpeed();
        var cyclesToRun = currentClockSpeed / framesPerSecond;
        
        var i = 0;
        do
        {
            Update();
            i++;
            if (i >= 2 * cyclesToRun)
            {
                break;
            }
        } while (!processor.VBlankTriggered());
    }

    // This can be replaced by getting/keeping a reference to IProcessor outside IGameBoy
    public IProcessor GetProcessor() => processor;

    
    // This can be replaced by getting/keeping a reference to IAddressBus outside IGameBoy
    public IAddressBus GetMemory() => addressBus;
}