using RetroEmu.Devices.DMG;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.GB.TestSetup;
using Xunit;

namespace RetroEmu.GB.Tests.MiniProgramTests;

public class MultiplierProgramTest
{
    private readonly IGameBoy _gameBoy = TestGameBoyBuilder.CreateBuilder().BuildGameBoy();

    [Theory]
    [InlineData(1, 0, 0)]
    [InlineData(0, 1, 0)]
    [InlineData(1, 1, 1)]
    [InlineData(2, 2, 4)]
    [InlineData(10, 2, 20)]
    [InlineData(13, 15, 195)]
    [InlineData(2, 100, 200)]
    [InlineData(1, 100, 100)]
    [InlineData(100, 1, 100)]
    public void MultiplierProgram_MultipliesTwoNumbersUsingALoop_ProductIsStoredCorrectlyInA(
        byte x, byte y, byte expectedProduct)
    {
        var cartridge = CartridgeBuilder
            .Create()
            .WithProgram([
                // A => General purpose register
                Opcode.Ld_B_N8, // B = x;
                x,
                Opcode.Ld_C_N8, // C = y;
                y,
                Opcode.Ld_A_C, // A = C;
                Opcode.Cp_A_N8, // if (y == 0) goto end
                0x00,
                Opcode.JpZ_N16,
                0x66,
                0x01,
                Opcode.Ld_A_B, // A = B;
                Opcode.Cp_A_N8, // if (x == 0) goto end
                0x00,
                Opcode.JpZ_N16,
                0x66,
                0x01,
                Opcode.Ld_A_N8, // A = 0;
                0x00,
                Opcode.Add_A_B, // A = A + B;
                Opcode.Dec_C, // Decrement C until zero
                Opcode.JpNZ_N16, // Jump to 0x0162
                0x62,
                0x01,
                Opcode.Nop // END
            ])
            .Build();
        _gameBoy.Load(cartridge);
        var processor = (ITestableProcessor)_gameBoy.GetProcessor();
        
        _gameBoy.RunWhile(() => processor.GetValueOfRegisterPC() < 0x0166);

        var actualProduct = processor.GetValueOfRegisterA();
        Assert.Equal(expectedProduct, actualProduct);
    }
}