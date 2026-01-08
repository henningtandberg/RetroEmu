namespace RetroEmu.Devices.DMG.CPU.Link;

public readonly record struct Data(byte SerialByte, int ClockSpeedHz);

public interface IWire
{
    public bool HasData();
    public void Write(Data data);
    public Data Read();
}