namespace RetroEmu.Devices.DMG.CPU;

public unsafe partial class Processor
{
    private (ushort, ushort) Sub(ushort input)
    {
        var registerA = *Registers.A;
        var result = (int)registerA - (int)input;

        if (result == 0)
        {
            SetFlag(Flag.Zero);
        }
        else
        {
            ClearFlag(Flag.Zero);
        }

        SetFlag(Flag.Subtract);

        if ((registerA & 0x0F) - (input & 0x0F) < 0) // TODO: Doublecheck if this is correct
        {
            SetFlag(Flag.HalfCarry);
        }
        else
        {
            ClearFlag(Flag.HalfCarry);
        }

        if (result < 0)
        {
            SetFlag(Flag.Carry);
        }
        else
        {
            ClearFlag(Flag.Carry);
        }

        return ((ushort)result, 4);
    }
}