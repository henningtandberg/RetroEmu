using RetroEmu.Devices.DMG;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.GB.TestSetup;
using Xunit;

namespace RetroEmu.GB.Tests.IsolatedOperationTests;

public class CbShiftTests
{
    private readonly IGameBoy _gameBoy = TestGameBoyBuilder.CreateBuilder().BuildGameBoy();
    
    [Theory]
    [ClassData(typeof(SLATestData))]
    public void CBOperation_SLA_ResultCyclesCarryAndZeroIsSetExpected(
        byte[] program, InitialState initialState, ExpectedState expectedState)
    {
        var cartridge = CartridgeBuilder.Create().WithProgram(program).Build();
        _gameBoy.Load(cartridge);
        _gameBoy.SetInitialState(initialState);

        var cycles = _gameBoy.Update();

        _gameBoy.AssertExpectedState(expectedState);
        Assert.Equal(expectedState.Cycles, cycles);
    }

    private class SLATestData : TheoryData<byte[], InitialState, ExpectedState>
    {
        public SLATestData()
        {
            Add([Opcode.Pre_CB, CBOpcode.Sla_A], new InitialState { A = 0b00001111 }, new ExpectedState { Cycles = 8, A = 0b00011110, CarryFlag = false, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Sla_B], new InitialState { B = 0b00001111 }, new ExpectedState { Cycles = 8, B = 0b00011110, CarryFlag = false, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Sla_C], new InitialState { C = 0b00001111 }, new ExpectedState { Cycles = 8, C = 0b00011110, CarryFlag = false, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Sla_D], new InitialState { D = 0b00001111 }, new ExpectedState { Cycles = 8, D = 0b00011110, CarryFlag = false, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Sla_E], new InitialState { E = 0b00001111 }, new ExpectedState { Cycles = 8, E = 0b00011110, CarryFlag = false, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Sla_H], new InitialState { H = 0b00001111 }, new ExpectedState { Cycles = 8, H = 0b00011110, CarryFlag = false, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Sla_L], new InitialState { L = 0b00001111 }, new ExpectedState { Cycles = 8, L = 0b00011110, CarryFlag = false, ZeroFlag = false });
            
            Add([Opcode.Pre_CB, CBOpcode.Sla_A], new InitialState { A = 0b11110000 }, new ExpectedState { Cycles = 8, A = 0b11100000, CarryFlag = true, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Sla_B], new InitialState { B = 0b11110000 }, new ExpectedState { Cycles = 8, B = 0b11100000, CarryFlag = true, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Sla_C], new InitialState { C = 0b11110000 }, new ExpectedState { Cycles = 8, C = 0b11100000, CarryFlag = true, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Sla_D], new InitialState { D = 0b11110000 }, new ExpectedState { Cycles = 8, D = 0b11100000, CarryFlag = true, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Sla_E], new InitialState { E = 0b11110000 }, new ExpectedState { Cycles = 8, E = 0b11100000, CarryFlag = true, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Sla_H], new InitialState { H = 0b11110000 }, new ExpectedState { Cycles = 8, H = 0b11100000, CarryFlag = true, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Sla_L], new InitialState { L = 0b11110000 }, new ExpectedState { Cycles = 8, L = 0b11100000, CarryFlag = true, ZeroFlag = false });
            
            Add([Opcode.Pre_CB, CBOpcode.Sla_A], new InitialState { A = 0b00000000 }, new ExpectedState { Cycles = 8, A = 0b00000000, CarryFlag = false, ZeroFlag = true });
            Add([Opcode.Pre_CB, CBOpcode.Sla_B], new InitialState { B = 0b00000000 }, new ExpectedState { Cycles = 8, B = 0b00000000, CarryFlag = false, ZeroFlag = true });
            Add([Opcode.Pre_CB, CBOpcode.Sla_C], new InitialState { C = 0b00000000 }, new ExpectedState { Cycles = 8, C = 0b00000000, CarryFlag = false, ZeroFlag = true });
            Add([Opcode.Pre_CB, CBOpcode.Sla_D], new InitialState { D = 0b00000000 }, new ExpectedState { Cycles = 8, D = 0b00000000, CarryFlag = false, ZeroFlag = true });
            Add([Opcode.Pre_CB, CBOpcode.Sla_E], new InitialState { E = 0b00000000 }, new ExpectedState { Cycles = 8, E = 0b00000000, CarryFlag = false, ZeroFlag = true });
            Add([Opcode.Pre_CB, CBOpcode.Sla_H], new InitialState { H = 0b00000000 }, new ExpectedState { Cycles = 8, H = 0b00000000, CarryFlag = false, ZeroFlag = true });
            Add([Opcode.Pre_CB, CBOpcode.Sla_L], new InitialState { L = 0b00000000 }, new ExpectedState { Cycles = 8, L = 0b00000000, CarryFlag = false, ZeroFlag = true });
        }
    }
    
