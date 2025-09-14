using RetroEmu.Devices.DMG;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.GB.TestSetup;
using Xunit;

namespace RetroEmu.GB.Tests.IsolatedOperationTests;

public class LoadInstructionsTests
{
    private readonly IGameBoy _gameBoy = TestGameBoyBuilder.CreateBuilder().BuildGameBoy();

    [Theory]
    [ClassData(typeof(ByteLoadsTestData))]
    public void InitialState_SingleLoadInstruction_ExpectedState(
        byte[] program, InitialState initialState, ExpectedState expectedState)
    {
        var cartridge = CartridgeBuilder.Create().WithProgram(program).Build();
        _gameBoy.Load(cartridge);
        _gameBoy.SetInitialState(initialState);

        _gameBoy.Update();

        _gameBoy.AssertExpectedState(expectedState);
    }
}

public class ByteLoadsTestData : TheoryData<byte[], InitialState, ExpectedState>
{
    public ByteLoadsTestData()
    {
        // LD A, r
        Add([Opcode.Ld_A_A], new InitialState { A = 0x12 }, new ExpectedState { A = 0x12 });
        Add([Opcode.Ld_A_B], new InitialState { B = 0x34 }, new ExpectedState { A = 0x34 });
        Add([Opcode.Ld_A_C], new InitialState { C = 0x56 }, new ExpectedState { A = 0x56 });
        Add([Opcode.Ld_A_D], new InitialState { D = 0x78 }, new ExpectedState { A = 0x78 });
        Add([Opcode.Ld_A_E], new InitialState { E = 0x9A }, new ExpectedState { A = 0x9A });
        Add([Opcode.Ld_A_H], new InitialState { H = 0xBC }, new ExpectedState { A = 0xBC });
        Add([Opcode.Ld_A_L], new InitialState { L = 0xDE }, new ExpectedState { A = 0xDE });
        Add([Opcode.Ld_A_XHL], new InitialState { HL = 0xC123, Memory = { [0xC123] = 0xAB } },
            new ExpectedState { A = 0xAB });

        // LD B, r
        Add([Opcode.Ld_B_A], new InitialState { A = 0x12 }, new ExpectedState { B = 0x12 });
        Add([Opcode.Ld_B_B], new InitialState { B = 0x34 }, new ExpectedState { B = 0x34 });
        Add([Opcode.Ld_B_C], new InitialState { C = 0x56 }, new ExpectedState { B = 0x56 });
        Add([Opcode.Ld_B_D], new InitialState { D = 0x78 }, new ExpectedState { B = 0x78 });
        Add([Opcode.Ld_B_E], new InitialState { E = 0x9A }, new ExpectedState { B = 0x9A });
        Add([Opcode.Ld_B_H], new InitialState { H = 0xBC }, new ExpectedState { B = 0xBC });
        Add([Opcode.Ld_B_L], new InitialState { L = 0xDE }, new ExpectedState { B = 0xDE });
        Add([Opcode.Ld_B_XHL], new InitialState { HL = 0xC123, Memory = { [0xC123] = 0x77 } },
            new ExpectedState { B = 0x77 });

        // LD C, r
        Add([Opcode.Ld_C_A], new InitialState { A = 0x12 }, new ExpectedState { C = 0x12 });
        Add([Opcode.Ld_C_B], new InitialState { B = 0x34 }, new ExpectedState { C = 0x34 });
        Add([Opcode.Ld_C_C], new InitialState { C = 0x56 }, new ExpectedState { C = 0x56 });
        Add([Opcode.Ld_C_D], new InitialState { D = 0x78 }, new ExpectedState { C = 0x78 });
        Add([Opcode.Ld_C_E], new InitialState { E = 0x9A }, new ExpectedState { C = 0x9A });
        Add([Opcode.Ld_C_H], new InitialState { H = 0xBC }, new ExpectedState { C = 0xBC });
        Add([Opcode.Ld_C_L], new InitialState { L = 0xDE }, new ExpectedState { C = 0xDE });
        Add([Opcode.Ld_C_XHL], new InitialState { HL = 0xC123, Memory = { [0xC123] = 0x66 } },
            new ExpectedState { C = 0x66 });

        // LD D, r
        Add([Opcode.Ld_D_A], new InitialState { A = 0x12 }, new ExpectedState { D = 0x12 });
        Add([Opcode.Ld_D_B], new InitialState { B = 0x34 }, new ExpectedState { D = 0x34 });
        Add([Opcode.Ld_D_C], new InitialState { C = 0x56 }, new ExpectedState { D = 0x56 });
        Add([Opcode.Ld_D_D], new InitialState { D = 0x78 }, new ExpectedState { D = 0x78 });
        Add([Opcode.Ld_D_E], new InitialState { E = 0x9A }, new ExpectedState { D = 0x9A });
        Add([Opcode.Ld_D_H], new InitialState { H = 0xBC }, new ExpectedState { D = 0xBC });
        Add([Opcode.Ld_D_L], new InitialState { L = 0xDE }, new ExpectedState { D = 0xDE });
        Add([Opcode.Ld_D_XHL], new InitialState { HL = 0xC123, Memory = { [0xC123] = 0x55 } },
            new ExpectedState { D = 0x55 });

        // LD E, r
        Add([Opcode.Ld_E_A], new InitialState { A = 0x12 }, new ExpectedState { E = 0x12 });
        Add([Opcode.Ld_E_B], new InitialState { B = 0x34 }, new ExpectedState { E = 0x34 });
        Add([Opcode.Ld_E_C], new InitialState { C = 0x56 }, new ExpectedState { E = 0x56 });
        Add([Opcode.Ld_E_D], new InitialState { D = 0x78 }, new ExpectedState { E = 0x78 });
        Add([Opcode.Ld_E_E], new InitialState { E = 0x9A }, new ExpectedState { E = 0x9A });
        Add([Opcode.Ld_E_H], new InitialState { H = 0xBC }, new ExpectedState { E = 0xBC });
        Add([Opcode.Ld_E_L], new InitialState { L = 0xDE }, new ExpectedState { E = 0xDE });
        Add([Opcode.Ld_E_XHL], new InitialState { HL = 0xC123, Memory = { [0xC123] = 0x44 } },
            new ExpectedState { E = 0x44 });

        // LD H, r
        Add([Opcode.Ld_H_A], new InitialState { A = 0x12 }, new ExpectedState { H = 0x12 });
        Add([Opcode.Ld_H_B], new InitialState { B = 0x34 }, new ExpectedState { H = 0x34 });
        Add([Opcode.Ld_H_C], new InitialState { C = 0x56 }, new ExpectedState { H = 0x56 });
        Add([Opcode.Ld_H_D], new InitialState { D = 0x78 }, new ExpectedState { H = 0x78 });
        Add([Opcode.Ld_H_E], new InitialState { E = 0x9A }, new ExpectedState { H = 0x9A });
        Add([Opcode.Ld_H_H], new InitialState { H = 0xBC }, new ExpectedState { H = 0xBC });
        Add([Opcode.Ld_H_L], new InitialState { L = 0xDE }, new ExpectedState { H = 0xDE });
        Add([Opcode.Ld_H_XHL], new InitialState { HL = 0xC123, Memory = { [0xC123] = 0x33 } },
            new ExpectedState { H = 0x33 });

        // LD L, r
        Add([Opcode.Ld_L_A], new InitialState { A = 0x12 }, new ExpectedState { L = 0x12 });
        Add([Opcode.Ld_L_B], new InitialState { B = 0x34 }, new ExpectedState { L = 0x34 });
        Add([Opcode.Ld_L_C], new InitialState { C = 0x56 }, new ExpectedState { L = 0x56 });
        Add([Opcode.Ld_L_D], new InitialState { D = 0x78 }, new ExpectedState { L = 0x78 });
        Add([Opcode.Ld_L_E], new InitialState { E = 0x9A }, new ExpectedState { L = 0x9A });
        Add([Opcode.Ld_L_H], new InitialState { H = 0xBC }, new ExpectedState { L = 0xBC });
        Add([Opcode.Ld_L_L], new InitialState { L = 0xDE }, new ExpectedState { L = 0xDE });
        Add([Opcode.Ld_L_XHL], new InitialState { HL = 0xC123, Memory = { [0xC123] = 0x22 } },
            new ExpectedState { L = 0x22 });

        // LD (HL), r
        Add([Opcode.Ld_XHL_A], new InitialState { A = 0x12, HL = 0xC123 },
            new ExpectedState { Memory = { [0xC123] = 0x12 } });
        Add([Opcode.Ld_XHL_B], new InitialState { B = 0x34, HL = 0xC123 },
            new ExpectedState { Memory = { [0xC123] = 0x34 } });
        Add([Opcode.Ld_XHL_C], new InitialState { C = 0x56, HL = 0xC123 },
            new ExpectedState { Memory = { [0xC123] = 0x56 } });
        Add([Opcode.Ld_XHL_D], new InitialState { D = 0x78, HL = 0xC123 },
            new ExpectedState { Memory = { [0xC123] = 0x78 } });
        Add([Opcode.Ld_XHL_E], new InitialState { E = 0x9A, HL = 0xC123 },
            new ExpectedState { Memory = { [0xC123] = 0x9A } });
        Add([Opcode.Ld_XHL_H], new InitialState { H = 0xC1, HL = 0xC123 },
            new ExpectedState { Memory = { [0xC123] = 0xC1 } });
        Add([Opcode.Ld_XHL_L], new InitialState { L = 0x23, HL = 0xC123 },
            new ExpectedState { Memory = { [0xC123] = 0x23 } });

        // LD rr, n16 (16-bit loads)
        Add([Opcode.Ld_BC_N16, 0x34, 0x12], new InitialState(), new ExpectedState { BC = 0x1234 });
        Add([Opcode.Ld_DE_N16, 0x78, 0x56], new InitialState(), new ExpectedState { DE = 0x5678 });
        Add([Opcode.Ld_HL_N16, 0xBC, 0x9A], new InitialState(), new ExpectedState { HL = 0x9ABC });
        Add([Opcode.Ld_SP_N16, 0xF0, 0xDE], new InitialState(), new ExpectedState { SP = 0xDEF0 });

        // LD r, n8 (immediate 8-bit loads)
        Add([Opcode.Ld_B_N8, 0x12], new InitialState(), new ExpectedState { B = 0x12 });
        Add([Opcode.Ld_C_N8, 0x34], new InitialState(), new ExpectedState { C = 0x34 });
        Add([Opcode.Ld_D_N8, 0x56], new InitialState(), new ExpectedState { D = 0x56 });
        Add([Opcode.Ld_E_N8, 0x78], new InitialState(), new ExpectedState { E = 0x78 });
        Add([Opcode.Ld_H_N8, 0x9A], new InitialState(), new ExpectedState { H = 0x9A });
        Add([Opcode.Ld_L_N8, 0xBC], new InitialState(), new ExpectedState { L = 0xBC });
        Add([Opcode.Ld_A_N8, 0xDE], new InitialState(), new ExpectedState { A = 0xDE });

        // LD A, (rr)
        Add([Opcode.Ld_A_XBC], new InitialState { BC = 0xC123, Memory = { [0xC123] = 0x56 } },
            new ExpectedState { A = 0x56 });
        Add([Opcode.Ld_A_XDE], new InitialState { DE = 0xC123, Memory = { [0xC123] = 0x9A } },
            new ExpectedState { A = 0x9A });
        Add([Opcode.Ld_A_XHLI], new InitialState { HL = 0xC123, Memory = { [0xC123] = 0x12 } },
            new ExpectedState { A = 0x12, HL = 0xC124 }); // HL incremented
        Add([Opcode.Ld_A_XHLD], new InitialState { HL = 0xC123, Memory = { [0xC123] = 0x34 } },
            new ExpectedState { A = 0x34, HL = 0xC122 }); // HL decremented

        // LD (rr), A
        Add([Opcode.Ld_XBC_A], new InitialState { A = 0x12, BC = 0xC123 },
            new ExpectedState { Memory = { [0xC123] = 0x12 } });
        Add([Opcode.Ld_XDE_A], new InitialState { A = 0x34, DE = 0xC123 },
            new ExpectedState { Memory = { [0xC123] = 0x34 } });
        Add([Opcode.Ld_XHLI_A], new InitialState { A = 0x56, HL = 0xC123 },
            new ExpectedState { Memory = { [0xC123] = 0x56 }, HL = 0xC124 }); // HL incremented
        Add([Opcode.Ld_XHLD_A], new InitialState { A = 0x78, HL = 0xC123 },
            new ExpectedState { Memory = { [0xC123] = 0x78 }, HL = 0xC122 }); // HL decremented

        // LD A, (nn) / LD (nn), A
        Add([Opcode.Ld_A_XN16, 0x23, 0xC1], new InitialState { Memory = { [0xC123] = 0x56 } },
            new ExpectedState { A = 0x56 });
        Add([Opcode.Ld_XN16_A, 0x23, 0xC1], new InitialState { A = 0x9A },
            new ExpectedState { Memory = { [0xC123] = 0x9A } });

        // LD A, (FF00 + C) / LD (FF00 + C), A
        Add([Opcode.Ld_A_XC], new InitialState { C = 0x05, Memory = { [0xFF05] = 0x34 } },
            new ExpectedState { A = 0x34 });
        Add([Opcode.Ld_XC_A], new InitialState { A = 0x56, C = 0x05 },
            new ExpectedState { Memory = { [0xFF05] = 0x56 } });

        // LD A, (FF00 + n8) / LD (FF00 + n8), A
        Add([Opcode.Ld_A_XN8, 0x05], new InitialState { Memory = { [0xFF05] = 0xAB } }, new ExpectedState { A = 0xAB });
        Add([Opcode.Ld_XN8_A, 0x05], new InitialState { A = 0xCD }, new ExpectedState { Memory = { [0xFF05] = 0xCD } });

        // LD (nn), SP
        Add([Opcode.Ld_XN16_SP, 0x23, 0xC1], new InitialState { SP = 0xBEEF },
            new ExpectedState { Memory = { [0xC123] = 0xEF, [0xC124] = 0xBE } });

        // LD SP, HL
        Add([Opcode.Ld_SP_HL], new InitialState { HL = 0xCAFE }, new ExpectedState { SP = 0xCAFE });

        // LD HL, SP + n8
        Add([Opcode.Ld_HL_SPN8, 0x05], new InitialState { SP = 0x1000 },
            new ExpectedState { HL = 0x1005 }); // example +5 offset
    }
}