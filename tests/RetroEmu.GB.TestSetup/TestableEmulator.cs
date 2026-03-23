using RetroEmu.Devices.GameBoy;
using RetroEmu.Devices.GameBoy.Cartridge;
using RetroEmu.Devices.GameBoy.CPU;
using RetroEmu.Devices.GameBoy.Disassembly;
using RetroEmu.Devices.GameBoy.Input;
using RetroEmu.Devices.GameBoy.Memory;
using RetroEmu.Devices.GameBoy.Serial;

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
    public ITestableAddressBus GetMemory() => (ITestableAddressBus)addressBus;
}