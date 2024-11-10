namespace RetroEmu.Devices.DMG.CPU;

public partial class Processor
{
    private (ushort, ushort) Dec(ushort input)
    {
        var result = (int)input - 1;

        if (result > 0xFF)
        {
            SetFlag(Flag.Carry);
        }
        else
        {
            ClearFlag(Flag.Carry);
        }

        if (result == 0x0F)
        {
            SetFlag(Flag.HalfCarry);
        }
        else
        {
            ClearFlag(Flag.HalfCarry);
        }

        SetFlag(Flag.Subtract);

        if (result == 0)
        {
            SetFlag(Flag.Zero);
        }
        else
        {
            ClearFlag(Flag.Zero);
        }

        return ((ushort)result, 4);
    }

    private (ushort, ushort) Dec16(ushort input)
    {
        var result = (int)input - 1;

        return ((ushort)result, 8);
    }
}