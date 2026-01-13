using RetroEmu.Devices.DMG;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.GB.TestSetup;
using Xunit;

namespace RetroEmu.GB.Tests.IsolatedOperationTests;

public class AndTests
{
    private readonly IGameBoy _gameBoy = TestGameBoyBuilder.CreateBuilder().BuildGameBoy();

    [Theory]
    [ClassData(typeof(AndNotZeroTestData))]
    public void And_NotZero_ResultStoredInRegisterAAndFlagsAreCorrect(
        byte[] program, InitialState initialState, ExpectedState expectedState)
    {
        var cartridge = CartridgeBuilder.Create().WithProgram(program).Build();
        _gameBoy.Load(cartridge);
        _gameBoy.SetInitialState(initialState);

        var cycles = _gameBoy.Update();

        _gameBoy.AssertExpectedState(expectedState);
        Assert.Equal(expectedState.Cycles, cycles);
    }

    private class AndNotZeroTestData : TheoryData<byte[], InitialState, ExpectedState>
    {
        public AndNotZeroTestData()
        {
            Add([Opcode.And_A_A], new InitialState { A = 0x08, },           new ExpectedState { Cycles = 4, A = 0x08, CarryFlag = false, HalfCarryFlag = true, ZeroFlag = false, SubtractFlag = false });
            Add([Opcode.And_A_B], new InitialState { A = 0x08, B = 0x0f },  new ExpectedState { Cycles = 4, A = 0x08, CarryFlag = false, HalfCarryFlag = true, ZeroFlag = false, SubtractFlag = false });
            Add([Opcode.And_A_C], new InitialState { A = 0x08, C = 0x0f },  new ExpectedState { Cycles = 4, A = 0x08, CarryFlag = false, HalfCarryFlag = true, ZeroFlag = false, SubtractFlag = false });
            Add([Opcode.And_A_D], new InitialState { A = 0x08, D = 0x0f },  new ExpectedState { Cycles = 4, A = 0x08, CarryFlag = false, HalfCarryFlag = true, ZeroFlag = false, SubtractFlag = false });
            Add([Opcode.And_A_E], new InitialState { A = 0x08, E = 0x0f },  new ExpectedState { Cycles = 4, A = 0x08, CarryFlag = false, HalfCarryFlag = true, ZeroFlag = false, SubtractFlag = false });
            Add([Opcode.And_A_H], new InitialState { A = 0x08, H = 0x0f },  new ExpectedState { Cycles = 4, A = 0x08, CarryFlag = false, HalfCarryFlag = true, ZeroFlag = false, SubtractFlag = false });
            Add([Opcode.And_A_L], new InitialState { A = 0x08, L = 0x0f },  new ExpectedState { Cycles = 4, A = 0x08, CarryFlag = false, HalfCarryFlag = true, ZeroFlag = false, SubtractFlag = false });
            
            Add([Opcode.And_A_N8, 0x0f], new InitialState { A = 0x08 }, new ExpectedState { Cycles = 8, A = 0x08, CarryFlag = false, HalfCarryFlag = true, ZeroFlag = false, SubtractFlag = false });
            Add([Opcode.And_A_XHL], new InitialState { A = 0x08, HL = 0xC000, Memory = { [0xC000] = 0x0f }}, new ExpectedState { Cycles = 8, A = 0x08, CarryFlag = false, HalfCarryFlag = true, ZeroFlag = false, SubtractFlag = false });
        }
    }
    
    [Theory]
    [ClassData(typeof(AndZeroTestData))]
    public void And_Zero_ResultStoredInRegisterAAndFlagsAreCorrect(
        byte[] program, InitialState initialState, ExpectedState expectedState)
    {
        var cartridge = CartridgeBuilder.Create().WithProgram(program).Build();
        _gameBoy.Load(cartridge);
        _gameBoy.SetInitialState(initialState);

        var cycles = _gameBoy.Update();

        _gameBoy.AssertExpectedState(expectedState);
        Assert.Equal(expectedState.Cycles, cycles);
    }

    private class AndZeroTestData : TheoryData<byte[], InitialState, ExpectedState>
    {
        public AndZeroTestData()
        {
            Add([Opcode.And_A_A], new InitialState { A = 0x00, },           new ExpectedState { Cycles = 4, A = 0x00, CarryFlag = false, HalfCarryFlag = true, ZeroFlag = true, SubtractFlag = false });
            Add([Opcode.And_A_B], new InitialState { A = 0x08, B = 0x10 },  new ExpectedState { Cycles = 4, A = 0x00, CarryFlag = false, HalfCarryFlag = true, ZeroFlag = true, SubtractFlag = false });
            Add([Opcode.And_A_C], new InitialState { A = 0x08, C = 0x10 },  new ExpectedState { Cycles = 4, A = 0x00, CarryFlag = false, HalfCarryFlag = true, ZeroFlag = true, SubtractFlag = false });
            Add([Opcode.And_A_D], new InitialState { A = 0x08, D = 0x10 },  new ExpectedState { Cycles = 4, A = 0x00, CarryFlag = false, HalfCarryFlag = true, ZeroFlag = true, SubtractFlag = false });
            Add([Opcode.And_A_E], new InitialState { A = 0x08, E = 0x10 },  new ExpectedState { Cycles = 4, A = 0x00, CarryFlag = false, HalfCarryFlag = true, ZeroFlag = true, SubtractFlag = false });
            Add([Opcode.And_A_H], new InitialState { A = 0x08, H = 0x10 },  new ExpectedState { Cycles = 4, A = 0x00, CarryFlag = false, HalfCarryFlag = true, ZeroFlag = true, SubtractFlag = false });
            Add([Opcode.And_A_L], new InitialState { A = 0x08, L = 0x10 },  new ExpectedState { Cycles = 4, A = 0x00, CarryFlag = false, HalfCarryFlag = true, ZeroFlag = true, SubtractFlag = false });
            
            Add([Opcode.And_A_N8, 0x10], new InitialState { A = 0x08 }, new ExpectedState { Cycles = 8, A = 0x00, CarryFlag = false, HalfCarryFlag = true, ZeroFlag = true, SubtractFlag = false });
            Add([Opcode.And_A_XHL], new InitialState { A = 0x08, HL = 0xC000, Memory = { [0xC000] = 0x10 }}, new ExpectedState { Cycles = 8, A = 0x00, CarryFlag = false, HalfCarryFlag = true, ZeroFlag = true, SubtractFlag = false });
        }
    }
}