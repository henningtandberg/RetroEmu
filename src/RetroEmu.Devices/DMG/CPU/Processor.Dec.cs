namespace RetroEmu.Devices.DMG.CPU;

public partial class Processor
{
    private (ushort, ushort) Dec(ushort input)
    {
        var result = (int)input - 1;

        SetFlagToValue(Flag.Zero, result == 0);
        SetFlag(Flag.Subtract);
        SetFlagToValue(Flag.HalfCarry, result == 0x0F);
        SetFlagToValue(Flag.Carry, result > 0xFF);

        return ((ushort)result, 4);
    }

    private (ushort, ushort) Dec16(ushort input)
    {
        var result = (int)input - 1;

        return ((ushort)result, 8);
    }
}