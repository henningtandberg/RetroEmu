namespace RetroEmu.Devices.DMG.CPU;

public partial class Processor
{
    private (ushort, ushort) RestartAtGivenAddress(ushort address)
    {
        Push16ToStack(Registers.PC);
        return new(address, 32);
    }
}