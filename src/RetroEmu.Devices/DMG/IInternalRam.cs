namespace RetroEmu.Devices.DMG;

public interface IInternalRam
{
    public void Reset();
    public byte Read(ushort address);
    public void Write(ushort address, byte value);
}