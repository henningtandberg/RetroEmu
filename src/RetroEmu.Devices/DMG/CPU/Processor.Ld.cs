using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU
{
    public unsafe partial class Processor
    {
        private void SetupLdInstructions()
        {
            _instructions[Opcode.Ld_B_N8] = new LoadInstruction(WriteType.B, FetchType.N8); // TODO: Doublecheck that the manual is wrong
            _instructions[Opcode.Ld_C_N8] = new LoadInstruction(WriteType.C, FetchType.N8); // TODO: Doublecheck that the manual is wrong
            _instructions[Opcode.Ld_D_N8] = new LoadInstruction(WriteType.D, FetchType.N8); // TODO: Doublecheck that the manual is wrong
            _instructions[Opcode.Ld_E_N8] = new LoadInstruction(WriteType.E, FetchType.N8); // TODO: Doublecheck that the manual is wrong
            _instructions[Opcode.Ld_H_N8] = new LoadInstruction(WriteType.H, FetchType.N8); // TODO: Doublecheck that the manual is wrong
            _instructions[Opcode.Ld_L_N8] = new LoadInstruction(WriteType.L, FetchType.N8); // TODO: Doublecheck that the manual is wrong

            _instructions[Opcode.Ld_A_A] = new LoadInstruction(WriteType.A, FetchType.A);
            _instructions[Opcode.Ld_A_B] = new LoadInstruction(WriteType.A, FetchType.B);
            _instructions[Opcode.Ld_A_C] = new LoadInstruction(WriteType.A, FetchType.C);
            _instructions[Opcode.Ld_A_D] = new LoadInstruction(WriteType.A, FetchType.D);
            _instructions[Opcode.Ld_A_E] = new LoadInstruction(WriteType.A, FetchType.E);
            _instructions[Opcode.Ld_A_H] = new LoadInstruction(WriteType.A, FetchType.H);
            _instructions[Opcode.Ld_A_L] = new LoadInstruction(WriteType.A, FetchType.L);
            _instructions[Opcode.Ld_A_XHL] = new LoadInstruction(WriteType.A, FetchType.XHL);

            _instructions[Opcode.Ld_B_B] = new LoadInstruction(WriteType.B, FetchType.B);
            _instructions[Opcode.Ld_B_C] = new LoadInstruction(WriteType.B, FetchType.C);
            _instructions[Opcode.Ld_B_D] = new LoadInstruction(WriteType.B, FetchType.D);
            _instructions[Opcode.Ld_B_E] = new LoadInstruction(WriteType.B, FetchType.E);
            _instructions[Opcode.Ld_B_H] = new LoadInstruction(WriteType.B, FetchType.H);
            _instructions[Opcode.Ld_B_L] = new LoadInstruction(WriteType.B, FetchType.L);
            _instructions[Opcode.Ld_B_XHL] = new LoadInstruction(WriteType.B, FetchType.XHL);

            _instructions[Opcode.Ld_C_B] = new LoadInstruction(WriteType.C, FetchType.B);
            _instructions[Opcode.Ld_C_C] = new LoadInstruction(WriteType.C, FetchType.C);
            _instructions[Opcode.Ld_C_D] = new LoadInstruction(WriteType.C, FetchType.D);
            _instructions[Opcode.Ld_C_E] = new LoadInstruction(WriteType.C, FetchType.E);
            _instructions[Opcode.Ld_C_H] = new LoadInstruction(WriteType.C, FetchType.H);
            _instructions[Opcode.Ld_C_L] = new LoadInstruction(WriteType.C, FetchType.L);
            _instructions[Opcode.Ld_C_XHL] = new LoadInstruction(WriteType.C, FetchType.XHL);
            _instructions[Opcode.Ld_C_A] = new LoadInstruction(WriteType.C, FetchType.A);

            _instructions[Opcode.Ld_A_XC] = new LoadInstruction(WriteType.A, FetchType.XC);
            _instructions[Opcode.Ld_XC_A] = new LoadInstruction(WriteType.XC, FetchType.A);
        }

    }
}
