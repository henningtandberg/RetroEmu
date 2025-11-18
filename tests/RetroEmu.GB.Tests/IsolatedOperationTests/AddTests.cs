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
public class AddTests
{
    private readonly IGameBoy _gameBoy = TestGameBoyBuilder.CreateBuilder().BuildGameBoy();

    [Theory]
    [ClassData(typeof(AddWithNoSideEffectsTestData))]
    public void Add_NoSideEffects(
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
    [ClassData(typeof(AddWithZeroFlagSetTestData))]
    public void Add_ZeroFlagSet(
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
    [ClassData(typeof(AddWithHalfCarryFlagSetTestData))]
    public void Add_HalfCarryFlagSet(
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
    [ClassData(typeof(AddWithCarryFlagSetTestData))]
    public void Add_CarryFlagSet(
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
    [ClassData(typeof(AddWithCarryFlagAndHalfCarryFlagSetTestData))]
    public void Add_CarryFlagAndHalfCarryFlagSet(
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

public class AddWithNoSideEffectsTestData : TheoryData<byte[], InitialState, ExpectedState>
{
    public AddWithNoSideEffectsTestData()
    {
        Add([Opcode.Add_A_A], new InitialState { A = 0x01 }, new ExpectedState { Cycles = 4, A = 0x02, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = false, CarryFlag = false });
        Add([Opcode.Add_A_B], new InitialState { A = 0x01, B = 0x01 }, new ExpectedState { Cycles = 4, A = 0x02, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = false, CarryFlag = false });
        Add([Opcode.Add_A_C], new InitialState { A = 0x01, C = 0x01 }, new ExpectedState { Cycles = 4, A = 0x02, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = false, CarryFlag = false });
        Add([Opcode.Add_A_D], new InitialState { A = 0x01, D = 0x01 }, new ExpectedState { Cycles = 4, A = 0x02, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = false, CarryFlag = false });
        Add([Opcode.Add_A_E], new InitialState { A = 0x01, E = 0x01 }, new ExpectedState { Cycles = 4, A = 0x02, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = false, CarryFlag = false });
        Add([Opcode.Add_A_H], new InitialState { A = 0x01, H = 0x01 }, new ExpectedState { Cycles = 4, A = 0x02, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = false, CarryFlag = false });
        Add([Opcode.Add_A_L], new InitialState { A = 0x01, L = 0x01 }, new ExpectedState { Cycles = 4, A = 0x02, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = false, CarryFlag = false });
        Add([Opcode.Add_A_N8, 0x01], new InitialState { A = 0x01 }, new ExpectedState { Cycles = 8, A = 0x02, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = false, CarryFlag = false });
        Add([Opcode.Add_A_XHL], new InitialState { A = 0x01, HL = 0xC123, Memory = { [0xC123] = 0x01 }}, new ExpectedState { Cycles = 8, A = 0x02, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = false, CarryFlag = false });
    }
}

public class AddWithZeroFlagSetTestData : TheoryData<byte[], InitialState, ExpectedState>
{
    public AddWithZeroFlagSetTestData()
    {
        Add([Opcode.Add_A_A], new InitialState { A = 0x00 }, new ExpectedState { Cycles = 4, A = 0x00, ZeroFlag = true, SubtractFlag = false, HalfCarryFlag = false, CarryFlag = false });
        Add([Opcode.Add_A_B], new InitialState { A = 0xFF, B = 0x01 }, new ExpectedState { Cycles = 4, A = 0x00, ZeroFlag = true, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = true });
        Add([Opcode.Add_A_C], new InitialState { A = 0xFF, C = 0x01 }, new ExpectedState { Cycles = 4, A = 0x00, ZeroFlag = true, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = true });
        Add([Opcode.Add_A_D], new InitialState { A = 0xFF, D = 0x01 }, new ExpectedState { Cycles = 4, A = 0x00, ZeroFlag = true, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = true });
        Add([Opcode.Add_A_E], new InitialState { A = 0xFF, E = 0x01 }, new ExpectedState { Cycles = 4, A = 0x00, ZeroFlag = true, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = true });
        Add([Opcode.Add_A_H], new InitialState { A = 0xFF, H = 0x01 }, new ExpectedState { Cycles = 4, A = 0x00, ZeroFlag = true, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = true });
        Add([Opcode.Add_A_L], new InitialState { A = 0xFF, L = 0x01 }, new ExpectedState { Cycles = 4, A = 0x00, ZeroFlag = true, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = true });
        Add([Opcode.Add_A_N8, 0x01], new InitialState { A = 0xFF }, new ExpectedState { Cycles = 8, A = 0x00, ZeroFlag = true, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = true });
        Add([Opcode.Add_A_XHL], new InitialState { A = 0xFF, HL = 0xC123, Memory = { [0xC123] = 0x01 }}, new ExpectedState { Cycles = 8, A = 0x00, ZeroFlag = true, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = true });
    }
}

public class AddWithHalfCarryFlagSetTestData : TheoryData<byte[], InitialState, ExpectedState>
{
    public AddWithHalfCarryFlagSetTestData()
    {
        Add([Opcode.Add_A_A], new InitialState { A = 0x0F }, new ExpectedState { Cycles = 4, A = 0x1E, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = false });
        Add([Opcode.Add_A_B], new InitialState { A = 0x0F, B = 0x01 }, new ExpectedState { Cycles = 4, A = 0x10, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = false });
        Add([Opcode.Add_A_C], new InitialState { A = 0x0F, C = 0x01 }, new ExpectedState { Cycles = 4, A = 0x10, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = false });
        Add([Opcode.Add_A_D], new InitialState { A = 0x0F, D = 0x01 }, new ExpectedState { Cycles = 4, A = 0x10, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = false });
        Add([Opcode.Add_A_E], new InitialState { A = 0x0F, E = 0x01 }, new ExpectedState { Cycles = 4, A = 0x10, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = false });
        Add([Opcode.Add_A_H], new InitialState { A = 0x0F, H = 0x01 }, new ExpectedState { Cycles = 4, A = 0x10, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = false });
        Add([Opcode.Add_A_L], new InitialState { A = 0x0F, L = 0x01 }, new ExpectedState { Cycles = 4, A = 0x10, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = false });
        Add([Opcode.Add_A_N8, 0x01], new InitialState { A = 0x0F }, new ExpectedState { Cycles = 8, A = 0x10, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = false });
        Add([Opcode.Add_A_XHL], new InitialState { A = 0x0F, HL = 0xC456, Memory = { [0xC456] = 0x01 }}, new ExpectedState { Cycles = 8, A = 0x10, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = false });
    }
}

public class AddWithCarryFlagSetTestData : TheoryData<byte[], InitialState, ExpectedState>
{
    public AddWithCarryFlagSetTestData()
    {
        Add([Opcode.Add_A_A], new InitialState { A = 0xF0 }, new ExpectedState { Cycles = 4, A = 0xE0, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = false, CarryFlag = true });
        Add([Opcode.Add_A_B], new InitialState { A = 0xF0, B = 0x20 }, new ExpectedState { Cycles = 4, A = 0x10, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = false, CarryFlag = true });
        Add([Opcode.Add_A_C], new InitialState { A = 0xF0, C = 0x20 }, new ExpectedState { Cycles = 4, A = 0x10, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = false, CarryFlag = true });
        Add([Opcode.Add_A_D], new InitialState { A = 0xF0, D = 0x20 }, new ExpectedState { Cycles = 4, A = 0x10, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = false, CarryFlag = true });
        Add([Opcode.Add_A_E], new InitialState { A = 0xF0, E = 0x20 }, new ExpectedState { Cycles = 4, A = 0x10, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = false, CarryFlag = true });
        Add([Opcode.Add_A_H], new InitialState { A = 0xF0, H = 0x20 }, new ExpectedState { Cycles = 4, A = 0x10, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = false, CarryFlag = true });
        Add([Opcode.Add_A_L], new InitialState { A = 0xF0, L = 0x20 }, new ExpectedState { Cycles = 4, A = 0x10, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = false, CarryFlag = true });
        Add([Opcode.Add_A_N8, 0x20], new InitialState { A = 0xF0 }, new ExpectedState { Cycles = 8, A = 0x10, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = false, CarryFlag = true });
        Add([Opcode.Add_A_XHL], new InitialState { A = 0xF0, HL = 0xC789, Memory = { [0xC789] = 0x20 }}, new ExpectedState { Cycles = 8, A = 0x10, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = false, CarryFlag = true });
    }
}

public class AddWithCarryFlagAndHalfCarryFlagSetTestData : TheoryData<byte[], InitialState, ExpectedState>
{
    public AddWithCarryFlagAndHalfCarryFlagSetTestData()
    {
        Add([Opcode.Add_A_A], new InitialState { A = 0x8F }, new ExpectedState { Cycles = 4, A = 0x1E, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = true });
        Add([Opcode.Add_A_B], new InitialState { A = 0x8F, B = 0x91 }, new ExpectedState { Cycles = 4, A = 0x20, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = true });
        Add([Opcode.Add_A_C], new InitialState { A = 0x8F, C = 0x91 }, new ExpectedState { Cycles = 4, A = 0x20, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = true });
        Add([Opcode.Add_A_D], new InitialState { A = 0x8F, D = 0x91 }, new ExpectedState { Cycles = 4, A = 0x20, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = true });
        Add([Opcode.Add_A_E], new InitialState { A = 0x8F, E = 0x91 }, new ExpectedState { Cycles = 4, A = 0x20, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = true });
        Add([Opcode.Add_A_H], new InitialState { A = 0x8F, H = 0x91 }, new ExpectedState { Cycles = 4, A = 0x20, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = true });
        Add([Opcode.Add_A_L], new InitialState { A = 0x8F, L = 0x91 }, new ExpectedState { Cycles = 4, A = 0x20, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = true });
        Add([Opcode.Add_A_N8, 0x91], new InitialState { A = 0x8F }, new ExpectedState { Cycles = 8, A = 0x20, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = true });
        Add([Opcode.Add_A_XHL], new InitialState { A = 0x8F, HL = 0xC321, Memory = { [0xC321] = 0x91 }}, new ExpectedState { Cycles = 8, A = 0x20, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = true });
    }
}
