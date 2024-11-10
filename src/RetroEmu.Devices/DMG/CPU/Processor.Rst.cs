namespace RetroEmu.Devices.DMG.CPU;

public unsafe partial class Processor
{
    public (ushort, ushort) RestartAtGivenAddress(ushort address)
    {
        Push16ToStack(*Registers.PC);
        return new(address, 32);
    }
}