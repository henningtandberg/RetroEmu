namespace RetroEmu.Devices.GameBoy.Serial;

public readonly record struct Data(byte SerialByte, int ClockSpeedHz);

public interface IWire
{
    public bool HasData();
    public void Write(Data data);
    public Data Read();
    public void Flush();
}