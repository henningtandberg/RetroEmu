using RetroEmu.Devices.GameBoy.Cartridge;
using RetroEmu.Devices.GameBoy.CPU.Interrupts;
using RetroEmu.Devices.GameBoy.Input;
using RetroEmu.Devices.GameBoy.Memory;
using RetroEmu.Devices.GameBoy.PPU;
using RetroEmu.Devices.GameBoy.Serial;
using RetroEmu.Devices.GameBoy.Timer;

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