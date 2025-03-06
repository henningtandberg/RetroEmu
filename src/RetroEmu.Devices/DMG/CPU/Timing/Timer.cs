using System;
using RetroEmu.Devices.DMG.CPU.Interrupts;

namespace RetroEmu.Devices.DMG.CPU.Timing;

public class Timer(IInterruptState interruptState) : ITimer
{
    private ushort _dividerInternal = 0xABD8;
    private byte _timerInternal = 0;
    private bool _hadOverflow = false;

    public byte Divider {
        get
        {
            return (byte)(_dividerInternal >> 0x08);
        }
        set
        {
            _dividerInternal = 0;
        }
    }

    public byte Counter
    {
        get
        {
            return _timerInternal;
        }
        set
        {
            _timerInternal = value;
        }
    }
    public byte Modulo { get; set; } = 0;
    public byte Control { get; set; } = 0xF8;
    
    public void Update(int cycles)
    {
        for (int i = 0; i < cycles / 4; i++)
        {
            int bitPosition = 0;
            // TODO: Make this switch nicer
            switch (Control & 0x3)
            {
                case 0b00:
                    bitPosition = 9;
                    break;
                case 0b01:
                    bitPosition = 3;
                    break;
                case 0b10:
                    bitPosition = 5;
                    break;
                case 0b11:
                    bitPosition = 7;
                    break;

            }
            var bitBefore = _dividerInternal & (0x01 << bitPosition);
            _dividerInternal += 4;
            var bitAfter = _dividerInternal & (0x01 << bitPosition);

            if (_hadOverflow)
            {
                _timerInternal = Modulo;
                interruptState.GenerateInterrupt(InterruptType.Timer);
                _hadOverflow = false;
            }

            if (bitBefore > 0 && bitAfter == 0 && TimerIncrementEnabled())
            {
                if (_timerInternal == 0xFF)
                {
                    _timerInternal = 0;
                    _hadOverflow = true;
                    interruptState.GenerateInterrupt(InterruptType.Timer);
                }
                else
                {
                    _timerInternal++;
                }
                
            }
        }
    }

    private bool TimerIncrementEnabled() => (Control & 0b100) == 0b100;

}