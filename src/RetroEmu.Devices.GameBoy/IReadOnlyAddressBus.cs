namespace RetroEmu.Devices.GameBoy;

internal interface IReadOnlyAddressBus
{
    byte Read(ushort address);
}