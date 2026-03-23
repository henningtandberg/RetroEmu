namespace RetroEmu.Devices.GameBoy.CPU;

public interface IDebugProcessor
{
    public Registers GetRegisters();
    
    public bool CarryFlagIsSet();
    public bool HalfCarryFlagIsSet(); 
    public bool SubtractFlagIsSet();
    public bool ZeroFlagIsSet();

    public bool GetInterruptMasterEnable();
    public byte GetInterruptEnable();
    public byte GetInterruptFlag();

    public byte GetLCDC();
}