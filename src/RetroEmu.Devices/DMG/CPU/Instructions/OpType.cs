namespace RetroEmu.Devices.DMG.CPU.Instructions;

internal enum OpType : byte
{
	Add,
	Add16,
	AddSP,
	Adc,
	And,
	Dec,
	Ld,
	Jp,
	JpConditionally,
	Sbc,
	Sub
}

internal enum ConditionalOpType : byte
{
    JpConditionally
}

