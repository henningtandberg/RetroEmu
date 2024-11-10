namespace RetroEmu.Devices.DMG.CPU;

public unsafe partial class Processor
{
    private (ushort, ushort) Add(ushort input)
    {
        var registerA = *Registers.A;
        var result = registerA + input;

        if (result > 0xFF)
        {
            SetFlag(Flag.Carry);
        }
        else
        {
            ClearFlag(Flag.Carry);
        }

        if ((registerA & 0x0F) + (input & 0x0F) > 0x0F)
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

        return ((ushort)result, 4);
    }

    private (ushort, ushort) Add16(ushort input)
    {
        var registerHL = *Registers.HL;
        var result = (int)registerHL + (int)input;

        if (result > 0xFFFF)
        {
            SetFlag(Flag.Carry);
        }
        else
        {
            ClearFlag(Flag.Carry);
        }

        if (result > 0x0FFF)
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

        return ((ushort)result, 8);
    }

    private (ushort, ushort) AddSP(ushort input)
    {
        var registerSP = *Registers.SP;
        var result = (int)registerSP + (int)input;

        if (result > 0xFFFF) // Set or reset according to operation?
        {
            SetFlag(Flag.Carry);
        }
        else
        {
            ClearFlag(Flag.Carry);
        }

        if (result > 0x0FFF) // Set or reset according to operation?
        {
            SetFlag(Flag.HalfCarry);
        }
        else
        {
            ClearFlag(Flag.HalfCarry);
        }

        ClearFlag(Flag.Subtract);
        ClearFlag(Flag.Zero);

        return ((ushort)result, 12); // cycles (Not sure why this one is more expensive)
    }
}