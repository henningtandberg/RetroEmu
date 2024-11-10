namespace RetroEmu.Devices.DMG.CPU;

public unsafe partial class Processor
{
	private (ushort, ushort) Cp(ushort input)
	{
		var registerA = *Registers.A;

        SetFlagToValue(Flag.Zero, registerA == input);
        SetFlag(Flag.Subtract);
		SetFlagToValue(Flag.HalfCarry, (registerA & 0x0F) < (input & 0x0F)); // TODO: Doublecheck if this is correct
        SetFlagToValue(Flag.Carry, registerA < input);

		return (0, 4);
	}
}