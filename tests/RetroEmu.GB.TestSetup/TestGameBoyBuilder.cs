using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using RetroEmu.Devices.DMG;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.Devices.DMG.ROM;

namespace RetroEmu.GB.TestSetup;

public class TestGameBoyBuilder
{
    private Action<TestableProcessor> _processorDelegate = processor => processor.Reset();
    private IDictionary<ushort, byte> _memory = new Dictionary<ushort, byte>();
    private bool _useFakeMemory;

    public static TestGameBoyBuilder CreateBuilder() => new();

    public TestGameBoyBuilder WithProcessor(Action<TestableProcessor> processorDelegate)
    {
        _processorDelegate = processorDelegate;
        return this;
    }
    
    public TestGameBoyBuilder WithMemory(Func<IDictionary<ushort, byte>> memorySetup)
    {
        _memory = memorySetup();
        _useFakeMemory = true;
        return this;
    }
    
    public IGameBoy BuildGameBoy()
    {
        var services = new ServiceCollection();

        services
            .AddDotMatrixGameBoy()
            .AddSingleton<ITestableProcessor, TestableProcessor>()
            .AddSingleton<IProcessor>(serviceProvider => serviceProvider.GetRequiredService<ITestableProcessor>());
        
        var serviceProvider = services.BuildServiceProvider();
        
        var processor = serviceProvider.GetRequiredService<ITestableProcessor>();
        _processorDelegate.Invoke((TestableProcessor) processor);
        
        return serviceProvider.GetRequiredService<IGameBoy>();
    }
}