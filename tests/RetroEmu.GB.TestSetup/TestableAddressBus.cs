using RetroEmu.Devices.GameBoy;
using RetroEmu.Devices.GameBoy.CPU.Interrupts;
using RetroEmu.Devices.GameBoy.CPU.Link;
using RetroEmu.Devices.GameBoy.CPU.PPU;
using RetroEmu.Devices.GameBoy.CPU.Timing;
using RetroEmu.Devices.GameBoy.ROM;

namespace RetroEmu.GB.TestSetup;

public class TestableAddressBus(
    ITimer timer,
    IPixelProcessingUnit pixelProcessingUnit,
    IInterruptState interruptState,
    IJoypad joypad,
    ISerial serial,
    ICartridge cartridge,
    IInternalRam internalRam)
    : AddressBus(timer, pixelProcessingUnit, interruptState, joypad, serial, cartridge, internalRam),
        ITestableAddressBus;