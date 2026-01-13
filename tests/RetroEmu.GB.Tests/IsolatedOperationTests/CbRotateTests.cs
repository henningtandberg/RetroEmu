using RetroEmu.Devices.DMG;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.GB.TestSetup;
using Xunit;

namespace RetroEmu.GB.Tests.IsolatedOperationTests;

public class CbRotateTests
{
    private readonly IGameBoy _gameBoy = TestGameBoyBuilder.CreateBuilder().BuildGameBoy();

    [Theory]
    [ClassData(typeof(RRTestData))]
    public void CBOperation_RR_ResultCyclesCarryAndZeroIsSetExpected(
        byte[] program, InitialState initialState, ExpectedState expectedState)
    {
        var cartridge = CartridgeBuilder.Create().WithProgram(program).Build();
        _gameBoy.Load(cartridge);
        _gameBoy.SetInitialState(initialState);

        var cycles = _gameBoy.Update();

        _gameBoy.AssertExpectedState(expectedState);
        Assert.Equal(expectedState.Cycles, cycles);
    }

    private class RRTestData : TheoryData<byte[], InitialState, ExpectedState>
    {
        public RRTestData()
        {
            // Right Rotate into carry
            Add([Opcode.Pre_CB, CBOpcode.Rr_A], new InitialState { A = 0b00001111, CarryFlag = false }, new ExpectedState { Cycles = 8, A = 0b00000111, CarryFlag = true, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Rr_B], new InitialState { B = 0b00001111, CarryFlag = false }, new ExpectedState { Cycles = 8, B = 0b00000111, CarryFlag = true, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Rr_C], new InitialState { C = 0b00001111, CarryFlag = false }, new ExpectedState { Cycles = 8, C = 0b00000111, CarryFlag = true, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Rr_D], new InitialState { D = 0b00001111, CarryFlag = false }, new ExpectedState { Cycles = 8, D = 0b00000111, CarryFlag = true, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Rr_E], new InitialState { E = 0b00001111, CarryFlag = false }, new ExpectedState { Cycles = 8, E = 0b00000111, CarryFlag = true, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Rr_H], new InitialState { H = 0b00001111, CarryFlag = false }, new ExpectedState { Cycles = 8, H = 0b00000111, CarryFlag = true, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Rr_L], new InitialState { L = 0b00001111, CarryFlag = false }, new ExpectedState { Cycles = 8, L = 0b00000111, CarryFlag = true, ZeroFlag = false });
            
            // Rotate carry into bit
            Add([Opcode.Pre_CB, CBOpcode.Rr_A], new InitialState { A = 0b00001111, CarryFlag = true }, new ExpectedState { Cycles = 8, A = 0b10000111, CarryFlag = true, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Rr_B], new InitialState { B = 0b00001111, CarryFlag = true }, new ExpectedState { Cycles = 8, B = 0b10000111, CarryFlag = true, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Rr_C], new InitialState { C = 0b00001111, CarryFlag = true }, new ExpectedState { Cycles = 8, C = 0b10000111, CarryFlag = true, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Rr_D], new InitialState { D = 0b00001111, CarryFlag = true }, new ExpectedState { Cycles = 8, D = 0b10000111, CarryFlag = true, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Rr_E], new InitialState { E = 0b00001111, CarryFlag = true }, new ExpectedState { Cycles = 8, E = 0b10000111, CarryFlag = true, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Rr_H], new InitialState { H = 0b00001111, CarryFlag = true }, new ExpectedState { Cycles = 8, H = 0b10000111, CarryFlag = true, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Rr_L], new InitialState { L = 0b00001111, CarryFlag = true }, new ExpectedState { Cycles = 8, L = 0b10000111, CarryFlag = true, ZeroFlag = false });
            
            // Right rotate carry into bit, register value is zero
            Add([Opcode.Pre_CB, CBOpcode.Rr_A], new InitialState { A = 0b0000000, CarryFlag = true }, new ExpectedState { Cycles = 8, A = 0b10000000, CarryFlag = false, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Rr_B], new InitialState { B = 0b0000000, CarryFlag = true }, new ExpectedState { Cycles = 8, B = 0b10000000, CarryFlag = false, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Rr_C], new InitialState { C = 0b0000000, CarryFlag = true }, new ExpectedState { Cycles = 8, C = 0b10000000, CarryFlag = false, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Rr_D], new InitialState { D = 0b0000000, CarryFlag = true }, new ExpectedState { Cycles = 8, D = 0b10000000, CarryFlag = false, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Rr_E], new InitialState { E = 0b0000000, CarryFlag = true }, new ExpectedState { Cycles = 8, E = 0b10000000, CarryFlag = false, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Rr_H], new InitialState { H = 0b0000000, CarryFlag = true }, new ExpectedState { Cycles = 8, H = 0b10000000, CarryFlag = false, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Rr_L], new InitialState { L = 0b0000000, CarryFlag = true }, new ExpectedState { Cycles = 8, L = 0b10000000, CarryFlag = false, ZeroFlag = false });
            
            // Right rotate, carry not set and register value is zero
            Add([Opcode.Pre_CB, CBOpcode.Rr_A], new InitialState { A = 0b0000000, CarryFlag = false }, new ExpectedState { Cycles = 8, A = 0b00000000, CarryFlag = false, ZeroFlag = true });
            Add([Opcode.Pre_CB, CBOpcode.Rr_B], new InitialState { B = 0b0000000, CarryFlag = false }, new ExpectedState { Cycles = 8, B = 0b00000000, CarryFlag = false, ZeroFlag = true });
            Add([Opcode.Pre_CB, CBOpcode.Rr_C], new InitialState { C = 0b0000000, CarryFlag = false }, new ExpectedState { Cycles = 8, C = 0b00000000, CarryFlag = false, ZeroFlag = true });
            Add([Opcode.Pre_CB, CBOpcode.Rr_D], new InitialState { D = 0b0000000, CarryFlag = false }, new ExpectedState { Cycles = 8, D = 0b00000000, CarryFlag = false, ZeroFlag = true });
            Add([Opcode.Pre_CB, CBOpcode.Rr_E], new InitialState { E = 0b0000000, CarryFlag = false }, new ExpectedState { Cycles = 8, E = 0b00000000, CarryFlag = false, ZeroFlag = true });
            Add([Opcode.Pre_CB, CBOpcode.Rr_H], new InitialState { H = 0b0000000, CarryFlag = false }, new ExpectedState { Cycles = 8, H = 0b00000000, CarryFlag = false, ZeroFlag = true });
            Add([Opcode.Pre_CB, CBOpcode.Rr_L], new InitialState { L = 0b0000000, CarryFlag = false }, new ExpectedState { Cycles = 8, L = 0b00000000, CarryFlag = false, ZeroFlag = true });
        }
    }
    
