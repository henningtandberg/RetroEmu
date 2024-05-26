using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU
{
	public partial class Processor
	{
		private void SetupDaaInstruction()
		{
            _instructions[Opcode.Daa] = new ALUInstruction(WriteType.A, ALUOpType.Daa, FetchType.A);
        }

		private (ushort, ushort) Daa(ushort input)
		{
			var digit1 = input % 10;
			var digit2 = input / 10 % 10;
			var digit3 = input / 100 % 10;

			var result = digit1 | (digit2 << 4);

			if (result == 0)
			{
				SetFlag(Flag.Zero);
			}

			if (digit3 != 0)
			{
				SetFlag(Flag.Carry);
			}
			else
			{
				ClearFlag(Flag.Carry);
			}
			
            ClearFlag(Flag.HalfCarry);

			return ((ushort)result, 4);
		}
    }
}