namespace RetroEmu.Devices.DMG.CPU;

public partial class Processor
{
	private (ushort, ushort) RotateLeftThroughCarry(byte input)
	{
		var lsbMask = (input & 0x80) > 0 ? 0x01 : 0;

		var result = (input << 1) | lsbMask;

		SetFlagToValue(Flag.Zero, result == 0);
        ClearFlag(Flag.Subtract);
		ClearFlag(Flag.HalfCarry);
        SetFlagToValue(Flag.Carry, lsbMask != 0);

        return ((ushort)result, 4);
	}
		
	private (ushort, ushort) RotateRightThroughCarry(byte input)
	{
		var msbMask = IsSet(Flag.Carry) ? 0x80 : 0;
			
		if ((input & 0x01) > 0)
		{
			SetFlag(Flag.Carry);
		}
		else
		{
			ClearFlag(Flag.Carry);
		}

		var result = (input >> 1) | msbMask;

		if (result == 0)
		{
			SetFlag(Flag.Zero);
		}
			
		ClearFlag(Flag.Subtract);
		ClearFlag(Flag.HalfCarry);
			
		return ((ushort)result, 4);
	}
}