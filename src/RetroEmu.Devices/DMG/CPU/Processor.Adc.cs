namespace RetroEmu.Devices.DMG.CPU;

public partial class Processor
{
	private (ushort, ushort) Adc(ushort input)
	{
		var carry = IsSet(Flag.Carry) ? 1 : 0;
		var registerA = Registers.A;
		var result = (int)registerA + (int)input + (int)carry;

		SetFlagToValue(Flag.Carry, result > 0xFF);
		SetFlagToValue(Flag.HalfCarry, result > 0x0F);
		ClearFlag(Flag.Subtract);
		SetFlagToValue(Flag.Zero, result == 0);

		Registers.A = (byte)result;
		return ((ushort)result, 4);
	}
}