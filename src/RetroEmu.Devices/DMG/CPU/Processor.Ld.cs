using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU
{
    public unsafe partial class Processor
    {
        private void SetupLdInstructions()
        {
            _ops[(int)OpType.Ld] = &Load;

            _instructions[Opcode.Ld_B_N8] = new GeneralInstruction(WriteType.B, OpType.Ld, FetchType.N8); // TODO: Doublecheck that the manual is wrong
            _instructions[Opcode.Ld_C_N8] = new GeneralInstruction(WriteType.C, OpType.Ld, FetchType.N8); // TODO: Doublecheck that the manual is wrong
            _instructions[Opcode.Ld_D_N8] = new GeneralInstruction(WriteType.D, OpType.Ld, FetchType.N8); // TODO: Doublecheck that the manual is wrong
            _instructions[Opcode.Ld_E_N8] = new GeneralInstruction(WriteType.E, OpType.Ld, FetchType.N8); // TODO: Doublecheck that the manual is wrong
            _instructions[Opcode.Ld_H_N8] = new GeneralInstruction(WriteType.H, OpType.Ld, FetchType.N8); // TODO: Doublecheck that the manual is wrong
            _instructions[Opcode.Ld_L_N8] = new GeneralInstruction(WriteType.L, OpType.Ld, FetchType.N8); // TODO: Doublecheck that the manual is wrong

            _instructions[Opcode.Ld_A_A] = new GeneralInstruction(WriteType.A, OpType.Ld, FetchType.A);
            _instructions[Opcode.Ld_A_B] = new GeneralInstruction(WriteType.A, OpType.Ld, FetchType.B);
            _instructions[Opcode.Ld_A_C] = new GeneralInstruction(WriteType.A, OpType.Ld, FetchType.C);
            _instructions[Opcode.Ld_A_D] = new GeneralInstruction(WriteType.A, OpType.Ld, FetchType.D);
            _instructions[Opcode.Ld_A_E] = new GeneralInstruction(WriteType.A, OpType.Ld, FetchType.E);
            _instructions[Opcode.Ld_A_H] = new GeneralInstruction(WriteType.A, OpType.Ld, FetchType.H);
            _instructions[Opcode.Ld_A_L] = new GeneralInstruction(WriteType.A, OpType.Ld, FetchType.L);
            _instructions[Opcode.Ld_A_XHL] = new GeneralInstruction(WriteType.A, OpType.Ld, FetchType.XHL);

            _instructions[Opcode.Ld_B_B] = new GeneralInstruction(WriteType.B, OpType.Ld, FetchType.B);
            _instructions[Opcode.Ld_B_C] = new GeneralInstruction(WriteType.B, OpType.Ld, FetchType.C);
            _instructions[Opcode.Ld_B_D] = new GeneralInstruction(WriteType.B, OpType.Ld, FetchType.D);
            _instructions[Opcode.Ld_B_E] = new GeneralInstruction(WriteType.B, OpType.Ld, FetchType.E);
            _instructions[Opcode.Ld_B_H] = new GeneralInstruction(WriteType.B, OpType.Ld, FetchType.H);
            _instructions[Opcode.Ld_B_L] = new GeneralInstruction(WriteType.B, OpType.Ld, FetchType.L);
            _instructions[Opcode.Ld_B_XHL] = new GeneralInstruction(WriteType.B, OpType.Ld, FetchType.XHL);

            _instructions[Opcode.Ld_C_B] = new GeneralInstruction(WriteType.C, OpType.Ld, FetchType.B);
            _instructions[Opcode.Ld_C_C] = new GeneralInstruction(WriteType.C, OpType.Ld, FetchType.C);
            _instructions[Opcode.Ld_C_D] = new GeneralInstruction(WriteType.C, OpType.Ld, FetchType.D);
            _instructions[Opcode.Ld_C_E] = new GeneralInstruction(WriteType.C, OpType.Ld, FetchType.E);
            _instructions[Opcode.Ld_C_H] = new GeneralInstruction(WriteType.C, OpType.Ld, FetchType.H);
            _instructions[Opcode.Ld_C_L] = new GeneralInstruction(WriteType.C, OpType.Ld, FetchType.L);
            _instructions[Opcode.Ld_C_XHL] = new GeneralInstruction(WriteType.C, OpType.Ld, FetchType.XHL);
            _instructions[Opcode.Ld_C_A] = new GeneralInstruction(WriteType.C, OpType.Ld, FetchType.A);

            _instructions[Opcode.Ld_A_XC] = new GeneralInstruction(WriteType.A, OpType.Ld, FetchType.XC);
            _instructions[Opcode.Ld_XC_A] = new GeneralInstruction(WriteType.XC, OpType.Ld, FetchType.A);
        }

        private static (byte, ushort) Load(Processor processor, ushort value)
        {
            return (4, value);
        }

    }
}
