
namespace RetroEmu.Devices.DMG.CPU
{
    public static class Opcode
    {
        // OpCode codes, used to allow explicit casting.
        public const byte
            Nop      = 0x00, Ld_BC_N16 = 0x01, Ld_XBC_A  = 0x02, Inc_BC   = 0x03, Inc_B      = 0x04, Dec_B    = 0x05, Ld_B_N8   = 0x06, Rlc_A    = 0x07, Ld_XN16_SP = 0x08, Add_HL_BC = 0x09, Ld_A_XBC  = 0x0A, Dec_BC  = 0x0B, Inc_C     = 0x0C, Dec_C    = 0x0D, Ld_C_N8   = 0x0E, Rrc_A   = 0x0F,
            Stop     = 0x10, Ld_DE_N16 = 0x11, Ld_XDE_A  = 0x12, Inc_DE   = 0x13, Inc_D      = 0x14, Dec_D    = 0x15, Ld_D_N8   = 0x16, Rla      = 0x17, Jr_N8      = 0x18, Add_HL_DE = 0x19, Ld_A_XDE  = 0x1A, Dec_DE  = 0x1B, Inc_E     = 0x1C, Dec_E    = 0x1D, Ld_E_N8   = 0x1E, Rra     = 0x1F,
            JrNZ_N8  = 0x20, Ld_HL_N16 = 0x21, Ld_XHLI_A = 0x22, Inc_HL   = 0x23, Inc_H      = 0x24, Dec_H    = 0x25, Ld_H_N8   = 0x26, Daa      = 0x27, JrZ_N8     = 0x28, Add_HL_HL = 0x29, Ld_A_XHLI = 0x2A, Dec_HL  = 0x2B, Inc_L     = 0x2C, Dec_L    = 0x2D, Ld_L_N8   = 0x2E, Cpl     = 0x2F,
            JrNC_N8  = 0x30, Ld_SP_N16 = 0x31, Ld_XHLD_A = 0x32, Inc_SP   = 0x33, Inc_XHL    = 0x34, Dec_XHL  = 0x35, Ld_XHL_N8 = 0x36, Scf      = 0x37, JrC_N8     = 0x38, Add_HL_SP = 0x39, Ld_A_XHLD = 0x3A, Dec_SP  = 0x3B, Inc_A     = 0x3C, Dec_A    = 0x3D, Ld_A_N8   = 0x3E, Ccf     = 0x3F,
            Ld_B_B   = 0x40, Ld_B_C    = 0x41, Ld_B_D    = 0x42, Ld_B_E   = 0x43, Ld_B_H     = 0x44, Ld_B_L   = 0x45, Ld_B_XHL  = 0x46, Ld_B_A   = 0x47, Ld_C_B     = 0x48, Ld_C_C    = 0x49, Ld_C_D    = 0x4A, Ld_C_E  = 0x4B, Ld_C_H    = 0x4C, Ld_C_L   = 0x4D, Ld_C_XHL  = 0x4E, Ld_C_A  = 0x4F,
            Ld_D_B   = 0x50, Ld_D_C    = 0x51, Ld_D_D    = 0x52, Ld_D_E   = 0x53, Ld_D_H     = 0x54, Ld_D_L   = 0x55, Ld_D_XHL  = 0x56, Ld_D_A   = 0x57, Ld_E_B     = 0x58, Ld_E_C    = 0x59, Ld_E_D    = 0x5A, Ld_E_E  = 0x5B, Ld_E_H    = 0x5C, Ld_E_L   = 0x5D, Ld_E_XHL  = 0x5E, Ld_E_A  = 0x5F,
            Ld_H_B   = 0x60, Ld_H_C    = 0x61, Ld_H_D    = 0x62, Ld_H_E   = 0x63, Ld_H_H     = 0x64, Ld_H_L   = 0x65, Ld_H_XHL  = 0x66, Ld_H_A   = 0x67, Ld_L_B     = 0x68, Ld_L_C    = 0x69, Ld_L_D    = 0x6A, Ld_L_E  = 0x6B, Ld_L_H    = 0x6C, Ld_L_L   = 0x6D, Ld_L_XHL  = 0x6E, Ld_L_A  = 0x6F,
            Ld_XHL_B = 0x70, Ld_XHL_C  = 0x71, Ld_XHL_D  = 0x72, Ld_XHL_E = 0x73, Ld_XHL_H   = 0x74, Ld_XHL_L = 0x75, Halt      = 0x76, Ld_XHL_A = 0x77, Ld_A_B     = 0x78, Ld_A_C    = 0x79, Ld_A_D    = 0x7A, Ld_A_E  = 0x7B, Ld_A_H    = 0x7C, Ld_A_L   = 0x7D, Ld_A_XHL  = 0x7E, Ld_A_A  = 0x7F,
            Add_A_B  = 0x80, Add_A_C   = 0x81, Add_A_D   = 0x82, Add_A_E  = 0x83, Add_A_H    = 0x84, Add_A_L  = 0x85, Add_A_XHL = 0x86, Add_A_A  = 0x87, Adc_A_B    = 0x88, Adc_A_C   = 0x89, Adc_A_D   = 0x8A, Adc_A_E = 0x8B, Adc_A_H   = 0x8C, Adc_A_L  = 0x8D, Adc_A_XHL = 0x8E, Adc_A_A = 0x8F,
            Sub_A_B  = 0x90, Sub_A_C   = 0x91, Sub_A_D   = 0x92, Sub_A_E  = 0x93, Sub_A_H    = 0x94, Sub_A_L  = 0x95, Sub_A_XHL = 0x96, Sub_A_A  = 0x97, Sbc_A_B    = 0x98, Sbc_A_C   = 0x99, Sbc_A_D   = 0x9A, Sbc_A_E = 0x9B, Sbc_A_H   = 0x9C, Sbc_A_L  = 0x9D, Sbc_A_XHL = 0x9E, Sbc_A_A = 0x9F,
            And_A_B  = 0xA0, And_A_C   = 0xA1, And_A_D   = 0xA2, And_A_E  = 0xA3, And_A_H    = 0xA4, And_A_L  = 0xA5, And_A_XHL = 0xA6, And_A_A  = 0xA7, Xor_A_B    = 0xA8, Xor_A_C   = 0xA9, Xor_A_D   = 0xAA, Xor_A_E = 0xAB, Xor_A_H   = 0xAC, Xor_A_L  = 0xAD, Xor_A_XHL = 0xAE, Xor_A_A = 0xAF,
            Or_A_B   = 0xB0, Or_A_C    = 0xB1, Or_A_D    = 0xB2, Or_A_E   = 0xB3, Or_A_H     = 0xB4, Or_A_L   = 0xB5, Or_A_XHL  = 0xB6, Or_A_A   = 0xB7, Cp_A_B     = 0xB8, Cp_A_C    = 0xB9, Cp_A_D    = 0xBA, Cp_A_E  = 0xBB, Cp_A_H    = 0xBC, Cp_A_L   = 0xBD, Cp_A_XHL  = 0xBE, Cp_A_A  = 0xBF,
            RetNZ    = 0xC0, Pop_BC    = 0xC1, JpNZ_N16  = 0xC2, Jp_N16   = 0xC3, CallNZ_N16 = 0xC4, Push_BC  = 0xC5, Add_A_N8  = 0xC6, Rst_00H  = 0xC7, RetZ       = 0xC8, Ret       = 0xC9, JpZ_N16   = 0xCA, Pre_CB  = 0xCB, CallZ_N16 = 0xCC, Call_N16 = 0xCD, Adc_A_N8  = 0xCE, Rst_08H = 0xCF,
            RetNC    = 0xD0, Pop_DE    = 0xD1, JpNC_N16  = 0xD2, U1       = 0xD3, CallNC_N16 = 0xD4, Push_DE  = 0xD5, Sub_A_N8  = 0xD6, Rst_10H  = 0xD7, RetC       = 0xD8, RetI      = 0xD9, JpC_N16   = 0xDA, U2      = 0xDB, CallC_N16 = 0xDC, U3       = 0xDD, Sbc_A_N8  = 0xDE, Rst_18H = 0xDF,
            Ld_XN8_A = 0xE0, Pop_HL    = 0xE1, Ld_XC_A   = 0xE2, U4       = 0xE3, U5         = 0xE4, Push_HL  = 0xE5, And_A_N8  = 0xE6, Rst_20H  = 0xE7, Add_SP_N8  = 0xE8, Jp_XHL    = 0xE9, Ld_XN16_A = 0xEA, U6      = 0xEB, U7        = 0xEC, U8       = 0xED, Xor_A_N8  = 0xEE, Rst_28H = 0xEF,
            Ld_A_XN8 = 0xF0, Pop_AF    = 0xF1, Ld_A_XC   = 0xF2, DI       = 0xF3, U9         = 0xF4, Push_AF  = 0xF5, Or_A_N8   = 0xF6, Rst_30H  = 0xF7, Ld_HL_SPN8 = 0xF8, Ld_SP_HL  = 0xF9, Ld_A_XN16 = 0xFA, EI      = 0xFB, U10       = 0xFC, U11      = 0xFD, Cp_A_N8   = 0xFE, Rst_38H = 0xFF;

