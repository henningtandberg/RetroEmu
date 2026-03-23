namespace RetroEmu.Devices.GameBoy.Memory;

public interface IInternalRam
{
    public void Reset();
    public byte Read(ushort address);
    public void Write(ushort address, byte value);
}