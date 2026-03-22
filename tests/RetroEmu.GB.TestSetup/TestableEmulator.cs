using RetroEmu.Devices.GameBoy;
using RetroEmu.Devices.GameBoy.CPU;
using RetroEmu.Devices.GameBoy.CPU.Link;
using RetroEmu.Devices.GameBoy.Disassembly;
using RetroEmu.Devices.GameBoy.ROM;

namespace RetroEmu.GB.TestSetup;

public class TestableEmulator(
    IDisassembler disassembler,
    ICartridge cartridge,
    IAddressBus addressBus,
    IProcessor processor,
    IJoypad joypad,
    ISerial serial)
    : GameBoy(disassembler, cartridge, addressBus, processor, joypad, serial),
        ITestableEmulator
{
    public ITestableProcessor GetProcessor() => (ITestableProcessor)processor;
}