namespace RetroEmu.Devices.DMG.CPU.Instructions;

// TODO: Better name for this enum?
internal enum FetchType : byte
{
	// 8-bit
	A, B, C, D, E, H, L, // Get value directly from register
	XBC, XDE, XHL, // Load value from address stored in double register
	XHLD, XHLI, // Load value from address at HL and increment/decrement HL
	XN16, // Load value from address in the next two opcodes
	N8, // Get value from the next opcode
	XN8, // Get value from the next opcode + 0xFF00.
	XC, // Get value from register C + 0xFF00.

	SPN8, // SP + signed immediate value (NB will set flags)

	// 16-bit
	BC, DE, HL, SP,
	N16,
	
	// Stack Fetch
	Pop
}
