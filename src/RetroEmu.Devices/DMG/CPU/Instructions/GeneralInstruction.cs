namespace RetroEmu.Devices.DMG.CPU.Instructions;

internal record GeneralInstruction(WriteType WriteOp, OpType Op, FetchType FetchOp) : IInstruction
{
	public int Execute(Processor processor) 
	{
		// TODO: Implement the contents of Processor.Update() here
		throw new System.NotImplementedException();
	}
}
