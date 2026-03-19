namespace RetroEmu.Devices.DMG.CPU.Instructions;

internal readonly struct Instruction(WriteType writeType, OpType opType, FetchType fetchType)
{
    public WriteType WriteType { get; } = writeType;
    public OpType OpType { get; } = opType;
    public FetchType FetchType { get; } = fetchType;
}