using System.Collections.Generic;
using RetroEmu.Devices.DMG;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.GB.TestSetup;
using Xunit;

namespace RetroEmu.GB.Tests.IsolatedOperationTests;

public class CbSwapTests
{
    private readonly IGameBoy _gameBoy = TestGameBoyBuilder.CreateBuilder().BuildGameBoy();
    
    [Theory]
    [ClassData(typeof(SwapTestData))]
    public void CBOperation_Swap_ValueIsSwappedAndZeroFlagIsSetCorrectly(
        byte[] program, InitialState initialState, ExpectedState expectedState)
    {
        var cartridge = CartridgeBuilder.Create().WithProgram(program).Build();
        _gameBoy.Load(cartridge);
        _gameBoy.SetInitialState(initialState);

        var cycles = _gameBoy.Update();

        _gameBoy.AssertExpectedState(expectedState);
        Assert.Equal(expectedState.Cycles, cycles);
    }

    private class SwapTestData : TheoryData<byte[], InitialState, ExpectedState>
    {
        public SwapTestData()
        {
            Add([Opcode.Pre_CB, CBOpcode.Swap_A], new InitialState { A = 0b00001111 }, new ExpectedState { Cycles = 8, A = 0b11110000, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Swap_A], new InitialState { A = 0b10101010 }, new ExpectedState { Cycles = 8, A = 0b10101010, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Swap_A], new InitialState { A = 0b11000011 }, new ExpectedState { Cycles = 8, A = 0b00111100, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Swap_A], new InitialState { A = 0b11111111 }, new ExpectedState { Cycles = 8, A = 0b11111111, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Swap_A], new InitialState { A = 0b00000000 }, new ExpectedState { Cycles = 8, A = 0b00000000, ZeroFlag = true });
            
            Add([Opcode.Pre_CB, CBOpcode.Swap_B], new InitialState { B = 0b00001111 }, new ExpectedState { Cycles = 8, B = 0b11110000, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Swap_B], new InitialState { B = 0b10101010 }, new ExpectedState { Cycles = 8, B = 0b10101010, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Swap_B], new InitialState { B = 0b11000011 }, new ExpectedState { Cycles = 8, B = 0b00111100, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Swap_B], new InitialState { B = 0b11111111 }, new ExpectedState { Cycles = 8, B = 0b11111111, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Swap_B], new InitialState { B = 0b00000000 }, new ExpectedState { Cycles = 8, B = 0b00000000, ZeroFlag = true });
            
            Add([Opcode.Pre_CB, CBOpcode.Swap_C], new InitialState { C = 0b00001111 }, new ExpectedState { Cycles = 8, C = 0b11110000, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Swap_C], new InitialState { C = 0b10101010 }, new ExpectedState { Cycles = 8, C = 0b10101010, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Swap_C], new InitialState { C = 0b11000011 }, new ExpectedState { Cycles = 8, C = 0b00111100, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Swap_C], new InitialState { C = 0b11111111 }, new ExpectedState { Cycles = 8, C = 0b11111111, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Swap_C], new InitialState { C = 0b00000000 }, new ExpectedState { Cycles = 8, C = 0b00000000, ZeroFlag = true });
            
            Add([Opcode.Pre_CB, CBOpcode.Swap_D], new InitialState { D = 0b00001111 }, new ExpectedState { Cycles = 8, D = 0b11110000, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Swap_D], new InitialState { D = 0b10101010 }, new ExpectedState { Cycles = 8, D = 0b10101010, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Swap_D], new InitialState { D = 0b11000011 }, new ExpectedState { Cycles = 8, D = 0b00111100, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Swap_D], new InitialState { D = 0b11111111 }, new ExpectedState { Cycles = 8, D = 0b11111111, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Swap_D], new InitialState { D = 0b00000000 }, new ExpectedState { Cycles = 8, D = 0b00000000, ZeroFlag = true });
            
            Add([Opcode.Pre_CB, CBOpcode.Swap_E], new InitialState { E = 0b00001111 }, new ExpectedState { Cycles = 8, E = 0b11110000, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Swap_E], new InitialState { E = 0b10101010 }, new ExpectedState { Cycles = 8, E = 0b10101010, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Swap_E], new InitialState { E = 0b11000011 }, new ExpectedState { Cycles = 8, E = 0b00111100, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Swap_E], new InitialState { E = 0b11111111 }, new ExpectedState { Cycles = 8, E = 0b11111111, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Swap_E], new InitialState { E = 0b00000000 }, new ExpectedState { Cycles = 8, E = 0b00000000, ZeroFlag = true });
            
            Add([Opcode.Pre_CB, CBOpcode.Swap_H], new InitialState { H = 0b00001111 }, new ExpectedState { Cycles = 8, H = 0b11110000, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Swap_H], new InitialState { H = 0b10101010 }, new ExpectedState { Cycles = 8, H = 0b10101010, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Swap_H], new InitialState { H = 0b11000011 }, new ExpectedState { Cycles = 8, H = 0b00111100, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Swap_H], new InitialState { H = 0b11111111 }, new ExpectedState { Cycles = 8, H = 0b11111111, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Swap_H], new InitialState { H = 0b00000000 }, new ExpectedState { Cycles = 8, H = 0b00000000, ZeroFlag = true });
            
            Add([Opcode.Pre_CB, CBOpcode.Swap_L], new InitialState { L = 0b00001111 }, new ExpectedState { Cycles = 8, L = 0b11110000, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Swap_L], new InitialState { L = 0b10101010 }, new ExpectedState { Cycles = 8, L = 0b10101010, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Swap_L], new InitialState { L = 0b11000011 }, new ExpectedState { Cycles = 8, L = 0b00111100, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Swap_L], new InitialState { L = 0b11111111 }, new ExpectedState { Cycles = 8, L = 0b11111111, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Swap_L], new InitialState { L = 0b00000000 }, new ExpectedState { Cycles = 8, L = 0b00000000, ZeroFlag = true });
            
            Add([Opcode.Pre_CB, CBOpcode.Swap_XHL], new InitialState { Memory = { [0xC000] = 0b00001111 }, HL = 0xC000 }, new ExpectedState { Cycles = 16, Memory = { [0xC000] = 0b11110000 }, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Swap_XHL], new InitialState { Memory = { [0xC000] = 0b10101010 }, HL = 0xC000 }, new ExpectedState { Cycles = 16, Memory = { [0xC000] = 0b10101010 }, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Swap_XHL], new InitialState { Memory = { [0xC000] = 0b11000011 }, HL = 0xC000 }, new ExpectedState { Cycles = 16, Memory = { [0xC000] = 0b00111100 }, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Swap_XHL], new InitialState { Memory = { [0xC000] = 0b11111111 }, HL = 0xC000 }, new ExpectedState { Cycles = 16, Memory = { [0xC000] = 0b11111111 }, ZeroFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Swap_XHL], new InitialState { Memory = { [0xC000] = 0b00000000 }, HL = 0xC000 }, new ExpectedState { Cycles = 16, Memory = { [0xC000] = 0b00000000 }, ZeroFlag = true });
        }
    }
}