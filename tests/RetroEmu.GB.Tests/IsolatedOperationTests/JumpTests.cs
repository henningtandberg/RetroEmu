using RetroEmu.Devices.DMG;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.GB.TestSetup;
using Xunit;

namespace RetroEmu.GB.Tests.IsolatedOperationTests;

public class JumpTests
{
    private readonly IGameBoy _gameBoy = TestGameBoyBuilder.CreateBuilder().BuildGameBoy();

    [Theory]
    [ClassData(typeof(JumpToAddressTestData))]
    public void AnyJumpToAddress_ProgramCounterIsUpdatedCorrectlyCorrectCyclesReturned(
            byte[] program, InitialState initialState, ExpectedState expectedState)
    {
        var cartridge = CartridgeBuilder.Create().WithProgram(program).Build();
        _gameBoy.Load(cartridge);
        _gameBoy.SetInitialState(initialState);

        var cycles = _gameBoy.Update();

        _gameBoy.AssertExpectedState(expectedState);
        Assert.Equal(expectedState.Cycles, cycles);
    }

    private class JumpToAddressTestData : TheoryData<byte[], InitialState, ExpectedState>
    {
        public JumpToAddressTestData()
        {
            Add([Opcode.Jp_N16, 0x34, 0x12], new InitialState(), new ExpectedState { Cycles = 16, PC = 0x1234 });
            
            Add([Opcode.JpC_N16, 0x34, 0x12], new InitialState { CarryFlag = true }, new ExpectedState { Cycles = 16, PC = 0x1234 });
            Add([Opcode.JpC_N16, 0x34, 0x12], new InitialState { CarryFlag = false }, new ExpectedState { Cycles = 12, PC = 0x0153 });
            Add([Opcode.JpNC_N16, 0x34, 0x12], new InitialState { CarryFlag = false }, new ExpectedState { Cycles = 16, PC = 0x1234 });
            Add([Opcode.JpNC_N16, 0x34, 0x12], new InitialState { CarryFlag = true }, new ExpectedState { Cycles = 12, PC = 0x0153 });
            
            Add([Opcode.JpZ_N16, 0x34, 0x12], new InitialState { ZeroFlag = true }, new ExpectedState { Cycles = 16, PC = 0x1234 });
            Add([Opcode.JpZ_N16, 0x34, 0x12], new InitialState { ZeroFlag = false }, new ExpectedState { Cycles = 12, PC = 0x0153 });
            Add([Opcode.JpNZ_N16, 0x34, 0x12], new InitialState { ZeroFlag = false }, new ExpectedState { Cycles = 16, PC = 0x1234 });
            Add([Opcode.JpNZ_N16, 0x34, 0x12], new InitialState { ZeroFlag = true }, new ExpectedState { Cycles = 12, PC = 0x0153 });
        }
    }
    
    [Theory]
    [ClassData(typeof(JumpRelativeTestData))]
    public void AnyJumpRelative_ProgramCounterIsUpdatedCorrectlyCorrectCyclesReturned(
        byte[] program, InitialState initialState, ExpectedState expectedState)
    {
        var cartridge = CartridgeBuilder.Create().WithProgram(program).Build();
        _gameBoy.Load(cartridge);
        _gameBoy.SetInitialState(initialState);

        var cycles = _gameBoy.Update();

        _gameBoy.AssertExpectedState(expectedState);
        Assert.Equal(expectedState.Cycles, cycles);
    }

    private class JumpRelativeTestData : TheoryData<byte[], InitialState, ExpectedState>
    {
        public JumpRelativeTestData()
        {
            Add([Opcode.Jr_N8, 0xFE], new InitialState(), new ExpectedState { Cycles = 12, PC = 0x0150 });
            Add([Opcode.Jr_N8, 0x02], new InitialState(), new ExpectedState { Cycles = 12, PC = 0x0154 });
            
            Add([Opcode.JrC_N8, 0xFE], new InitialState { CarryFlag = true }, new ExpectedState { Cycles = 12, PC = 0x0150 });
            Add([Opcode.JrC_N8, 0x02], new InitialState { CarryFlag = true }, new ExpectedState { Cycles = 12, PC = 0x0154 });
            Add([Opcode.JrC_N8, 0xFE], new InitialState { CarryFlag = false }, new ExpectedState { Cycles = 8, PC = 0x0154 });
            
            Add([Opcode.JrNC_N8, 0xFE], new InitialState { CarryFlag = false }, new ExpectedState { Cycles = 12, PC = 0x0150 });
            Add([Opcode.JrNC_N8, 0x02], new InitialState { CarryFlag = false }, new ExpectedState { Cycles = 12, PC = 0x0154 });
            Add([Opcode.JrNC_N8, 0xFE], new InitialState { CarryFlag = true }, new ExpectedState { Cycles = 8, PC = 0x0154 });
            
            Add([Opcode.JrZ_N8, 0xFE], new InitialState { ZeroFlag = true }, new ExpectedState { Cycles = 12, PC = 0x0150 });
            Add([Opcode.JrZ_N8, 0x02], new InitialState { ZeroFlag = true }, new ExpectedState { Cycles = 12, PC = 0x0154 });
            Add([Opcode.JrZ_N8, 0xFE], new InitialState { ZeroFlag = false }, new ExpectedState { Cycles = 8, PC = 0x0154 });
            
            Add([Opcode.JrNZ_N8, 0xFE], new InitialState { ZeroFlag = false }, new ExpectedState { Cycles = 12, PC = 0x0150 });
            Add([Opcode.JrNZ_N8, 0x02], new InitialState { ZeroFlag = false }, new ExpectedState { Cycles = 12, PC = 0x0154 });
            Add([Opcode.JrNZ_N8, 0xFE], new InitialState { ZeroFlag = true }, new ExpectedState { Cycles = 8, PC = 0x0154 });
        }
    }
}