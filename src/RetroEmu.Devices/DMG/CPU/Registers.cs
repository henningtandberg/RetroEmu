namespace RetroEmu.Devices.DMG.CPU;

public class Registers
{
	private Register16Bit _af;
	private Register16Bit _bc;
	private Register16Bit _de;
	private Register16Bit _hl;
	private Register16Bit _sp;
	private Register16Bit _pc;

	public ref byte A => ref _af.BH;
	public ref byte F => ref _af.BL;
	public ref byte B => ref _bc.BH;
	public ref byte C => ref _bc.BL;
	public ref byte D => ref _de.BH;
	public ref byte E => ref _de.BL;
	public ref byte H => ref _hl.BH;
	public ref byte L => ref _hl.BL;

	public ref ushort AF => ref _af.W;
	public ref ushort BC => ref _bc.W;
	public ref ushort DE => ref _de.W;
	public ref ushort HL => ref _hl.W;
	public ref ushort SP => ref _sp.W;
	public ref ushort PC => ref _pc.W;
}