using System;
using RetroEmu.Devices.DMG.CPU.Interrupts;

namespace RetroEmu.Devices.DMG.CPU.Timing;

public class Timer(IInterruptState interruptState) : ITimer
{
    private ulong _totalCyclesTimer = 0;
    private ushort _dividerInternal = 0xABD8;
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

    public byte Counter { get; set; }
    public byte Modulo { get; set; } = 0;
    public byte Control { get; set; } = 0xF8;

    public void SetSpeed(int speed)
    {
        _timerSpeed = speed;
    }
    
    // TODO: Vi må skrive nye registerverdier til riktig adresse i minnet
    public void Update(int cycles)
    {
        _dividerInternal += (ushort)cycles;
        
        _totalCyclesTimer += (ulong)(cycles * _timerSpeed);
        
        var timerPeriod = GetTimerPeriod();
        if (_totalCyclesTimer < timerPeriod)
            return;
        
        _totalCyclesTimer -= timerPeriod;
        if (Counter == 0xFF)
        {
            Counter = Modulo;
            interruptState.GenerateInterrupt(InterruptType.Timer);
        }

        if (TimerIncrementEnabled())
        {
            Counter++;
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