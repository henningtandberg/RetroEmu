using System;

namespace RetroEmu.Devices.DMG.CPU.Interrupts;

public class InterruptState : IInterruptState
{
    // For all interrupt registers:
    // Bit 0 -> V-Blank
    // Bit 1 -> LCDC
    // Bit 2 -> Timer overflow
    // Bit 3 -> Serial I/O transfer complete
    // Bit 4 -> Buttons
    
    // IE Register 0xFFFF
    public byte InterruptEnable { get; set; }
    // IF Register 0xFF0F
    public byte InterruptFlag { get; set; } = 0xE1;
    
    // IME
    private bool _interruptMasterEnable;
    private byte _disableInterruptCounter;
    private byte _enableInterruptCounter;

    private static InterruptType[] interruptsByPriority = [InterruptType.VBlank, InterruptType.LCDC, InterruptType.Timer, InterruptType.Serial, InterruptType.Button];

    public ushort GetInterruptStartingAddress(InterruptType type) =>
        type switch
        {
            InterruptType.VBlank => 0x40,
            InterruptType.LCDC => 0x48,
            InterruptType.Timer => 0x50,
            InterruptType.Serial => 0x58,
            InterruptType.Button => 0x60,
            _ => throw new NotImplementedException()
        };
    
    // For the time being, this only exists for the sake of the unit tests
    public bool IsInterruptMasterEnabled() => _interruptMasterEnable;

    public void SetInterruptMasterEnable(bool value)
    {
        _interruptMasterEnable = value;
    }
    
    public void SetInterruptEnable(InterruptType type, bool value)
    {
        if (value)
        {
            InterruptEnable |= (byte)type;
        }
        else
        {
            InterruptEnable &= (byte)~(byte)type;
        }
    }
    
    // Step 1 of interrupt procedure "When an interrupt is generated, the IF flag will be set"
    public void GenerateInterrupt(InterruptType type)
    {
        InterruptFlag |= (byte)type;
    }

    public void Update()
    {
        if (_disableInterruptCounter == 1)
        {
            _disableInterruptCounter = 0;
            _interruptMasterEnable = false;
        }
        else if (_disableInterruptCounter > 1)
        {
            _disableInterruptCounter--;
        }
        
        if (_enableInterruptCounter == 1)
        {
            _enableInterruptCounter = 0;
            _interruptMasterEnable = true;
        }
        else if (_enableInterruptCounter > 1)
        {
            _enableInterruptCounter--;
        }
    }

    public byte GetSelectedInterrupt()
    {
        // Iterate through IF by priority
        byte selectedInterrupt = 0;
        foreach (InterruptType interrupt in interruptsByPriority)
        {
            if (IsInterruptEnabledAndTriggered(interrupt))
            {
                selectedInterrupt = (byte)interrupt;
                break;
            }
        }
        return selectedInterrupt;
    }

    private bool IsInterruptEnabledAndTriggered(InterruptType interrupt)
    {
        return (InterruptEnable & (byte)interrupt) != 0 && (InterruptFlag & (byte)interrupt) != 0;
    }

    public void ResetInterruptFlag(byte selectedInterrupt)
    {
        InterruptFlag &= (byte)~selectedInterrupt;
    }

    public bool InterruptMasterEnableIsDisabledAndThereIsAPendingInterrupt()
    {
        return !_interruptMasterEnable && (InterruptFlag & InterruptEnable) != 0;
    }

    public void ResetEnableInterruptCounter()
    {
        _enableInterruptCounter = 2;
    }

    public void ResetDisableInterruptCounter()
    {
        _disableInterruptCounter = 2;
    }
}