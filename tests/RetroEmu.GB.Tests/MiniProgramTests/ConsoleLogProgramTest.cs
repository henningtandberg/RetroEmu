using RetroEmu.Devices.DMG;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.GB.TestSetup;
using Xunit;

namespace RetroEmu.GB.Tests.MiniProgramTests;

public class ConsoleLogProgramTest
{
    private readonly IGameBoy _gameBoy = TestGameBoyBuilder
        .CreateBuilder()
        .BuildGameBoy();
    
    private readonly byte[] _consoleLogHelloCartridge = CartridgeBuilder
        .Create()
        .WithProgram([
            Opcode.Ld_A_N8,
            (byte)'H',
            Opcode.Ld_XN16_A,
            0x01,
            0xFF,
            Opcode.Ld_A_N8,
            0x81,
            Opcode.Ld_XN16_A,
            0x02,
            0xFF,
            Opcode.Ld_A_N8,
            (byte)'E',
            Opcode.Ld_XN16_A,
            0x01,
            0xFF,
            Opcode.Ld_A_N8,
            0x81,
            Opcode.Ld_XN16_A,
            0x02,
            0xFF,
            Opcode.Ld_A_N8,
            (byte)'L',
            Opcode.Ld_XN16_A,
            0x01,
            0xFF,
            Opcode.Ld_A_N8,
            0x81,
            Opcode.Ld_XN16_A,
            0x02,
            0xFF,
            Opcode.Ld_A_N8,
            (byte)'L',
            Opcode.Ld_XN16_A,
            0x01,
            0xFF,
            Opcode.Ld_A_N8,
            0x81,
            Opcode.Ld_XN16_A,
            0x02,
            0xFF,
            Opcode.Ld_A_N8,
            (byte)'O',
            Opcode.Ld_XN16_A,
            0x01,
            0xFF,
            Opcode.Ld_A_N8,
            0x81,
            Opcode.Ld_XN16_A,
            0x02,
            0xFF,
        ])
        .Build();
    
    [Fact]
    public void ProgramThatWritesHelloUsingSBAndSC_OutputsHello()
    {
        _gameBoy.Load(_consoleLogHelloCartridge);
        var processor = (ITestableProcessor)_gameBoy.GetProcessor();
        processor.SetProgramCounter(0x0150); // Skip program start routine at 0x0100 (NOP + JP N16)
        
        _gameBoy.RunWhile(() => processor.GetValueOfRegisterPC() < (0x0150 + 0x32));
        
        Assert.Equal("HELLO", _gameBoy.GetOutput());
    }
}