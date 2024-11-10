namespace RetroEmu.Devices.DMG.CPU;

public unsafe partial class Processor
{
    private (ushort, ushort) Sub(ushort input)
    {
        var registerA = *Registers.A;
        var result = (int)registerA - (int)input;

        SetFlagToValue(Flag.Zero, result == 0);
        SetFlag(Flag.Subtract);
        SetFlagToValue(Flag.HalfCarry, (registerA & 0x0F) < (input & 0x0F));
        SetFlagToValue(Flag.Carry, result < 0);

        return ((ushort)result, 4);
    }
}