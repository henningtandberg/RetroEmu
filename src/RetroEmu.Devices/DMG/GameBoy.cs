using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using RetroEmu.Devices.Disassembly;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.Devices.DMG.CPU.Link;
using RetroEmu.Devices.DMG.ROM;

namespace RetroEmu.Devices.DMG;

public class GameBoy(
    IDisassembler disassembler,
    ICartridge cartridge,
    IAddressBus addressBus,
    IProcessor processor,
    IJoypad joypad,
    ISerial serial)
    : IGameBoy
{
    public string GetOutput() => addressBus.GetOutput();

    public void Reset()
    {
        addressBus.Reset();
        processor.Reset();
        serial.Reset();
    }

    public void Load(byte[] cartridgeMemory)
    {
        Reset();
        cartridge.Load(cartridgeMemory);
    }

    public void ButtonPressed(Button button) =>
        joypad.PressButton((byte)button);

    public void ButtonReleased(Button button) =>
        joypad.ReleaseButton((byte)button);

    public void DPadPressed(DPad direction) =>
        joypad.PressDPad((byte)direction);

    public void DPadReleased(DPad direction) =>
        joypad.ReleaseDPad((byte)direction);

    public CartridgeHeader GetCartridgeInfo() =>
        cartridge.GetCartridgeInfo();


    private static readonly Stopwatch Stopwatch = Stopwatch.StartNew();
    private long _cycles = 0;
    private long _iterations = 0;
    private const int PrintIntervalMs = 1000; // print every second

    public int Update()
    {
        disassembler.DisassembleNextInstruction();

        int update = processor.Update();
        _cycles += update;
        _iterations++;

        if (Stopwatch.ElapsedMilliseconds >= PrintIntervalMs)
        {
            var elapsed = Stopwatch.Elapsed.TotalSeconds;
            var cyclesPerSec = _cycles / elapsed;
            var iterationsPerSec = _iterations / elapsed;
            
            Console.WriteLine($"Cycles/s: {cyclesPerSec:N0} - Iterations/s: {iterationsPerSec:N0}");
            
            Stopwatch.Restart();
            _cycles = 0;
            _iterations = 0;
        }

        return update;
    }

    private const double DefaultFramesPerSecond = double.MaxValue;
    /// <summary>
    /// Update until VBlank, with backup in case LCD is turned off. do-while is necessary to break GameBoy out of
    /// VBlank on next update.
    /// </summary>
    /// <param name="frameRate">If not provided, the GameBoy will run at a steady 59.7275 frame rate</param>
    public void RunAt(double frameRate = DefaultFramesPerSecond)
    {
        var currentClockSpeed = processor.GetCurrentClockSpeed();
        var cyclesToRun = currentClockSpeed / frameRate;
        
        var i = 0;
        do
        {
            Update();
            i++;
            if (i >= 2 * cyclesToRun)
            {
                break;
            }
        } while (!processor.VBlankTriggered());
    }

    // This can be replaced by getting/keeping a reference to IProcessor outside IGameBoy
    public IProcessor GetProcessor() => processor;

    
    // This can be replaced by getting/keeping a reference to IAddressBus outside IGameBoy
    public IAddressBus GetMemory() => addressBus;
}