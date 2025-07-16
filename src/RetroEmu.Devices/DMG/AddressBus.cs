using System;
using RetroEmu.Devices.DMG.CPU.Interrupts;
using RetroEmu.Devices.DMG.CPU.PPU;
using RetroEmu.Devices.DMG.CPU.Timing;
using RetroEmu.Devices.DMG.ROM;

namespace RetroEmu.Devices.DMG;

public class AddressBus(
    ITimer timer,
    IPixelProcessingUnit pixelProcessingUnit,
    IInterruptState interruptState,
    IJoypad joypad,
    ICartridge cartridge) : IAddressBus
{
    private readonly byte[] _memory = new byte[0x10000];

    string output = "";

    public string GetOutput()
    {
        return output;
    }

    public void Reset()
    {
        Array.Clear(_memory, 0, _memory.Length);
    }

    public byte Read(ushort address) => address switch
    {
        <= 0x7FFF => cartridge.ReadROM(address),
        <= 0x9FFF => pixelProcessingUnit.ReadVRAM(address),
        <= 0xBFFF => cartridge.ReadRAM(address),
        <= 0xDFFF => _memory[address],
        <= 0xFDFF => _memory[address - 0x2000], // Echo ram of 0xC000 -> 0xDE00
        <= 0xFE9F => pixelProcessingUnit.ReadOAM(address),
        <= 0xFEFF => 0x00, // Unused
        0xFF00 => joypad.P1,
        0xFF01 => 0x00, // SB - Serial transfer
        0xFF02 => 0x7E, // SC - Serial control - 0x7E is the expected startup value
        0xFF03 => 0x00, // Unused
        0xFF04 => timer.Divider,
        0xFF05 => timer.Counter,
        0xFF06 => timer.Modulo,
        0xFF07 => timer.Control,
        <= 0xFF0E => 0x00, // Unused
        0xFF0F => interruptState.InterruptFlag,
        <= 0xFF25 => 0x00, // Sound Registers
        <= 0xFF29 => 0x00, // Unused
        <= 0xFF39 => 0x00, // Sound RAM
        0xFF40 => pixelProcessingUnit.LCDC,
        0xFF41 => pixelProcessingUnit.STAT,
        0xFF42 => pixelProcessingUnit.SCY,
        0xFF43 => pixelProcessingUnit.SCX,
        0xFF44 => pixelProcessingUnit.LY,
        0xFF45 => pixelProcessingUnit.LYC,
        0xFF46 => 0xFF, // OAM DMA
        0xFF47 => 0xFC, // BGP - BG Palette - 0xFC is expected startup value
        0xFF48 => 0xFF, // OBP0 - Object Palette 0 - 0xFF is expected startup value
        0xFF49 => 0xFF, // OBP1 - Object Palette 1 - 0xFF is expected startup value
        0xFF4A => pixelProcessingUnit.WY,
        0xFF4B => pixelProcessingUnit.WX,
        <= 0xFF7F => 0x00, // Unused
        <= 0xFFFE => _memory[address], // HRAM - High RAM
        0xFFFF => interruptState.InterruptEnable
    };

    public void Write(ushort address, byte value)
    {
        switch (address)
        {
            case <= 0x7FFF:
                cartridge.WriteROM(address, value);
                break;
            case <= 0x9FFF:
                pixelProcessingUnit.WriteVRAM(address, value);
                break;
            case <= 0xBFFF:
                cartridge.WriteRAM(address, value);
                break;
            case <= 0xDFFF:
                _memory[address] = value;
                break;
            case <= 0xFDFF: // Echo ram of 0xC000 -> 0xDE00
                _memory[address - 0x2000] = value;
                break;
            case <= 0xFE9F:
                pixelProcessingUnit.WriteOAM(address, value);
                break;
            case 0xFF00:
                joypad.P1 = value;
                break;
            case 0xFF01: // SB - Serial transfer
                _memory[address] = value;
                break;
            case 0xFF02: // SC - Serial Control
            {
                if (value == 0x81)
                {
                    var letter = (char)_memory[0xFF01]; // Get value from SB

                    output += letter;
                    Console.Write(letter);
                }

                break;
            }
            case 0xFF03: // Unused
                break;
            case 0xFF04:
                timer.Divider = 0;
                break;
            case 0xFF05:
                timer.Counter = value;
                break;
            case 0xFF06:
                timer.Modulo = value;
                break;
            case 0xFF07:
                timer.Control = value;
                break;
            case <= 0xFF0E: // Unused
                break;
            case 0xFF0F:
                interruptState.InterruptFlag = value;
                break;
            case <= 0xFF25: // Sound Registers
            case <= 0xFF29: // Unused
            case <= 0xFF39: // Sound RAM
                break;
            case 0xFF40:
                pixelProcessingUnit.LCDC = value;
                break;
            case 0xFF41:
                pixelProcessingUnit.STAT = value;
                break;
            case 0xFF42:
                pixelProcessingUnit.SCY = value;
                break;
            case 0xFF43:
                pixelProcessingUnit.SCX = value;
                break;
            case 0xFF45:
                pixelProcessingUnit.LYC = value;
                break;
            case 0xFF46:
                pixelProcessingUnit.StartDMATransfer(value, this);
                break;
            case 0xFF47: // BGP - BG Palette
            case 0xFF48: // OBP0 - Object Palette 0
            case 0xFF49: // OBP1 - Object Palette 1
                break;
            case 0xFF4A:
                pixelProcessingUnit.WY = value;
                break;
            case 0xFF4B:
                pixelProcessingUnit.WX = value;
                break;
            case <= 0xFF7F: // Unused
                break;
            case <= 0xFFFE: // HRAM - High RAM
                _memory[address] = value;
                break;
            case 0xFFFF:
                interruptState.InterruptEnable = value;
                break;
        }
    }
}