namespace RetroEmu.Devices.DMG.CPU;

public partial class Processor
{

    private (ushort, ushort) Inc(ushort input)
    {
        var result = input + 1;

        SetFlagToValue(Flag.Zero, (byte)result == 0);
        ClearFlag(Flag.Subtract);
        SetFlagToValue(Flag.HalfCarry, (result & 0x10) != 0x00);

        return ((ushort)result, 4);
    }

    private (ushort, ushort) Inc16(ushort input)
    {
        var result = input + 1;

        return ((ushort)result, 8);
    }
}