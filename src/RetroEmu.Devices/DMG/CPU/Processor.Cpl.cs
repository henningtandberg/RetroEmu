namespace RetroEmu.Devices.DMG.CPU;

public partial class Processor
{
	private (ushort, ushort) Cpl(ushort input)
	{
		var result = ~input & 0xFF;

		SetFlag(Flag.Subtract);
		SetFlag(Flag.HalfCarry);

		return ((ushort)result, 4);
	}
}