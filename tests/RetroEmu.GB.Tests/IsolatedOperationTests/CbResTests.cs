using RetroEmu.Devices.DMG;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.GB.TestSetup;
using Xunit;

namespace RetroEmu.GB.Tests.IsolatedOperationTests;

public class CbResTests
{
    private readonly IGameBoy _gameBoy = TestGameBoyBuilder.CreateBuilder().BuildGameBoy();

    [Theory]
    [ClassData(typeof(ResNr8TestData))]
    public void CBOperation_ResNr8_BitNIsReset(
        byte[] program, InitialState initialState, ExpectedState expectedState)
    {
        var cartridge = CartridgeBuilder.Create().WithProgram(program).Build();
        _gameBoy.Load(cartridge);
        _gameBoy.SetInitialState(initialState);

        var cycles = _gameBoy.Update();

        _gameBoy.AssertExpectedState(expectedState);
        Assert.Equal(expectedState.Cycles, cycles);
    }

    private class ResNr8TestData : TheoryData<byte[], InitialState, ExpectedState>
    {
        public ResNr8TestData()
        {
            // Non-zero Cases
            Add([Opcode.Pre_CB, CBOpcode.Res0_A], new InitialState { A = 0x01 }, new ExpectedState { Cycles = 8, A = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res1_A], new InitialState { A = 0x02 }, new ExpectedState { Cycles = 8, A = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res2_A], new InitialState { A = 0x04 }, new ExpectedState { Cycles = 8, A = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res3_A], new InitialState { A = 0x08 }, new ExpectedState { Cycles = 8, A = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res4_A], new InitialState { A = 0x10 }, new ExpectedState { Cycles = 8, A = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res5_A], new InitialState { A = 0x20 }, new ExpectedState { Cycles = 8, A = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res6_A], new InitialState { A = 0x40 }, new ExpectedState { Cycles = 8, A = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res7_A], new InitialState { A = 0x80 }, new ExpectedState { Cycles = 8, A = 0x00 });
            
            Add([Opcode.Pre_CB, CBOpcode.Res0_B], new InitialState { B = 0x01 }, new ExpectedState { Cycles = 8, B = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res1_B], new InitialState { B = 0x02 }, new ExpectedState { Cycles = 8, B = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res2_B], new InitialState { B = 0x04 }, new ExpectedState { Cycles = 8, B = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res3_B], new InitialState { B = 0x08 }, new ExpectedState { Cycles = 8, B = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res4_B], new InitialState { B = 0x10 }, new ExpectedState { Cycles = 8, B = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res5_B], new InitialState { B = 0x20 }, new ExpectedState { Cycles = 8, B = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res6_B], new InitialState { B = 0x40 }, new ExpectedState { Cycles = 8, B = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res7_B], new InitialState { B = 0x80 }, new ExpectedState { Cycles = 8, B = 0x00 });
            
            Add([Opcode.Pre_CB, CBOpcode.Res0_C], new InitialState { C = 0x01 }, new ExpectedState { Cycles = 8, C = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res1_C], new InitialState { C = 0x02 }, new ExpectedState { Cycles = 8, C = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res2_C], new InitialState { C = 0x04 }, new ExpectedState { Cycles = 8, C = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res3_C], new InitialState { C = 0x08 }, new ExpectedState { Cycles = 8, C = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res4_C], new InitialState { C = 0x10 }, new ExpectedState { Cycles = 8, C = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res5_C], new InitialState { C = 0x20 }, new ExpectedState { Cycles = 8, C = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res6_C], new InitialState { C = 0x40 }, new ExpectedState { Cycles = 8, C = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res7_C], new InitialState { C = 0x80 }, new ExpectedState { Cycles = 8, C = 0x00 });
            
            Add([Opcode.Pre_CB, CBOpcode.Res0_D], new InitialState { D = 0x01 }, new ExpectedState { Cycles = 8, D = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res1_D], new InitialState { D = 0x02 }, new ExpectedState { Cycles = 8, D = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res2_D], new InitialState { D = 0x04 }, new ExpectedState { Cycles = 8, D = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res3_D], new InitialState { D = 0x08 }, new ExpectedState { Cycles = 8, D = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res4_D], new InitialState { D = 0x10 }, new ExpectedState { Cycles = 8, D = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res5_D], new InitialState { D = 0x20 }, new ExpectedState { Cycles = 8, D = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res6_D], new InitialState { D = 0x40 }, new ExpectedState { Cycles = 8, D = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res7_D], new InitialState { D = 0x80 }, new ExpectedState { Cycles = 8, D = 0x00 });
            
            Add([Opcode.Pre_CB, CBOpcode.Res0_E], new InitialState { E = 0x01 }, new ExpectedState { Cycles = 8, E = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res1_E], new InitialState { E = 0x02 }, new ExpectedState { Cycles = 8, E = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res2_E], new InitialState { E = 0x04 }, new ExpectedState { Cycles = 8, E = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res3_E], new InitialState { E = 0x08 }, new ExpectedState { Cycles = 8, E = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res4_E], new InitialState { E = 0x10 }, new ExpectedState { Cycles = 8, E = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res5_E], new InitialState { E = 0x20 }, new ExpectedState { Cycles = 8, E = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res6_E], new InitialState { E = 0x40 }, new ExpectedState { Cycles = 8, E = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res7_E], new InitialState { E = 0x80 }, new ExpectedState { Cycles = 8, E = 0x00 });
            
            Add([Opcode.Pre_CB, CBOpcode.Res0_H], new InitialState { H = 0x01 }, new ExpectedState { Cycles = 8, H = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res1_H], new InitialState { H = 0x02 }, new ExpectedState { Cycles = 8, H = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res2_H], new InitialState { H = 0x04 }, new ExpectedState { Cycles = 8, H = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res3_H], new InitialState { H = 0x08 }, new ExpectedState { Cycles = 8, H = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res4_H], new InitialState { H = 0x10 }, new ExpectedState { Cycles = 8, H = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res5_H], new InitialState { H = 0x20 }, new ExpectedState { Cycles = 8, H = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res6_H], new InitialState { H = 0x40 }, new ExpectedState { Cycles = 8, H = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res7_H], new InitialState { H = 0x80 }, new ExpectedState { Cycles = 8, H = 0x00 });
            
            Add([Opcode.Pre_CB, CBOpcode.Res0_L], new InitialState { L = 0x01 }, new ExpectedState { Cycles = 8, L = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res1_L], new InitialState { L = 0x02 }, new ExpectedState { Cycles = 8, L = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res2_L], new InitialState { L = 0x04 }, new ExpectedState { Cycles = 8, L = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res3_L], new InitialState { L = 0x08 }, new ExpectedState { Cycles = 8, L = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res4_L], new InitialState { L = 0x10 }, new ExpectedState { Cycles = 8, L = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res5_L], new InitialState { L = 0x20 }, new ExpectedState { Cycles = 8, L = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res6_L], new InitialState { L = 0x40 }, new ExpectedState { Cycles = 8, L = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res7_L], new InitialState { L = 0x80 }, new ExpectedState { Cycles = 8, L = 0x00 });
            
            // Zero Cases
            Add([Opcode.Pre_CB, CBOpcode.Res0_A], new InitialState { A = 0x00 }, new ExpectedState { Cycles = 8, A = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res1_A], new InitialState { A = 0x00 }, new ExpectedState { Cycles = 8, A = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res2_A], new InitialState { A = 0x00 }, new ExpectedState { Cycles = 8, A = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res3_A], new InitialState { A = 0x00 }, new ExpectedState { Cycles = 8, A = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res4_A], new InitialState { A = 0x00 }, new ExpectedState { Cycles = 8, A = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res5_A], new InitialState { A = 0x00 }, new ExpectedState { Cycles = 8, A = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res6_A], new InitialState { A = 0x00 }, new ExpectedState { Cycles = 8, A = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res7_A], new InitialState { A = 0x00 }, new ExpectedState { Cycles = 8, A = 0x00 });
            
            Add([Opcode.Pre_CB, CBOpcode.Res0_B], new InitialState { B = 0x00 }, new ExpectedState { Cycles = 8, B = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res1_B], new InitialState { B = 0x00 }, new ExpectedState { Cycles = 8, B = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res2_B], new InitialState { B = 0x00 }, new ExpectedState { Cycles = 8, B = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res3_B], new InitialState { B = 0x00 }, new ExpectedState { Cycles = 8, B = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res4_B], new InitialState { B = 0x00 }, new ExpectedState { Cycles = 8, B = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res5_B], new InitialState { B = 0x00 }, new ExpectedState { Cycles = 8, B = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res6_B], new InitialState { B = 0x00 }, new ExpectedState { Cycles = 8, B = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res7_B], new InitialState { B = 0x00 }, new ExpectedState { Cycles = 8, B = 0x00 });
            
            Add([Opcode.Pre_CB, CBOpcode.Res0_C], new InitialState { C = 0x00 }, new ExpectedState { Cycles = 8, C = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res1_C], new InitialState { C = 0x00 }, new ExpectedState { Cycles = 8, C = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res2_C], new InitialState { C = 0x00 }, new ExpectedState { Cycles = 8, C = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res3_C], new InitialState { C = 0x00 }, new ExpectedState { Cycles = 8, C = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res4_C], new InitialState { C = 0x00 }, new ExpectedState { Cycles = 8, C = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res5_C], new InitialState { C = 0x00 }, new ExpectedState { Cycles = 8, C = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res6_C], new InitialState { C = 0x00 }, new ExpectedState { Cycles = 8, C = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res7_C], new InitialState { C = 0x00 }, new ExpectedState { Cycles = 8, C = 0x00 });
            
            Add([Opcode.Pre_CB, CBOpcode.Res0_D], new InitialState { D = 0x00 }, new ExpectedState { Cycles = 8, D = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res1_D], new InitialState { D = 0x00 }, new ExpectedState { Cycles = 8, D = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res2_D], new InitialState { D = 0x00 }, new ExpectedState { Cycles = 8, D = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res3_D], new InitialState { D = 0x00 }, new ExpectedState { Cycles = 8, D = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res4_D], new InitialState { D = 0x00 }, new ExpectedState { Cycles = 8, D = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res5_D], new InitialState { D = 0x00 }, new ExpectedState { Cycles = 8, D = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res6_D], new InitialState { D = 0x00 }, new ExpectedState { Cycles = 8, D = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res7_D], new InitialState { D = 0x00 }, new ExpectedState { Cycles = 8, D = 0x00 });
            
            Add([Opcode.Pre_CB, CBOpcode.Res0_E], new InitialState { E = 0x00 }, new ExpectedState { Cycles = 8, E = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res1_E], new InitialState { E = 0x00 }, new ExpectedState { Cycles = 8, E = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res2_E], new InitialState { E = 0x00 }, new ExpectedState { Cycles = 8, E = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res3_E], new InitialState { E = 0x00 }, new ExpectedState { Cycles = 8, E = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res4_E], new InitialState { E = 0x00 }, new ExpectedState { Cycles = 8, E = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res5_E], new InitialState { E = 0x00 }, new ExpectedState { Cycles = 8, E = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res6_E], new InitialState { E = 0x00 }, new ExpectedState { Cycles = 8, E = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res7_E], new InitialState { E = 0x00 }, new ExpectedState { Cycles = 8, E = 0x00 });
            
            Add([Opcode.Pre_CB, CBOpcode.Res0_H], new InitialState { H = 0x00 }, new ExpectedState { Cycles = 8, H = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res1_H], new InitialState { H = 0x00 }, new ExpectedState { Cycles = 8, H = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res2_H], new InitialState { H = 0x00 }, new ExpectedState { Cycles = 8, H = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res3_H], new InitialState { H = 0x00 }, new ExpectedState { Cycles = 8, H = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res4_H], new InitialState { H = 0x00 }, new ExpectedState { Cycles = 8, H = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res5_H], new InitialState { H = 0x00 }, new ExpectedState { Cycles = 8, H = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res6_H], new InitialState { H = 0x00 }, new ExpectedState { Cycles = 8, H = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res7_H], new InitialState { H = 0x00 }, new ExpectedState { Cycles = 8, H = 0x00 });
            
            Add([Opcode.Pre_CB, CBOpcode.Res0_L], new InitialState { L = 0x00 }, new ExpectedState { Cycles = 8, L = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res1_L], new InitialState { L = 0x00 }, new ExpectedState { Cycles = 8, L = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res2_L], new InitialState { L = 0x00 }, new ExpectedState { Cycles = 8, L = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res3_L], new InitialState { L = 0x00 }, new ExpectedState { Cycles = 8, L = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res4_L], new InitialState { L = 0x00 }, new ExpectedState { Cycles = 8, L = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res5_L], new InitialState { L = 0x00 }, new ExpectedState { Cycles = 8, L = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res6_L], new InitialState { L = 0x00 }, new ExpectedState { Cycles = 8, L = 0x00 });
            Add([Opcode.Pre_CB, CBOpcode.Res7_L], new InitialState { L = 0x00 }, new ExpectedState { Cycles = 8, L = 0x00 });
        }
    }
    