    [Theory]
    [ClassData(typeof(RLTestData))]
    public void CBOperation_RL_ResultCyclesCarryAndZeroIsSetExpected(
        byte[] program, InitialState initialState, ExpectedState expectedState)
    {
        var cartridge = CartridgeBuilder.Create().WithProgram(program).Build();
        _gameBoy.Load(cartridge);
        _gameBoy.SetInitialState(initialState);

        var cycles = _gameBoy.Update();

        _gameBoy.AssertExpectedState(expectedState);
        Assert.Equal(expectedState.Cycles, cycles);
    }

    private class RLTestData : TheoryData<byte[], InitialState, ExpectedState>
    {
        public RLTestData()
        {
            // Left rotate into carry
            Add([Opcode.Pre_CB, CBOpcode.Rl_A], new InitialState { A = 0b11110000, CarryFlag = false }, new ExpectedState { Cycles = 8, A = 0b11100000, CarryFlag = true, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Rl_B], new InitialState { B = 0b11110000, CarryFlag = false }, new ExpectedState { Cycles = 8, B = 0b11100000, CarryFlag = true, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Rl_C], new InitialState { C = 0b11110000, CarryFlag = false }, new ExpectedState { Cycles = 8, C = 0b11100000, CarryFlag = true, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Rl_D], new InitialState { D = 0b11110000, CarryFlag = false }, new ExpectedState { Cycles = 8, D = 0b11100000, CarryFlag = true, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Rl_E], new InitialState { E = 0b11110000, CarryFlag = false }, new ExpectedState { Cycles = 8, E = 0b11100000, CarryFlag = true, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Rl_H], new InitialState { H = 0b11110000, CarryFlag = false }, new ExpectedState { Cycles = 8, H = 0b11100000, CarryFlag = true, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Rl_L], new InitialState { L = 0b11110000, CarryFlag = false }, new ExpectedState { Cycles = 8, L = 0b11100000, CarryFlag = true, ZeroFlag = false });
            
            // Left rotate carry into bit
            Add([Opcode.Pre_CB, CBOpcode.Rl_A], new InitialState { A = 0b11110000, CarryFlag = true }, new ExpectedState { Cycles = 8, A = 0b11100001, CarryFlag = true, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Rl_B], new InitialState { B = 0b11110000, CarryFlag = true }, new ExpectedState { Cycles = 8, B = 0b11100001, CarryFlag = true, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Rl_C], new InitialState { C = 0b11110000, CarryFlag = true }, new ExpectedState { Cycles = 8, C = 0b11100001, CarryFlag = true, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Rl_D], new InitialState { D = 0b11110000, CarryFlag = true }, new ExpectedState { Cycles = 8, D = 0b11100001, CarryFlag = true, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Rl_E], new InitialState { E = 0b11110000, CarryFlag = true }, new ExpectedState { Cycles = 8, E = 0b11100001, CarryFlag = true, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Rl_H], new InitialState { H = 0b11110000, CarryFlag = true }, new ExpectedState { Cycles = 8, H = 0b11100001, CarryFlag = true, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Rl_L], new InitialState { L = 0b11110000, CarryFlag = true }, new ExpectedState { Cycles = 8, L = 0b11100001, CarryFlag = true, ZeroFlag = false });
            
            // Left rotate carry into bit, register value is zero
            Add([Opcode.Pre_CB, CBOpcode.Rl_A], new InitialState { A = 0b0000000, CarryFlag = true }, new ExpectedState { Cycles = 8, A = 0b00000001, CarryFlag = false, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Rl_B], new InitialState { B = 0b0000000, CarryFlag = true }, new ExpectedState { Cycles = 8, B = 0b00000001, CarryFlag = false, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Rl_C], new InitialState { C = 0b0000000, CarryFlag = true }, new ExpectedState { Cycles = 8, C = 0b00000001, CarryFlag = false, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Rl_D], new InitialState { D = 0b0000000, CarryFlag = true }, new ExpectedState { Cycles = 8, D = 0b00000001, CarryFlag = false, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Rl_E], new InitialState { E = 0b0000000, CarryFlag = true }, new ExpectedState { Cycles = 8, E = 0b00000001, CarryFlag = false, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Rl_H], new InitialState { H = 0b0000000, CarryFlag = true }, new ExpectedState { Cycles = 8, H = 0b00000001, CarryFlag = false, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Rl_L], new InitialState { L = 0b0000000, CarryFlag = true }, new ExpectedState { Cycles = 8, L = 0b00000001, CarryFlag = false, ZeroFlag = false });
            
            // Left rotate, carry not set and register value is zero
            Add([Opcode.Pre_CB, CBOpcode.Rl_A], new InitialState { A = 0b0000000, CarryFlag = false }, new ExpectedState { Cycles = 8, A = 0b00000000, CarryFlag = false, ZeroFlag = true });
            Add([Opcode.Pre_CB, CBOpcode.Rl_B], new InitialState { B = 0b0000000, CarryFlag = false }, new ExpectedState { Cycles = 8, B = 0b00000000, CarryFlag = false, ZeroFlag = true });
            Add([Opcode.Pre_CB, CBOpcode.Rl_C], new InitialState { C = 0b0000000, CarryFlag = false }, new ExpectedState { Cycles = 8, C = 0b00000000, CarryFlag = false, ZeroFlag = true });
            Add([Opcode.Pre_CB, CBOpcode.Rl_D], new InitialState { D = 0b0000000, CarryFlag = false }, new ExpectedState { Cycles = 8, D = 0b00000000, CarryFlag = false, ZeroFlag = true });
            Add([Opcode.Pre_CB, CBOpcode.Rl_E], new InitialState { E = 0b0000000, CarryFlag = false }, new ExpectedState { Cycles = 8, E = 0b00000000, CarryFlag = false, ZeroFlag = true });
            Add([Opcode.Pre_CB, CBOpcode.Rl_H], new InitialState { H = 0b0000000, CarryFlag = false }, new ExpectedState { Cycles = 8, H = 0b00000000, CarryFlag = false, ZeroFlag = true });
            Add([Opcode.Pre_CB, CBOpcode.Rl_L], new InitialState { L = 0b0000000, CarryFlag = false }, new ExpectedState { Cycles = 8, L = 0b00000000, CarryFlag = false, ZeroFlag = true });
        }
    }
    
