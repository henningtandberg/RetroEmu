namespace RetroEmu.Devices.DMG.CPU;

public unsafe partial class Processor
{

    private (ushort, ushort) CallConditionally(ushort input, bool condition)
    {
        var nextInstruction = *Registers.PC;

        if (!condition)
            return new(*Registers.PC, 4);
            
        Push16ToStack(nextInstruction);

        return new(input, 8);
    }
}