using System;

namespace RetroEmu.Devices.DMG.CPU.Interrupts;

public class InterruptState : IInterruptState
{
    // IME
    public bool InterruptMasterEnable { get; set; }

    // For all interrupt registers:
    // Bit 0 -> V-Blank
    // Bit 1 -> LCDC
    // Bit 2 -> Timer overflow
    // Bit 3 -> Serial I/O transfer complete
    // Bit 4 -> Buttons

    // IE Register 0xFFFF
    public byte InterruptEnable { get; set; }
    // IF Register 0xFF0F
    public byte InterruptFlag { get; set; }
    public byte DisableInterruptCounter { get; set; }
    public byte EnableInterruptCounter { get; set; }

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
    public bool IsInterruptMasterEnabled() => InterruptMasterEnable;

    public void SetInterruptMasterEnable(bool value)
    {
        InterruptMasterEnable = value;
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
        if (DisableInterruptCounter == 1)
        {
            DisableInterruptCounter = 0;
            InterruptMasterEnable = false;
        }
        else if (DisableInterruptCounter > 1)
        {
            DisableInterruptCounter--;
        }
        
        if (EnableInterruptCounter == 1)
        {
            EnableInterruptCounter = 0;
            InterruptMasterEnable = true;
        }
        else if (EnableInterruptCounter > 1)
        {
            EnableInterruptCounter--;
        }
    }

    public byte GetSelectedInterrupt()
    {
        // Iterate through IF by priority
        InterruptType[] interruptsByPriority = [InterruptType.VBlank, InterruptType.LCDC, InterruptType.Timer, InterruptType.Serial, InterruptType.Button];
        byte selectedInterrupt = 0;
        foreach (InterruptType interrupt in interruptsByPriority)
        {
            // If interrupt is enabled and triggered
            if ((InterruptEnable & (byte)interrupt) != 0 && (InterruptFlag & (byte)interrupt) != 0)
            {
                selectedInterrupt = (byte)interrupt;
                break;
            }
        }
        return selectedInterrupt;
    }

    public void ResetInterruptFlag(byte selectedInterrupt)
    {
        InterruptFlag &= (byte)~selectedInterrupt;
    }

    public bool InterruptMasterEnableIsDisabledAndThereIsAPendingInterrupt()
    {
        return !InterruptMasterEnable && InterruptFlag != 0;
    }
}