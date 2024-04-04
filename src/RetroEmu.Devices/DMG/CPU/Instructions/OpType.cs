namespace RetroEmu.Devices.DMG.CPU.Instructions;

internal enum ALUOpType : byte
{
	Add,
	Add16,
	AddSP,
	Adc,
	And,
	Dec,
	Sbc,
	Sub,
	Rotate,
	RotateThroughCarry,
	Inc
}

internal enum ConditionalOpType : byte
{
    Jp
}

internal enum RotateOpType : byte
{
	Rotate,
	RotateThroughCarry
}
