namespace RetroEmu.Devices.DMG.CPU.Instructions;

internal class NopInstruction : IInstruction
{
    public int Execute(Processor _)
    {
        return 4;
    }
}