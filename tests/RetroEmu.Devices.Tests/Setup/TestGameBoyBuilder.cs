using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using RetroEmu.Devices.DMG;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.Devices.Tests.Setup.MemoryFakes;

namespace RetroEmu.Devices.Tests.Setup;

public class TestGameBoyBuilder
{
    private Action<Processor> _processorDelegate;
    private IDictionary<ushort, byte> _memory;

    public static TestGameBoyBuilder CreateBuilder() => new();

    public TestGameBoyBuilder WithProcessor(Action<Processor> processorDelegate)
    {
        _processorDelegate = processorDelegate;
        return this;
    }
    
    public TestGameBoyBuilder WithMemory(Func<IDictionary<ushort, byte>> memorySetup)
    {
        _memory =  memorySetup();
        return this;
    }
    
    public IGameBoy BuildGameBoy()
    {
        var serviceProvider = new ServiceCollection()
            .AddDotMatrixGameBoy()
            .AddSingleton<IMemory>(new FakeMemory(_memory))
            .BuildServiceProvider();

        var processor = serviceProvider.GetRequiredService<IProcessor>();
        _processorDelegate?.Invoke((Processor) processor);
        
        return serviceProvider.GetRequiredService<IGameBoy>();
    }
    
}