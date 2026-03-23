using System.Collections.Generic;

namespace RetroEmu.Devices.GameBoy.Cartridge;

public interface IDebugCartridge
{
    public List<byte[]> GetRomBanks();
}