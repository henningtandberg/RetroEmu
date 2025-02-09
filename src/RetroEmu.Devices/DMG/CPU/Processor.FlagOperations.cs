namespace RetroEmu.Devices.DMG.CPU;

public partial class Processor
{
    protected bool IsSet(Flag flag)
    {
        return (Registers.F & (byte)flag) > 0;
    }
        
    private void ToggleFlag(Flag flag)
    {
        Registers.F ^= (byte)flag;
    }

    protected void SetFlag(Flag flag)
    {
        Registers.F |= (byte)flag;
    }
        
    protected void ClearFlag(Flag flag)
    {
        Registers.F &= (byte)~flag;
    }

    protected void SetFlagToValue(Flag flag, bool value)
    {
        if (value)
        {
            SetFlag(flag);
        }
        else
        {
            ClearFlag(flag);
        }
    }
}