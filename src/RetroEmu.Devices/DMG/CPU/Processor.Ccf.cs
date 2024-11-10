namespace RetroEmu.Devices.DMG.CPU;

public partial class Processor
{
    private (ushort, ushort) Ccf(ushort _)
    {
        ClearFlag(Flag.Subtract);
        ClearFlag(Flag.HalfCarry);
        
        ToggleFlag(Flag.Carry);

        return (_, 4);
    }
}