using RetroEmu.Devices.DMG;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.GB.TestSetup;
using Xunit;

namespace RetroEmu.GB.Tests.IsolatedOperationTests;

public class CbSetTests
{
    private readonly IGameBoy _gameBoy = TestGameBoyBuilder.CreateBuilder().BuildGameBoy();

    [Theory]
    [ClassData(typeof(SetNr8TestData))]
    public void CBOperation_SetNr8_BitNIsSet(
        byte[] program, InitialState initialState, ExpectedState expectedState)
    {
        var cartridge = CartridgeBuilder.Create().WithProgram(program).Build();
        _gameBoy.Load(cartridge);
        _gameBoy.SetInitialState(initialState);

        var cycles = _gameBoy.Update();

        _gameBoy.AssertExpectedState(expectedState);
        Assert.Equal(expectedState.Cycles, cycles);
    }

    private class SetNr8TestData : TheoryData<byte[], InitialState, ExpectedState>
    {
        public SetNr8TestData()
        {
            // Non-zero Cases
            Add([Opcode.Pre_CB, CBOpcode.Set0_A], new InitialState { A = 0x01 }, new ExpectedState { Cycles = 8, A = 0x01 });
            Add([Opcode.Pre_CB, CBOpcode.Set1_A], new InitialState { A = 0x02 }, new ExpectedState { Cycles = 8, A = 0x02 });
            Add([Opcode.Pre_CB, CBOpcode.Set2_A], new InitialState { A = 0x04 }, new ExpectedState { Cycles = 8, A = 0x04 });
            Add([Opcode.Pre_CB, CBOpcode.Set3_A], new InitialState { A = 0x08 }, new ExpectedState { Cycles = 8, A = 0x08 });
            Add([Opcode.Pre_CB, CBOpcode.Set4_A], new InitialState { A = 0x10 }, new ExpectedState { Cycles = 8, A = 0x10 });
            Add([Opcode.Pre_CB, CBOpcode.Set5_A], new InitialState { A = 0x20 }, new ExpectedState { Cycles = 8, A = 0x20 });
            Add([Opcode.Pre_CB, CBOpcode.Set6_A], new InitialState { A = 0x40 }, new ExpectedState { Cycles = 8, A = 0x40 });
            Add([Opcode.Pre_CB, CBOpcode.Set7_A], new InitialState { A = 0x80 }, new ExpectedState { Cycles = 8, A = 0x80 });
                                                                                                                         
            Add([Opcode.Pre_CB, CBOpcode.Set0_B], new InitialState { B = 0x01 }, new ExpectedState { Cycles = 8, B = 0x01 });
            Add([Opcode.Pre_CB, CBOpcode.Set1_B], new InitialState { B = 0x02 }, new ExpectedState { Cycles = 8, B = 0x02 });
            Add([Opcode.Pre_CB, CBOpcode.Set2_B], new InitialState { B = 0x04 }, new ExpectedState { Cycles = 8, B = 0x04 });
            Add([Opcode.Pre_CB, CBOpcode.Set3_B], new InitialState { B = 0x08 }, new ExpectedState { Cycles = 8, B = 0x08 });
            Add([Opcode.Pre_CB, CBOpcode.Set4_B], new InitialState { B = 0x10 }, new ExpectedState { Cycles = 8, B = 0x10 });
            Add([Opcode.Pre_CB, CBOpcode.Set5_B], new InitialState { B = 0x20 }, new ExpectedState { Cycles = 8, B = 0x20 });
            Add([Opcode.Pre_CB, CBOpcode.Set6_B], new InitialState { B = 0x40 }, new ExpectedState { Cycles = 8, B = 0x40 });
            Add([Opcode.Pre_CB, CBOpcode.Set7_B], new InitialState { B = 0x80 }, new ExpectedState { Cycles = 8, B = 0x80 });
                                                                                                                         
            Add([Opcode.Pre_CB, CBOpcode.Set0_C], new InitialState { C = 0x01 }, new ExpectedState { Cycles = 8, C = 0x01 });
            Add([Opcode.Pre_CB, CBOpcode.Set1_C], new InitialState { C = 0x02 }, new ExpectedState { Cycles = 8, C = 0x02 });
            Add([Opcode.Pre_CB, CBOpcode.Set2_C], new InitialState { C = 0x04 }, new ExpectedState { Cycles = 8, C = 0x04 });
            Add([Opcode.Pre_CB, CBOpcode.Set3_C], new InitialState { C = 0x08 }, new ExpectedState { Cycles = 8, C = 0x08 });
            Add([Opcode.Pre_CB, CBOpcode.Set4_C], new InitialState { C = 0x10 }, new ExpectedState { Cycles = 8, C = 0x10 });
            Add([Opcode.Pre_CB, CBOpcode.Set5_C], new InitialState { C = 0x20 }, new ExpectedState { Cycles = 8, C = 0x20 });
            Add([Opcode.Pre_CB, CBOpcode.Set6_C], new InitialState { C = 0x40 }, new ExpectedState { Cycles = 8, C = 0x40 });
            Add([Opcode.Pre_CB, CBOpcode.Set7_C], new InitialState { C = 0x80 }, new ExpectedState { Cycles = 8, C = 0x80 });
                                                                                                                         
            Add([Opcode.Pre_CB, CBOpcode.Set0_D], new InitialState { D = 0x01 }, new ExpectedState { Cycles = 8, D = 0x01 });
            Add([Opcode.Pre_CB, CBOpcode.Set1_D], new InitialState { D = 0x02 }, new ExpectedState { Cycles = 8, D = 0x02 });
            Add([Opcode.Pre_CB, CBOpcode.Set2_D], new InitialState { D = 0x04 }, new ExpectedState { Cycles = 8, D = 0x04 });
            Add([Opcode.Pre_CB, CBOpcode.Set3_D], new InitialState { D = 0x08 }, new ExpectedState { Cycles = 8, D = 0x08 });
            Add([Opcode.Pre_CB, CBOpcode.Set4_D], new InitialState { D = 0x10 }, new ExpectedState { Cycles = 8, D = 0x10 });
            Add([Opcode.Pre_CB, CBOpcode.Set5_D], new InitialState { D = 0x20 }, new ExpectedState { Cycles = 8, D = 0x20 });
            Add([Opcode.Pre_CB, CBOpcode.Set6_D], new InitialState { D = 0x40 }, new ExpectedState { Cycles = 8, D = 0x40 });
            Add([Opcode.Pre_CB, CBOpcode.Set7_D], new InitialState { D = 0x80 }, new ExpectedState { Cycles = 8, D = 0x80 });
                                                                                                                         
            Add([Opcode.Pre_CB, CBOpcode.Set0_E], new InitialState { E = 0x01 }, new ExpectedState { Cycles = 8, E = 0x01 });
            Add([Opcode.Pre_CB, CBOpcode.Set1_E], new InitialState { E = 0x02 }, new ExpectedState { Cycles = 8, E = 0x02 });
            Add([Opcode.Pre_CB, CBOpcode.Set2_E], new InitialState { E = 0x04 }, new ExpectedState { Cycles = 8, E = 0x04 });
            Add([Opcode.Pre_CB, CBOpcode.Set3_E], new InitialState { E = 0x08 }, new ExpectedState { Cycles = 8, E = 0x08 });
            Add([Opcode.Pre_CB, CBOpcode.Set4_E], new InitialState { E = 0x10 }, new ExpectedState { Cycles = 8, E = 0x10 });
            Add([Opcode.Pre_CB, CBOpcode.Set5_E], new InitialState { E = 0x20 }, new ExpectedState { Cycles = 8, E = 0x20 });
            Add([Opcode.Pre_CB, CBOpcode.Set6_E], new InitialState { E = 0x40 }, new ExpectedState { Cycles = 8, E = 0x40 });
            Add([Opcode.Pre_CB, CBOpcode.Set7_E], new InitialState { E = 0x80 }, new ExpectedState { Cycles = 8, E = 0x80 });
                                                                                                                         
            Add([Opcode.Pre_CB, CBOpcode.Set0_H], new InitialState { H = 0x01 }, new ExpectedState { Cycles = 8, H = 0x01 });
            Add([Opcode.Pre_CB, CBOpcode.Set1_H], new InitialState { H = 0x02 }, new ExpectedState { Cycles = 8, H = 0x02 });
            Add([Opcode.Pre_CB, CBOpcode.Set2_H], new InitialState { H = 0x04 }, new ExpectedState { Cycles = 8, H = 0x04 });
            Add([Opcode.Pre_CB, CBOpcode.Set3_H], new InitialState { H = 0x08 }, new ExpectedState { Cycles = 8, H = 0x08 });
            Add([Opcode.Pre_CB, CBOpcode.Set4_H], new InitialState { H = 0x10 }, new ExpectedState { Cycles = 8, H = 0x10 });
            Add([Opcode.Pre_CB, CBOpcode.Set5_H], new InitialState { H = 0x20 }, new ExpectedState { Cycles = 8, H = 0x20 });
            Add([Opcode.Pre_CB, CBOpcode.Set6_H], new InitialState { H = 0x40 }, new ExpectedState { Cycles = 8, H = 0x40 });
            Add([Opcode.Pre_CB, CBOpcode.Set7_H], new InitialState { H = 0x80 }, new ExpectedState { Cycles = 8, H = 0x80 });
                                                                                                                         
            Add([Opcode.Pre_CB, CBOpcode.Set0_L], new InitialState { L = 0x01 }, new ExpectedState { Cycles = 8, L = 0x01 });
            Add([Opcode.Pre_CB, CBOpcode.Set1_L], new InitialState { L = 0x02 }, new ExpectedState { Cycles = 8, L = 0x02 });
            Add([Opcode.Pre_CB, CBOpcode.Set2_L], new InitialState { L = 0x04 }, new ExpectedState { Cycles = 8, L = 0x04 });
            Add([Opcode.Pre_CB, CBOpcode.Set3_L], new InitialState { L = 0x08 }, new ExpectedState { Cycles = 8, L = 0x08 });
            Add([Opcode.Pre_CB, CBOpcode.Set4_L], new InitialState { L = 0x10 }, new ExpectedState { Cycles = 8, L = 0x10 });
            Add([Opcode.Pre_CB, CBOpcode.Set5_L], new InitialState { L = 0x20 }, new ExpectedState { Cycles = 8, L = 0x20 });
            Add([Opcode.Pre_CB, CBOpcode.Set6_L], new InitialState { L = 0x40 }, new ExpectedState { Cycles = 8, L = 0x40 });
            Add([Opcode.Pre_CB, CBOpcode.Set7_L], new InitialState { L = 0x80 }, new ExpectedState { Cycles = 8, L = 0x80 });
            
            // Zero Cases
            Add([Opcode.Pre_CB, CBOpcode.Set0_A], new InitialState { A = 0x00 }, new ExpectedState { Cycles = 8, A = 0x01 });
            Add([Opcode.Pre_CB, CBOpcode.Set1_A], new InitialState { A = 0x00 }, new ExpectedState { Cycles = 8, A = 0x02 });
            Add([Opcode.Pre_CB, CBOpcode.Set2_A], new InitialState { A = 0x00 }, new ExpectedState { Cycles = 8, A = 0x04 });
            Add([Opcode.Pre_CB, CBOpcode.Set3_A], new InitialState { A = 0x00 }, new ExpectedState { Cycles = 8, A = 0x08 });
            Add([Opcode.Pre_CB, CBOpcode.Set4_A], new InitialState { A = 0x00 }, new ExpectedState { Cycles = 8, A = 0x10 });
            Add([Opcode.Pre_CB, CBOpcode.Set5_A], new InitialState { A = 0x00 }, new ExpectedState { Cycles = 8, A = 0x20 });
            Add([Opcode.Pre_CB, CBOpcode.Set6_A], new InitialState { A = 0x00 }, new ExpectedState { Cycles = 8, A = 0x40 });
            Add([Opcode.Pre_CB, CBOpcode.Set7_A], new InitialState { A = 0x00 }, new ExpectedState { Cycles = 8, A = 0x80 });
                                                                                                                         
            Add([Opcode.Pre_CB, CBOpcode.Set0_B], new InitialState { B = 0x00 }, new ExpectedState { Cycles = 8, B = 0x01 });
            Add([Opcode.Pre_CB, CBOpcode.Set1_B], new InitialState { B = 0x00 }, new ExpectedState { Cycles = 8, B = 0x02 });
            Add([Opcode.Pre_CB, CBOpcode.Set2_B], new InitialState { B = 0x00 }, new ExpectedState { Cycles = 8, B = 0x04 });
            Add([Opcode.Pre_CB, CBOpcode.Set3_B], new InitialState { B = 0x00 }, new ExpectedState { Cycles = 8, B = 0x08 });
            Add([Opcode.Pre_CB, CBOpcode.Set4_B], new InitialState { B = 0x00 }, new ExpectedState { Cycles = 8, B = 0x10 });
            Add([Opcode.Pre_CB, CBOpcode.Set5_B], new InitialState { B = 0x00 }, new ExpectedState { Cycles = 8, B = 0x20 });
            Add([Opcode.Pre_CB, CBOpcode.Set6_B], new InitialState { B = 0x00 }, new ExpectedState { Cycles = 8, B = 0x40 });
            Add([Opcode.Pre_CB, CBOpcode.Set7_B], new InitialState { B = 0x00 }, new ExpectedState { Cycles = 8, B = 0x80 });
                                                                                                                         
            Add([Opcode.Pre_CB, CBOpcode.Set0_C], new InitialState { C = 0x00 }, new ExpectedState { Cycles = 8, C = 0x01 });
            Add([Opcode.Pre_CB, CBOpcode.Set1_C], new InitialState { C = 0x00 }, new ExpectedState { Cycles = 8, C = 0x02 });
            Add([Opcode.Pre_CB, CBOpcode.Set2_C], new InitialState { C = 0x00 }, new ExpectedState { Cycles = 8, C = 0x04 });
            Add([Opcode.Pre_CB, CBOpcode.Set3_C], new InitialState { C = 0x00 }, new ExpectedState { Cycles = 8, C = 0x08 });
            Add([Opcode.Pre_CB, CBOpcode.Set4_C], new InitialState { C = 0x00 }, new ExpectedState { Cycles = 8, C = 0x10 });
            Add([Opcode.Pre_CB, CBOpcode.Set5_C], new InitialState { C = 0x00 }, new ExpectedState { Cycles = 8, C = 0x20 });
            Add([Opcode.Pre_CB, CBOpcode.Set6_C], new InitialState { C = 0x00 }, new ExpectedState { Cycles = 8, C = 0x40 });
            Add([Opcode.Pre_CB, CBOpcode.Set7_C], new InitialState { C = 0x00 }, new ExpectedState { Cycles = 8, C = 0x80 });
                                                                                                                         
            Add([Opcode.Pre_CB, CBOpcode.Set0_D], new InitialState { D = 0x00 }, new ExpectedState { Cycles = 8, D = 0x01 });
            Add([Opcode.Pre_CB, CBOpcode.Set1_D], new InitialState { D = 0x00 }, new ExpectedState { Cycles = 8, D = 0x02 });
            Add([Opcode.Pre_CB, CBOpcode.Set2_D], new InitialState { D = 0x00 }, new ExpectedState { Cycles = 8, D = 0x04 });
            Add([Opcode.Pre_CB, CBOpcode.Set3_D], new InitialState { D = 0x00 }, new ExpectedState { Cycles = 8, D = 0x08 });
            Add([Opcode.Pre_CB, CBOpcode.Set4_D], new InitialState { D = 0x00 }, new ExpectedState { Cycles = 8, D = 0x10 });
            Add([Opcode.Pre_CB, CBOpcode.Set5_D], new InitialState { D = 0x00 }, new ExpectedState { Cycles = 8, D = 0x20 });
            Add([Opcode.Pre_CB, CBOpcode.Set6_D], new InitialState { D = 0x00 }, new ExpectedState { Cycles = 8, D = 0x40 });
            Add([Opcode.Pre_CB, CBOpcode.Set7_D], new InitialState { D = 0x00 }, new ExpectedState { Cycles = 8, D = 0x80 });
                                                                                                                         
            Add([Opcode.Pre_CB, CBOpcode.Set0_E], new InitialState { E = 0x00 }, new ExpectedState { Cycles = 8, E = 0x01 });
            Add([Opcode.Pre_CB, CBOpcode.Set1_E], new InitialState { E = 0x00 }, new ExpectedState { Cycles = 8, E = 0x02 });
            Add([Opcode.Pre_CB, CBOpcode.Set2_E], new InitialState { E = 0x00 }, new ExpectedState { Cycles = 8, E = 0x04 });
            Add([Opcode.Pre_CB, CBOpcode.Set3_E], new InitialState { E = 0x00 }, new ExpectedState { Cycles = 8, E = 0x08 });
            Add([Opcode.Pre_CB, CBOpcode.Set4_E], new InitialState { E = 0x00 }, new ExpectedState { Cycles = 8, E = 0x10 });
            Add([Opcode.Pre_CB, CBOpcode.Set5_E], new InitialState { E = 0x00 }, new ExpectedState { Cycles = 8, E = 0x20 });
            Add([Opcode.Pre_CB, CBOpcode.Set6_E], new InitialState { E = 0x00 }, new ExpectedState { Cycles = 8, E = 0x40 });
            Add([Opcode.Pre_CB, CBOpcode.Set7_E], new InitialState { E = 0x00 }, new ExpectedState { Cycles = 8, E = 0x80 });
                                                                                                                         
            Add([Opcode.Pre_CB, CBOpcode.Set0_H], new InitialState { H = 0x00 }, new ExpectedState { Cycles = 8, H = 0x01 });
            Add([Opcode.Pre_CB, CBOpcode.Set1_H], new InitialState { H = 0x00 }, new ExpectedState { Cycles = 8, H = 0x02 });
            Add([Opcode.Pre_CB, CBOpcode.Set2_H], new InitialState { H = 0x00 }, new ExpectedState { Cycles = 8, H = 0x04 });
            Add([Opcode.Pre_CB, CBOpcode.Set3_H], new InitialState { H = 0x00 }, new ExpectedState { Cycles = 8, H = 0x08 });
            Add([Opcode.Pre_CB, CBOpcode.Set4_H], new InitialState { H = 0x00 }, new ExpectedState { Cycles = 8, H = 0x10 });
            Add([Opcode.Pre_CB, CBOpcode.Set5_H], new InitialState { H = 0x00 }, new ExpectedState { Cycles = 8, H = 0x20 });
            Add([Opcode.Pre_CB, CBOpcode.Set6_H], new InitialState { H = 0x00 }, new ExpectedState { Cycles = 8, H = 0x40 });
            Add([Opcode.Pre_CB, CBOpcode.Set7_H], new InitialState { H = 0x00 }, new ExpectedState { Cycles = 8, H = 0x80 });
                                                                                                                         
            Add([Opcode.Pre_CB, CBOpcode.Set0_L], new InitialState { L = 0x00 }, new ExpectedState { Cycles = 8, L = 0x01 });
            Add([Opcode.Pre_CB, CBOpcode.Set1_L], new InitialState { L = 0x00 }, new ExpectedState { Cycles = 8, L = 0x02 });
            Add([Opcode.Pre_CB, CBOpcode.Set2_L], new InitialState { L = 0x00 }, new ExpectedState { Cycles = 8, L = 0x04 });
            Add([Opcode.Pre_CB, CBOpcode.Set3_L], new InitialState { L = 0x00 }, new ExpectedState { Cycles = 8, L = 0x08 });
            Add([Opcode.Pre_CB, CBOpcode.Set4_L], new InitialState { L = 0x00 }, new ExpectedState { Cycles = 8, L = 0x10 });
            Add([Opcode.Pre_CB, CBOpcode.Set5_L], new InitialState { L = 0x00 }, new ExpectedState { Cycles = 8, L = 0x20 });
            Add([Opcode.Pre_CB, CBOpcode.Set6_L], new InitialState { L = 0x00 }, new ExpectedState { Cycles = 8, L = 0x40 });
            Add([Opcode.Pre_CB, CBOpcode.Set7_L], new InitialState { L = 0x00 }, new ExpectedState { Cycles = 8, L = 0x80 });
        }
    }
    
