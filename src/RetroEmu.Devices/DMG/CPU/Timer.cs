using System;

namespace RetroEmu.Devices.DMG.CPU;

public class Timer : ITimer
{
    private ulong _totalCyclesTimer = 0;
    private ulong _totalCyclesDivider = 0;
    private int _timerSpeed = 1;
    
    private const int MachineCyclesPerClockCycle = 4;
    private const int DividerPeriod = 16 * 4;
    
    public byte Divider { get; set; }
    public byte Counter { get; set; }
    public byte Modulo { get; set; } = 0;
    public byte Control { get; set; } = 0;

    public void SetSpeed(int speed)
    {
        _timerSpeed = speed;
    }
    
    // TODO: Vi må skrive nye registerverdier til riktig adresse i minnet
    public bool Update(int cycles)
    {
        _totalCyclesDivider += (ulong)cycles;
        if (_totalCyclesDivider >= DividerPeriod)
        {
            _totalCyclesDivider -= DividerPeriod;
            Divider++;
        }
        
        _totalCyclesTimer += (ulong)(cycles * _timerSpeed);
        var timerPeriod = GetTimerPeriod();
        if (_totalCyclesTimer >= timerPeriod)
        {
            _totalCyclesTimer -= timerPeriod;
            if (Counter == 0xFF)
            {
                Counter = Modulo;
                return true;
            }

            if (TimerIncrementEnabled())
            {
                Counter++;
            }
        }

        return false;
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