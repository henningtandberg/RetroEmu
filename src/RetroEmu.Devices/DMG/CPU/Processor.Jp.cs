namespace RetroEmu.Devices.DMG.CPU;

public unsafe partial class Processor
{
    private (ushort, ushort) Jump(ushort input)
    {
        return new (input, 8);
    }
    
    private (ushort, ushort) JumpConditionally(ushort input, bool condition)
    {
        return condition
            ? Jump(input)
            : new (*Registers.PC, 4);
    }
    
    private (ushort, ushort) JumpRelative(ushort input)
    {
        var target = *Registers.PC + (sbyte)input;
        return new ((ushort)target, 8);
    }

    private (ushort, ushort) JumpRelativeConditionally(ushort input, bool condition)
    {
        return condition
            ? JumpRelative(input)
            : new (*Registers.PC, 4);
    }
}