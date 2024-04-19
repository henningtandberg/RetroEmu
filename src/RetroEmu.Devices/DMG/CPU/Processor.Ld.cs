using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU
{
    public unsafe partial class Processor
    {
        private void SetupLdInstructions()
        {

            _instructions[Opcode.Ld_A_B] = new LoadInstruction(WriteType.A, FetchType.B);
            _instructions[Opcode.Ld_A_C] = new LoadInstruction(WriteType.A, FetchType.C);
            _instructions[Opcode.Ld_A_D] = new LoadInstruction(WriteType.A, FetchType.D);
            _instructions[Opcode.Ld_A_E] = new LoadInstruction(WriteType.A, FetchType.E);
            _instructions[Opcode.Ld_A_H] = new LoadInstruction(WriteType.A, FetchType.H);
            _instructions[Opcode.Ld_A_L] = new LoadInstruction(WriteType.A, FetchType.L);
            _instructions[Opcode.Ld_A_XHL] = new LoadInstruction(WriteType.A, FetchType.XHL);
            _instructions[Opcode.Ld_A_A] = new LoadInstruction(WriteType.A, FetchType.A);
            _instructions[Opcode.Ld_A_N8] = new LoadInstruction(WriteType.A, FetchType.N8);

            _instructions[Opcode.Ld_B_B] = new LoadInstruction(WriteType.B, FetchType.B);
            _instructions[Opcode.Ld_B_C] = new LoadInstruction(WriteType.B, FetchType.C);
            _instructions[Opcode.Ld_B_D] = new LoadInstruction(WriteType.B, FetchType.D);
            _instructions[Opcode.Ld_B_E] = new LoadInstruction(WriteType.B, FetchType.E);
            _instructions[Opcode.Ld_B_H] = new LoadInstruction(WriteType.B, FetchType.H);
            _instructions[Opcode.Ld_B_L] = new LoadInstruction(WriteType.B, FetchType.L);
            _instructions[Opcode.Ld_B_XHL] = new LoadInstruction(WriteType.B, FetchType.XHL);
            _instructions[Opcode.Ld_B_A] = new LoadInstruction(WriteType.B, FetchType.A);
            _instructions[Opcode.Ld_B_N8] = new LoadInstruction(WriteType.B, FetchType.N8);

            _instructions[Opcode.Ld_C_B] = new LoadInstruction(WriteType.C, FetchType.B);
            _instructions[Opcode.Ld_C_C] = new LoadInstruction(WriteType.C, FetchType.C);
            _instructions[Opcode.Ld_C_D] = new LoadInstruction(WriteType.C, FetchType.D);
            _instructions[Opcode.Ld_C_E] = new LoadInstruction(WriteType.C, FetchType.E);
            _instructions[Opcode.Ld_C_H] = new LoadInstruction(WriteType.C, FetchType.H);
            _instructions[Opcode.Ld_C_L] = new LoadInstruction(WriteType.C, FetchType.L);
            _instructions[Opcode.Ld_C_XHL] = new LoadInstruction(WriteType.C, FetchType.XHL);
            _instructions[Opcode.Ld_C_A] = new LoadInstruction(WriteType.C, FetchType.A);
            _instructions[Opcode.Ld_C_N8] = new LoadInstruction(WriteType.C, FetchType.N8);

            _instructions[Opcode.Ld_D_B] = new LoadInstruction(WriteType.D, FetchType.B);
            _instructions[Opcode.Ld_D_C] = new LoadInstruction(WriteType.D, FetchType.C);
            _instructions[Opcode.Ld_D_D] = new LoadInstruction(WriteType.D, FetchType.D);
            _instructions[Opcode.Ld_D_E] = new LoadInstruction(WriteType.D, FetchType.E);
            _instructions[Opcode.Ld_D_H] = new LoadInstruction(WriteType.D, FetchType.H);
            _instructions[Opcode.Ld_D_L] = new LoadInstruction(WriteType.D, FetchType.L);
            _instructions[Opcode.Ld_D_XHL] = new LoadInstruction(WriteType.D, FetchType.XHL);
            _instructions[Opcode.Ld_D_A] = new LoadInstruction(WriteType.D, FetchType.A);
            _instructions[Opcode.Ld_D_N8] = new LoadInstruction(WriteType.D, FetchType.N8);

            _instructions[Opcode.Ld_E_B] = new LoadInstruction(WriteType.E, FetchType.B);
            _instructions[Opcode.Ld_E_C] = new LoadInstruction(WriteType.E, FetchType.C);
            _instructions[Opcode.Ld_E_D] = new LoadInstruction(WriteType.E, FetchType.D);
            _instructions[Opcode.Ld_E_E] = new LoadInstruction(WriteType.E, FetchType.E);
            _instructions[Opcode.Ld_E_H] = new LoadInstruction(WriteType.E, FetchType.H);
            _instructions[Opcode.Ld_E_L] = new LoadInstruction(WriteType.E, FetchType.L);
            _instructions[Opcode.Ld_E_XHL] = new LoadInstruction(WriteType.E, FetchType.XHL);
            _instructions[Opcode.Ld_E_A] = new LoadInstruction(WriteType.E, FetchType.A);
            _instructions[Opcode.Ld_E_N8] = new LoadInstruction(WriteType.E, FetchType.N8);

            _instructions[Opcode.Ld_H_B] = new LoadInstruction(WriteType.H, FetchType.B);
            _instructions[Opcode.Ld_H_C] = new LoadInstruction(WriteType.H, FetchType.C);
            _instructions[Opcode.Ld_H_D] = new LoadInstruction(WriteType.H, FetchType.D);
            _instructions[Opcode.Ld_H_E] = new LoadInstruction(WriteType.H, FetchType.E);
            _instructions[Opcode.Ld_H_H] = new LoadInstruction(WriteType.H, FetchType.H);
            _instructions[Opcode.Ld_H_L] = new LoadInstruction(WriteType.H, FetchType.L);
            _instructions[Opcode.Ld_H_XHL] = new LoadInstruction(WriteType.H, FetchType.XHL);
            _instructions[Opcode.Ld_H_A] = new LoadInstruction(WriteType.H, FetchType.A);
            _instructions[Opcode.Ld_H_N8] = new LoadInstruction(WriteType.H, FetchType.N8);

            _instructions[Opcode.Ld_L_B] = new LoadInstruction(WriteType.L, FetchType.B);
            _instructions[Opcode.Ld_L_C] = new LoadInstruction(WriteType.L, FetchType.C);
            _instructions[Opcode.Ld_L_D] = new LoadInstruction(WriteType.L, FetchType.D);
            _instructions[Opcode.Ld_L_E] = new LoadInstruction(WriteType.L, FetchType.E);
            _instructions[Opcode.Ld_L_H] = new LoadInstruction(WriteType.L, FetchType.H);
            _instructions[Opcode.Ld_L_L] = new LoadInstruction(WriteType.L, FetchType.L);
            _instructions[Opcode.Ld_L_XHL] = new LoadInstruction(WriteType.L, FetchType.XHL);
            _instructions[Opcode.Ld_L_A] = new LoadInstruction(WriteType.L, FetchType.A);
            _instructions[Opcode.Ld_L_N8] = new LoadInstruction(WriteType.L, FetchType.N8);

            _instructions[Opcode.Ld_XHL_B] = new LoadInstruction(WriteType.XHL, FetchType.B);
            _instructions[Opcode.Ld_XHL_C] = new LoadInstruction(WriteType.XHL, FetchType.C);
            _instructions[Opcode.Ld_XHL_D] = new LoadInstruction(WriteType.XHL, FetchType.D);
            _instructions[Opcode.Ld_XHL_E] = new LoadInstruction(WriteType.XHL, FetchType.E);
            _instructions[Opcode.Ld_XHL_H] = new LoadInstruction(WriteType.XHL, FetchType.H);
            _instructions[Opcode.Ld_XHL_L] = new LoadInstruction(WriteType.XHL, FetchType.L);
            _instructions[Opcode.Ld_XHL_A] = new LoadInstruction(WriteType.XHL, FetchType.A);
            _instructions[Opcode.Ld_XHL_N8] = new LoadInstruction(WriteType.L, FetchType.N8);

            _instructions[Opcode.Ld_A_XC] = new LoadInstruction(WriteType.A, FetchType.XC);
            _instructions[Opcode.Ld_XC_A] = new LoadInstruction(WriteType.XC, FetchType.A);

            _instructions[Opcode.Ld_BC_N16] = new LoadInstruction(WriteType.BC, FetchType.N16);
            _instructions[Opcode.Ld_DE_N16] = new LoadInstruction(WriteType.DE, FetchType.N16);
            _instructions[Opcode.Ld_HL_N16] = new LoadInstruction(WriteType.DE, FetchType.N16);
            _instructions[Opcode.Ld_SP_N16] = new LoadInstruction(WriteType.SP, FetchType.N16);

            _instructions[Opcode.Ld_XBC_A] = new LoadInstruction(WriteType.XBC, FetchType.A);
            _instructions[Opcode.Ld_XDE_A] = new LoadInstruction(WriteType.XDE, FetchType.A);
            _instructions[Opcode.Ld_XHLI_A] = new LoadInstruction(WriteType.XHLI, FetchType.A);
            _instructions[Opcode.Ld_XHLD_A] = new LoadInstruction(WriteType.XHLD, FetchType.A);

            _instructions[Opcode.Ld_A_XN8] = new LoadInstruction(WriteType.A, FetchType.XN8);
            _instructions[Opcode.Ld_XN8_A] = new LoadInstruction(WriteType.XN8, FetchType.A);

            _instructions[Opcode.Ld_XN16_SP] = new LoadInstruction(WriteType.XN16, FetchType.SP);

            _instructions[Opcode.Ld_A_XBC] = new LoadInstruction(WriteType.A, FetchType.XBC);
            _instructions[Opcode.Ld_A_XDE] = new LoadInstruction(WriteType.A, FetchType.XDE);
            _instructions[Opcode.Ld_A_XHLI] = new LoadInstruction(WriteType.A, FetchType.XHLI);
            _instructions[Opcode.Ld_A_XHLD] = new LoadInstruction(WriteType.A, FetchType.XHLD);

            //_instructions[Opcode.Ld_HL_SPN8] = new LoadInstruction(WriteType.HL, FetchType.SPN8); TODO
            _instructions[Opcode.Ld_SP_HL] = new LoadInstruction(WriteType.SP, FetchType.HL);

            _instructions[Opcode.Ld_XN16_A] = new LoadInstruction(WriteType.XN16, FetchType.A);
            _instructions[Opcode.Ld_A_XN16] = new LoadInstruction(WriteType.A, FetchType.XN16);
        }
    }
}
