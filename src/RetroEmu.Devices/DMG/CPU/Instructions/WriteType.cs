namespace RetroEmu.Devices.DMG.CPU.Instructions;

internal enum WriteType : byte
{
	// 8-bit
	A,
	B,
	C,
	D,
	E,
	H,
	L,
	XBC,
	XDE,
	XHL, // Store value in memory at address stored in double register
	XHLD,
	XHLI, // Store value in memory at address stored in HL, then increment/decrement HL
	XN16, // Store value in memory at address in the next two opcodes
	XC, // Store at address C + 0xFF00, TODO: Better name?
	XN8, // Store at next opcode + 0xFF00, TODO: Better name?

	// 16-bit
	AF,
	BC,
	DE,
	HL,
    SP,
	PC,
	
	// Stack Push
	Push,
	
	// Compares do not write to any register
	None
}

