namespace RetroEmu.Devices.DMG.CPU
{
    public unsafe partial class Processor
    {
        public bool IsSet(Flag flag)
        {
            return (*Registers.F & (byte)flag) > 0;
        }
        
        private void ToggleFlag(Flag flag)
        {
            *Registers.F ^= (byte)flag;
        }

        public void SetFlag(Flag flag)
        {
            *Registers.F |= (byte)flag;
        }
        
        public void ClearFlag(Flag flag)
        {
            *Registers.F &= (byte)~flag;
        }

        private void ClearAllFlags()
        {
            *Registers.F &= 0x00;
        }
    }
}