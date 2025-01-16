namespace RetroEmu.Devices.DMG.CPU;

public partial class Processor
{
    private (ushort, ushort) Add(ushort input)
    {
        var registerA = Registers.A;
        var result = registerA + input;

        SetFlagToValue(Flag.Carry, result > 0xFF);
        SetFlagToValue(Flag.HalfCarry, (registerA & 0x0F) + (input & 0x0F) > 0x0F);
        ClearFlag(Flag.Subtract);
        SetFlagToValue(Flag.Zero, result == 0);

        return ((ushort)result, 4);
    }

    private (ushort, ushort) Add16(ushort input)
    {
        var registerHL = Registers.HL;
        var result = (int)registerHL + (int)input;

        SetFlagToValue(Flag.Carry, result > 0xFFFF);
        SetFlagToValue(Flag.HalfCarry, result > 0x0FFF);
        ClearFlag(Flag.Subtract);
        SetFlagToValue(Flag.Zero, result == 0);

        return ((ushort)result, 8);
    }

    private (ushort, ushort) AddSP(ushort input)
    {
        var registerSP = Registers.SP;
        var result = (int)registerSP + (int)input;

        SetFlagToValue(Flag.Carry, result > 0xFFFF); // Set or reset according to operation?
        SetFlagToValue(Flag.HalfCarry, result > 0x0FFF); // Set or reset according to operation?
        ClearFlag(Flag.Subtract);
        ClearFlag(Flag.Zero);

        return ((ushort)result, 12); // cycles (Not sure why this one is more expensive)
    }
}