    [Theory]
    [ClassData(typeof(SRATestData))]
    public void CBOperation_SRA_ResultCyclesCarryAndZeroIsSetExpected(
        byte[] program, InitialState initialState, ExpectedState expectedState)
    {
        var cartridge = CartridgeBuilder.Create().WithProgram(program).Build();
        _gameBoy.Load(cartridge);
        _gameBoy.SetInitialState(initialState);

        var cycles = _gameBoy.Update();

        _gameBoy.AssertExpectedState(expectedState);
        Assert.Equal(expectedState.Cycles, cycles);
    }

    private class SRATestData : TheoryData<byte[], InitialState, ExpectedState>
    {
        public SRATestData()
        {
            Add([Opcode.Pre_CB, CBOpcode.Sra_A], new InitialState { A = 0b00001111 }, new ExpectedState { Cycles = 8, A = 0b00000111, CarryFlag = true, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Sra_B], new InitialState { B = 0b00001111 }, new ExpectedState { Cycles = 8, B = 0b00000111, CarryFlag = true, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Sra_C], new InitialState { C = 0b00001111 }, new ExpectedState { Cycles = 8, C = 0b00000111, CarryFlag = true, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Sra_D], new InitialState { D = 0b00001111 }, new ExpectedState { Cycles = 8, D = 0b00000111, CarryFlag = true, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Sra_E], new InitialState { E = 0b00001111 }, new ExpectedState { Cycles = 8, E = 0b00000111, CarryFlag = true, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Sra_H], new InitialState { H = 0b00001111 }, new ExpectedState { Cycles = 8, H = 0b00000111, CarryFlag = true, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Sra_L], new InitialState { L = 0b00001111 }, new ExpectedState { Cycles = 8, L = 0b00000111, CarryFlag = true, ZeroFlag = false });
            
            Add([Opcode.Pre_CB, CBOpcode.Sra_A], new InitialState { A = 0b11110000 }, new ExpectedState { Cycles = 8, A = 0b11111000, CarryFlag = false, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Sra_B], new InitialState { B = 0b11110000 }, new ExpectedState { Cycles = 8, B = 0b11111000, CarryFlag = false, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Sra_C], new InitialState { C = 0b11110000 }, new ExpectedState { Cycles = 8, C = 0b11111000, CarryFlag = false, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Sra_D], new InitialState { D = 0b11110000 }, new ExpectedState { Cycles = 8, D = 0b11111000, CarryFlag = false, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Sra_E], new InitialState { E = 0b11110000 }, new ExpectedState { Cycles = 8, E = 0b11111000, CarryFlag = false, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Sra_H], new InitialState { H = 0b11110000 }, new ExpectedState { Cycles = 8, H = 0b11111000, CarryFlag = false, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Sra_L], new InitialState { L = 0b11110000 }, new ExpectedState { Cycles = 8, L = 0b11111000, CarryFlag = false, ZeroFlag = false });
            
            Add([Opcode.Pre_CB, CBOpcode.Sra_A], new InitialState { A = 0b00000000 }, new ExpectedState { Cycles = 8, A = 0b00000000, CarryFlag = false, ZeroFlag = true });
            Add([Opcode.Pre_CB, CBOpcode.Sra_B], new InitialState { B = 0b00000000 }, new ExpectedState { Cycles = 8, B = 0b00000000, CarryFlag = false, ZeroFlag = true });
            Add([Opcode.Pre_CB, CBOpcode.Sra_C], new InitialState { C = 0b00000000 }, new ExpectedState { Cycles = 8, C = 0b00000000, CarryFlag = false, ZeroFlag = true });
            Add([Opcode.Pre_CB, CBOpcode.Sra_D], new InitialState { D = 0b00000000 }, new ExpectedState { Cycles = 8, D = 0b00000000, CarryFlag = false, ZeroFlag = true });
            Add([Opcode.Pre_CB, CBOpcode.Sra_E], new InitialState { E = 0b00000000 }, new ExpectedState { Cycles = 8, E = 0b00000000, CarryFlag = false, ZeroFlag = true });
            Add([Opcode.Pre_CB, CBOpcode.Sra_H], new InitialState { H = 0b00000000 }, new ExpectedState { Cycles = 8, H = 0b00000000, CarryFlag = false, ZeroFlag = true });
            Add([Opcode.Pre_CB, CBOpcode.Sra_L], new InitialState { L = 0b00000000 }, new ExpectedState { Cycles = 8, L = 0b00000000, CarryFlag = false, ZeroFlag = true });
        }
    }
    