    [Theory]
    [ClassData(typeof(RRCTestData))]
    public void CBOperation_RRC_ResultCyclesCarryAndZeroIsSetExpected(
        byte[] program, InitialState initialState, ExpectedState expectedState)
    {
        var cartridge = CartridgeBuilder.Create().WithProgram(program).Build();
        _gameBoy.Load(cartridge);
        _gameBoy.SetInitialState(initialState);

        var cycles = _gameBoy.Update();

        _gameBoy.AssertExpectedState(expectedState);
        Assert.Equal(expectedState.Cycles, cycles);
    }

    private class RRCTestData : TheoryData<byte[], InitialState, ExpectedState>
    {
        public RRCTestData()
        {
            // Right rotate, set carry
            Add([Opcode.Pre_CB, CBOpcode.Rrc_A], new InitialState { A = 0b00001111, CarryFlag = false }, new ExpectedState { Cycles = 8, A = 0b10000111, CarryFlag = true, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Rrc_B], new InitialState { B = 0b00001111, CarryFlag = false }, new ExpectedState { Cycles = 8, B = 0b10000111, CarryFlag = true, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Rrc_C], new InitialState { C = 0b00001111, CarryFlag = false }, new ExpectedState { Cycles = 8, C = 0b10000111, CarryFlag = true, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Rrc_D], new InitialState { D = 0b00001111, CarryFlag = false }, new ExpectedState { Cycles = 8, D = 0b10000111, CarryFlag = true, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Rrc_E], new InitialState { E = 0b00001111, CarryFlag = false }, new ExpectedState { Cycles = 8, E = 0b10000111, CarryFlag = true, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Rrc_H], new InitialState { H = 0b00001111, CarryFlag = false }, new ExpectedState { Cycles = 8, H = 0b10000111, CarryFlag = true, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Rrc_L], new InitialState { L = 0b00001111, CarryFlag = false }, new ExpectedState { Cycles = 8, L = 0b10000111, CarryFlag = true, ZeroFlag = false });
            
            // Right rotate, carry is set, register value is zero
            Add([Opcode.Pre_CB, CBOpcode.Rrc_A], new InitialState { A = 0b0000000, CarryFlag = true }, new ExpectedState { Cycles = 8, A = 0b00000000, CarryFlag = false, ZeroFlag = true });
            Add([Opcode.Pre_CB, CBOpcode.Rrc_B], new InitialState { B = 0b0000000, CarryFlag = true }, new ExpectedState { Cycles = 8, B = 0b00000000, CarryFlag = false, ZeroFlag = true });
            Add([Opcode.Pre_CB, CBOpcode.Rrc_C], new InitialState { C = 0b0000000, CarryFlag = true }, new ExpectedState { Cycles = 8, C = 0b00000000, CarryFlag = false, ZeroFlag = true });
            Add([Opcode.Pre_CB, CBOpcode.Rrc_D], new InitialState { D = 0b0000000, CarryFlag = true }, new ExpectedState { Cycles = 8, D = 0b00000000, CarryFlag = false, ZeroFlag = true });
            Add([Opcode.Pre_CB, CBOpcode.Rrc_E], new InitialState { E = 0b0000000, CarryFlag = true }, new ExpectedState { Cycles = 8, E = 0b00000000, CarryFlag = false, ZeroFlag = true });
            Add([Opcode.Pre_CB, CBOpcode.Rrc_H], new InitialState { H = 0b0000000, CarryFlag = true }, new ExpectedState { Cycles = 8, H = 0b00000000, CarryFlag = false, ZeroFlag = true });
            Add([Opcode.Pre_CB, CBOpcode.Rrc_L], new InitialState { L = 0b0000000, CarryFlag = true }, new ExpectedState { Cycles = 8, L = 0b00000000, CarryFlag = false, ZeroFlag = true });
            
            // Right rotate, carry not set, register value is zero
            Add([Opcode.Pre_CB, CBOpcode.Rrc_A], new InitialState { A = 0b0000000, CarryFlag = false }, new ExpectedState { Cycles = 8, A = 0b00000000, CarryFlag = false, ZeroFlag = true });
            Add([Opcode.Pre_CB, CBOpcode.Rrc_B], new InitialState { B = 0b0000000, CarryFlag = false }, new ExpectedState { Cycles = 8, B = 0b00000000, CarryFlag = false, ZeroFlag = true });
            Add([Opcode.Pre_CB, CBOpcode.Rrc_C], new InitialState { C = 0b0000000, CarryFlag = false }, new ExpectedState { Cycles = 8, C = 0b00000000, CarryFlag = false, ZeroFlag = true });
            Add([Opcode.Pre_CB, CBOpcode.Rrc_D], new InitialState { D = 0b0000000, CarryFlag = false }, new ExpectedState { Cycles = 8, D = 0b00000000, CarryFlag = false, ZeroFlag = true });
            Add([Opcode.Pre_CB, CBOpcode.Rrc_E], new InitialState { E = 0b0000000, CarryFlag = false }, new ExpectedState { Cycles = 8, E = 0b00000000, CarryFlag = false, ZeroFlag = true });
            Add([Opcode.Pre_CB, CBOpcode.Rrc_H], new InitialState { H = 0b0000000, CarryFlag = false }, new ExpectedState { Cycles = 8, H = 0b00000000, CarryFlag = false, ZeroFlag = true });
            Add([Opcode.Pre_CB, CBOpcode.Rrc_L], new InitialState { L = 0b0000000, CarryFlag = false }, new ExpectedState { Cycles = 8, L = 0b00000000, CarryFlag = false, ZeroFlag = true });
        }
    }
    
