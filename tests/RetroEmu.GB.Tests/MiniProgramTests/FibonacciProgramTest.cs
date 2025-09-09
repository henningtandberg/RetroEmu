using RetroEmu.Devices.DMG;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.GB.TestSetup;
using Xunit;

namespace RetroEmu.GB.Tests.MiniProgramTests;

public class FibonacciProgramTest
{
    private readonly IGameBoy _gameBoy = TestGameBoyBuilder.CreateBuilder().BuildGameBoy();

    [Theory]
    [InlineData(1, 1, 1, 2)]
    [InlineData(1, 1, 2, 3)]
    [InlineData(1, 1, 4, 8)]
    [InlineData(1, 1, 5, 13)]
    [InlineData(1, 1, 6, 21)]
    [InlineData(1, 1, 7, 34)]
    [InlineData(1, 1, 8, 55)]
    [InlineData(1, 1, 9, 89)]
    [InlineData(1, 1, 10, 144)]
    [InlineData(1, 1, 11, 233)]
    public void FibonacciProgram_RunSpecificAmountOfCycles_ResultIsCalculatedCorrectly(
        byte a, byte b, byte iterations, byte expectedValue)
    {
        // Pseudocode:
        // B = a;
        // C = b;
        // L = iterations;
        // do
        // {
        //   A = B
        //   A = A + C;
        //   B = C;
        //   C = A;
        //   L--;
        // } while (L > 0)
        var cartridge = CartridgeBuilder
            .Create()
            .WithProgram([
                Opcode.Ld_B_N8,
                a,
                Opcode.Ld_C_N8,
                b,
                Opcode.Ld_H_N8,
                0,
                Opcode.Ld_L_N8,
                iterations,
                Opcode.Ld_A_B,
                Opcode.Add_A_C,
                Opcode.Ld_B_C,
                Opcode.Ld_C_A,
                Opcode.Dec_L,
                Opcode.JpNZ_N16,
                0x58,
                0x01, // 0x0158
                Opcode.Ld_A_C
            ])
            .Build();
        var instructionCount = 4 + 6 * iterations + 1;
        _gameBoy.Load(cartridge);

        _gameBoy.RunFor(instructionCount);

        var processor = (ITestableProcessor)_gameBoy.GetProcessor();
        Assert.Equal(expectedValue, processor.GetValueOfRegisterA());
    }
}