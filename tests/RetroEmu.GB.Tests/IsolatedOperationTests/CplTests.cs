using System.Collections.Generic;
using RetroEmu.Devices.DMG;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.GB.TestSetup;
using Xunit;

namespace RetroEmu.GB.Tests.IsolatedOperationTests;

public class CplTests
{
    private readonly IGameBoy _gameBoy = TestGameBoyBuilder.CreateBuilder().BuildGameBoy();

    [Theory]
    [ClassData(typeof(CplTestData))]
    public void Cpl_RegisterAIsComplementedFlagsAndCyclesAreCorrect(
        byte[] program, InitialState initialState, ExpectedState expectedState)
    {
        var cartridge = CartridgeBuilder.Create().WithProgram(program).Build();
        _gameBoy.Load(cartridge);
        _gameBoy.SetInitialState(initialState);

        var cycles = _gameBoy.Update();

        _gameBoy.AssertExpectedState(expectedState);
        Assert.Equal(expectedState.Cycles, cycles);
    }

    private class CplTestData : TheoryData<byte[], InitialState, ExpectedState>
    {
        public CplTestData()
        {
            Add([Opcode.Cpl], new InitialState { A = 0x01 }, new ExpectedState { Cycles = 4, A = 0xFE, CarryFlag = false, HalfCarryFlag = true, ZeroFlag = false, SubtractFlag = true });
            Add([Opcode.Cpl], new InitialState { A = 0xFE }, new ExpectedState { Cycles = 4, A = 0x01, CarryFlag = false, HalfCarryFlag = true, ZeroFlag = false, SubtractFlag = true });
        }
    }
}