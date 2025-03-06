namespace RetroEmu.Devices.DMG;

public interface IJoypad
{
    public byte P1 { get; set; }

    public void Update();
}