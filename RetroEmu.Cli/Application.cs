using RetroEmu.Devices.DMG;

namespace RetroEmu.Cli;

public class Application(IGameBoy gameBoy) : IApplication
{
    public void Run(string[] args)
    {
        if (args.Length == 0)
        {
            throw new ArgumentException("No ROM file specified.");
        }
        
        gameBoy.Reset();
        var rom = File.ReadAllBytes(args[0]);
        gameBoy.Load(rom);
        
        while (true)
        {
            gameBoy.Update();
        } 
    }
}