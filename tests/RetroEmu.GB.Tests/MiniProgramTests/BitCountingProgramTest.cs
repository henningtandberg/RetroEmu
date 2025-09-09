using RetroEmu.Devices.DMG;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.GB.TestSetup;
using Xunit;

namespace RetroEmu.GB.Tests.MiniProgramTests;

public class BitCountingProgramTest
{
    private readonly IGameBoy _gameBoy = TestGameBoyBuilder.CreateBuilder().BuildGameBoy();
    
    [Theory]
    [InlineData(0x01, 1)]
    [InlineData(0x02, 1)]
    [InlineData(0x0f, 4)]
    [InlineData(0xf1, 5)]
    [InlineData(0xff, 8)]
    public void BitCountingProgramUsingRotateLeftThroughCarry1_RunWhileStillBitsToCount_AmountOfBitsAreCalculatedCorrectly(byte a, byte expectedValue)
    {
        // Pseudocode:
        // B = a; <- Input
        // C = 1; <- Current bit we are looking at
        // D = 0; <- Bit count
        // do
        // {
        //   A = B;
        //   A = A & C;
        //   A = A + 0xff; <- Will overflow if A != 0;
        //   A = 0;
        //   if (CarryFlag is set) A = 1;
        //   A = A + D;
        //   D = A;
        //   A = C;
        //   A << 1;
        //   C = A;
        // } while (CarryFlag not set)
        var cartridge = CartridgeBuilder
             .Create()
             .WithProgram([
                Opcode.Ld_B_N8,
                a,
                Opcode.Ld_C_N8,
                1,
                Opcode.Ld_D_N8,
                0,
                Opcode.Ld_A_B,
                Opcode.And_A_C,
                Opcode.Add_A_N8,
                0xff,
                Opcode.Ld_A_N8,
                0x00,
                Opcode.Adc_A_N8,
                0x00,
                Opcode.Add_A_D,
                Opcode.Ld_D_A,
                Opcode.Ld_A_C,
                Opcode.Rlc_A,
                Opcode.Ld_C_A,
                Opcode.JpNC_N16,
                0x56,
                0x01,
                Opcode.Ld_A_D,
                Opcode.Nop
             ])
             .Build();
        _gameBoy.Load(cartridge);
        var processor = (ITestableProcessor)_gameBoy.GetProcessor();
        processor.SetProgramCounter(0x0150); // Skip program start routine at 0x0100 (NOP + JP N16)
        
        while (processor.GetValueOfRegisterPC() != 0x0168)
        {
            _ = _gameBoy.Update();
        }
        Assert.Equal(expectedValue, processor.GetValueOfRegisterA());
    }
    
    [Theory]
    [InlineData(0x01, 1)]
    [InlineData(0x02, 1)]
    [InlineData(0x0f, 4)]
    [InlineData(0xf1, 5)]
    [InlineData(0xff, 8)]
    public void BitCountingProgramUsingRotateRightThroughCarry1_RunWhileStillBitsToCount_AmountOfBitsAreCalculatedCorrectly(byte a, byte expectedValue)
    {
           // Pseudocode:
           // B = a; <- Input
           // C = 0x80; <- Current bit we are looking at
           // D = 0; <- Bit count
           // do
           // {
           //   A = B;
           //   A = A & C;
           //   A = A + 0xff; <- Will overflow if A != 0;
           //   A = 0;
           //   if (CarryFlag is set) A = 1;
           //   A = A + D;
           //   D = A;
           //   A = C;
           //   A >> 1;
           //   C = A;
           // } while (CarryFlag not set)
           var cartridge = CartridgeBuilder
               .Create()
               .WithProgram([
                   Opcode.Ld_B_N8,
                   a,
                   Opcode.Ld_C_N8,
                   0x80,
                   Opcode.Ld_D_N8,
                   0,
                   Opcode.Ld_A_B,
                   Opcode.And_A_C,
                   Opcode.Add_A_N8,
                   0xff,
                   Opcode.Ld_A_N8,
                   0x00,
                   Opcode.Adc_A_N8,
                   0x00,
                   Opcode.Add_A_D,
                   Opcode.Ld_D_A,
                   Opcode.Ld_A_C,
                   Opcode.Rrc_A,
                   Opcode.Ld_C_A,
                   Opcode.JpNC_N16,
                   0x56,
                   0x01,
                   Opcode.Ld_A_D,
                   Opcode.Nop
               ])
               .Build();
        _gameBoy.Load(cartridge);
        var processor = (TestableProcessor)_gameBoy.GetProcessor();
        processor.SetProgramCounter(0x0150);
        
        while (processor.GetValueOfRegisterPC() != 0x0168)
        {
            _ = _gameBoy.Update();
        }
        
        Assert.Equal(expectedValue, processor.GetValueOfRegisterA());
    }

