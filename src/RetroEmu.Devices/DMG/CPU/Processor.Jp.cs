namespace RetroEmu.Devices.DMG.CPU
{
    public unsafe partial class Processor
    {
        private void SetupJpInstructions()
        {
            _ops[(int)OpType.JpNZ] = &JumpNZ;

            _instructions[OPC.JpNZ_N16] = new Instruction(WriteType.RegPC, OpType.JpNZ, FetchType.ImmediateValue16);

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
