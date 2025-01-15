namespace RetroEmu.Devices.DMG.CPU;

public partial class Processor
{
    private (ushort, ushort) Pop(ushort input)
    {
        return (input, 4);
    }
}