namespace RetroEmu.Devices.DMG.CPU.Instructions;

internal enum OpType : byte
{
	Add,
	Add16,
	AddSp,
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
	Cpl,
	Ccf,
	Daa,
	Scf,
	Ld,
	Nop,
	Rst
}

internal enum ConditionalOpType : byte
{
    Jp,
    Jr,
    Call,
    Ret
}

internal enum RotateOpType : byte
{
	Rotate,
	RotateThroughCarry
}
