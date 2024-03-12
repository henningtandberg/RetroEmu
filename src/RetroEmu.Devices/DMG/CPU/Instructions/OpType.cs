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
	Sbc,
	Sub
}

internal enum ConditionalOpType : byte
{
    Jp
}

