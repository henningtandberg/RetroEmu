namespace RetroEmu.Devices.GameBoy;

public interface IDebugInternalRam
{
    public byte[] GetWorkRam();
    public byte[] GetHighRam();
}