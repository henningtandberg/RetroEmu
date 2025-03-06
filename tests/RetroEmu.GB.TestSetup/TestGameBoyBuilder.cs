using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using RetroEmu.Devices.DMG;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.Devices.DMG.CPU.Interrupts;
using RetroEmu.Devices.DMG.CPU.PPU;
using RetroEmu.Devices.DMG.CPU.Timing;
using RetroEmu.GB.TestSetup.MemoryFakes;

namespace RetroEmu.GB.TestSetup;

public class TestGameBoyBuilder
{
    private Action<TestableProcessor> _processorDelegate;
    private IDictionary<ushort, byte> _memory = new Dictionary<ushort, byte>();

    public static TestGameBoyBuilder CreateBuilder() => new();

    public TestGameBoyBuilder WithProcessor(Action<TestableProcessor> processorDelegate)
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
        var services = new ServiceCollection()
            .AddDotMatrixGameBoy();
        
        //var currentProcessorImplementation = services
        //    .FirstOrDefault(descriptor => descriptor.ServiceType == typeof(IProcessor));
        //
        //if (currentProcessorImplementation is not null)
        //{
        //    services.Remove(currentProcessorImplementation);
        //}

        services
            .AddSingleton<ITestableProcessor, TestableProcessor>()
            .AddSingleton<IProcessor>(serviceProvider =>
                serviceProvider.GetService<ITestableProcessor>());
        
        if (_memory.Count > 0)
        {
            services.AddSingleton<IMemory, FakeMemory>(provider =>
                new FakeMemory(
                    timer: provider.GetRequiredService<ITimer>(),
                    pixelProcessingUnit: provider.GetRequiredService<IPixelProcessingUnit>(),
                    interruptState: provider.GetRequiredService<IInterruptState>(),
                    joypad: provider.GetRequiredService<IJoypad>(),
                    _memory));
        }

        var serviceProvider = services.BuildServiceProvider();
        var processor = serviceProvider.GetRequiredService<ITestableProcessor>();
        _processorDelegate?.Invoke((TestableProcessor) processor);
        
        return serviceProvider.GetRequiredService<IGameBoy>();
    }
    
}