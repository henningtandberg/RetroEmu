namespace RetroEmu.Devices.DMG;

internal interface IReadOnlyAddressBus
{
    byte Read(ushort address);
}