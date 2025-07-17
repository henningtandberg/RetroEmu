using System.Collections.Generic;

namespace RetroEmu.Devices.DMG.ROM;

public interface IDebugCartridge
{
    public List<byte[]> GetRomBanks();
}