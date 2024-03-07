using System;

namespace RetroEmu.Devices.DMG.CPU;

public static class EnumImplementation
{
    public static int Size<T>()
    {
        return Enum.GetNames(typeof(T)).Length;
    }
}