        // OpCode enum, useful for debugging and ToString.
        public enum OpcodeEnum : byte
        {
            Nop,      Ld_BC_N16, Ld_XBC_A,  Inc_BC,   Inc_B,      Dec_B,    Ld_B_N8,   Rlc_A,    Ld_XN16_SP, Add_HL_BC, Ld_A_XBC,  Dec_BC,  Inc_C,     Dec_C,    Ld_C_N8,   Rrc_A,
            Stop,     Ld_DE_N16, Ld_XDE_A,  Inc_DE,   Inc_D,      Dec_D,    Ld_D_N8,   Rla,      Jr_N8,      Add_HL_DE, Ld_A_XDE,  Dec_DE,  Inc_E,     Dec_E,    Ld_E_N8,   Rra,
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

    public static class CBOpcode
    {
        public const byte
            Rlc_B  = 0x00, Rlc_C  = 0x01, Rlc_D  = 0x02, Rlc_E  = 0x03, Rlc_H  = 0x04, Rlc_L  = 0x05, Rlc_XHL  = 0x06, Rlc_A  = 0x07, Rrc_B  = 0x08, Rrc_C  = 0x09, Rrc_D  = 0x0A, Rrc_E  = 0x0B, Rrc_H  = 0x0C, Rrc_L  = 0x0D, Rrc_XHL  = 0x0E, Rrc_A  = 0x0F,
            Rl_B   = 0x10, Rl_C   = 0x11, Rl_D   = 0x12, Rl_E   = 0x13, Rl_H   = 0x14, Rl_L   = 0x15, Rl_XHL   = 0x16, Rl_A   = 0x17, Rr_B   = 0x18, Rr_C   = 0x19, Rr_D   = 0x1A, Rr_E   = 0x1B, Rr_H   = 0x1C, Rr_L   = 0x1D, Rr_XHL   = 0x1E, Rr_A   = 0x1F,
            Sla_B  = 0x20, Sla_C  = 0x21, Sla_D  = 0x22, Sla_E  = 0x23, Sla_H  = 0x24, Sla_L  = 0x25, Sla_XHL  = 0x26, Sla_A  = 0x27, Sra_B  = 0x28, Sra_C  = 0x29, Sra_D  = 0x2A, Sra_E  = 0x2B, Sra_H  = 0x2C, Sra_L  = 0x2D, Sra_XHL  = 0x2E, Sra_A  = 0x2F,
            Swap_B = 0x30, Swap_C = 0x31, Swap_D = 0x32, Swap_E = 0x33, Swap_H = 0x34, Swap_L = 0x35, Swap_XHL = 0x36, Swap_A = 0x37, Srl_B  = 0x38, Srl_C  = 0x39, Srl_D  = 0x3A, Srl_E  = 0x3B, Srl_H  = 0x3C, Srl_L  = 0x3D, Srl_XHL  = 0x3E, Srl_A  = 0x3F,
            Bit0_B = 0x40, Bit0_C = 0x41, Bit0_D = 0x42, Bit0_E = 0x43, Bit0_H = 0x44, Bit0_L = 0x45, Bit0_XHL = 0x46, Bit0_A = 0x47, Bit1_B = 0x48, Bit1_C = 0x49, Bit1_D = 0x4A, Bit1_E = 0x4B, Bit1_H = 0x4C, Bit1_L = 0x4D, Bit1_XHL = 0x4E, Bit1_A = 0x4F,
            Bit2_B = 0x50, Bit2_C = 0x51, Bit2_D = 0x52, Bit2_E = 0x53, Bit2_H = 0x54, Bit2_L = 0x55, Bit2_XHL = 0x56, Bit2_A = 0x57, Bit3_B = 0x58, Bit3_C = 0x59, Bit3_D = 0x5A, Bit3_E = 0x5B, Bit3_H = 0x5C, Bit3_L = 0x5D, Bit3_XHL = 0x5E, Bit3_A = 0x5F,
            Bit4_B = 0x60, Bit4_C = 0x61, Bit4_D = 0x62, Bit4_E = 0x63, Bit4_H = 0x64, Bit4_L = 0x65, Bit4_XHL = 0x66, Bit4_A = 0x67, Bit5_B = 0x68, Bit5_C = 0x69, Bit5_D = 0x6A, Bit5_E = 0x6B, Bit5_H = 0x6C, Bit5_L = 0x6D, Bit5_XHL = 0x6E, Bit5_A = 0x6F,
            Bit6_B = 0x70, Bit6_C = 0x71, Bit6_D = 0x72, Bit6_E = 0x73, Bit6_H = 0x74, Bit6_L = 0x75, Bit6_XHL = 0x76, Bit6_A = 0x77, Bit7_B = 0x78, Bit7_C = 0x79, Bit7_D = 0x7A, Bit7_E = 0x7B, Bit7_H = 0x7C, Bit7_L = 0x7D, Bit7_XHL = 0x7E, Bit7_A = 0x7F,
            Res0_B = 0x80, Res0_C = 0x81, Res0_D = 0x82, Res0_E = 0x83, Res0_H = 0x84, Res0_L = 0x85, Res0_XHL = 0x86, Res0_A = 0x87, Res1_B = 0x88, Res1_C = 0x89, Res1_D = 0x8A, Res1_E = 0x8B, Res1_H = 0x8C, Res1_L = 0x8D, Res1_XHL = 0x8E, Res1_A = 0x8F,
            Res2_B = 0x90, Res2_C = 0x91, Res2_D = 0x92, Res2_E = 0x93, Res2_H = 0x94, Res2_L = 0x95, Res2_XHL = 0x96, Res2_A = 0x97, Res3_B = 0x98, Res3_C = 0x99, Res3_D = 0x9A, Res3_E = 0x9B, Res3_H = 0x9C, Res3_L = 0x9D, Res3_XHL = 0x9E, Res3_A = 0x9F,
            Res4_B = 0xA0, Res4_C = 0xA1, Res4_D = 0xA2, Res4_E = 0xA3, Res4_H = 0xA4, Res4_L = 0xA5, Res4_XHL = 0xA6, Res4_A = 0xA7, Res5_B = 0xA8, Res5_C = 0xA9, Res5_D = 0xAA, Res5_E = 0xAB, Res5_H = 0xAC, Res5_L = 0xAD, Res5_XHL = 0xAE, Res5_A = 0xAF,
            Res6_B = 0xB0, Res6_C = 0xB1, Res6_D = 0xB2, Res6_E = 0xB3, Res6_H = 0xB4, Res6_L = 0xB5, Res6_XHL = 0xB6, Res6_A = 0xB7, Res7_B = 0xB8, Res7_C = 0xB9, Res7_D = 0xBA, Res7_E = 0xBB, Res7_H = 0xBC, Res7_L = 0xBD, Res7_XHL = 0xBE, Res7_A = 0xBF,
            Set0_B = 0xC0, Set0_C = 0xC1, Set0_D = 0xC2, Set0_E = 0xC3, Set0_H = 0xC4, Set0_L = 0xC5, Set0_XHL = 0xC6, Set0_A = 0xC7, Set1_B = 0xC8, Set1_C = 0xC9, Set1_D = 0xCA, Set1_E = 0xCB, Set1_H = 0xCC, Set1_L = 0xCD, Set1_XHL = 0xCE, Set1_A = 0xCF,
            Set2_B = 0xD0, Set2_C = 0xD1, Set2_D = 0xD2, Set2_E = 0xD3, Set2_H = 0xD4, Set2_L = 0xD5, Set2_XHL = 0xD6, Set2_A = 0xD7, Set3_B = 0xD8, Set3_C = 0xD9, Set3_D = 0xDA, Set3_E = 0xDB, Set3_H = 0xDC, Set3_L = 0xDD, Set3_XHL = 0xDE, Set3_A = 0xDF,
            Set4_B = 0xE0, Set4_C = 0xE1, Set4_D = 0xE2, Set4_E = 0xE3, Set4_H = 0xE4, Set4_L = 0xE5, Set4_XHL = 0xE6, Set4_A = 0xE7, Set5_B = 0xE8, Set5_C = 0xE9, Set5_D = 0xEA, Set5_E = 0xEB, Set5_H = 0xEC, Set5_L = 0xED, Set5_XHL = 0xEE, Set5_A = 0xEF,
            Set6_B = 0xF0, Set6_C = 0xF1, Set6_D = 0xF2, Set6_E = 0xF3, Set6_H = 0xF4, Set6_L = 0xF5, Set6_XHL = 0xF6, Set6_A = 0xF7, Set7_B = 0xF8, Set7_C = 0xF9, Set7_D = 0xFA, Set7_E = 0xFB, Set7_H = 0xFC, Set7_L = 0xFD, Set7_XHL = 0xFE, Set7_A = 0xFF;

