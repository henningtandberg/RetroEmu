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
            .AddSingleton<IProcessor>(serviceProvider => serviceProvider.GetService<ITestableProcessor>());

        if (_useFakeMemory)
        {
            services.AddSingleton<IMemory>(new MemoryFake(_memory));
        }
        
        var serviceProvider = services.BuildServiceProvider();
        
        var processor = serviceProvider.GetRequiredService<ITestableProcessor>();
        _processorDelegate.Invoke((TestableProcessor) processor);
        
        return serviceProvider.GetRequiredService<IGameBoy>();
    }
}

public class MemoryFake(IDictionary<ushort, byte> memory) : IMemory
{
    public string GetOutput()
    {
        // TODO: Remove
        return string.Empty;
    }

    public void Reset()
    {
        // TODO: Remove
    }

    public byte Read(ushort address) =>
        memory.TryGetValue(address, out var value)
            ? value
            : (byte)0;

    public void Write(ushort address, byte value)
    {
        if (memory.TryAdd(address, value))
        {
            return;
        }
        
        memory[address] = value;
    }

    public void Load(byte[] rom)
    {
        // TODO: Remove
    }
}