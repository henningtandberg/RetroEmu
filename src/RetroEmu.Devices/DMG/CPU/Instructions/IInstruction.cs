namespace RetroEmu.Devices.DMG.CPU.Instructions;

internal interface IInstruction
{
	unsafe int Execute(Processor processor);
}