using RetroEmu.Devices.DMG;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.GB.TestSetup;
using Xunit;

namespace RetroEmu.GB.Tests.IsolatedOperationTests;

public class CbBitTests
{
    private readonly IGameBoy _gameBoy = TestGameBoyBuilder.CreateBuilder().BuildGameBoy();

    [Theory]
    [ClassData(typeof(BitNr8TestData))]
    public void CBOperation_BitNr8_BitNIsToggledAndZeroFlagIsSetCorrectly(
        byte[] program, InitialState initialState, ExpectedState expectedState)
    {
        var cartridge = CartridgeBuilder.Create().WithProgram(program).Build();
        _gameBoy.Load(cartridge);
        _gameBoy.SetInitialState(initialState);

        var cycles = _gameBoy.Update();

        _gameBoy.AssertExpectedState(expectedState);
        Assert.Equal(expectedState.Cycles, cycles);
    }

    private class BitNr8TestData : TheoryData<byte[], InitialState, ExpectedState>
    {
        public BitNr8TestData()
        {
            // Non-zero Cases
            Add([Opcode.Pre_CB, CBOpcode.Bit0_A], new InitialState { A = 0b11111111 }, new ExpectedState { Cycles = 8, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit1_A], new InitialState { A = 0b11111111 }, new ExpectedState { Cycles = 8, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit2_A], new InitialState { A = 0b11111111 }, new ExpectedState { Cycles = 8, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit3_A], new InitialState { A = 0b11111111 }, new ExpectedState { Cycles = 8, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit4_A], new InitialState { A = 0b11111111 }, new ExpectedState { Cycles = 8, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit5_A], new InitialState { A = 0b11111111 }, new ExpectedState { Cycles = 8, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit6_A], new InitialState { A = 0b11111111 }, new ExpectedState { Cycles = 8, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit7_A], new InitialState { A = 0b11111111 }, new ExpectedState { Cycles = 8, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            
            Add([Opcode.Pre_CB, CBOpcode.Bit0_B], new InitialState { B = 0b11111111 }, new ExpectedState { Cycles = 8, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit1_B], new InitialState { B = 0b11111111 }, new ExpectedState { Cycles = 8, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit2_B], new InitialState { B = 0b11111111 }, new ExpectedState { Cycles = 8, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit3_B], new InitialState { B = 0b11111111 }, new ExpectedState { Cycles = 8, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit4_B], new InitialState { B = 0b11111111 }, new ExpectedState { Cycles = 8, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit5_B], new InitialState { B = 0b11111111 }, new ExpectedState { Cycles = 8, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit6_B], new InitialState { B = 0b11111111 }, new ExpectedState { Cycles = 8, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit7_B], new InitialState { B = 0b11111111 }, new ExpectedState { Cycles = 8, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            
            Add([Opcode.Pre_CB, CBOpcode.Bit0_C], new InitialState { C = 0b11111111 }, new ExpectedState { Cycles = 8, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit1_C], new InitialState { C = 0b11111111 }, new ExpectedState { Cycles = 8, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit2_C], new InitialState { C = 0b11111111 }, new ExpectedState { Cycles = 8, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit3_C], new InitialState { C = 0b11111111 }, new ExpectedState { Cycles = 8, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit4_C], new InitialState { C = 0b11111111 }, new ExpectedState { Cycles = 8, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit5_C], new InitialState { C = 0b11111111 }, new ExpectedState { Cycles = 8, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit6_C], new InitialState { C = 0b11111111 }, new ExpectedState { Cycles = 8, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit7_C], new InitialState { C = 0b11111111 }, new ExpectedState { Cycles = 8, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            
            Add([Opcode.Pre_CB, CBOpcode.Bit0_D], new InitialState { D = 0b11111111 }, new ExpectedState { Cycles = 8, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit1_D], new InitialState { D = 0b11111111 }, new ExpectedState { Cycles = 8, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit2_D], new InitialState { D = 0b11111111 }, new ExpectedState { Cycles = 8, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit3_D], new InitialState { D = 0b11111111 }, new ExpectedState { Cycles = 8, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit4_D], new InitialState { D = 0b11111111 }, new ExpectedState { Cycles = 8, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit5_D], new InitialState { D = 0b11111111 }, new ExpectedState { Cycles = 8, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit6_D], new InitialState { D = 0b11111111 }, new ExpectedState { Cycles = 8, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit7_D], new InitialState { D = 0b11111111 }, new ExpectedState { Cycles = 8, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            
            Add([Opcode.Pre_CB, CBOpcode.Bit0_E], new InitialState { E = 0b11111111 }, new ExpectedState { Cycles = 8, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit1_E], new InitialState { E = 0b11111111 }, new ExpectedState { Cycles = 8, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit2_E], new InitialState { E = 0b11111111 }, new ExpectedState { Cycles = 8, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit3_E], new InitialState { E = 0b11111111 }, new ExpectedState { Cycles = 8, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit4_E], new InitialState { E = 0b11111111 }, new ExpectedState { Cycles = 8, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit5_E], new InitialState { E = 0b11111111 }, new ExpectedState { Cycles = 8, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit6_E], new InitialState { E = 0b11111111 }, new ExpectedState { Cycles = 8, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit7_E], new InitialState { E = 0b11111111 }, new ExpectedState { Cycles = 8, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            
            Add([Opcode.Pre_CB, CBOpcode.Bit0_H], new InitialState { H = 0b11111111 }, new ExpectedState { Cycles = 8, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit1_H], new InitialState { H = 0b11111111 }, new ExpectedState { Cycles = 8, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit2_H], new InitialState { H = 0b11111111 }, new ExpectedState { Cycles = 8, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit3_H], new InitialState { H = 0b11111111 }, new ExpectedState { Cycles = 8, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit4_H], new InitialState { H = 0b11111111 }, new ExpectedState { Cycles = 8, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit5_H], new InitialState { H = 0b11111111 }, new ExpectedState { Cycles = 8, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit6_H], new InitialState { H = 0b11111111 }, new ExpectedState { Cycles = 8, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit7_H], new InitialState { H = 0b11111111 }, new ExpectedState { Cycles = 8, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            
            Add([Opcode.Pre_CB, CBOpcode.Bit0_L], new InitialState { L = 0b11111111 }, new ExpectedState { Cycles = 8, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit1_L], new InitialState { L = 0b11111111 }, new ExpectedState { Cycles = 8, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit2_L], new InitialState { L = 0b11111111 }, new ExpectedState { Cycles = 8, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit3_L], new InitialState { L = 0b11111111 }, new ExpectedState { Cycles = 8, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit4_L], new InitialState { L = 0b11111111 }, new ExpectedState { Cycles = 8, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit5_L], new InitialState { L = 0b11111111 }, new ExpectedState { Cycles = 8, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit6_L], new InitialState { L = 0b11111111 }, new ExpectedState { Cycles = 8, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit7_L], new InitialState { L = 0b11111111 }, new ExpectedState { Cycles = 8, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            
            // Zero Cases
            Add([Opcode.Pre_CB, CBOpcode.Bit0_A], new InitialState { A = 0b00000000 }, new ExpectedState { Cycles = 8, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit1_A], new InitialState { A = 0b00000000 }, new ExpectedState { Cycles = 8, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit2_A], new InitialState { A = 0b00000000 }, new ExpectedState { Cycles = 8, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit3_A], new InitialState { A = 0b00000000 }, new ExpectedState { Cycles = 8, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit4_A], new InitialState { A = 0b00000000 }, new ExpectedState { Cycles = 8, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit5_A], new InitialState { A = 0b00000000 }, new ExpectedState { Cycles = 8, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit6_A], new InitialState { A = 0b00000000 }, new ExpectedState { Cycles = 8, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit7_A], new InitialState { A = 0b00000000 }, new ExpectedState { Cycles = 8, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            
            Add([Opcode.Pre_CB, CBOpcode.Bit0_B], new InitialState { B = 0b00000000 }, new ExpectedState { Cycles = 8, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit1_B], new InitialState { B = 0b00000000 }, new ExpectedState { Cycles = 8, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit2_B], new InitialState { B = 0b00000000 }, new ExpectedState { Cycles = 8, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit3_B], new InitialState { B = 0b00000000 }, new ExpectedState { Cycles = 8, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit4_B], new InitialState { B = 0b00000000 }, new ExpectedState { Cycles = 8, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit5_B], new InitialState { B = 0b00000000 }, new ExpectedState { Cycles = 8, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit6_B], new InitialState { B = 0b00000000 }, new ExpectedState { Cycles = 8, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit7_B], new InitialState { B = 0b00000000 }, new ExpectedState { Cycles = 8, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            
            Add([Opcode.Pre_CB, CBOpcode.Bit0_C], new InitialState { C = 0b00000000 }, new ExpectedState { Cycles = 8, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit1_C], new InitialState { C = 0b00000000 }, new ExpectedState { Cycles = 8, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit2_C], new InitialState { C = 0b00000000 }, new ExpectedState { Cycles = 8, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit3_C], new InitialState { C = 0b00000000 }, new ExpectedState { Cycles = 8, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit4_C], new InitialState { C = 0b00000000 }, new ExpectedState { Cycles = 8, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit5_C], new InitialState { C = 0b00000000 }, new ExpectedState { Cycles = 8, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit6_C], new InitialState { C = 0b00000000 }, new ExpectedState { Cycles = 8, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit7_C], new InitialState { C = 0b00000000 }, new ExpectedState { Cycles = 8, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            
            Add([Opcode.Pre_CB, CBOpcode.Bit0_D], new InitialState { D = 0b00000000 }, new ExpectedState { Cycles = 8, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit1_D], new InitialState { D = 0b00000000 }, new ExpectedState { Cycles = 8, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit2_D], new InitialState { D = 0b00000000 }, new ExpectedState { Cycles = 8, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit3_D], new InitialState { D = 0b00000000 }, new ExpectedState { Cycles = 8, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit4_D], new InitialState { D = 0b00000000 }, new ExpectedState { Cycles = 8, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit5_D], new InitialState { D = 0b00000000 }, new ExpectedState { Cycles = 8, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit6_D], new InitialState { D = 0b00000000 }, new ExpectedState { Cycles = 8, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit7_D], new InitialState { D = 0b00000000 }, new ExpectedState { Cycles = 8, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            
            Add([Opcode.Pre_CB, CBOpcode.Bit0_E], new InitialState { E = 0b00000000 }, new ExpectedState { Cycles = 8, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit1_E], new InitialState { E = 0b00000000 }, new ExpectedState { Cycles = 8, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit2_E], new InitialState { E = 0b00000000 }, new ExpectedState { Cycles = 8, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit3_E], new InitialState { E = 0b00000000 }, new ExpectedState { Cycles = 8, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit4_E], new InitialState { E = 0b00000000 }, new ExpectedState { Cycles = 8, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit5_E], new InitialState { E = 0b00000000 }, new ExpectedState { Cycles = 8, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit6_E], new InitialState { E = 0b00000000 }, new ExpectedState { Cycles = 8, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit7_E], new InitialState { E = 0b00000000 }, new ExpectedState { Cycles = 8, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            
            Add([Opcode.Pre_CB, CBOpcode.Bit0_H], new InitialState { H = 0b00000000 }, new ExpectedState { Cycles = 8, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit1_H], new InitialState { H = 0b00000000 }, new ExpectedState { Cycles = 8, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit2_H], new InitialState { H = 0b00000000 }, new ExpectedState { Cycles = 8, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit3_H], new InitialState { H = 0b00000000 }, new ExpectedState { Cycles = 8, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit4_H], new InitialState { H = 0b00000000 }, new ExpectedState { Cycles = 8, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit5_H], new InitialState { H = 0b00000000 }, new ExpectedState { Cycles = 8, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit6_H], new InitialState { H = 0b00000000 }, new ExpectedState { Cycles = 8, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit7_H], new InitialState { H = 0b00000000 }, new ExpectedState { Cycles = 8, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            
            Add([Opcode.Pre_CB, CBOpcode.Bit0_L], new InitialState { L = 0b00000000 }, new ExpectedState { Cycles = 8, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit1_L], new InitialState { L = 0b00000000 }, new ExpectedState { Cycles = 8, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit2_L], new InitialState { L = 0b00000000 }, new ExpectedState { Cycles = 8, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit3_L], new InitialState { L = 0b00000000 }, new ExpectedState { Cycles = 8, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit4_L], new InitialState { L = 0b00000000 }, new ExpectedState { Cycles = 8, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit5_L], new InitialState { L = 0b00000000 }, new ExpectedState { Cycles = 8, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit6_L], new InitialState { L = 0b00000000 }, new ExpectedState { Cycles = 8, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit7_L], new InitialState { L = 0b00000000 }, new ExpectedState { Cycles = 8, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
        }
    }
    
    [Theory]
    [ClassData(typeof(BitNXHLTestData))]
    public void CBOperation_BitNXHL_BitNOfXHLIsToggledAndZeroFlagIsSetCorrectly(
        byte[] program, InitialState initialState, ExpectedState expectedState)
    {
        var cartridge = CartridgeBuilder.Create().WithProgram(program).Build();
        _gameBoy.Load(cartridge);
        _gameBoy.SetInitialState(initialState);

        var cycles = _gameBoy.Update();

        _gameBoy.AssertExpectedState(expectedState);
        Assert.Equal(expectedState.Cycles, cycles);
    }

    private class BitNXHLTestData : TheoryData<byte[], InitialState, ExpectedState>
    {
        public BitNXHLTestData()
        {
            // Non-Zero Cases
            Add([Opcode.Pre_CB, CBOpcode.Bit0_XHL], new InitialState { HL = 0xC000, Memory = { [0xC000] = 0b11111111 } }, new ExpectedState { Cycles = 16, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit1_XHL], new InitialState { HL = 0xC000, Memory = { [0xC000] = 0b11111111 } }, new ExpectedState { Cycles = 16, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit2_XHL], new InitialState { HL = 0xC000, Memory = { [0xC000] = 0b11111111 } }, new ExpectedState { Cycles = 16, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit3_XHL], new InitialState { HL = 0xC000, Memory = { [0xC000] = 0b11111111 } }, new ExpectedState { Cycles = 16, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit4_XHL], new InitialState { HL = 0xC000, Memory = { [0xC000] = 0b11111111 } }, new ExpectedState { Cycles = 16, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit5_XHL], new InitialState { HL = 0xC000, Memory = { [0xC000] = 0b11111111 } }, new ExpectedState { Cycles = 16, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit6_XHL], new InitialState { HL = 0xC000, Memory = { [0xC000] = 0b11111111 } }, new ExpectedState { Cycles = 16, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit7_XHL], new InitialState { HL = 0xC000, Memory = { [0xC000] = 0b11111111 } }, new ExpectedState { Cycles = 16, ZeroFlag = false, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            
            // Zero Cases
            Add([Opcode.Pre_CB, CBOpcode.Bit0_XHL], new InitialState { HL = 0xC000, Memory = { [0xC000] = 0b00000000 } }, new ExpectedState { Cycles = 16, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit1_XHL], new InitialState { HL = 0xC000, Memory = { [0xC000] = 0b00000000 } }, new ExpectedState { Cycles = 16, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit2_XHL], new InitialState { HL = 0xC000, Memory = { [0xC000] = 0b00000000 } }, new ExpectedState { Cycles = 16, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit3_XHL], new InitialState { HL = 0xC000, Memory = { [0xC000] = 0b00000000 } }, new ExpectedState { Cycles = 16, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit4_XHL], new InitialState { HL = 0xC000, Memory = { [0xC000] = 0b00000000 } }, new ExpectedState { Cycles = 16, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit5_XHL], new InitialState { HL = 0xC000, Memory = { [0xC000] = 0b00000000 } }, new ExpectedState { Cycles = 16, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit6_XHL], new InitialState { HL = 0xC000, Memory = { [0xC000] = 0b00000000 } }, new ExpectedState { Cycles = 16, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
            Add([Opcode.Pre_CB, CBOpcode.Bit7_XHL], new InitialState { HL = 0xC000, Memory = { [0xC000] = 0b00000000 } }, new ExpectedState { Cycles = 16, ZeroFlag = true, CarryFlag = false, HalfCarryFlag = true, SubtractFlag = false });
        }
    }
}