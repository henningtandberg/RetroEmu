using System;
using RetroEmu.Devices.DMG.CPU.Interrupts;

namespace RetroEmu.Devices.DMG;

public class Joypad(IInterruptState interruptState) : IJoypad
{
    private byte _dPad = 0x0F;
    private byte _buttons = 0x0F;

    private const byte DPadAndButtonsEnabled = 0x30;
    private const byte DPadEnabled = 0x20;
    private const byte ButtonsEnabled = 0x10;
    private const byte NeitherEnabled = 0x00;
    
    // P1 - Joypad - 0xCF is the expected startup value
    private byte _previousP1 = 0xCF;
    private byte _currentP1 = 0xCF;
    
    public byte P1 {
        get => (_currentP1 & 0x30) switch  {
            0x00 => (byte)(_currentP1 | 0x0F | (_dPad & _buttons)),
            0x10 => (byte)((_currentP1 & 0xF0) | _buttons),
            0x20 => (byte)((_currentP1 & 0xF0) | _dPad),
            0x30 => (byte)(_currentP1 | 0x0F), // Maybe a case for 0x30 is needed, but we don't know what it does yet
            _ => throw new ArgumentOutOfRangeException()
        };
        set => _currentP1 = (byte)((_currentP1 & 0x0F) | (value & 0xF0));
    }

    public void PressButton(byte button)
    {
        _buttons &= (byte)~(1 << button);
        _buttons &= 0x0F;
    }
    
    public void ReleaseButton(byte button)
    {
        _buttons |= (byte)(1 << button);
        _buttons &= 0x0F;
    }

    public void PressDPad(byte direction)
    {
        _dPad &= (byte)~(1 << direction);
        _dPad &= 0x0F;
    }

    public void ReleaseDPad(byte direction)
    {
        _dPad |= (byte)(1 << direction);
        _dPad &= 0x0F;
    }

    public void Update()
    {
        if (P1 == _previousP1)
        {
            return; // No change
        }
        
        switch (_currentP1 & 0x30)
        {
            case NeitherEnabled:
            case DPadAndButtonsEnabled:
                _previousP1 = P1;
                return;
            case DPadEnabled:
            case ButtonsEnabled:
                if ((P1 & 0x0F) < (_previousP1 & 0x0F))
                {
                    interruptState.GenerateInterrupt(InterruptType.Button);
                }
                break;
        }
        
        _previousP1 = P1;
    }
}