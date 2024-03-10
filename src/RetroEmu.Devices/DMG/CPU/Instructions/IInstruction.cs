namespace RetroEmu.Devices.DMG.CPU.Instructions;

internal interface IInstruction
{
	public WriteType WriteOp { get; }
	public OpType Op { get; }
	public FetchType FetchOp { get; }
}