using RetroEmu.Devices.DMG;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.GB.TestSetup;
using Xunit;

namespace RetroEmu.GB.Tests.IsolatedOperationTests;

public class CallTests
{
    private readonly IGameBoy _gameBoy = TestGameBoyBuilder.CreateBuilder().BuildGameBoy();

    [Theory]
    [ClassData(typeof(CallTestData))]
    public void AnyCall_ProgramCounterIsUpdatedCorrectlyCorrectCyclesReturnedAndNextInstructionIsPushedToStackWhenCalled(
            byte[] program, InitialState initialState, ExpectedState expectedState)
    {
        var cartridge = CartridgeBuilder.Create().WithProgram(program).Build();
        _gameBoy.Load(cartridge);
        _gameBoy.SetInitialState(initialState);

        var cycles = _gameBoy.Update();

        _gameBoy.AssertExpectedState(expectedState);
        Assert.Equal(expectedState.Cycles, cycles);
    }
    

    public class CallTestData : TheoryData<byte[], InitialState, ExpectedState>
    {
        public CallTestData()
        {
            Add([Opcode.Call_N16, 0x34, 0x12], new InitialState { SP = 0xE000 }, new ExpectedState { Cycles = 24, PC = 0x1234, SP = 0xDFFE, Stack = [ 0x53, 0x01 ] });
            
            Add([Opcode.CallC_N16, 0x34, 0x12], new InitialState { CarryFlag = true, SP = 0xE000 }, new ExpectedState { Cycles = 24, PC = 0x1234, SP = 0xDFFE, Stack = [ 0x53, 0x01 ] });
            Add([Opcode.CallC_N16, 0x34, 0x12], new InitialState { CarryFlag = false, SP = 0xE000 }, new ExpectedState { Cycles = 12, PC = 0x0153, SP = 0xE000 });
            Add([Opcode.CallNC_N16, 0x34, 0x12], new InitialState { CarryFlag = false, SP = 0xE000 }, new ExpectedState { Cycles = 24, PC = 0x1234, SP = 0xDFFE, Stack = [ 0x53, 0x01 ] });
            Add([Opcode.CallNC_N16, 0x34, 0x12], new InitialState { CarryFlag = true, SP = 0xE000 }, new ExpectedState { Cycles = 12, PC = 0x0153, SP = 0xE000 });
            
            Add([Opcode.CallZ_N16, 0x34, 0x12], new InitialState { ZeroFlag = true, SP = 0xE000 }, new ExpectedState { Cycles = 24, PC = 0x1234, SP = 0xDFFE, Stack = [ 0x53, 0x01 ] });
            Add([Opcode.CallZ_N16, 0x34, 0x12], new InitialState { ZeroFlag = false, SP = 0xE000 }, new ExpectedState { Cycles = 12, PC = 0x0153, SP = 0xE000 });
            Add([Opcode.CallNZ_N16, 0x34, 0x12], new InitialState { ZeroFlag = false, SP = 0xE000 }, new ExpectedState { Cycles = 24, PC = 0x1234, SP = 0xDFFE, Stack = [ 0x53, 0x01 ] });
            Add([Opcode.CallNZ_N16, 0x34, 0x12], new InitialState { ZeroFlag = true, SP = 0xE000 }, new ExpectedState { Cycles = 12, PC = 0x0153, SP = 0xE000 });
        }
    }
}