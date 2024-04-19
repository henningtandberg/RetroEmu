namespace RetroEmu.Devices.DMG.CPU.Instructions;

internal enum ALUOpType : byte
{
	Add,
	Add16,
	AddSP,
	Adc,
	And,
	Dec,
	Dec16,
	Sbc,
	Sub,
	Inc,
	Inc16,
	Cp,
	Or,
	Xor,
}

internal enum ConditionalOpType : byte
{
    Jp,
    Jr
}

internal enum RotateOpType : byte
{
	Rotate,
	RotateThroughCarry
}
