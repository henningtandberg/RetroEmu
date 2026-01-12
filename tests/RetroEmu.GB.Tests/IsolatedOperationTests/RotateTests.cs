using RetroEmu.Devices.DMG;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.GB.TestSetup;
using Xunit;

namespace RetroEmu.GB.Tests.IsolatedOperationTests;

public class RotateTests
{
    private readonly IGameBoy _gameBoy = TestGameBoyBuilder.CreateBuilder().BuildGameBoy();

    [Theory]
    [ClassData(typeof(RotateTestData))]
    public void
        AnyRotate_ValueIsRotatedCarryFlagIsSetAppropriatelyAndCyclesAreCorrect(
            byte[] program, InitialState initialState, ExpectedState expectedState)
    {
        var cartridge = CartridgeBuilder.Create().WithProgram(program).Build();
        _gameBoy.Load(cartridge);
        _gameBoy.SetInitialState(initialState);

        var cycles = _gameBoy.Update();

        _gameBoy.AssertExpectedState(expectedState);
        Assert.Equal(expectedState.Cycles, cycles);
    }

    private class RotateTestData : TheoryData<byte[], InitialState, ExpectedState>
    {
        public RotateTestData()
        {
            Add([Opcode.Rlc_A], new InitialState { A = 0b11110000, CarryFlag = false }, new ExpectedState { Cycles = 4, A = 0b11100001, CarryFlag = true, HalfCarryFlag = false, ZeroFlag = false, SubtractFlag = false});
            Add([Opcode.Rlc_A], new InitialState { A = 0b00000000, CarryFlag = true },  new ExpectedState { Cycles = 4, A = 0b00000000, CarryFlag = false, HalfCarryFlag = false, ZeroFlag = false, SubtractFlag = false});
            Add([Opcode.Rla],   new InitialState { A = 0b11110000, CarryFlag = false }, new ExpectedState { Cycles = 4, A = 0b11100000, CarryFlag = true, HalfCarryFlag = false, ZeroFlag = false, SubtractFlag = false});
            Add([Opcode.Rla],   new InitialState { A = 0b00000000, CarryFlag = true },  new ExpectedState { Cycles = 4, A = 0b00000001, CarryFlag = false, HalfCarryFlag = false, ZeroFlag = false, SubtractFlag = false});
            Add([Opcode.Rrc_A], new InitialState { A = 0b00001111, CarryFlag = false }, new ExpectedState { Cycles = 4, A = 0b10000111, CarryFlag = true, HalfCarryFlag = false, ZeroFlag = false, SubtractFlag = false});
            Add([Opcode.Rrc_A], new InitialState { A = 0b00000000, CarryFlag = true },  new ExpectedState { Cycles = 4, A = 0b00000000, CarryFlag = false, HalfCarryFlag = false, ZeroFlag = false, SubtractFlag = false});
            Add([Opcode.Rra],   new InitialState { A = 0b00001111, CarryFlag = false }, new ExpectedState { Cycles = 4, A = 0b00000111, CarryFlag = true, HalfCarryFlag = false, ZeroFlag = false, SubtractFlag = false});
            Add([Opcode.Rra],   new InitialState { A = 0b00000000, CarryFlag = true },  new ExpectedState { Cycles = 4, A = 0b10000000, CarryFlag = false, HalfCarryFlag = false, ZeroFlag = false, SubtractFlag = false});
        }
    }
}