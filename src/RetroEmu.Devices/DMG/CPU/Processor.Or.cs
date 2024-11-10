namespace RetroEmu.Devices.DMG.CPU;

public unsafe partial class Processor
{
	private (ushort, ushort) Or(ushort input)
	{
		var registerA = *Registers.A;
		var result = registerA | input;

		if (result == 0)
		{
			SetFlag(Flag.Zero);
		}
		else
		{
			ClearFlag(Flag.Zero);
		}

		ClearFlag(Flag.Subtract);
		ClearFlag(Flag.HalfCarry);
		ClearFlag(Flag.Carry);

		return ((ushort)result, 4);
	}
}