    [Theory]
    [ClassData(typeof(SetNXHLTestData))]
    public void CBOperation_SetNXHL_BitNOfXHLIsSet(
        byte[] program, InitialState initialState, ExpectedState expectedState)
    {
        var cartridge = CartridgeBuilder.Create().WithProgram(program).Build();
        _gameBoy.Load(cartridge);
        _gameBoy.SetInitialState(initialState);

        var cycles = _gameBoy.Update();

        _gameBoy.AssertExpectedState(expectedState);
        Assert.Equal(expectedState.Cycles, cycles);
    }

    private class SetNXHLTestData : TheoryData<byte[], InitialState, ExpectedState>
    {
        public SetNXHLTestData()
        {
            // Non-Zero Cases
            Add([Opcode.Pre_CB, CBOpcode.Set0_XHL], new InitialState { HL = 0xC000, Memory = { [0xC000] = 0x01 } }, new ExpectedState { Cycles = 16, Memory = { [0xC000] = 0x01 } });
            Add([Opcode.Pre_CB, CBOpcode.Set1_XHL], new InitialState { HL = 0xC000, Memory = { [0xC000] = 0x02 } }, new ExpectedState { Cycles = 16, Memory = { [0xC000] = 0x02 } });
            Add([Opcode.Pre_CB, CBOpcode.Set2_XHL], new InitialState { HL = 0xC000, Memory = { [0xC000] = 0x04 } }, new ExpectedState { Cycles = 16, Memory = { [0xC000] = 0x04 } });
            Add([Opcode.Pre_CB, CBOpcode.Set3_XHL], new InitialState { HL = 0xC000, Memory = { [0xC000] = 0x08 } }, new ExpectedState { Cycles = 16, Memory = { [0xC000] = 0x08 } });
            Add([Opcode.Pre_CB, CBOpcode.Set4_XHL], new InitialState { HL = 0xC000, Memory = { [0xC000] = 0x10 } }, new ExpectedState { Cycles = 16, Memory = { [0xC000] = 0x10 } });
            Add([Opcode.Pre_CB, CBOpcode.Set5_XHL], new InitialState { HL = 0xC000, Memory = { [0xC000] = 0x20 } }, new ExpectedState { Cycles = 16, Memory = { [0xC000] = 0x20 } });
            Add([Opcode.Pre_CB, CBOpcode.Set6_XHL], new InitialState { HL = 0xC000, Memory = { [0xC000] = 0x40 } }, new ExpectedState { Cycles = 16, Memory = { [0xC000] = 0x40 } });
            Add([Opcode.Pre_CB, CBOpcode.Set7_XHL], new InitialState { HL = 0xC000, Memory = { [0xC000] = 0x80 } }, new ExpectedState { Cycles = 16, Memory = { [0xC000] = 0x80 } });
            
            // Zero Cases
            Add([Opcode.Pre_CB, CBOpcode.Set0_XHL], new InitialState { HL = 0xC000, Memory = { [0xC000] = 0x00 } }, new ExpectedState { Cycles = 16, Memory = { [0xC000] = 0x01 } });
            Add([Opcode.Pre_CB, CBOpcode.Set1_XHL], new InitialState { HL = 0xC000, Memory = { [0xC000] = 0x00 } }, new ExpectedState { Cycles = 16, Memory = { [0xC000] = 0x02 } });
            Add([Opcode.Pre_CB, CBOpcode.Set2_XHL], new InitialState { HL = 0xC000, Memory = { [0xC000] = 0x00 } }, new ExpectedState { Cycles = 16, Memory = { [0xC000] = 0x04 } });
            Add([Opcode.Pre_CB, CBOpcode.Set3_XHL], new InitialState { HL = 0xC000, Memory = { [0xC000] = 0x00 } }, new ExpectedState { Cycles = 16, Memory = { [0xC000] = 0x08 } });
            Add([Opcode.Pre_CB, CBOpcode.Set4_XHL], new InitialState { HL = 0xC000, Memory = { [0xC000] = 0x00 } }, new ExpectedState { Cycles = 16, Memory = { [0xC000] = 0x10 } });
            Add([Opcode.Pre_CB, CBOpcode.Set5_XHL], new InitialState { HL = 0xC000, Memory = { [0xC000] = 0x00 } }, new ExpectedState { Cycles = 16, Memory = { [0xC000] = 0x20 } });
            Add([Opcode.Pre_CB, CBOpcode.Set6_XHL], new InitialState { HL = 0xC000, Memory = { [0xC000] = 0x00 } }, new ExpectedState { Cycles = 16, Memory = { [0xC000] = 0x40 } });
            Add([Opcode.Pre_CB, CBOpcode.Set7_XHL], new InitialState { HL = 0xC000, Memory = { [0xC000] = 0x00 } }, new ExpectedState { Cycles = 16, Memory = { [0xC000] = 0x80 } });
        }
    }
}