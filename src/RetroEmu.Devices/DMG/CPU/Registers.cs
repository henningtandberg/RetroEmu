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
	public byte F
	{
		get => _af.BL;
		set => _af.BL = (byte)(value & 0xF0);
	}
	public ref byte B => ref _bc.BH;
	public ref byte C => ref _bc.BL;
	public ref byte D => ref _de.BH;
	public ref byte E => ref _de.BL;
	public ref byte H => ref _hl.BH;
	public ref byte L => ref _hl.BL;

	public ushort AF
	{
        get => _af.W;
        set => _af.W = (ushort)(value & 0xFFF0);
	}
	public ref ushort BC => ref _bc.W;
	public ref ushort DE => ref _de.W;
	public ref ushort HL => ref _hl.W;
	public ref ushort SP => ref _sp.W;
	public ref ushort PC => ref _pc.W;

	public byte WriteAF(ushort value)
	{
		_af.W = value;
		return 0;
	}
}