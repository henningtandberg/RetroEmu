using System;
using System.Runtime.InteropServices.JavaScript;
using System.Text;

namespace RetroEmu.GB.TestSetup;

public class CartridgeBuilder
{
    private byte[] _cartridgeData;
    private byte[] _programData = [];

    private readonly byte[] _beginCodeExecutionPoint =
    [
        0x00, // NOP
        0xC3, // JP 0x0150
        0x50,
        0x01
    ];
    
    private readonly byte[] _scrollingGraphic =
    [
        0xCE, 0xED, 0x66, 0x66, 0xCC, 0x0D, 0x00, 0x0B, 0x03, 0x73, 0x00, 0x83, 0x00, 0x0C, 0x00, 0x0D,
        0x00, 0x08, 0x11, 0x1F, 0x88, 0x89, 0x00, 0x0E, 0xDC, 0xCC, 0x6E, 0xE6, 0xDD, 0xDD, 0xD9, 0x99,
        0xBB, 0xBB, 0x67, 0x63, 0x6E, 0x0E, 0xEC, 0xCC, 0xDD, 0xDC, 0x99, 0x9F, 0xBB, 0xB9, 0x33, 0x3E
    ];
    
    private const ushort TimerInterruptAddress = 0x50;
    private const int TimerInterruptHandlerSize = 8;
    private readonly byte[] _timerInterruptHandler = new byte[TimerInterruptHandlerSize];
    
    private const ushort JoypadInterruptAddress = 0x60;
    private const int JoypadInterruptHandlerSize = 8;
    private readonly byte[] _joypadInterruptHandler = new byte[JoypadInterruptHandlerSize];

    private string _gameTitle = "RetroEmu";
    private byte _gameBoyColorFlag;
    private byte _superGameBoyFlag;
    private byte _cartridgeType;
    private byte _romSize;
    private byte _ramSize;
    private byte _destinationCode = 0x01; // Non-Japanese
    private byte _licenseCode;


    private CartridgeBuilder()
    { }
    
    public static CartridgeBuilder Create() => new();

    public CartridgeBuilder WithGameTitle(string gameTitle)
    {
        _gameTitle = gameTitle;
        return this;
    }

    public CartridgeBuilder WithColorGameBoyEnabled()
    {
        _gameBoyColorFlag = 0x80;
        return this;
    }
    
    public CartridgeBuilder WithSuperGameBoySuperFunctionsEnabled()
    {
        _superGameBoyFlag = 0x03;
        return this;
    }

    public CartridgeBuilder WithGameBoyCartridgeType(byte cartridgeType)
    {
        _cartridgeType = cartridgeType;
        return this;
    }

    public CartridgeBuilder WithRomSize(byte romSize)
    {
        _romSize = romSize;
        return this;
    }

    public CartridgeBuilder WithRamSize(byte ramSize)
    {
        _ramSize = ramSize;
        return this;
    }

    public CartridgeBuilder WithDestinationCode(byte destinationCode)
    {
        _destinationCode = destinationCode;
        return this;
    }

    public CartridgeBuilder WithLicenseCodeOld(byte licenseCode)
    {
        _licenseCode = licenseCode;
        return this;
    }

    public CartridgeBuilder WithProgram(byte[] program)
    {
        _programData = new byte[program.Length];
        Buffer.BlockCopy(program, 0, _programData, 0, program.Length);
        return this;
    }

    public CartridgeBuilder WithJoypadInterruptHandler(byte[] joypadInterruptHandler)
    {
        Buffer.BlockCopy(joypadInterruptHandler, 0, _joypadInterruptHandler, 0, joypadInterruptHandler.Length);
        return this;
    }

    public CartridgeBuilder WithTimerInterruptHandler(byte[] timerInterruptHandler)
    {
        Buffer.BlockCopy(timerInterruptHandler, 0, _timerInterruptHandler, 0, timerInterruptHandler.Length);
        return this;
    }
    
    public byte[] Build()
    {
        var romSizeInBytes = _romSize switch
        {
            0x00 => 32 * 1024,
            0x01 => 64 * 1024,
            0x02 => 128 * 1024,
            0x03 => 256 * 1024,
            0x04 => 512 * 1024,
            0x05 => 1024 * 1024,
            0x06 => 2048 * 1024,
            0x52 => 1152 * 1024,
            0x53 => 1280 * 1024,
            0x54 => 1536 * 1024,
            _ => throw new ArgumentOutOfRangeException()
        };
        
        _cartridgeData = new byte[romSizeInBytes];
        
        SetJoypadInterruptHandler();
        SetTimerInterruptHandler();
        SetBeginCodeExecutionPoint();
        SetScrollGraphic();
        SetGameTitle();
        SetProgramData();
        
        _cartridgeData[0x0143] = _gameBoyColorFlag;
        _cartridgeData[0x0144] = (byte)((_licenseCode >> 4) & 0x0F);
        _cartridgeData[0x0145] = (byte)(_licenseCode & 0x0F);
        _cartridgeData[0x0146] = _superGameBoyFlag;
        _cartridgeData[0x0147] = _cartridgeType;
        _cartridgeData[0x0148] = _romSize;
        _cartridgeData[0x0149] = _ramSize;
        _cartridgeData[0x014A] = _destinationCode;
        _cartridgeData[0x014B] = _licenseCode;
        
        return _cartridgeData;
    }

    private void SetTimerInterruptHandler() =>
        Buffer.BlockCopy(_timerInterruptHandler, 0, _cartridgeData, TimerInterruptAddress, _timerInterruptHandler.Length);
    
    private void SetJoypadInterruptHandler() =>
        Buffer.BlockCopy(_joypadInterruptHandler, 0, _cartridgeData, JoypadInterruptAddress, _joypadInterruptHandler.Length);

    private void SetBeginCodeExecutionPoint() =>
        Buffer.BlockCopy(_beginCodeExecutionPoint, 0, _cartridgeData, 0x0100, _beginCodeExecutionPoint.Length);

    private void SetScrollGraphic() =>
        Buffer.BlockCopy(_scrollingGraphic, 0, _cartridgeData, 0x0104, _scrollingGraphic.Length);
    
    private void SetProgramData() =>
        Buffer.BlockCopy(_programData, 0, _cartridgeData, 0x0150, _programData.Length);
    
    private void SetGameTitle()
    {
        var endIndexOfTitle = _gameTitle.Length > 16 ? 16 : _gameTitle.Length;
        var croppedTitle = _gameTitle[..endIndexOfTitle].ToUpper();
        var titleBytes = Encoding.ASCII.GetBytes(croppedTitle);
        
        Buffer.BlockCopy(titleBytes, 0, _cartridgeData, 0x0134, titleBytes.Length);
    }
}