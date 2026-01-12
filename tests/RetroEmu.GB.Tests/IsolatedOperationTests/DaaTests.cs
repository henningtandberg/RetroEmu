using System.Collections.Generic;
using RetroEmu.Devices.DMG;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.GB.TestSetup;
using Xunit;

namespace RetroEmu.GB.Tests.IsolatedOperationTests;

public class DaaTests
{
    private readonly IGameBoy _gameBoy = TestGameBoyBuilder.CreateBuilder().BuildGameBoy();

    [Theory]
    [ClassData(typeof(DaaTestData))]
    public void Daa_RegisterAIsComplementedFlagsAndCyclesAreCorrect(
        byte[] program, InitialState initialState, ExpectedState expectedState)
    {
        var cartridge = CartridgeBuilder.Create().WithProgram(program).Build();
        _gameBoy.Load(cartridge);
        _gameBoy.SetInitialState(initialState);

        _gameBoy.Update();
        var cycles = _gameBoy.Update(); // The DAA instruction

        _gameBoy.AssertExpectedState(expectedState);
        Assert.Equal(expectedState.Cycles, cycles);
    }

    private class DaaTestData : TheoryData<byte[], InitialState, ExpectedState>
    {
        public DaaTestData()
        {
            Add([Opcode.Add_A_B, Opcode.Daa], new InitialState { PC = 0x0150, A = 0x00, B = 0x00 }, new ExpectedState { Cycles = 4, A = 0x00, ZeroFlag = true,  CarryFlag = false, HalfCarryFlag = false, SubtractFlag = false });
            Add([Opcode.Add_A_B, Opcode.Daa], new InitialState { PC = 0x0150, A = 0x01, B = 0x00 }, new ExpectedState { Cycles = 4, A = 0x01, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = false, SubtractFlag = false });
            Add([Opcode.Add_A_B, Opcode.Daa], new InitialState { PC = 0x0150, A = 0x00, B = 0x01 }, new ExpectedState { Cycles = 4, A = 0x01, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = false, SubtractFlag = false });
            Add([Opcode.Add_A_B, Opcode.Daa], new InitialState { PC = 0x0150, A = 0x10, B = 0x01 }, new ExpectedState { Cycles = 4, A = 0x11, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = false, SubtractFlag = false });
            Add([Opcode.Add_A_B, Opcode.Daa], new InitialState { PC = 0x0150, A = 0x20, B = 0x20 }, new ExpectedState { Cycles = 4, A = 0x40, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = false, SubtractFlag = false });
            Add([Opcode.Add_A_B, Opcode.Daa], new InitialState { PC = 0x0150, A = 0x38, B = 0x45 }, new ExpectedState { Cycles = 4, A = 0x83, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = false, SubtractFlag = false });
            Add([Opcode.Add_A_B, Opcode.Daa], new InitialState { PC = 0x0150, A = 0x38, B = 0x41 }, new ExpectedState { Cycles = 4, A = 0x79, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = false, SubtractFlag = false });
            Add([Opcode.Add_A_B, Opcode.Daa], new InitialState { PC = 0x0150, A = 0x83, B = 0x54 }, new ExpectedState { Cycles = 4, A = 0x37, ZeroFlag = false, CarryFlag = true,  HalfCarryFlag = false, SubtractFlag = false });
            Add([Opcode.Add_A_B, Opcode.Daa], new InitialState { PC = 0x0150, A = 0x88, B = 0x44 }, new ExpectedState { Cycles = 4, A = 0x32, ZeroFlag = false, CarryFlag = true,  HalfCarryFlag = false, SubtractFlag = false });
            Add([Opcode.Add_A_B, Opcode.Daa], new InitialState { PC = 0x0150, A = 0x99, B = 0x01 }, new ExpectedState { Cycles = 4, A = 0x00, ZeroFlag = true,  CarryFlag = true,  HalfCarryFlag = false, SubtractFlag = false });
        }
    }
}