namespace RetroEmu.Devices.DMG.CPU;

public unsafe partial class Processor
{
    private (ushort, ushort) JumpConditionally(ushort input, bool condition)
    {
        return condition
            ? new (input, 8)
            : new (*Registers.PC, 4);
    }

    private (ushort, ushort) JumpRelativeConditionally(ushort input, bool condition)
    {
        sbyte jmp = (sbyte)input;
        var target = *Registers.PC + jmp;
        return JumpConditionally((ushort)target, condition);
    }
}