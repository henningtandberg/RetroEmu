namespace RetroEmu.Devices.DMG.CPU;

public unsafe partial class Processor
{
	private (ushort, ushort) Cp(ushort input)
	{
		var registerA = *Registers.A;
			
		ClearAllFlags();
		SetFlag(Flag.Subtract);
			
		if (registerA - input == 0)
		{
			SetFlag(Flag.Zero);
		}
			
		if ((registerA & 0x0F) - (input & 0x0F) < 0) // TODO: Doublecheck if this is correct
		{
			SetFlag(Flag.HalfCarry);
		}

		if (registerA < input)
		{
			SetFlag(Flag.Carry);
		}

		return (0, 4);
	}
}