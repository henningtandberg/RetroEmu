namespace RetroEmu.Devices.GameBoy.Timer;

public interface ITimer
{
    public byte Divider { get; set; }
    public byte Counter { get; set; }
    public byte Modulo { get; set; }
    public byte Control { get; set; }
    
    public void Update(int cycles);
}