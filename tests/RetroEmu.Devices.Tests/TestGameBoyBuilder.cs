using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using RetroEmu.Devices.DMG;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.Devices.Tests.MemoryFakes;

namespace RetroEmu.Devices.Tests;

public class TestGameBoyBuilder
{
    private Action<Processor> _processorDelegate;
    private IReadOnlyDictionary<ushort, byte> _memory;

    public static TestGameBoyBuilder CreateBuilder() => new();

    public TestGameBoyBuilder WithProcessor(Action<Processor> processorDelegate)
    {
        _processorDelegate = processorDelegate;
        return this;
    }
    
    public TestGameBoyBuilder WithMemory(Func<IReadOnlyDictionary<ushort, byte>> memorySetup)
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