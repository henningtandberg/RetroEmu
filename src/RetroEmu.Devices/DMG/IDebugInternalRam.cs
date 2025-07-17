namespace RetroEmu.Devices.DMG;

public interface IDebugInternalRam
{
    public byte[] GetWorkRam();
    public byte[] GetHighRam();
}