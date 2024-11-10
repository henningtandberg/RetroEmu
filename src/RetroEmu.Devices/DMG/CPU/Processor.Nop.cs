namespace RetroEmu.Devices.DMG.CPU;

public partial class Processor
{
    private (ushort, ushort) Nop(ushort _)
    {
        // Here we trust that the programmes have set the Fetch and Write to None!
        return (0, 4);
    }
}