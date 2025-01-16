namespace RetroEmu.Devices.DMG.CPU;

public partial class Processor
{
	private (ushort, ushort) Xor(ushort input)
	{
		var registerA = Registers.A;
		var result = registerA ^ input;

		SetFlagToValue(Flag.Zero, result == 0);
		ClearFlag(Flag.Subtract);
		ClearFlag(Flag.HalfCarry);
		ClearFlag(Flag.Carry);

		return ((ushort)result, 4);
	}
}