namespace RetroEmu.Devices.DMG.CPU
{
    public unsafe partial class Processor
    {
        private void SetupLdInstructions()
        {
            _ops[(int)OpType.Ld] = &Load;

            _instructions[OPC.Ld_B_N8] = new Instruction(FetchType.ImmediateValue, OpType.Ld, WriteType.RegB); // TODO: Doublecheck that the manual is wrong
            _instructions[OPC.Ld_C_N8] = new Instruction(FetchType.ImmediateValue, OpType.Ld, WriteType.RegC); // TODO: Doublecheck that the manual is wrong
            _instructions[OPC.Ld_D_N8] = new Instruction(FetchType.ImmediateValue, OpType.Ld, WriteType.RegD); // TODO: Doublecheck that the manual is wrong
            _instructions[OPC.Ld_E_N8] = new Instruction(FetchType.ImmediateValue, OpType.Ld, WriteType.RegE); // TODO: Doublecheck that the manual is wrong
            _instructions[OPC.Ld_H_N8] = new Instruction(FetchType.ImmediateValue, OpType.Ld, WriteType.RegH); // TODO: Doublecheck that the manual is wrong
            _instructions[OPC.Ld_L_N8] = new Instruction(FetchType.ImmediateValue, OpType.Ld, WriteType.RegL); // TODO: Doublecheck that the manual is wrong

            _instructions[OPC.Ld_A_A] = new Instruction(FetchType.RegA, OpType.Ld, WriteType.RegA);
            _instructions[OPC.Ld_A_B] = new Instruction(FetchType.RegB, OpType.Ld, WriteType.RegA);
            _instructions[OPC.Ld_A_C] = new Instruction(FetchType.RegC, OpType.Ld, WriteType.RegA);
            _instructions[OPC.Ld_A_D] = new Instruction(FetchType.RegD, OpType.Ld, WriteType.RegA);
            _instructions[OPC.Ld_A_E] = new Instruction(FetchType.RegE, OpType.Ld, WriteType.RegA);
            _instructions[OPC.Ld_A_H] = new Instruction(FetchType.RegH, OpType.Ld, WriteType.RegA);
            _instructions[OPC.Ld_A_L] = new Instruction(FetchType.RegL, OpType.Ld, WriteType.RegA);
            _instructions[OPC.Ld_A_XHL] = new Instruction(FetchType.AddressHL, OpType.Ld, WriteType.RegA);

            _instructions[OPC.Ld_B_B] = new Instruction(FetchType.RegB, OpType.Ld, WriteType.RegB);
            _instructions[OPC.Ld_B_C] = new Instruction(FetchType.RegC, OpType.Ld, WriteType.RegB);
            _instructions[OPC.Ld_B_D] = new Instruction(FetchType.RegD, OpType.Ld, WriteType.RegB);
            _instructions[OPC.Ld_B_E] = new Instruction(FetchType.RegE, OpType.Ld, WriteType.RegB);
            _instructions[OPC.Ld_B_H] = new Instruction(FetchType.RegH, OpType.Ld, WriteType.RegB);
            _instructions[OPC.Ld_B_L] = new Instruction(FetchType.RegL, OpType.Ld, WriteType.RegB);
            _instructions[OPC.Ld_B_XHL] = new Instruction(FetchType.AddressHL, OpType.Ld, WriteType.RegB);

            _instructions[OPC.Ld_C_B] = new Instruction(FetchType.RegB, OpType.Ld, WriteType.RegC);
            _instructions[OPC.Ld_C_C] = new Instruction(FetchType.RegC, OpType.Ld, WriteType.RegC);
            _instructions[OPC.Ld_C_D] = new Instruction(FetchType.RegD, OpType.Ld, WriteType.RegC);
            _instructions[OPC.Ld_C_E] = new Instruction(FetchType.RegE, OpType.Ld, WriteType.RegC);
            _instructions[OPC.Ld_C_H] = new Instruction(FetchType.RegH, OpType.Ld, WriteType.RegC);
            _instructions[OPC.Ld_C_L] = new Instruction(FetchType.RegL, OpType.Ld, WriteType.RegC);
            _instructions[OPC.Ld_C_XHL] = new Instruction(FetchType.AddressHL, OpType.Ld, WriteType.RegC);
            _instructions[OPC.Ld_C_A] = new Instruction(FetchType.RegA, OpType.Ld, WriteType.RegC);

            _instructions[OPC.Ld_A_XC] = new Instruction(FetchType.Address_RegC_0xFF00, OpType.Ld, WriteType.RegA);
            _instructions[OPC.Ld_XC_A] = new Instruction(FetchType.RegA, OpType.Ld, WriteType.Address_RegC_0xFF00);
        }

        private static (byte, ushort) Load(Processor processor, ushort value)
        {
            return (4, value);
        }

    }
}
