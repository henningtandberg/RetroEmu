using System.Collections.Generic;

namespace RetroEmu.GB.TestSetup;

public record struct ExpectedState()
{
    public byte? A { get; init; }
    public byte? B { get; init; }
    public byte? C { get; init; }
    public byte? D { get; init; }
    public byte? E { get; init; }
    public byte? H { get; init; }
    public byte? L { get; init; }

    public ushort? AF { get; init; }
    public ushort? BC { get; init; }
    public ushort? DE { get; init; }
    public ushort? HL { get; init; }
    public ushort? PC { get; init; }
    public ushort? SP { get; init; }

    public bool? ZeroFlag { get; init; }
    public bool? SubtractFlag { get; init; }
    public bool? HalfCarryFlag { get; init; }
    public bool? CarryFlag { get; init; }
    
    public Dictionary<ushort, byte> Memory { get; init; } = [];
    public byte[] Stack { get; init; } = [];
    
    public int Cycles { get; init; }
}
