using System.Runtime.InteropServices;

namespace RetroEmu.GB.TestSetup;

[StructLayout(LayoutKind.Explicit)]
internal class Union16Bit
{
    [FieldOffset(0)] public ushort W;
    [FieldOffset(0)] public byte BL;
    [FieldOffset(1)] public byte BH;
}