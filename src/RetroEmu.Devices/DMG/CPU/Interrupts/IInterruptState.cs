namespace RetroEmu.Devices.DMG.CPU.Interrupts;

public interface IInterruptState
{
    public bool InterruptMasterEnable { get; set; }
    public byte InterruptEnable { get; set; }
    public byte InterruptFlag { get; set; }
    public byte DisableInterruptCounter { get; set; }
    public byte EnableInterruptCounter { get; set; }
        
    public bool IsInterruptMasterEnabled();
    public void SetInterruptMasterEnable(bool value);
    
    public ushort GetInterruptStartingAddress(InterruptType type);
    public void SetInterruptEnable(InterruptType type, bool value);
    public void GenerateInterrupt(InterruptType type);
    public void Update();
    public byte GetSelectedInterrupt();
    public void ResetInterruptFlag(byte selectedInterrupt);
    public bool InterruptMasterEnableIsDisabledAndThereIsAPendingInterrupt();
}