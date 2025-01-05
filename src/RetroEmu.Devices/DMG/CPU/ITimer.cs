namespace RetroEmu.Devices.DMG.CPU;

public interface ITimer
{
    public byte Divider { get; set; }
    public byte Counter { get; set; }
    public byte Modulo { get; set; }
    public byte Control { get; set; }
    
    public void SetSpeed(int speed);
    public bool Update(int cycles);
}