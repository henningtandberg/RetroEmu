namespace RetroEmu.Devices.DMG.CPU.Interrupts;

public interface IInterruptState
{
    public bool InterruptMasterEnable { get; set; }
    public byte InterruptEnable { get; set; }
    public byte InterruptFlag { get; set; }
    public byte DisableInterruptCounter { get; set; }
    public byte EnableInterruptCounter { get; set; }
        
    public ushort GetInterruptStartingAddress(InterruptType type);
}