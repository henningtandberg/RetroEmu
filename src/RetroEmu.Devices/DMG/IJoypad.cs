namespace RetroEmu.Devices.DMG;

public interface IJoypad
{
    public byte P1 { get; set; }

    public void PressButton(byte button);
    public void ReleaseButton(byte button);
    public void PressDPad(byte direction);
    public void ReleaseDPad(byte direction);
    public void Update();
}