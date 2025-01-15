namespace RetroEmu.Devices.DMG.CPU;

public unsafe partial class Processor
{
    private (ushort, ushort) Return(ushort _)
    {
        var (_, nextInstruction) = Pop16FromStack();
        return (nextInstruction, 8);
    }
    
    private (ushort, ushort) ReturnConditionally(ushort input, bool condition)
    {
        if (!condition)
            return (input, 8);
            
        var (popCycles, nextInstruction) = Pop16FromStack();

        return (nextInstruction, (ushort)(popCycles + 8));
    }
}