using System.Runtime.InteropServices;

namespace RetroEmu.Devices.DMG
{
    [StructLayout(LayoutKind.Explicit)]
    public struct Register16Bit
    {
        [FieldOffset(0)] public ushort W;
        [FieldOffset(0)] public byte BL;
        [FieldOffset(1)] public byte BH;
    }
}