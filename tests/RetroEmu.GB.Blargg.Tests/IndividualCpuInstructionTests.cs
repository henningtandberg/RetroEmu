using RetroEmu.Devices.DMG;
using RetroEmu.GB.TestSetup;
using Xunit.Abstractions;

namespace RetroEmu.GB.Blargg.Tests;

public class IndividualCpuInstructionTests(ITestOutputHelper output)
{
    private readonly IGameBoy _gameBoy = TestGameBoyBuilder
        .CreateBuilder()
        .WithProcessor(processor =>
            processor.SetProgramCounter(0x0100))
        .BuildGameBoy();
    
    [Fact]
    public void One_Special()
    {
        var cartridge_memory = File.ReadAllBytes("Resources/cpu_instrs/01-special.gb");
        _gameBoy.Load(cartridge_memory);

        for (var i = 0; i < 2_000_000; i++)
        {
            _ = _gameBoy.Update();
        }

        var actualOutput = _gameBoy.GetOutput();
        output.WriteLine(actualOutput);
        Assert.Equal("01-special\n\n\nPassed\n", actualOutput);
    }
    
    [Fact]
    public void Two_Interrupts()
    {
        var cartridge_memory = File.ReadAllBytes("Resources/cpu_instrs/02-interrupts.gb");
        _gameBoy.Load(cartridge_memory);

        for (var i = 0; i < 2_000_000; i++)
        {
            _ = _gameBoy.Update();
        }

        var actualOutput = _gameBoy.GetOutput();
        output.WriteLine(actualOutput);
        Assert.Equal("02-interrupts\n\n\nPassed\n", actualOutput);
    }
    
    [Fact]
    public void Three_OpSpHl()
    {
        var cartridge_memory = File.ReadAllBytes("Resources/cpu_instrs/03-op sp,hl.gb");
        _gameBoy.Load(cartridge_memory);

        for (var i = 0; i < 2_000_000; i++)
        {
            _ = _gameBoy.Update();
        }

        var actualOutput = _gameBoy.GetOutput();
        output.WriteLine(actualOutput);
        Assert.Equal("03-op sp,hl\n\n\nPassed\n", actualOutput);
    }
    
    [Fact]
    public void Four_OPrimm()
    {
        var cartridge_memory = File.ReadAllBytes("Resources/cpu_instrs/04-op r,imm.gb");
        _gameBoy.Load(cartridge_memory);

        for (var i = 0; i < 2_000_000; i++)
        {
            _ = _gameBoy.Update();
        }

        var actualOutput = _gameBoy.GetOutput();
        output.WriteLine(actualOutput);
        Assert.Equal("04-op r,imm\n\n\nPassed\n", actualOutput);
    }
    
    [Fact]
    public void Five_OPrp()
    {
        var cartridge_memory = File.ReadAllBytes("Resources/cpu_instrs/05-op rp.gb");
        _gameBoy.Load(cartridge_memory);

        for (var i = 0; i < 2_000_000; i++)
        {
            _ = _gameBoy.Update();
        }

        var actualOutput = _gameBoy.GetOutput();
        output.WriteLine(actualOutput);
        Assert.Equal("05-op rp\n\n\nPassed\n", actualOutput);
    }
    
    [Fact]
    public void Six_LDrr()
    {
        var cartridge_memory = File.ReadAllBytes("Resources/cpu_instrs/06-ld r,r.gb");
        _gameBoy.Load(cartridge_memory);

        for (var i = 0; i < 330_000; i++)
        {
            _ = _gameBoy.Update();
        }

        var actualOutput = _gameBoy.GetOutput();
        output.WriteLine(actualOutput);
        Assert.Equal("06-ld r,r\n\n\nPassed\n", actualOutput);
    }
    
    [Fact]
    public void Seven_JrJpCallRetRst()
    {
        var cartridge_memory = File.ReadAllBytes("Resources/cpu_instrs/07-jr,jp,call,ret,rst.gb");
        _gameBoy.Load(cartridge_memory);

        for (var i = 0; i < 10_000_000; i++)
        {
            _ = _gameBoy.Update();
        }

        var actualOutput = _gameBoy.GetOutput();
        output.WriteLine(actualOutput);
        Assert.Equal("07-jr,jp,call,ret,rst\n\n\nPassed\n", actualOutput);
    }
    
    [Fact]
    public void Eight_MiscInstrs()
    {
        var cartridge_memory = File.ReadAllBytes("Resources/cpu_instrs/08-misc instrs.gb");
        _gameBoy.Load(cartridge_memory);

        for (var i = 0; i < 10_000_000; i++)
        {
            _ = _gameBoy.Update();
        }

        var actualOutput = _gameBoy.GetOutput();
        output.WriteLine(actualOutput);
        Assert.Equal("08-misc instrs\n\n\nPassed\n", actualOutput);
    }
    
    [Fact]
    public void Nine_OPrr()
    {
        var cartridge_memory = File.ReadAllBytes("Resources/cpu_instrs/09-op r,r.gb");
        _gameBoy.Load(cartridge_memory);

        for (var i = 0; i < 10_000_000; i++)
        {
            _ = _gameBoy.Update();
        }

        //52839
        var actualOutput = _gameBoy.GetOutput();
        output.WriteLine(actualOutput);
        Assert.Equal("09-op r,r\n\n\nPassed\n", actualOutput);
    }
    
    [Fact]
    public void Ten_BitOps()
    {
        var cartridge_memory = File.ReadAllBytes("Resources/cpu_instrs/10-bit ops.gb");
        _gameBoy.Load(cartridge_memory);

        for (var i = 0; i < 10_000_000; i++)
        {
            _ = _gameBoy.Update();
        }

        //52839
        var actualOutput = _gameBoy.GetOutput();
        output.WriteLine(actualOutput);
        Assert.Equal("10-bit ops\n\n\nPassed\n", actualOutput);
    }
    
    [Fact]
    public void Eleven_OpAHL()
    {
        var cartridge_memory = File.ReadAllBytes("Resources/cpu_instrs/11-op a,xhl.gb");
        _gameBoy.Load(cartridge_memory);

        for (var i = 0; i < 10_000_000; i++)
        {
            _ = _gameBoy.Update();
        }

        //52839
        var actualOutput = _gameBoy.GetOutput();
        output.WriteLine(actualOutput);
        Assert.Equal("11-op a,(hl)\n\n\nPassed\n", actualOutput);
    }
}