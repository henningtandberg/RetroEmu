using RetroEmu.Devices.DMG;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.GB.TestSetup;
using Xunit;

namespace RetroEmu.GB.Tests.IsolatedOperationTests;

public class RstTests
{
    private readonly IGameBoy _gameBoy = TestGameBoyBuilder.CreateBuilder().BuildGameBoy();

    [Theory]
    [ClassData(typeof(RstTestData))]
    public void
        AnyRst_ProgramCounterIsUpdatedCorrectlyCorrectCyclesReturned(
            byte[] program, InitialState initialState, ExpectedState expectedState)
    {
        var cartridge = CartridgeBuilder.Create().WithProgram(program).Build();
        _gameBoy.Load(cartridge);
        _gameBoy.SetInitialState(initialState);

        var cycles = _gameBoy.Update();

        _gameBoy.AssertExpectedState(expectedState);
        Assert.Equal(expectedState.Cycles, cycles);
    }

    private class RstTestData : TheoryData<byte[], InitialState, ExpectedState>
    {
        public RstTestData()
        {
            Add([Opcode.Rst_00H], new InitialState { SP = 0x0102 }, new ExpectedState { Cycles = 32, PC = 0x00, SP = 0x0100 });
            Add([Opcode.Rst_08H], new InitialState { SP = 0x0102 }, new ExpectedState { Cycles = 32, PC = 0x08, SP = 0x0100 });
            Add([Opcode.Rst_10H], new InitialState { SP = 0x0102 }, new ExpectedState { Cycles = 32, PC = 0x10, SP = 0x0100 });
            Add([Opcode.Rst_18H], new InitialState { SP = 0x0102 }, new ExpectedState { Cycles = 32, PC = 0x18, SP = 0x0100 });
            Add([Opcode.Rst_20H], new InitialState { SP = 0x0102 }, new ExpectedState { Cycles = 32, PC = 0x20, SP = 0x0100 });
            Add([Opcode.Rst_28H], new InitialState { SP = 0x0102 }, new ExpectedState { Cycles = 32, PC = 0x28, SP = 0x0100 });
            Add([Opcode.Rst_30H], new InitialState { SP = 0x0102 }, new ExpectedState { Cycles = 32, PC = 0x30, SP = 0x0100 });
            Add([Opcode.Rst_38H], new InitialState { SP = 0x0102 }, new ExpectedState { Cycles = 32, PC = 0x38, SP = 0x0100 });
        }
    }
}