using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU;

public unsafe partial class Processor
{
	private (ushort, ushort) Adc(ushort input)
	{
		var carry = IsSet(Flag.Carry) ? 1 : 0;
		var registerA = *Registers.A;
		var result = (int)registerA + (int)input + (int)carry;

		if (result > 0xFF)
		{
			SetFlag(Flag.Carry);
		}
		else
		{
			ClearFlag(Flag.Carry);
		}

		if (result > 0x0F)
		{
			SetFlag(Flag.HalfCarry);
		}
		else
		{
			ClearFlag(Flag.HalfCarry);
		}

		ClearFlag(Flag.Subtract);

		if (result == 0)
		{
			SetFlag(Flag.Zero);
		}
		else
		{
			ClearFlag(Flag.Zero);
		}

		*Registers.A = (byte)result;
		return ((ushort)result, 4);
	}
}