namespace RetroEmu.Devices.DMG.CPU;

public partial class Processor
{
    private (ushort, ushort) RotateLeft(byte input)
    {
        var newCarry = (input & 0x80) > 0;
        var lsbMask = newCarry ? 0x01 : 0x00;

        var result = (input << 1) | lsbMask;

        SetFlagToValue(Flag.Zero, result == 0);
        ClearFlag(Flag.Subtract);
        ClearFlag(Flag.HalfCarry);
        SetFlagToValue(Flag.Carry, newCarry);

        return ((ushort)result, 4);
    }

    private (ushort, ushort) RotateLeftThroughCarry(byte input)
    {
        var newCarry = (input & 0x80) > 0;
        var lsbMask = IsSet(Flag.Carry) ? 0x01 : 0x00;

        var result = (input << 1) | lsbMask;

		SetFlagToValue(Flag.Zero, result == 0);
        ClearFlag(Flag.Subtract);
		ClearFlag(Flag.HalfCarry);
        SetFlagToValue(Flag.Carry, newCarry);

        return ((ushort)result, 4);
	}

    private (ushort, ushort) RotateRight(byte input)
    {
        var newCarry = (input & 0x01) > 0;
        var msbMask = newCarry ? 0x80 : 0x00;

        var result = (input >> 1) | msbMask;

        SetFlagToValue(Flag.Zero, result == 0);
        ClearFlag(Flag.Subtract);
        ClearFlag(Flag.HalfCarry);
        SetFlagToValue(Flag.Carry, newCarry);


        return ((ushort)result, 4);
    }

    private (ushort, ushort) RotateRightThroughCarry(byte input)
    {
        var newCarry = (input & 0x01) > 0;
        var msbMask = IsSet(Flag.Carry) ? 0x80 : 0x00;

        var result = (input >> 1) | msbMask;

        SetFlagToValue(Flag.Zero, result == 0);
        ClearFlag(Flag.Subtract);
		ClearFlag(Flag.HalfCarry);
        SetFlagToValue(Flag.Carry, newCarry);


        return ((ushort)result, 4);
	}
}