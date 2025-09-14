using System.Collections.Generic;

namespace RetroEmu.GB.TestSetup;

public record struct InitialState()
{
    private Union16Bit _af = new() { W = 0x0000 };
    private Union16Bit _bc = new() { W = 0x0000 };
    private Union16Bit _de = new() { W = 0x0000 };
    private Union16Bit _hl = new() { W = 0x0000 };
    private Union16Bit _sp = new() { W = 0xFFFE };
    private Union16Bit _pc = new() { W = 0x0150 };

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

    public bool ZeroFlag { get; init; } = false;
    public bool SubtractFlag { get; init; } = false;
    public bool HalfCarryFlag { get; init; } = false;
    public bool CarryFlag { get; init; } = false;
    
    public Dictionary<ushort, byte> Memory { get; } = [];
}