    [Theory]
    [ClassData(typeof(SRLTestData))]
    public void CBOperation_SRL_ResultCyclesCarryAndZeroIsSetExpected(
        byte[] program, InitialState initialState, ExpectedState expectedState)
    {
        var cartridge = CartridgeBuilder.Create().WithProgram(program).Build();
        _gameBoy.Load(cartridge);
        _gameBoy.SetInitialState(initialState);

        var cycles = _gameBoy.Update();

        _gameBoy.AssertExpectedState(expectedState);
        Assert.Equal(expectedState.Cycles, cycles);
    }

    private class SRLTestData : TheoryData<byte[], InitialState, ExpectedState>
    {
        public SRLTestData()
        {
            Add([Opcode.Pre_CB, CBOpcode.Srl_A], new InitialState { A = 0b00001111 }, new ExpectedState { Cycles = 8, A = 0b00000111, CarryFlag = true, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Srl_B], new InitialState { B = 0b00001111 }, new ExpectedState { Cycles = 8, B = 0b00000111, CarryFlag = true, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Srl_C], new InitialState { C = 0b00001111 }, new ExpectedState { Cycles = 8, C = 0b00000111, CarryFlag = true, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Srl_D], new InitialState { D = 0b00001111 }, new ExpectedState { Cycles = 8, D = 0b00000111, CarryFlag = true, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Srl_E], new InitialState { E = 0b00001111 }, new ExpectedState { Cycles = 8, E = 0b00000111, CarryFlag = true, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Srl_H], new InitialState { H = 0b00001111 }, new ExpectedState { Cycles = 8, H = 0b00000111, CarryFlag = true, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Srl_L], new InitialState { L = 0b00001111 }, new ExpectedState { Cycles = 8, L = 0b00000111, CarryFlag = true, ZeroFlag = false });
            
            Add([Opcode.Pre_CB, CBOpcode.Srl_A], new InitialState { A = 0b11110000 }, new ExpectedState { Cycles = 8, A = 0b01111000, CarryFlag = false, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Srl_B], new InitialState { B = 0b11110000 }, new ExpectedState { Cycles = 8, B = 0b01111000, CarryFlag = false, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Srl_C], new InitialState { C = 0b11110000 }, new ExpectedState { Cycles = 8, C = 0b01111000, CarryFlag = false, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Srl_D], new InitialState { D = 0b11110000 }, new ExpectedState { Cycles = 8, D = 0b01111000, CarryFlag = false, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Srl_E], new InitialState { E = 0b11110000 }, new ExpectedState { Cycles = 8, E = 0b01111000, CarryFlag = false, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Srl_H], new InitialState { H = 0b11110000 }, new ExpectedState { Cycles = 8, H = 0b01111000, CarryFlag = false, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Srl_L], new InitialState { L = 0b11110000 }, new ExpectedState { Cycles = 8, L = 0b01111000, CarryFlag = false, ZeroFlag = false });
            
            Add([Opcode.Pre_CB, CBOpcode.Srl_A], new InitialState { A = 0b00000000 }, new ExpectedState { Cycles = 8, A = 0b00000000, CarryFlag = false, ZeroFlag = true });
            Add([Opcode.Pre_CB, CBOpcode.Srl_B], new InitialState { B = 0b00000000 }, new ExpectedState { Cycles = 8, B = 0b00000000, CarryFlag = false, ZeroFlag = true });
            Add([Opcode.Pre_CB, CBOpcode.Srl_C], new InitialState { C = 0b00000000 }, new ExpectedState { Cycles = 8, C = 0b00000000, CarryFlag = false, ZeroFlag = true });
            Add([Opcode.Pre_CB, CBOpcode.Srl_D], new InitialState { D = 0b00000000 }, new ExpectedState { Cycles = 8, D = 0b00000000, CarryFlag = false, ZeroFlag = true });
            Add([Opcode.Pre_CB, CBOpcode.Srl_E], new InitialState { E = 0b00000000 }, new ExpectedState { Cycles = 8, E = 0b00000000, CarryFlag = false, ZeroFlag = true });
            Add([Opcode.Pre_CB, CBOpcode.Srl_H], new InitialState { H = 0b00000000 }, new ExpectedState { Cycles = 8, H = 0b00000000, CarryFlag = false, ZeroFlag = true });
            Add([Opcode.Pre_CB, CBOpcode.Srl_L], new InitialState { L = 0b00000000 }, new ExpectedState { Cycles = 8, L = 0b00000000, CarryFlag = false, ZeroFlag = true });
        }
    }
}