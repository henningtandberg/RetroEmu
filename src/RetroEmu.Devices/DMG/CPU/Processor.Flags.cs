namespace RetroEmu.Devices.DMG.CPU;

public partial class Processor
{
    public bool IsSet(Flag flag)
    {
        return (Registers.F & (byte)flag) > 0;
    }
        
    private void ToggleFlag(Flag flag)
    {
        Registers.F ^= (byte)flag;
    }

    public void SetFlag(Flag flag)
    {
        Registers.F |= (byte)flag;
    }
        
    public void ClearFlag(Flag flag)
    {
        Registers.F &= (byte)~flag;
    }

    public void SetFlagToValue(Flag flag, bool value)
    {
        // Set flag to value, true is "set", false is "clear"
        if (value)
        {
            SetFlag(flag);
        }
        else
        {
            ClearFlag(flag);
        }
    }

    private void ClearAllFlags()
    {
        Registers.F &= 0x00;
    }
}