    [Theory]
    [ClassData(typeof(RLCTestData))]
    public void CBOperation_RLC_ResultCyclesCarryAndZeroIsSetExpected(
        byte[] program, InitialState initialState, ExpectedState expectedState)
    {
        var cartridge = CartridgeBuilder.Create().WithProgram(program).Build();
        _gameBoy.Load(cartridge);
        _gameBoy.SetInitialState(initialState);

        var cycles = _gameBoy.Update();

        _gameBoy.AssertExpectedState(expectedState);
        Assert.Equal(expectedState.Cycles, cycles);
    }

    private class RLCTestData : TheoryData<byte[], InitialState, ExpectedState>
    {
        public RLCTestData()
        {
            // Left rotate, set carry
            Add([Opcode.Pre_CB, CBOpcode.Rlc_A], new InitialState { A = 0b11110000, CarryFlag = false }, new ExpectedState { Cycles = 8, A = 0b11100001, CarryFlag = true, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Rlc_B], new InitialState { B = 0b11110000, CarryFlag = false }, new ExpectedState { Cycles = 8, B = 0b11100001, CarryFlag = true, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Rlc_C], new InitialState { C = 0b11110000, CarryFlag = false }, new ExpectedState { Cycles = 8, C = 0b11100001, CarryFlag = true, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Rlc_D], new InitialState { D = 0b11110000, CarryFlag = false }, new ExpectedState { Cycles = 8, D = 0b11100001, CarryFlag = true, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Rlc_E], new InitialState { E = 0b11110000, CarryFlag = false }, new ExpectedState { Cycles = 8, E = 0b11100001, CarryFlag = true, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Rlc_H], new InitialState { H = 0b11110000, CarryFlag = false }, new ExpectedState { Cycles = 8, H = 0b11100001, CarryFlag = true, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Rlc_L], new InitialState { L = 0b11110000, CarryFlag = false }, new ExpectedState { Cycles = 8, L = 0b11100001, CarryFlag = true, ZeroFlag = false });
            
            // Left rotate, carry is set, register value is zero
            Add([Opcode.Pre_CB, CBOpcode.Rlc_A], new InitialState { A = 0b0000000, CarryFlag = true }, new ExpectedState { Cycles = 8, A = 0b00000000, CarryFlag = false, ZeroFlag = true });
            Add([Opcode.Pre_CB, CBOpcode.Rlc_B], new InitialState { B = 0b0000000, CarryFlag = true }, new ExpectedState { Cycles = 8, B = 0b00000000, CarryFlag = false, ZeroFlag = true });
            Add([Opcode.Pre_CB, CBOpcode.Rlc_C], new InitialState { C = 0b0000000, CarryFlag = true }, new ExpectedState { Cycles = 8, C = 0b00000000, CarryFlag = false, ZeroFlag = true });
            Add([Opcode.Pre_CB, CBOpcode.Rlc_D], new InitialState { D = 0b0000000, CarryFlag = true }, new ExpectedState { Cycles = 8, D = 0b00000000, CarryFlag = false, ZeroFlag = true });
            Add([Opcode.Pre_CB, CBOpcode.Rlc_E], new InitialState { E = 0b0000000, CarryFlag = true }, new ExpectedState { Cycles = 8, E = 0b00000000, CarryFlag = false, ZeroFlag = true });
            Add([Opcode.Pre_CB, CBOpcode.Rlc_H], new InitialState { H = 0b0000000, CarryFlag = true }, new ExpectedState { Cycles = 8, H = 0b00000000, CarryFlag = false, ZeroFlag = true });
            Add([Opcode.Pre_CB, CBOpcode.Rlc_L], new InitialState { L = 0b0000000, CarryFlag = true }, new ExpectedState { Cycles = 8, L = 0b00000000, CarryFlag = false, ZeroFlag = true });
            
            // Left rotate, carry not set, register value is zero
            Add([Opcode.Pre_CB, CBOpcode.Rlc_A], new InitialState { A = 0b0000000, CarryFlag = false }, new ExpectedState { Cycles = 8, A = 0b00000000, CarryFlag = false, ZeroFlag = true });
            Add([Opcode.Pre_CB, CBOpcode.Rlc_B], new InitialState { B = 0b0000000, CarryFlag = false }, new ExpectedState { Cycles = 8, B = 0b00000000, CarryFlag = false, ZeroFlag = true });
            Add([Opcode.Pre_CB, CBOpcode.Rlc_C], new InitialState { C = 0b0000000, CarryFlag = false }, new ExpectedState { Cycles = 8, C = 0b00000000, CarryFlag = false, ZeroFlag = true });
            Add([Opcode.Pre_CB, CBOpcode.Rlc_D], new InitialState { D = 0b0000000, CarryFlag = false }, new ExpectedState { Cycles = 8, D = 0b00000000, CarryFlag = false, ZeroFlag = true });
            Add([Opcode.Pre_CB, CBOpcode.Rlc_E], new InitialState { E = 0b0000000, CarryFlag = false }, new ExpectedState { Cycles = 8, E = 0b00000000, CarryFlag = false, ZeroFlag = true });
            Add([Opcode.Pre_CB, CBOpcode.Rlc_H], new InitialState { H = 0b0000000, CarryFlag = false }, new ExpectedState { Cycles = 8, H = 0b00000000, CarryFlag = false, ZeroFlag = true });
            Add([Opcode.Pre_CB, CBOpcode.Rlc_L], new InitialState { L = 0b0000000, CarryFlag = false }, new ExpectedState { Cycles = 8, L = 0b00000000, CarryFlag = false, ZeroFlag = true });
        }
    }
}