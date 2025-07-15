using RetroEmu.Devices.DMG.CPU;

namespace RetroEmu.Devices.DMG;

public interface IDebugProcessor : IProcessor
{
    public Registers GetRegisters();
    
    public bool CarryFlagIsSet();
    public bool HalfCarryFlagIsSet(); 
    public bool SubtractFlagIsSet();
    public bool ZeroFlagIsSet();
}