    [Theory]
    [InlineData(0x01, 1)]
    [InlineData(0x02, 1)]
    [InlineData(0x0f, 4)]
    [InlineData(0xf1, 5)]
    [InlineData(0xff, 8)]
    public void BitCountingProgramUsingRotateLeftThroughCarry2_RunWhileStillBitsToCount_AmountOfBitsAreCalculatedCorrectly(byte a, byte expectedValue)
    {
        var cartridge = CartridgeBuilder
            .Create()
            .WithProgram([
                Opcode.Ld_A_N8, // A = a; <- Input
                a,
                Opcode.Ld_B_N8, // B = 8; <- Bits left to count
                0x08,
                Opcode.Ld_C_N8, // C = 0; <- Bit count
                0,
                Opcode.Rlc_A, // A << 1; <- Old bit 7 is now in carry flag
                Opcode.JpNC_N16, // Jump to 0x015B if carry flag is not set
                0x5B,
                0x01,
                Opcode.Inc_C, // Increment C (Counter)
                Opcode.Dec_B, // Decrement B (Bits left to count)
                Opcode.JpNZ_N16, // Jump to 0x0156
                0x56,
                0x01,
                Opcode.Ld_A_C // Load count into A
            ])
            .Build();
        _gameBoy.Load(cartridge);
        var processor = (ITestableProcessor)_gameBoy.GetProcessor();
        processor.SetProgramCounter(0x0150);
        
        _gameBoy.RunWhile(() => processor.GetValueOfRegisterPC() != 0x0161);
        
        Assert.Equal(expectedValue, processor.GetValueOfRegisterA());
    }
    
    [Theory]
    [InlineData(0x01, 1)]
    [InlineData(0x02, 1)]
    [InlineData(0x0f, 4)]
    [InlineData(0xf1, 5)]
    [InlineData(0xff, 8)]
    public void BitCountingProgramUsingRotateRightThroughCarry2_RunWhileStillBitsToCount_AmountOfBitsAreCalculatedCorrectly(byte a, byte expectedValue)
    {
        var cartridge = CartridgeBuilder
            .Create()
            .WithProgram([
                Opcode.Ld_A_N8, // A = a; <- Input
                a,
                Opcode.Ld_B_N8, // B = 8; <- Bits left to count
                0x08,
                Opcode.Ld_C_N8, // C = 0; <- Bit count
                0,
                Opcode.Rrc_A, // A >> 1; <- Old bit 0 is now in carry flag
                Opcode.JpNC_N16, // Jump to 0x015B if carry flag is not set
                0x5B,
                0x01,
                Opcode.Inc_C, // Increment C (Counter)
                Opcode.Dec_B, // Decrement B (Bits left to count)
                Opcode.JpNZ_N16, // Jump to 0x0156
                0x56,
                0x01,
                Opcode.Ld_A_C // Load count into A
            ])
            .Build();
        _gameBoy.Load(cartridge);
        var processor = (ITestableProcessor)_gameBoy.GetProcessor();
        
        _gameBoy.RunWhile(() => processor.GetValueOfRegisterPC() != 0x0161);
        
        Assert.Equal(expectedValue, processor.GetValueOfRegisterA());
    }
}