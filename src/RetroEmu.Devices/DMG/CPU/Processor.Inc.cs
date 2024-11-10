namespace RetroEmu.Devices.DMG.CPU;

public partial class Processor
{

    private (ushort, ushort) Inc(ushort input)
    {
        var result = input + 1;

        if ((result & 0x10) != 0x00)
        {
            SetFlag(Flag.HalfCarry);
        }
        else
        {
            ClearFlag(Flag.HalfCarry);
        }

        SetFlag(Flag.Subtract);

        if ((byte)result == 0)
        {
            SetFlag(Flag.Zero);
        }
        else
        {
            ClearFlag(Flag.Zero);
        }

        return ((ushort)result, 4);
    }

    private (ushort, ushort) Inc16(ushort input)
    {
        var result = input + 1;

        return ((ushort)result, 8);
    }
}