namespace RetroEmu.Devices.DMG.CPU;

public partial class Processor
{
    private (ushort, ushort) Call(ushort input)
    {
        var nextInstruction = Registers.PC;
        Push16ToStack(nextInstruction);

        return new(input, 16);
    }

    private (ushort, ushort) CallConditionally(ushort input, bool condition)
    {
        return condition
            ? Call(input)
            : new(Registers.PC, 4);
    }
}