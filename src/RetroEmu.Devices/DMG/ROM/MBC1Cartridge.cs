using System;

namespace RetroEmu.Devices.DMG.ROM;

public class MBC1Cartridge : ICartridge
{
    private CartridgeHeader _cartridgeHeader;
    private byte[] _cartridgeRom;
    private byte[] _cartridgeRam;
    private bool _ramEnabled = false;
    private byte _romBankNumber = 0x01;
    private byte _ramBankNumber;
    private byte _modeSelect;

    public CartridgeHeader GetCartridgeInfo() => _cartridgeHeader;

    public void Load(byte[] rom)
    {
        _cartridgeHeader = CartridgeHeaderBuilder
            .Create(rom)
            .Build();

        _cartridgeRom = new byte[_cartridgeHeader.RomSizeInfo.SizeBytes];
        Buffer.BlockCopy(rom, 0, _cartridgeRom, 0, _cartridgeRom.Length);

        _cartridgeRam = new byte[_cartridgeHeader.RamSizeInfo.SizeBytes];
    }

    public byte ReadROM(ushort address)
    {
        var actualAddress = (_modeSelect, address) switch
        {
            (0, <= 0x3FFF) => address,
            (1, <= 0x3FFF) => (ushort)(0x4000 * ZeroBankNumber + address),
            (_, <= 0x7FFF) => (ushort)(0x4000 * HighBankNumber + (address - 0x4000)),
            _ => throw new ArgumentOutOfRangeException("Should not get here!?")
        };

        return _cartridgeRom[actualAddress];
    }

    private static byte SetBitTo(byte value, byte bitNumber, byte bitValue)
    {
        var mask = (byte)((0x01 & bitValue) << bitNumber);
        var valueWitBitDisabled = (byte)(value & ~(0x01 << bitNumber));
        return (byte)(valueWitBitDisabled | mask);
    }

    private byte HighBankNumber => _cartridgeHeader.RomSizeInfo.BankCount switch
    {
        <= 32 => (byte)(_romBankNumber & RomBankNumberMask),
        64 => SetBitTo((byte)(_romBankNumber & RomBankNumberMask), 5, _ramBankNumber),
        128 => SetBitTo(SetBitTo((byte)(_romBankNumber & RomBankNumberMask), 5, _ramBankNumber), 6, (byte)(_ramBankNumber >> 1)),
        var romBankCount =>
            throw new ArgumentOutOfRangeException($"HighBankNumber undetermined as RomBankCount is: {romBankCount}")
    };

    private byte ZeroBankNumber => _cartridgeHeader.RomSizeInfo.BankCount switch
    {
        <= 32 => 0,
        64 => (byte)(_ramBankNumber << 5 & 0b0010000),
        128 => (byte)(_ramBankNumber << 5 & 0b0110000),
        var romBankCount =>
            throw new ArgumentOutOfRangeException($"ZeroBankNumber undetermined as RomBankCount is: {romBankCount}")
    };

    public void WriteROM(ushort address, byte value)
    {
        switch (address)
        {
            case <= 0x1FFF:
                SetExternalRam(value);
                break;
            case <= 0x3FFF:
                SetRomBankNumber(value);
                break;
            case <= 0x5FFF:
                SetRamBankNumber(value);
                break;
            case <= 0x7FFF:
                SetModeSelect(value);
                break;
            default: throw new Exception("Hmmm");
        }
    }

    private const byte ModeSelectMask = 0x01;
    private void SetModeSelect(byte value) =>
        _modeSelect = (byte)(value & ModeSelectMask);

    private const byte RamBankNumberMask = 0x03;
    private void SetRamBankNumber(byte value) =>
        _ramBankNumber = (byte)(value & RamBankNumberMask);

    private byte RomBankNumberMask => _cartridgeHeader.RomSizeInfo.BankCount switch
    {
        2 => 0x01,
        4 => 0x03,
        8 => 0x07,
        16 => 0x0F,
        <= 128 => 0x1F,
        var value => throw new ArgumentOutOfRangeException($"RomBankNumber provided was {value}. Is that allowed?")
    };
    
    private void SetRomBankNumber(byte value) =>
        _romBankNumber = (byte)(value & RomBankNumberMask) is var romBankNumber && romBankNumber != 0
            ? romBankNumber
            : (byte)0x01;

    private void SetExternalRam(byte value) =>
        _ramEnabled = value == 0x0A;

    public byte ReadRAM(ushort address)
    {
        if (!_ramEnabled)
            return 0xFF;

        var calculatedAddress = CalculateRAMAddress(address);
        return _cartridgeRam[calculatedAddress];
    }

    public void WriteRAM(ushort address, byte value)
    {
        if (!_ramEnabled)
            return;
        
        var calculateAddress = CalculateRAMAddress(address);
        _cartridgeRam[calculateAddress] = value;
    }

    private ushort CalculateRAMAddress(ushort address)
    {
        var ramSizeKiloBytes = _cartridgeHeader.RamSizeInfo.SizeBytes / 1024;

        var actualAddress = (_modeSelect, ramSizeKiloBytes) switch
        {
            (_, 2 or 8) => (ushort)((address - 0xA000) % _cartridgeHeader.RamSizeInfo.SizeBytes),
            (1, 32) => (ushort)(0x2000 * _ramBankNumber + (address - 0xA000)),
            (0 , 32) => (ushort)(address - 0xA000),
            _ => throw new ArgumentOutOfRangeException($"ModeSelect {_modeSelect}, and RAM in KiloBytes {ramSizeKiloBytes} not recognized as as valid combination")
        };
        return actualAddress;
    }
}