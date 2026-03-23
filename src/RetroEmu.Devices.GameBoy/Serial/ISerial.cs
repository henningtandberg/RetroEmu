namespace RetroEmu.Devices.GameBoy.Serial;

public interface ISerial
{
    public byte SerialControl { get; set; }
    public byte SerialByte { get; set; }

    public void Reset();
    public void Update(int cycles);
}