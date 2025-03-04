using System.Collections.Generic;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.GB.TestSetup;
using Xunit;

namespace RetroEmu.GB.Tests.MiniProgramTests;

public class ConsoleLogProgramTest
{
    [Fact]
    public static void
        WithMemory_WriteToSBSC_Check()
    {
        var gameBoy = TestGameBoyBuilder
           .CreateBuilder()
           .WithProcessor(processor => processor
               .Set8BitGeneralPurposeRegisters(0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0)
               .SetProgramCounter(0x0000))
           .WithMemory(() => new Dictionary<ushort, byte>
           {
               [0x0000] = Opcode.Ld_A_N8,
               [0x0001] = (byte)'H',
               [0x0002] = Opcode.Ld_XN16_A,
               [0x0003] = 0x01,
               [0x0004] = 0xFF,
               [0x0005] = Opcode.Ld_A_N8,
               [0x0006] = 0x81,
               [0x0007] = Opcode.Ld_XN16_A,
               [0x0008] = 0x02,
               [0x0009] = 0xFF,
               [0x000A] = Opcode.Ld_A_N8,
               [0x000B] = (byte)'E',
               [0x000C] = Opcode.Ld_XN16_A,
               [0x000D] = 0x01,
               [0x000E] = 0xFF,
               [0x000F] = Opcode.Ld_A_N8,
               [0x0010] = 0x81,
               [0x0011] = Opcode.Ld_XN16_A,
               [0x0012] = 0x02,
               [0x0013] = 0xFF,
               [0x0014] = Opcode.Ld_A_N8,
               [0x0015] = (byte)'L',
               [0x0016] = Opcode.Ld_XN16_A,
               [0x0017] = 0x01,
               [0x0018] = 0xFF,
               [0x0019] = Opcode.Ld_A_N8,
               [0x001A] = 0x81,
               [0x001B] = Opcode.Ld_XN16_A,
               [0x001C] = 0x02,
               [0x001D] = 0xFF,
               [0x001E] = Opcode.Ld_A_N8,
               [0x001F] = (byte)'L',
               [0x0020] = Opcode.Ld_XN16_A,
               [0x0021] = 0x01,
               [0x0022] = 0xFF,
               [0x0023] = Opcode.Ld_A_N8,
               [0x0024] = 0x81,
               [0x0025] = Opcode.Ld_XN16_A,
               [0x0026] = 0x02,
               [0x0027] = 0xFF,
               [0x0028] = Opcode.Ld_A_N8,
               [0x0029] = (byte)'O',
               [0x002A] = Opcode.Ld_XN16_A,
               [0x002B] = 0x01,
               [0x002C] = 0xFF,
               [0x002D] = Opcode.Ld_A_N8,
               [0x002E] = 0x81,
               [0x002F] = Opcode.Ld_XN16_A,
               [0x0030] = 0x02,
               [0x0031] = 0xFF,
           })
           .BuildGameBoy();

        var processor = (ITestableProcessor)gameBoy.GetProcessor();
        while (processor.GetValueOfRegisterPC() < 0x32)
        {
            _ = gameBoy.Update();
        }
        Assert.Equal("HELLO", gameBoy.GetOutput());
    }
    
}