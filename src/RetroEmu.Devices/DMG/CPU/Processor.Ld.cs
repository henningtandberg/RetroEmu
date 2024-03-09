namespace RetroEmu.Devices.DMG.CPU
{
    public unsafe partial class Processor
    {
        private void SetupLdInstructions()
        {
            _ops[(int)OpType.Ld] = &Load;

            _instructions[OPC.Ld_B_N8] = new Instruction(WriteType.RegB, OpType.Ld, FetchType.ImmediateValue); // TODO: Doublecheck that the manual is wrong
            _instructions[OPC.Ld_C_N8] = new Instruction(WriteType.RegC, OpType.Ld, FetchType.ImmediateValue); // TODO: Doublecheck that the manual is wrong
            _instructions[OPC.Ld_D_N8] = new Instruction(WriteType.RegD, OpType.Ld, FetchType.ImmediateValue); // TODO: Doublecheck that the manual is wrong
            _instructions[OPC.Ld_E_N8] = new Instruction(WriteType.RegE, OpType.Ld, FetchType.ImmediateValue); // TODO: Doublecheck that the manual is wrong
            _instructions[OPC.Ld_H_N8] = new Instruction(WriteType.RegH, OpType.Ld, FetchType.ImmediateValue); // TODO: Doublecheck that the manual is wrong
            _instructions[OPC.Ld_L_N8] = new Instruction(WriteType.RegL, OpType.Ld, FetchType.ImmediateValue); // TODO: Doublecheck that the manual is wrong

            _instructions[OPC.Ld_A_A] = new Instruction(WriteType.RegA, OpType.Ld, FetchType.RegA);
            _instructions[OPC.Ld_A_B] = new Instruction(WriteType.RegA, OpType.Ld, FetchType.RegB);
            _instructions[OPC.Ld_A_C] = new Instruction(WriteType.RegA, OpType.Ld, FetchType.RegC);
            _instructions[OPC.Ld_A_D] = new Instruction(WriteType.RegA, OpType.Ld, FetchType.RegD);
            _instructions[OPC.Ld_A_E] = new Instruction(WriteType.RegA, OpType.Ld, FetchType.RegE);
            _instructions[OPC.Ld_A_H] = new Instruction(WriteType.RegA, OpType.Ld, FetchType.RegH);
            _instructions[OPC.Ld_A_L] = new Instruction(WriteType.RegA, OpType.Ld, FetchType.RegL);
            _instructions[OPC.Ld_A_XHL] = new Instruction(WriteType.RegA, OpType.Ld, FetchType.AddressHL);

            _instructions[OPC.Ld_B_B] = new Instruction(WriteType.RegB, OpType.Ld, FetchType.RegB);
            _instructions[OPC.Ld_B_C] = new Instruction(WriteType.RegB, OpType.Ld, FetchType.RegC);
            _instructions[OPC.Ld_B_D] = new Instruction(WriteType.RegB, OpType.Ld, FetchType.RegD);
            _instructions[OPC.Ld_B_E] = new Instruction(WriteType.RegB, OpType.Ld, FetchType.RegE);
            _instructions[OPC.Ld_B_H] = new Instruction(WriteType.RegB, OpType.Ld, FetchType.RegH);
            _instructions[OPC.Ld_B_L] = new Instruction(WriteType.RegB, OpType.Ld, FetchType.RegL);
            _instructions[OPC.Ld_B_XHL] = new Instruction(WriteType.RegB, OpType.Ld, FetchType.AddressHL);

            _instructions[OPC.Ld_C_B] = new Instruction(WriteType.RegC, OpType.Ld, FetchType.RegB);
            _instructions[OPC.Ld_C_C] = new Instruction(WriteType.RegC, OpType.Ld, FetchType.RegC);
            _instructions[OPC.Ld_C_D] = new Instruction(WriteType.RegC, OpType.Ld, FetchType.RegD);
            _instructions[OPC.Ld_C_E] = new Instruction(WriteType.RegC, OpType.Ld, FetchType.RegE);
            _instructions[OPC.Ld_C_H] = new Instruction(WriteType.RegC, OpType.Ld, FetchType.RegH);
            _instructions[OPC.Ld_C_L] = new Instruction(WriteType.RegC, OpType.Ld, FetchType.RegL);
            _instructions[OPC.Ld_C_XHL] = new Instruction(WriteType.RegC, OpType.Ld, FetchType.AddressHL);
            _instructions[OPC.Ld_C_A] = new Instruction(WriteType.RegC, OpType.Ld, FetchType.RegA);

            _instructions[OPC.Ld_A_XC] = new Instruction(WriteType.RegA, OpType.Ld, FetchType.Address_RegC_0xFF00);
            _instructions[OPC.Ld_XC_A] = new Instruction(WriteType.Address_RegC_0xFF00, OpType.Ld, FetchType.RegA);
        }

        private static (byte, ushort) Load(Processor processor, ushort value)
        {
            return (4, value);
        }

    }
}
