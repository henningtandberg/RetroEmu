using RetroEmu.Devices.GameBoy;

namespace RetroEmu.GB.TestSetup;

public interface ITestableEmulator : IGameBoy
{
    public ITestableProcessor GetProcessor();
}