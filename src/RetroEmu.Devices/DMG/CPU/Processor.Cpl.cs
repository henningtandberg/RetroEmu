using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU
{
	public partial class Processor
	{
		private void SetupCplInstruction()
		{
            _instructions[Opcode.Cpl] = new ALUInstruction(WriteType.A, ALUOpType.Cpl, FetchType.A);
        }

		private (ushort, ushort) Cpl(ushort input)
		{
			var result = ~input & 0xFF;

            SetFlag(Flag.Subtract);
            SetFlag(Flag.HalfCarry);

			return ((ushort)result, 4);
		}
    }
}