using System;
using RetroEmu.Devices.DMG.CPU.Interrupts;

namespace RetroEmu.Devices.DMG.CPU.Timing;

public class Timer(IInterruptState interruptState) : ITimer
{
    private ushort _dividerInternal = 0xABD8;
    private byte _timerInternal = 0;
    private int _timerSpeed = 1;
    
    private const int MachineCyclesPerClockCycle = 4;
    private const int DividerPeriod = 16 * 4;

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

    public void SetSpeed(int speed)
    {
        _timerSpeed = speed;
    }
    
    // TODO: Vi må skrive nye registerverdier til riktig adresse i minnet
    public void Update(int cycles)
    {
        for (int i = 0; i < cycles; i++)
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
            _dividerInternal++;
            var bitAfter = _dividerInternal & (0x01 << bitPosition);

            if (bitBefore > 0 && bitAfter == 0 && TimerIncrementEnabled())
            {
                _timerInternal++;
                if (_timerInternal == 0xFF)
                {
                    _timerInternal = Modulo;
                    interruptState.GenerateInterrupt(InterruptType.Timer);
                }
            }
        }
    }

    private bool TimerIncrementEnabled() => (Control & 0b100) == 0b100;

    private ulong GetTimerPeriod() => (Control & 0b11) switch
    {
        0b00 => 256 * MachineCyclesPerClockCycle,
        0b01 => 4 * MachineCyclesPerClockCycle,
        0b10 => 16 * MachineCyclesPerClockCycle,
        0b11 => 64 * MachineCyclesPerClockCycle,
        _ => throw new InvalidOperationException("Invalid timer period")
    };
}