namespace RetroEmu.Devices.DMG.CPU;

public unsafe partial class Processor
{
	private (ushort, ushort) And(ushort input)
	{
		var registerA = *Registers.A;
		var result = (int)registerA & (int)input;

		SetFlagToValue(Flag.Zero, result == 0);
		ClearFlag(Flag.Subtract);
		SetFlag(Flag.HalfCarry);
		ClearFlag(Flag.Carry);

		return ((ushort)result, 4);
	}
}