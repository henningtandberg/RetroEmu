using System.Text;
using RetroEmu.Devices.DMG;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.GB.TestSetup;
using Xunit;

namespace RetroEmu.GB.Tests.MiniProgramTests;

public class ConsoleLogProgramTest
{
    private static readonly WireFake WireFake = new();
    
    private readonly IGameBoy _gameBoy = TestGameBoyBuilder
        .CreateBuilder()
        .WithWireFake(WireFake)
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
            Opcode.Ld_A_XN16,
            0x02,
            0xFF,
            Opcode.Pre_CB,
            CBOpcode.Bit7_A,
            Opcode.JrNZ_N8,
            0xF9,
            
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
            Opcode.Ld_A_XN16,
            0x02,
            0xFF,
            Opcode.Pre_CB,
            CBOpcode.Bit7_A,
            Opcode.JrNZ_N8,
            0xF9,
            
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
            Opcode.Ld_A_XN16,
            0x02,
            0xFF,
            Opcode.Pre_CB,
            CBOpcode.Bit7_A,
            Opcode.JrNZ_N8,
            0xF9,
            
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
            Opcode.Ld_A_XN16,
            0x02,
            0xFF,
            Opcode.Pre_CB,
            CBOpcode.Bit7_A,
            Opcode.JrNZ_N8,
            0xF9,
            
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
            Opcode.Ld_A_XN16,
            0x02,
            0xFF,
            Opcode.Pre_CB,
            CBOpcode.Bit7_A,
            Opcode.JrNZ_N8,
            0xF9,
            
            Opcode.Ld_BC_N16,   // Set BC to 1337 to indicate finished program
            0x39,
            0x05,
            Opcode.Jr_N8,       // Spin forever
            0xFE
        ])
        .Build();
    
    [Fact]
    public void ProgramThatWritesHelloUsingSBAndSC_OutputsHello()
    {
        _gameBoy.Load(_consoleLogHelloCartridge);
        var processor = (ITestableProcessor)_gameBoy.GetProcessor();
        
        _gameBoy.RunWhile(() => processor.GetValueOfRegisterBC() != 1337); // 1337 == 0x0539
        
        var actualOutput = WireFake.AllOutgoingData();
        Assert.Equal("HELLO", Encoding.ASCII.GetString(actualOutput));
    }
}