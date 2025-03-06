using RetroEmu.Devices.DMG.CPU.Interrupts;

namespace RetroEmu.Devices.DMG;

public class Joypad(IInterruptState interruptState) : IJoypad
{
    // P1 - Joypad - 0xCF is the expected startup value
    private byte _previousP1;
    private byte _currentP1 = 0xCF;
    public byte P1 {
        get => _currentP1;
        set
        {
            _previousP1 = _currentP1;
            _currentP1 = value;
        }
    }
    
    public void Update()
    {
        if (_currentP1 == _previousP1)
        {
            // No change
            return;
        }
        
        switch (_currentP1 & 0x30)
        {
            // Both buttons and dpad
            case 0x30:
                _currentP1 = _previousP1 = 0xCF;
                return;
            // None
            case 0x00:
                return;
        }

        // One of the P1 bits have gone from high to low
        if ((_currentP1 & 0x0F) < (_previousP1 & 0x0F))
        {
            interruptState.GenerateInterrupt(InterruptType.Button);
        }
    }
}