        public enum OpcodeEnum : byte
        {
            Rlc_B,  Rlc_C,  Rlc_D,  Rlc_E,  Rlc_H,  Rlc_L,  Rlc_XHL,  Rlc_A,  Rrc_B,  Rrc_C,  Rrc_D,  Rrc_E,  Rrc_H,  Rrc_L,  Rrc_XHL,  Rrc_A,
            Rl_B,   Rl_C,   Rl_D,   Rl_E,   Rl_H,   Rl_L,   Rl_XHL,   Rl_A,   Rr_B,   Rr_C,   Rr_D,   Rr_E,   Rr_H,   Rr_L,   Rr_XHL,   Rr_A,
            Sla_B,  Sla_C,  Sla_D,  Sla_E,  Sla_H,  Sla_L,  Sla_XHL,  Sla_A,  Sra_B,  Sra_C,  Sra_D,  Sra_E,  Sra_H,  Sra_L,  Sra_XHL,  Sra_A,
            Swap_B, Swap_C, Swap_D, Swap_E, Swap_H, Swap_L, Swap_XHL, Swap_A, Srl_B , Srl_C,  Srl_D,  Srl_E,  Srl_H,  Srl_L,  Srl_XHL,  Srl_A,
            Bit0_B, Bit0_C, Bit0_D, Bit0_E, Bit0_H, Bit0_L, Bit0_XHL, Bit0_A, Bit1_B, Bit1_C, Bit1_D, Bit1_E, Bit1_H, Bit1_L, Bit1_XHL, Bit1_A,
            Bit2_B, Bit2_C, Bit2_D, Bit2_E, Bit2_H, Bit2_L, Bit2_XHL, Bit2_A, Bit3_B, Bit3_C, Bit3_D, Bit3_E, Bit3_H, Bit3_L, Bit3_XHL, Bit3_A,
            Bit4_B, Bit4_C, Bit4_D, Bit4_E, Bit4_H, Bit4_L, Bit4_XHL, Bit4_A, Bit5_B, Bit5_C, Bit5_D, Bit5_E, Bit5_H, Bit5_L, Bit5_XHL, Bit5_A,
            Bit6_B, Bit6_C, Bit6_D, Bit6_E, Bit6_H, Bit6_L, Bit6_XHL, Bit6_A, Bit7_B, Bit7_C, Bit7_D, Bit7_E, Bit7_H, Bit7_L, Bit7_XHL, Bit7_A,
            Res0_B, Res0_C, Res0_D, Res0_E, Res0_H, Res0_L, Res0_XHL, Res0_A, Res1_B, Res1_C, Res1_D, Res1_E, Res1_H, Res1_L, Res1_XHL, Res1_A,
            Res2_B, Res2_C, Res2_D, Res2_E, Res2_H, Res2_L, Res2_XHL, Res2_A, Res3_B, Res3_C, Res3_D, Res3_E, Res3_H, Res3_L, Res3_XHL, Res3_A,
            Res4_B, Res4_C, Res4_D, Res4_E, Res4_H, Res4_L, Res4_XHL, Res4_A, Res5_B, Res5_C, Res5_D, Res5_E, Res5_H, Res5_L, Res5_XHL, Res5_A,
            Res6_B, Res6_C, Res6_D, Res6_E, Res6_H, Res6_L, Res6_XHL, Res6_A, Res7_B, Res7_C, Res7_D, Res7_E, Res7_H, Res7_L, Res7_XHL, Res7_A,
            Set0_B, Set0_C, Set0_D, Set0_E, Set0_H, Set0_L, Set0_XHL, Set0_A, Set1_B, Set1_C, Set1_D, Set1_E, Set1_H, Set1_L, Set1_XHL, Set1_A,
            Set2_B, Set2_C, Set2_D, Set2_E, Set2_H, Set2_L, Set2_XHL, Set2_A, Set3_B, Set3_C, Set3_D, Set3_E, Set3_H, Set3_L, Set3_XHL, Set3_A,
            Set4_B, Set4_C, Set4_D, Set4_E, Set4_H, Set4_L, Set4_XHL, Set4_A, Set5_B, Set5_C, Set5_D, Set5_E, Set5_H, Set5_L, Set5_XHL, Set5_A,
            Set6_B, Set6_C, Set6_D, Set6_E, Set6_H, Set6_L, Set6_XHL, Set6_A, Set7_B, Set7_C, Set7_D, Set7_E, Set7_H, Set7_L, Set7_XHL, Set7_A
        }

    }
}