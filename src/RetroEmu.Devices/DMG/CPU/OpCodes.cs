
namespace RetroEmu.Devices.DMG.CPU
{
    internal class OpCodes
    {
        enum OPC
        {
            Nop,      Ld_BC_N16, Ld_XBC_A,  Inc_BC,   Inc_B,      Dec_B,    Ld_B_N8,   Rlc_A,    Ld_XN16_SP, Add_HL_BC, Ld_A_XBC,  Dec_BC,  Inc_C,     Dec_C,    Ld_C_N8,   Rrc_A,
            Stop,     Ld_DE_N16, Ld_XDE_A,  Inc_DE,   Inc_D,      Dec_D,    Ld_D,N8,   Rla,      Jr_N8,      Add_HL_DE, Ld_A_XDE,  Dec_DE,  Inc_E,     Dec_E,    Ld_E_N8,   Rra,
            JrNZ_N8,  Ld_HL_N16, Ld_XHLI_A, Inc_HL,   Inc_H,      Dec_H,    LD_H_N8,   Daa,      JrZ_N8,     Add_HL_HL, Ld_A_XHLI, Dec_HL,  Inc_L,     Dec_L,    Ld_L_N8,   Cpl,
            JrNC_N8,  Ld_SP_N16, Ld_XHLD_A, Inc_SP,   Inc_XHL,    Dec_XHL,  LD_XHL_N8, Scf,      JrC_N8,     Add_HL_SP, Ld_A_XHLD, Dec_SP,  Inc_A,     Dec_A,    Ld_A_N8,   Ccf,
            Ld_B_B,   Ld_B_C,    Ld_B_D,    Ld_B_E,   Ld_B_H,     Ld_B_L,   Ld_B_XHL,  Ld_B_A,   Ld_C_B,     Ld_C_C,    Ld_C_D,    Ld_C_E,  Ld_C_H,    Ld_C_L,   Ld_C_XHL,  Ld_C_A,
            Ld_D_B,   Ld_D_C,    Ld_D_D,    Ld_D_E,   Ld_D_H,     Ld_D_L,   Ld_D_XHL,  Ld_D_A,   Ld_E_B,     Ld_E_C,    Ld_E_D,    Ld_E_E,  Ld_E_H,    Ld_E_L,   Ld_E_XHL,  Ld_E_A,
            Ld_H_B,   Ld_H_C,    Ld_H_D,    Ld_H_E,   Ld_H_H,     Ld_H_L,   Ld_H_XHL,  Ld_H_A,   Ld_L_B,     Ld_L_C,    Ld_L_D,    Ld_L_E,  Ld_L_H,    Ld_L_L,   Ld_L_XHL,  Ld_L_A,
            Ld_XHL_B, Ld_XHL_C,  Ld_XHL_D,  Ld_XHL_E, Ld_XHL_H,   Ld_XHL_L, Halt,      Ld_XHL_A, Ld_A_B,     Ld_A_C,    Ld_A_D,    Ld_A_E,  Ld_A_H,    Ld_A_L,   Ld_A_XHL,  Ld_A_A,
            Add_A_B,  Add_A_C,   Add_A_D,   Add_A_E,  Add_A_H,    Add_A_L,  Add_A_XHL, Add_A_A,  Adc_A_B,    Adc_A_C,   Adc_A_D,   Adc_A_E, Adc_A_H,   Adc_A_L,  Adc_A_XHL, Adc_A_A,
            Sub_A_B,  Sub_A_C,   Sub_A_D,   Sub_A_E,  Sub_A_H,    Sub_A_L,  Sub_A_XHL, Sub_A_A,  Sbc_A_B,    Sbc_A_C,   Sbc_A_D,   Sbc_A_E, Sbc_A_H,   Sbc_A_L,  Sbc_A_XHL, Sbc_A_A,
            And_A_B,  And_A_C,   And_A_D,   And_A_E,  And_A_H,    And_A_L,  And_A_XHL, And_A_A,  Xor_A_B,    Xor_A_C,   Xor_A_D,   Xor_A_E, Xor_A_H,   Xor_A_L,  Xor_A_XHL, Xor_A_A,
            Or_A_B,   Or_A_C,    Or_A_D,    Or_A_E,   Or_A_H,     Or_A_L,   Or_A_XHL,  Or_A_A,   Cp_A_B,     Cp_A_C,    Cp_A_D,    Cp_A_E,  Cp_A_H,    Cp_A_L,   Cp_A_XHL,  Cp_A_A,
            RetNZ,    Pop_BC,    JpNZ_N16,  Jp_N16,   CallNZ_N16, Push_BC,  Add_A_N8,  Rst_00H,  RetZ,       Ret,       JpZ_N16,   Pre_CB,  CallZ_N16, Call_N16, Adc_A_N8,  Rst_08H,
            RetNC,    Pop_DE,    JpNC_N16,  U1,       CallNC_N16, Push_DE,  Sub_A_N8,  Rst_10H,  RetC,       RetI,      JpC_N16,   U2,      CallC_N16, U3,       Sbc_A_N8,  Rst_18H,
            Ld_XN8_A, Pop_HL,    LD_XC_A,   U4,       U5,         Push_HL,  And_A_N8,  Rst_20H,  Add_SP_N8,  Jp_XHL,    Ld_XN16_A, U6,      U7,        U8,       Xor_A_N8,  Rst_28H,
            Ld_A_XN8, Pop_AF,    LD_A_XC,   DI,       U9,         Push_AF,  Or_A_N8,   Rst_30H,  Ld_HL_SPN8, Ld_SP_HL,  Ld_A_XN16, EI,      U10,       U11,      Cp_A_N8,   Rst_38H
        }
    }
}
