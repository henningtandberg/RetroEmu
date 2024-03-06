namespace RetroEmu.Devices.DMG.CPU
{
    public unsafe partial class Processor
    {
        private void SetupLdInstructions()
        {
            _ops[(int)OpType.Ld] = &Load;

            _instructions[0x06] = new Instruction(FetchType.ImmediateValue, OpType.Ld, WriteType.RegB); // TODO: Doublecheck that the manual is wrong
            _instructions[0x0E] = new Instruction(FetchType.ImmediateValue, OpType.Ld, WriteType.RegC); // TODO: Doublecheck that the manual is wrong
            _instructions[0x16] = new Instruction(FetchType.ImmediateValue, OpType.Ld, WriteType.RegD); // TODO: Doublecheck that the manual is wrong
            _instructions[0x1E] = new Instruction(FetchType.ImmediateValue, OpType.Ld, WriteType.RegE); // TODO: Doublecheck that the manual is wrong
            _instructions[0x26] = new Instruction(FetchType.ImmediateValue, OpType.Ld, WriteType.RegH); // TODO: Doublecheck that the manual is wrong
            _instructions[0x2E] = new Instruction(FetchType.ImmediateValue, OpType.Ld, WriteType.RegL); // TODO: Doublecheck that the manual is wrong

            _instructions[0x7F] = new Instruction(FetchType.RegA, OpType.Ld, WriteType.RegA);
            _instructions[0x78] = new Instruction(FetchType.RegB, OpType.Ld, WriteType.RegA);
            _instructions[0x79] = new Instruction(FetchType.RegC, OpType.Ld, WriteType.RegA);
            _instructions[0x7A] = new Instruction(FetchType.RegD, OpType.Ld, WriteType.RegA);
            _instructions[0x7B] = new Instruction(FetchType.RegE, OpType.Ld, WriteType.RegA);
            _instructions[0x7C] = new Instruction(FetchType.RegH, OpType.Ld, WriteType.RegA);
            _instructions[0x7D] = new Instruction(FetchType.RegL, OpType.Ld, WriteType.RegA);
            _instructions[0x7E] = new Instruction(FetchType.AddressHL, OpType.Ld, WriteType.RegA);

            _instructions[0x40] = new Instruction(FetchType.RegB, OpType.Ld, WriteType.RegB);
            _instructions[0x41] = new Instruction(FetchType.RegC, OpType.Ld, WriteType.RegB);
            _instructions[0x42] = new Instruction(FetchType.RegD, OpType.Ld, WriteType.RegB);
            _instructions[0x43] = new Instruction(FetchType.RegE, OpType.Ld, WriteType.RegB);
            _instructions[0x44] = new Instruction(FetchType.RegH, OpType.Ld, WriteType.RegB);
            _instructions[0x45] = new Instruction(FetchType.RegL, OpType.Ld, WriteType.RegB);
            _instructions[0x46] = new Instruction(FetchType.AddressHL, OpType.Ld, WriteType.RegB);

            _instructions[0x48] = new Instruction(FetchType.RegB, OpType.Ld, WriteType.RegC);
            _instructions[0x49] = new Instruction(FetchType.RegC, OpType.Ld, WriteType.RegC);
            _instructions[0x4A] = new Instruction(FetchType.RegD, OpType.Ld, WriteType.RegC);
            _instructions[0x4B] = new Instruction(FetchType.RegE, OpType.Ld, WriteType.RegC);
            _instructions[0x4C] = new Instruction(FetchType.RegH, OpType.Ld, WriteType.RegC);
            _instructions[0x4D] = new Instruction(FetchType.RegL, OpType.Ld, WriteType.RegC);
            _instructions[0x4E] = new Instruction(FetchType.AddressHL, OpType.Ld, WriteType.RegC);

        }

        private static (byte, ushort) Load(Processor processor, ushort value)
        {
            return (4, value);
        }

    }
}
