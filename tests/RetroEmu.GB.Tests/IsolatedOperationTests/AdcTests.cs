using RetroEmu.Devices.DMG;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.GB.TestSetup;
using Xunit;

namespace RetroEmu.GB.Tests.IsolatedOperationTests;

public class AdcTests
{
    private readonly IGameBoy _gameBoy = TestGameBoyBuilder.CreateBuilder().BuildGameBoy();

    [Theory]
    [ClassData(typeof(AdcWithNoSideEffectsTestData))]
    public void Adc_NoSideEffects(
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
    [ClassData(typeof(AdcWithZeroFlagSetTestData))]
    public void Adc_ZeroFlagSet(
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
    [ClassData(typeof(AdcWithHalfCarryFlagSetTestData))]
    public void Adc_HalfCarryFlagSet(
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
    [ClassData(typeof(AdcWithCarryFlagSetTestData))]
    public void Adc_CarryFlagSet(
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
    [ClassData(typeof(AdcWithCarryFlagAndHalfCarryFlagSetTestData))]
    public void Adc_CarryFlagAndHalfCarryFlagSet(
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

public class AdcWithNoSideEffectsTestData : TheoryData<byte[], InitialState, ExpectedState>
{
    public AdcWithNoSideEffectsTestData()
    {
        Add([Opcode.Adc_A_A], new InitialState { A = 0x01, CarryFlag = true },           new ExpectedState { Cycles = 4, A = 0x03, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = false, CarryFlag = false });
        Add([Opcode.Adc_A_B], new InitialState { A = 0x01, B = 0x01, CarryFlag = true }, new ExpectedState { Cycles = 4, A = 0x03, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = false, CarryFlag = false });
        Add([Opcode.Adc_A_C], new InitialState { A = 0x01, C = 0x01, CarryFlag = true }, new ExpectedState { Cycles = 4, A = 0x03, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = false, CarryFlag = false });
        Add([Opcode.Adc_A_D], new InitialState { A = 0x01, D = 0x01, CarryFlag = true }, new ExpectedState { Cycles = 4, A = 0x03, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = false, CarryFlag = false });
        Add([Opcode.Adc_A_E], new InitialState { A = 0x01, E = 0x01, CarryFlag = true }, new ExpectedState { Cycles = 4, A = 0x03, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = false, CarryFlag = false });
        Add([Opcode.Adc_A_H], new InitialState { A = 0x01, H = 0x01, CarryFlag = true }, new ExpectedState { Cycles = 4, A = 0x03, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = false, CarryFlag = false });
        Add([Opcode.Adc_A_L], new InitialState { A = 0x01, L = 0x01, CarryFlag = true }, new ExpectedState { Cycles = 4, A = 0x03, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = false, CarryFlag = false });
        Add([Opcode.Adc_A_N8, 0x01], new InitialState { A = 0x01, CarryFlag = true },    new ExpectedState { Cycles = 8, A = 0x03, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = false, CarryFlag = false });
        Add([Opcode.Adc_A_XHL], new InitialState { A = 0x01, HL = 0xC123, Memory = { [0xC123] = 0x01 }, CarryFlag = true }, new ExpectedState { Cycles = 8, A = 0x03, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = false, CarryFlag = false });
    }
}

public class AdcWithZeroFlagSetTestData : TheoryData<byte[], InitialState, ExpectedState>
{
    public AdcWithZeroFlagSetTestData()
    {
        Add([Opcode.Adc_A_A], new InitialState { A = 0x00, CarryFlag = false },          new ExpectedState { Cycles = 4, A = 0x00, ZeroFlag = true, SubtractFlag = false, HalfCarryFlag = false, CarryFlag = false });
        Add([Opcode.Adc_A_B], new InitialState { A = 0xFF, B = 0x00, CarryFlag = true }, new ExpectedState { Cycles = 4, A = 0x00, ZeroFlag = true, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = true });
        Add([Opcode.Adc_A_C], new InitialState { A = 0xFF, C = 0x00, CarryFlag = true }, new ExpectedState { Cycles = 4, A = 0x00, ZeroFlag = true, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = true });
        Add([Opcode.Adc_A_D], new InitialState { A = 0xFF, D = 0x00, CarryFlag = true }, new ExpectedState { Cycles = 4, A = 0x00, ZeroFlag = true, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = true });
        Add([Opcode.Adc_A_E], new InitialState { A = 0xFF, E = 0x00, CarryFlag = true }, new ExpectedState { Cycles = 4, A = 0x00, ZeroFlag = true, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = true });
        Add([Opcode.Adc_A_H], new InitialState { A = 0xFF, H = 0x00, CarryFlag = true }, new ExpectedState { Cycles = 4, A = 0x00, ZeroFlag = true, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = true });
        Add([Opcode.Adc_A_L], new InitialState { A = 0xFF, L = 0x00, CarryFlag = true }, new ExpectedState { Cycles = 4, A = 0x00, ZeroFlag = true, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = true });
        Add([Opcode.Adc_A_N8, 0x00], new InitialState { A = 0xFF, CarryFlag = true },    new ExpectedState { Cycles = 8, A = 0x00, ZeroFlag = true, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = true });
        Add([Opcode.Adc_A_XHL], new InitialState { A = 0xFF, HL = 0xC123, Memory = { [0xC123] = 0x00 }, CarryFlag = true}, new ExpectedState { Cycles = 8, A = 0x00, ZeroFlag = true, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = true });
    }
}

public class AdcWithHalfCarryFlagSetTestData : TheoryData<byte[], InitialState, ExpectedState>
{
    public AdcWithHalfCarryFlagSetTestData()
    {
        Add([Opcode.Adc_A_A], new InitialState { A = 0x0F }, new ExpectedState { Cycles = 4, A = 0x1E, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = false });
        Add([Opcode.Adc_A_B], new InitialState { A = 0x0F, B = 0x01 }, new ExpectedState { Cycles = 4, A = 0x10, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = false });
        Add([Opcode.Adc_A_C], new InitialState { A = 0x0F, C = 0x01 }, new ExpectedState { Cycles = 4, A = 0x10, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = false });
        Add([Opcode.Adc_A_D], new InitialState { A = 0x0F, D = 0x01 }, new ExpectedState { Cycles = 4, A = 0x10, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = false });
        Add([Opcode.Adc_A_E], new InitialState { A = 0x0F, E = 0x01 }, new ExpectedState { Cycles = 4, A = 0x10, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = false });
        Add([Opcode.Adc_A_H], new InitialState { A = 0x0F, H = 0x01 }, new ExpectedState { Cycles = 4, A = 0x10, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = false });
        Add([Opcode.Adc_A_L], new InitialState { A = 0x0F, L = 0x01 }, new ExpectedState { Cycles = 4, A = 0x10, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = false });
        Add([Opcode.Adc_A_N8, 0x01], new InitialState { A = 0x0F }, new ExpectedState { Cycles = 8, A = 0x10, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = false });
        Add([Opcode.Adc_A_XHL], new InitialState { A = 0x0F, HL = 0xC456, Memory = { [0xC456] = 0x01 }}, new ExpectedState { Cycles = 8, A = 0x10, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = false });
    }
}

public class AdcWithCarryFlagSetTestData : TheoryData<byte[], InitialState, ExpectedState>
{
    public AdcWithCarryFlagSetTestData()
    {
        Add([Opcode.Adc_A_A], new InitialState { A = 0xF0 }, new ExpectedState { Cycles = 4, A = 0xE0, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = false, CarryFlag = true });
        Add([Opcode.Adc_A_B], new InitialState { A = 0xF0, B = 0x20 }, new ExpectedState { Cycles = 4, A = 0x10, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = false, CarryFlag = true });
        Add([Opcode.Adc_A_C], new InitialState { A = 0xF0, C = 0x20 }, new ExpectedState { Cycles = 4, A = 0x10, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = false, CarryFlag = true });
        Add([Opcode.Adc_A_D], new InitialState { A = 0xF0, D = 0x20 }, new ExpectedState { Cycles = 4, A = 0x10, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = false, CarryFlag = true });
        Add([Opcode.Adc_A_E], new InitialState { A = 0xF0, E = 0x20 }, new ExpectedState { Cycles = 4, A = 0x10, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = false, CarryFlag = true });
        Add([Opcode.Adc_A_H], new InitialState { A = 0xF0, H = 0x20 }, new ExpectedState { Cycles = 4, A = 0x10, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = false, CarryFlag = true });
        Add([Opcode.Adc_A_L], new InitialState { A = 0xF0, L = 0x20 }, new ExpectedState { Cycles = 4, A = 0x10, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = false, CarryFlag = true });
        Add([Opcode.Adc_A_N8, 0x20], new InitialState { A = 0xF0 }, new ExpectedState { Cycles = 8, A = 0x10, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = false, CarryFlag = true });
        Add([Opcode.Adc_A_XHL], new InitialState { A = 0xF0, HL = 0xC789, Memory = { [0xC789] = 0x20 }}, new ExpectedState { Cycles = 8, A = 0x10, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = false, CarryFlag = true });
    }
}

public class AdcWithCarryFlagAndHalfCarryFlagSetTestData : TheoryData<byte[], InitialState, ExpectedState>
{
    public AdcWithCarryFlagAndHalfCarryFlagSetTestData()
    {
        Add([Opcode.Adc_A_A], new InitialState { A = 0x8F }, new ExpectedState { Cycles = 4, A = 0x1E, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = true });
        Add([Opcode.Adc_A_B], new InitialState { A = 0x8F, B = 0x91 }, new ExpectedState { Cycles = 4, A = 0x20, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = true });
        Add([Opcode.Adc_A_C], new InitialState { A = 0x8F, C = 0x91 }, new ExpectedState { Cycles = 4, A = 0x20, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = true });
        Add([Opcode.Adc_A_D], new InitialState { A = 0x8F, D = 0x91 }, new ExpectedState { Cycles = 4, A = 0x20, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = true });
        Add([Opcode.Adc_A_E], new InitialState { A = 0x8F, E = 0x91 }, new ExpectedState { Cycles = 4, A = 0x20, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = true });
        Add([Opcode.Adc_A_H], new InitialState { A = 0x8F, H = 0x91 }, new ExpectedState { Cycles = 4, A = 0x20, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = true });
        Add([Opcode.Adc_A_L], new InitialState { A = 0x8F, L = 0x91 }, new ExpectedState { Cycles = 4, A = 0x20, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = true });
        Add([Opcode.Adc_A_N8, 0x91], new InitialState { A = 0x8F }, new ExpectedState { Cycles = 8, A = 0x20, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = true });
        Add([Opcode.Adc_A_XHL], new InitialState { A = 0x8F, HL = 0xC321, Memory = { [0xC321] = 0x91 }}, new ExpectedState { Cycles = 8, A = 0x20, ZeroFlag = false, SubtractFlag = false, HalfCarryFlag = true, CarryFlag = true });
    }
}
