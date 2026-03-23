namespace RetroEmu.Devices.GameBoy.Memory;

internal interface IReadOnlyAddressBus
{
    byte Read(ushort address);
}