    [Theory]
    [ClassData(typeof(ResNXHLTestData))]
    public void CBOperation_ResNXHL_BitNOfXHLIsReset(
        byte[] program, InitialState initialState, ExpectedState expectedState)
    {
        var cartridge = CartridgeBuilder.Create().WithProgram(program).Build();
        _gameBoy.Load(cartridge);
        _gameBoy.SetInitialState(initialState);

        var cycles = _gameBoy.Update();

        _gameBoy.AssertExpectedState(expectedState);
        Assert.Equal(expectedState.Cycles, cycles);
    }

    private class ResNXHLTestData : TheoryData<byte[], InitialState, ExpectedState>
    {
        public ResNXHLTestData()
        {
            // Non-Zero Cases
            Add([Opcode.Pre_CB, CBOpcode.Res0_XHL], new InitialState { HL = 0xC000, Memory = { [0xC000] = 0x01 } }, new ExpectedState { Cycles = 16, Memory = { [0xC000] = 0x00 } });
            Add([Opcode.Pre_CB, CBOpcode.Res1_XHL], new InitialState { HL = 0xC000, Memory = { [0xC000] = 0x02 } }, new ExpectedState { Cycles = 16, Memory = { [0xC000] = 0x00 } });
            Add([Opcode.Pre_CB, CBOpcode.Res2_XHL], new InitialState { HL = 0xC000, Memory = { [0xC000] = 0x04 } }, new ExpectedState { Cycles = 16, Memory = { [0xC000] = 0x00 } });
            Add([Opcode.Pre_CB, CBOpcode.Res3_XHL], new InitialState { HL = 0xC000, Memory = { [0xC000] = 0x08 } }, new ExpectedState { Cycles = 16, Memory = { [0xC000] = 0x00 } });
            Add([Opcode.Pre_CB, CBOpcode.Res4_XHL], new InitialState { HL = 0xC000, Memory = { [0xC000] = 0x10 } }, new ExpectedState { Cycles = 16, Memory = { [0xC000] = 0x00 } });
            Add([Opcode.Pre_CB, CBOpcode.Res5_XHL], new InitialState { HL = 0xC000, Memory = { [0xC000] = 0x20 } }, new ExpectedState { Cycles = 16, Memory = { [0xC000] = 0x00 } });
            Add([Opcode.Pre_CB, CBOpcode.Res6_XHL], new InitialState { HL = 0xC000, Memory = { [0xC000] = 0x40 } }, new ExpectedState { Cycles = 16, Memory = { [0xC000] = 0x00 } });
            Add([Opcode.Pre_CB, CBOpcode.Res7_XHL], new InitialState { HL = 0xC000, Memory = { [0xC000] = 0x80 } }, new ExpectedState { Cycles = 16, Memory = { [0xC000] = 0x00 } });
            
            // Zero Cases
            Add([Opcode.Pre_CB, CBOpcode.Res0_XHL], new InitialState { HL = 0xC000, Memory = { [0xC000] = 0x00 } }, new ExpectedState { Cycles = 16, Memory = { [0xC000] = 0x00 } });
            Add([Opcode.Pre_CB, CBOpcode.Res1_XHL], new InitialState { HL = 0xC000, Memory = { [0xC000] = 0x00 } }, new ExpectedState { Cycles = 16, Memory = { [0xC000] = 0x00 } });
            Add([Opcode.Pre_CB, CBOpcode.Res2_XHL], new InitialState { HL = 0xC000, Memory = { [0xC000] = 0x00 } }, new ExpectedState { Cycles = 16, Memory = { [0xC000] = 0x00 } });
            Add([Opcode.Pre_CB, CBOpcode.Res3_XHL], new InitialState { HL = 0xC000, Memory = { [0xC000] = 0x00 } }, new ExpectedState { Cycles = 16, Memory = { [0xC000] = 0x00 } });
            Add([Opcode.Pre_CB, CBOpcode.Res4_XHL], new InitialState { HL = 0xC000, Memory = { [0xC000] = 0x00 } }, new ExpectedState { Cycles = 16, Memory = { [0xC000] = 0x00 } });
            Add([Opcode.Pre_CB, CBOpcode.Res5_XHL], new InitialState { HL = 0xC000, Memory = { [0xC000] = 0x00 } }, new ExpectedState { Cycles = 16, Memory = { [0xC000] = 0x00 } });
            Add([Opcode.Pre_CB, CBOpcode.Res6_XHL], new InitialState { HL = 0xC000, Memory = { [0xC000] = 0x00 } }, new ExpectedState { Cycles = 16, Memory = { [0xC000] = 0x00 } });
            Add([Opcode.Pre_CB, CBOpcode.Res7_XHL], new InitialState { HL = 0xC000, Memory = { [0xC000] = 0x00 } }, new ExpectedState { Cycles = 16, Memory = { [0xC000] = 0x00 } });
        }
    }
}