namespace RetroEmu.Devices.DMG.CPU;

public partial class Processor
{
    private (ushort, ushort) Ld(ushort input)
    {
        return (input, 4);
    }
}