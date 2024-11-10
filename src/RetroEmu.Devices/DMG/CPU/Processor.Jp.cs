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
        var target = *Registers.PC + input;
        return JumpConditionally((ushort)target, condition);
    }
}