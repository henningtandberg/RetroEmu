namespace RetroEmu.Devices.DMG.CPU;

public unsafe partial class Processor
{
    private (ushort, ushort) ReturnConditionally(ushort input, bool condition)
    {
        if (!condition)
            return new(*Registers.PC, 4);
            
        var (_, nextInstruction) = Pop16FromStack();

        return new(nextInstruction, 8);
    }
}