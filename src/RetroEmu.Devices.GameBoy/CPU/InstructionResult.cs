namespace RetroEmu.Devices.GameBoy.CPU;

public readonly struct FetchResult(int cycles, ushort value)
{
    public int Cycles { get; } = cycles;
    public ushort Value { get; } = value;

    public void Deconstruct(out int cycles, out ushort value)
    {
        cycles = Cycles;
        value = Value;
    }
}

public readonly struct OperationResult(ushort value, int cycles)
{
    public ushort Value { get; } = value;
    public int Cycles { get; } = cycles;

    public void Deconstruct(out ushort value, out int cycles)
    {
        value = Value;
        cycles = Cycles;
    }
}
