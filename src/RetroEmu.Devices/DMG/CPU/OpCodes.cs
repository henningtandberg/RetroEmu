
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
            Or_A_B   = 0xB0, Or_A_C    = 0xB1, Or_A_D    = 0xB2, Or_A_E   = 0xB3, Or_A_H     = 0xB4, Or_A_L   = 0xB5, Or_A_XHL  = 0xB6, Or_A_A   = 0xB7, Cp_A_B     = 0xB8, Cp_A_C    = 0xB9, Cp_A_D    = 0xBA, Cp_A_E  = 0xBB, Cp_A_H    = 0xBC, Cp_A_    = 0xBD, Cp_A_XHL  = 0xBE, Cp_A_A  = 0xBF,
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
}