namespace RetroEmu.Devices.DMG.CPU.Link;

public interface ISerial
{
    public byte SerialControl { get; set; }
    public byte SerialByte { get; set; }

    public void Reset();
    public void Update(int cycles);
}