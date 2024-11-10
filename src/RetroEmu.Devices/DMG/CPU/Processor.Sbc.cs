namespace RetroEmu.Devices.DMG.CPU;

public unsafe partial class Processor
{
    private (ushort, ushort) Sbc(ushort input)
    {
        var carry = IsSet(Flag.Carry) ? 1 : 0;
        var registerA = *Registers.A;
        var result = (int)registerA - (int)input - (int)carry;

        SetFlagToValue(Flag.Zero, result == 0);
        SetFlag(Flag.Subtract);
        SetFlagToValue(Flag.HalfCarry, (registerA & 0x0F) < (input & 0x0F)); // TODO: Doublecheck if this is correct
        SetFlagToValue(Flag.Carry, result < 0);

        return ((ushort)result, 4);
    }
}