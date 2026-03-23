namespace RetroEmu.Devices.GameBoy.Memory;

public interface IDebugInternalRam
{
    public byte[] GetWorkRam();
    public byte[] GetHighRam();
}