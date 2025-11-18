using RetroEmu.Devices.DMG;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.GB.TestSetup;
using Xunit;

namespace RetroEmu.GB.Tests.IsolatedOperationTests;

/// <summary>
/// ADD A, r / n / (HL) instruction tests
///
/// Covers scenarios:
/// 1. ZeroFlag set
/// 2. HalfCarry set
/// 3. Carry set
/// 4. HalfCarry + Carry set
///
/// Work RAM used for memory cases: 0xC000–0xDFFF
/// </summary>
public class AddHLTests
{
    private readonly IGameBoy _gameBoy = TestGameBoyBuilder.CreateBuilder().BuildGameBoy();

    [Theory]
    [ClassData(typeof(AddHLWithNoSideEffectsTestData))]
    public void AddHL_NoSideEffects(
        byte[] program, InitialState initialState, ExpectedState expectedState)
    {
        var cartridge = CartridgeBuilder.Create().WithProgram(program).Build();
        _gameBoy.Load(cartridge);
        _gameBoy.SetInitialState(initialState);

        var cycles = _gameBoy.Update();

        _gameBoy.AssertExpectedState(expectedState);
        Assert.Equal(expectedState.Cycles, cycles);
    }
    
    [Theory]
    [ClassData(typeof(AddHLWithHalfCarryFlagSetTestData))]
    public void AddHL_HalfCarryFlagSet(
        byte[] program, InitialState initialState, ExpectedState expectedState)
    {
        var cartridge = CartridgeBuilder.Create().WithProgram(program).Build();
        _gameBoy.Load(cartridge);
        _gameBoy.SetInitialState(initialState);

        var cycles = _gameBoy.Update();

        _gameBoy.AssertExpectedState(expectedState);
        Assert.Equal(expectedState.Cycles, cycles);
    }
    
    [Theory]
    [ClassData(typeof(AddHLWithCarryFlagSetTestData))]
    public void AddHL_CarryFlagSet(
        byte[] program, InitialState initialState, ExpectedState expectedState)
    {
        var cartridge = CartridgeBuilder.Create().WithProgram(program).Build();
        _gameBoy.Load(cartridge);
        _gameBoy.SetInitialState(initialState);

        var cycles = _gameBoy.Update();

        _gameBoy.AssertExpectedState(expectedState);
        Assert.Equal(expectedState.Cycles, cycles);
    }
    
    [Theory]
    [ClassData(typeof(AddHLWithCarryFlagAndHalfCarryFlagSetTestData))]
    public void AddHL_CarryFlagAndHalfCarryFlagSet(
        byte[] program, InitialState initialState, ExpectedState expectedState)
    {
        var cartridge = CartridgeBuilder.Create().WithProgram(program).Build();
        _gameBoy.Load(cartridge);
        _gameBoy.SetInitialState(initialState);

        var cycles = _gameBoy.Update();

        _gameBoy.AssertExpectedState(expectedState);
        Assert.Equal(expectedState.Cycles, cycles);
    }
}

public class AddHLWithNoSideEffectsTestData : TheoryData<byte[], InitialState, ExpectedState>
{
    public AddHLWithNoSideEffectsTestData()
    {
        Add([Opcode.Add_HL_HL], new InitialState { HL = 0x0001 },              new ExpectedState { Cycles = 8, HL = 0x0002, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = false, CarryFlag = false });
        Add([Opcode.Add_HL_BC], new InitialState { HL = 0x0001, BC = 0x0001 }, new ExpectedState { Cycles = 8, HL = 0x0002, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = false, CarryFlag = false });
        Add([Opcode.Add_HL_DE], new InitialState { HL = 0x0001, DE = 0x0001 }, new ExpectedState { Cycles = 8, HL = 0x0002, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = false, CarryFlag = false });
        Add([Opcode.Add_HL_SP], new InitialState { HL = 0x0001, SP = 0x0001 }, new ExpectedState { Cycles = 8, HL = 0x0002, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = false, CarryFlag = false });
    }
}

public class AddHLWithHalfCarryFlagSetTestData : TheoryData<byte[], InitialState, ExpectedState>
{
    public AddHLWithHalfCarryFlagSetTestData()
    {
        Add([Opcode.Add_HL_HL], new InitialState { HL = 0x0FFF },              new ExpectedState { Cycles = 8, HL = 0x1FFE, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = false });
        Add([Opcode.Add_HL_BC], new InitialState { HL = 0x0FFF, BC = 0x0001 }, new ExpectedState { Cycles = 8, HL = 0x1000, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = false });
        Add([Opcode.Add_HL_DE], new InitialState { HL = 0x0FFF, DE = 0x0001 }, new ExpectedState { Cycles = 8, HL = 0x1000, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = false });
        Add([Opcode.Add_HL_SP], new InitialState { HL = 0x0FFF, SP = 0x0001 }, new ExpectedState { Cycles = 8, HL = 0x1000, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = false });
    }
}

public class AddHLWithCarryFlagSetTestData : TheoryData<byte[], InitialState, ExpectedState>
{
    public AddHLWithCarryFlagSetTestData()
    {
        Add([Opcode.Add_HL_HL], new InitialState { HL = 0xE000 },              new ExpectedState { Cycles = 8, HL = 0xC000, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = false, CarryFlag = true });
        Add([Opcode.Add_HL_BC], new InitialState { HL = 0xE000, BC = 0xE000 }, new ExpectedState { Cycles = 8, HL = 0xC000, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = false, CarryFlag = true });
        Add([Opcode.Add_HL_DE], new InitialState { HL = 0xE000, DE = 0xE000 }, new ExpectedState { Cycles = 8, HL = 0xC000, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = false, CarryFlag = true });
        Add([Opcode.Add_HL_SP], new InitialState { HL = 0xE000, SP = 0xE000 }, new ExpectedState { Cycles = 8, HL = 0xC000, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = false, CarryFlag = true });
    }
}

public class AddHLWithCarryFlagAndHalfCarryFlagSetTestData : TheoryData<byte[], InitialState, ExpectedState>
{
    public AddHLWithCarryFlagAndHalfCarryFlagSetTestData()
    {
        Add([Opcode.Add_HL_HL], new InitialState { HL = 0xEE00 },              new ExpectedState { Cycles = 8, HL = 0xDC00, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = true });
        Add([Opcode.Add_HL_BC], new InitialState { HL = 0xEE00, BC = 0xEE00 }, new ExpectedState { Cycles = 8, HL = 0xDC00, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = true });
        Add([Opcode.Add_HL_DE], new InitialState { HL = 0xEE00, DE = 0xEE00 }, new ExpectedState { Cycles = 8, HL = 0xDC00, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = true });
        Add([Opcode.Add_HL_SP], new InitialState { HL = 0xEE00, SP = 0xEE00 }, new ExpectedState { Cycles = 8, HL = 0xDC00, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = true });
    }
}
