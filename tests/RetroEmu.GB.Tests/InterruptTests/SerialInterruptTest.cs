using RetroEmu.Devices.DMG;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.GB.TestSetup;
using Xunit;

namespace RetroEmu.GB.Tests.InterruptTests;

public class SerialInterruptTest
{
    private const byte ExpectedValueOfRegisterA = 0x17;
    private static readonly WireFake WireFake = new();
    
    private readonly IGameBoy _gameBoy = TestGameBoyBuilder
        .CreateBuilder()
        .WithProcessor(processor =>
        {
            processor.SetInterruptMasterEnableToValue(true);
            processor.SetSerialInterruptEnableToValue(true);
        })
        .WithWireFake(WireFake)
        .BuildGameBoy();
    
    /// <summary>
    /// This program causes 0x75 to be shifted out of the
    /// serial port and a byte to be shifted into 0xFF01 (SerialControl)
    ///
    /// In this example we assume that the internal clock is
    /// selected this it should take:
    ///
    ///     1 / SERIAL_CLOCK_SPEED * 8
    ///   = 1 / 8192 Hz * 8
    ///   = 1 / 122 microseconds * 8
    ///   = 976 microseconds
    ///
    /// But we run in an infinite loop until we are in the
    /// serial interrupt handler which should happen after
    /// approx 1 ms. If the handler is not triggered after
    /// a timeout we assume the test failed.
    /// </summary>
    private readonly byte[] _masterModeSerialProgram = CartridgeBuilder
        .Create()
        .WithProgram([
            Opcode.Ld_A_N8,     // LD A, $75        # Data to transfer
            0x75,
            Opcode.Ld_HL_N16,   // LD HL, $FF01
            0x01,
            0xFF,
            Opcode.Ld_XHL_A,    // LD ($FF01), A    # Move data to transfer to SerialControl
            Opcode.Ld_A_N8,     // LD A, $81
            0x81,
            Opcode.Ld_HL_N16,   // LD HL, $FF02
            0x02,
            0xFF,
            Opcode.Ld_XHL_A,    // LD ($FF02), A    # Enable Serial transfer
            Opcode.Jr_N8,       // Enter never ending loop
            0xFE
        ])
        .WithSerialInterruptHandler([
            Opcode.Ld_A_N8,
            ExpectedValueOfRegisterA,
            Opcode.RetI
        ])
        .Build();
    
    [Fact]
    public void MasterModeSerialProgram_SerialInterruptIsEnabled_SerialInterruptIsTriggeredOnShiftedByte()
    {
        _gameBoy.Load(_masterModeSerialProgram);
        var processor = (ITestableProcessor)_gameBoy.GetProcessor();
        processor.SetProgramCounter(0x0150); // Skip program start routine at 0x0100 (NOP + JP N16)

        _gameBoy.RunFor(amountOfInstructions: 1200); // It should take 1024 NOPs for the transfer to complete 
        
        var result = processor.GetValueOfRegisterA();
        Assert.Equal(ExpectedValueOfRegisterA, result);
        var data = WireFake.DequeueOutgoingData();
        Assert.Equal(0x75, data.SerialByte);
        Assert.Equal(8192, data.ClockSpeedHz);
    }
}