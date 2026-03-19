using System.Collections.Generic;

namespace RetroEmu.Devices.GameBoy.ROM;

public interface IDebugCartridge
{
    public List<byte[]> GetRomBanks();
}