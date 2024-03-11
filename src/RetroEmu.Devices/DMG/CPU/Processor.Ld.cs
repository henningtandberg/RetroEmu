using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU
{
    public unsafe partial class Processor
    {
        private void SetupLdInstructions()
        {
            _ops[(int)OpType.Ld] = &Load;

            _instructions[Opcode.Ld_B_N8] = new Instruction(WriteType.B, OpType.Ld, FetchType.N8); // TODO: Doublecheck that the manual is wrong
            _instructions[Opcode.Ld_C_N8] = new Instruction(WriteType.C, OpType.Ld, FetchType.N8); // TODO: Doublecheck that the manual is wrong
            _instructions[Opcode.Ld_D_N8] = new Instruction(WriteType.D, OpType.Ld, FetchType.N8); // TODO: Doublecheck that the manual is wrong
            _instructions[Opcode.Ld_E_N8] = new Instruction(WriteType.E, OpType.Ld, FetchType.N8); // TODO: Doublecheck that the manual is wrong
            _instructions[Opcode.Ld_H_N8] = new Instruction(WriteType.H, OpType.Ld, FetchType.N8); // TODO: Doublecheck that the manual is wrong
            _instructions[Opcode.Ld_L_N8] = new Instruction(WriteType.L, OpType.Ld, FetchType.N8); // TODO: Doublecheck that the manual is wrong

            _instructions[Opcode.Ld_A_A] = new Instruction(WriteType.A, OpType.Ld, FetchType.A);
            _instructions[Opcode.Ld_A_B] = new Instruction(WriteType.A, OpType.Ld, FetchType.B);
            _instructions[Opcode.Ld_A_C] = new Instruction(WriteType.A, OpType.Ld, FetchType.C);
            _instructions[Opcode.Ld_A_D] = new Instruction(WriteType.A, OpType.Ld, FetchType.D);
            _instructions[Opcode.Ld_A_E] = new Instruction(WriteType.A, OpType.Ld, FetchType.E);
            _instructions[Opcode.Ld_A_H] = new Instruction(WriteType.A, OpType.Ld, FetchType.H);
            _instructions[Opcode.Ld_A_L] = new Instruction(WriteType.A, OpType.Ld, FetchType.L);
            _instructions[Opcode.Ld_A_XHL] = new Instruction(WriteType.A, OpType.Ld, FetchType.XHL);

            _instructions[Opcode.Ld_B_B] = new Instruction(WriteType.B, OpType.Ld, FetchType.B);
            _instructions[Opcode.Ld_B_C] = new Instruction(WriteType.B, OpType.Ld, FetchType.C);
            _instructions[Opcode.Ld_B_D] = new Instruction(WriteType.B, OpType.Ld, FetchType.D);
            _instructions[Opcode.Ld_B_E] = new Instruction(WriteType.B, OpType.Ld, FetchType.E);
            _instructions[Opcode.Ld_B_H] = new Instruction(WriteType.B, OpType.Ld, FetchType.H);
            _instructions[Opcode.Ld_B_L] = new Instruction(WriteType.B, OpType.Ld, FetchType.L);
            _instructions[Opcode.Ld_B_XHL] = new Instruction(WriteType.B, OpType.Ld, FetchType.XHL);

            _instructions[Opcode.Ld_C_B] = new Instruction(WriteType.C, OpType.Ld, FetchType.B);
            _instructions[Opcode.Ld_C_C] = new Instruction(WriteType.C, OpType.Ld, FetchType.C);
            _instructions[Opcode.Ld_C_D] = new Instruction(WriteType.C, OpType.Ld, FetchType.D);
            _instructions[Opcode.Ld_C_E] = new Instruction(WriteType.C, OpType.Ld, FetchType.E);
            _instructions[Opcode.Ld_C_H] = new Instruction(WriteType.C, OpType.Ld, FetchType.H);
            _instructions[Opcode.Ld_C_L] = new Instruction(WriteType.C, OpType.Ld, FetchType.L);
            _instructions[Opcode.Ld_C_XHL] = new Instruction(WriteType.C, OpType.Ld, FetchType.XHL);
            _instructions[Opcode.Ld_C_A] = new Instruction(WriteType.C, OpType.Ld, FetchType.A);

            _instructions[Opcode.Ld_A_XC] = new Instruction(WriteType.A, OpType.Ld, FetchType.XC);
            _instructions[Opcode.Ld_XC_A] = new Instruction(WriteType.XC, OpType.Ld, FetchType.A);
        }

        private static OperationOutput Load(Processor processor, IOperationInput operationInput) => Load(operationInput);
        private static OperationOutput Load(IOperationInput operationInput)
        {
            return new OperationOutput(operationInput.Value, 4);
        }

    }
}
