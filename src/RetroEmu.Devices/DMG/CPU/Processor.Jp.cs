namespace RetroEmu.Devices.DMG.CPU
{
    public unsafe partial class Processor
    {
        private void SetupJpInstructions()
        {
            _ops[(int)OpType.JpNZ] = &JumpNZ;

            _instructions[Opcode.JpNZ_N16] = new Instruction(WriteType.PC, OpType.JpNZ, FetchType.N16);

        }

        private static (byte, ushort) JumpNZ(Processor processor, ushort value)
        {
            var new_pc = *processor.Registers.PC;
            if (!processor.IsSet(Flag.Zero))
            {
                new_pc = value;
            }
            return (4, new_pc);
        }

    }
}
