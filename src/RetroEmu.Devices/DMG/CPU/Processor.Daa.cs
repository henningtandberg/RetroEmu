namespace RetroEmu.Devices.DMG.CPU;

public partial class Processor
{
	private (ushort, ushort) Daa(ushort input)
	{
		var digit1 = input % 10;
		var digit2 = input / 10 % 10;
		var digit3 = input / 100 % 10;

		var result = digit1 | (digit2 << 4);

		SetFlagToValue(Flag.Zero, result == 0);
		SetFlagToValue(Flag.Carry, digit3 != 0);
		ClearFlag(Flag.HalfCarry);

		return ((ushort)result, 4);
	}
}