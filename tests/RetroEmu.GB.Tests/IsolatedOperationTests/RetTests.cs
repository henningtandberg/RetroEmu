using RetroEmu.Devices.DMG;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.GB.TestSetup;
using Xunit;

namespace RetroEmu.GB.Tests.IsolatedOperationTests;

public class RetTests
{
    private readonly IGameBoy _gameBoy = TestGameBoyBuilder.CreateBuilder().BuildGameBoy();

    [Theory]
    [ClassData(typeof(RetTestData))]
    public void AnyRet_ProgramCounterIsUpdatedCorrectlyCorrectCyclesReturnedAndNextInstructionIsPoppedFromStackWhenReturned(
            byte[] program, InitialState initialState, ExpectedState expectedState)
    {
        var cartridge = CartridgeBuilder.Create().WithProgram(program).Build();
        _gameBoy.Load(cartridge);
        _gameBoy.SetInitialState(initialState);

        var cycles = _gameBoy.Update();

        _gameBoy.AssertExpectedState(expectedState);
        Assert.Equal(expectedState.Cycles, cycles);
    }
    

    private class RetTestData : TheoryData<byte[], InitialState, ExpectedState>
    {
        public RetTestData()
        {
            Add([Opcode.Ret], new InitialState { SP = 0xDFFE, Memory = { [0xDFFE] = 0x34, [0xDFFF] = 0x12 }}, new ExpectedState { Cycles = 8, PC = 0x1234, SP = 0xE000 });
            
            Add([Opcode.RetC], new InitialState { CarryFlag = true, SP = 0xDFFE, Memory = { [0xDFFE] = 0x34, [0xDFFF] = 0x12 }}, new ExpectedState { Cycles = 20, PC = 0x1234, SP = 0xE000 });
            Add([Opcode.RetC], new InitialState { CarryFlag = false, SP = 0xDFFE, Memory = { [0xDFFE] = 0x34, [0xDFFF] = 0x12 }}, new ExpectedState { Cycles = 8, PC = 0x1234, SP = 0xDFFE, Stack = [ 0x34, 0x12 ] });
            
            Add([Opcode.RetNC], new InitialState { CarryFlag = false, SP = 0xDFFE, Memory = { [0xDFFE] = 0x34, [0xDFFF] = 0x12 }}, new ExpectedState { Cycles = 20, PC = 0x1234, SP = 0xE000 });
            Add([Opcode.RetNC], new InitialState { CarryFlag = true, SP = 0xDFFE, Memory = { [0xDFFE] = 0x34, [0xDFFF] = 0x12 }}, new ExpectedState { Cycles = 8, PC = 0x1234, SP = 0xDFFE, Stack = [ 0x34, 0x12 ] });
            
            Add([Opcode.RetZ], new InitialState { ZeroFlag = true, SP = 0xDFFE, Memory = { [0xDFFE] = 0x34, [0xDFFF] = 0x12 }}, new ExpectedState { Cycles = 20, PC = 0x1234, SP = 0xE000 });
            Add([Opcode.RetZ], new InitialState { ZeroFlag = false, SP = 0xDFFE, Memory = { [0xDFFE] = 0x34, [0xDFFF] = 0x12 }}, new ExpectedState { Cycles = 8, PC = 0x1234, SP = 0xDFFE, Stack = [ 0x34, 0x12 ] });
            
            Add([Opcode.RetNZ], new InitialState { ZeroFlag = false, SP = 0xDFFE, Memory = { [0xDFFE] = 0x34, [0xDFFF] = 0x12 }}, new ExpectedState { Cycles = 20, PC = 0x1234, SP = 0xE000 });
            Add([Opcode.RetNZ], new InitialState { ZeroFlag = true, SP = 0xDFFE, Memory = { [0xDFFE] = 0x34, [0xDFFF] = 0x12 }}, new ExpectedState { Cycles = 8, PC = 0x1234, SP = 0xDFFE, Stack = [ 0x34, 0x12 ] });
        }
    }
}