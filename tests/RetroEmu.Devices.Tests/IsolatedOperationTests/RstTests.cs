using System.Collections.Generic;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.Devices.Tests.Setup;
using Xunit;

namespace RetroEmu.Devices.Tests.IsolatedOperationTests;

public class RstTests
{
    [Theory]
    [InlineData( Opcode.Rst_00H, 0x00)]
    [InlineData( Opcode.Rst_08H, 0x08)]
    [InlineData( Opcode.Rst_10H, 0x10)]
    [InlineData( Opcode.Rst_18H, 0x18)]
    [InlineData( Opcode.Rst_20H, 0x20)]
    [InlineData( Opcode.Rst_28H, 0x28)]
    [InlineData( Opcode.Rst_30H, 0x30)]
    [InlineData( Opcode.Rst_38H, 0x38)]
    public static void Rst_ProgramCounterAndStackPointerSetCorrectly(byte opcode, ushort expectedPc)
    {
        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithProcessor(processor => processor
                .Set8BitGeneralPurposeRegisters(0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00)
                .SetFlags(false, false, false, false)
                .SetStackPointer(0x0102)
                .SetProgramCounter(0x0001)
            )
            .WithMemory(() => new Dictionary<ushort, byte>
            {
                [0x0001] = opcode
            })
            .BuildGameBoy();
        
        var cycles = gameBoy.Update();
        
        var processor = gameBoy.GetProcessor();
        Assert.Equal(32, cycles);
        Assert.Equal(expectedPc, processor.GetValueOfRegisterPC());
        Assert.Equal(0x0100, processor.GetValueOfRegisterSP());
    }
}