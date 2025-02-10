using System.Collections.Generic;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.Devices.Tests.Setup;
using Xunit;

namespace RetroEmu.Devices.Tests.IsolatedOperationTests;

public class LdTests
{
    [Fact]
    public static unsafe void LD_A_XHLI_AddressIsIncrementedAndValueLoaded()
    {
        var expectedCycles = 8;
        var initialHLAddress = (ushort)0x0111;
        var initialHLValue = (byte)0xA0;

        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithProcessor(processor => processor
                .Set8BitGeneralPurposeRegisters(0x00, 0x00, 0x00, 0x00, 0x00, (byte)(initialHLAddress >> 8), (byte)(initialHLAddress & 0xff))
                .SetFlags(false, false, false, false)
                .SetProgramCounter(0x0001)
            )
            .WithMemory(() => new Dictionary<ushort, byte>
            {
                [0x0001] = Opcode.Ld_A_XHLI,
                [initialHLAddress] = initialHLValue
            })
            .BuildGameBoy();
        
        var cycles = gameBoy.Update();
        
        var processor = (ITestableProcessor)gameBoy.GetProcessor();
        Assert.Equal(expectedCycles, cycles);
        Assert.Equal(initialHLValue , processor.GetValueOfRegisterA());
        Assert.Equal(initialHLAddress + 1, processor.GetValueOfRegisterHL());
    }

    [Fact]
    public static unsafe void LD_A_XHLD_AddressIsDecrementedAndValueLoaded()
    {
        var expectedCycles = 8;
        var initialHLAddress = (ushort)0x0111;
        var initialHLValue = (byte)0xA0;

        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithProcessor(processor => processor
                .Set8BitGeneralPurposeRegisters(0x00, 0x00, 0x00, 0x00, 0x00, (byte)(initialHLAddress >> 8), (byte)(initialHLAddress & 0xff))
                .SetFlags(false, false, false, false)
                .SetProgramCounter(0x0001)
            )
            .WithMemory(() => new Dictionary<ushort, byte>
            {
                [0x0001] = Opcode.Ld_A_XHLD,
                [initialHLAddress] = initialHLValue
            })
            .BuildGameBoy();

        var cycles = gameBoy.Update();

        var processor = (ITestableProcessor)gameBoy.GetProcessor();
        Assert.Equal(expectedCycles, cycles);
        Assert.Equal(initialHLValue, processor.GetValueOfRegisterA());
        Assert.Equal(initialHLAddress - 1, processor.GetValueOfRegisterHL());
    }

    [Fact]
    public static unsafe void LD_XHLI_A_AddressIsIncrementedAndValueStored()
    {
        var expectedCycles = 8;
        var initialHLAddress = (ushort)0x0111;
        var initialAValue = (byte)0xA0;

        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithProcessor(processor => processor
                .Set8BitGeneralPurposeRegisters(initialAValue, 0x00, 0x00, 0x00, 0x00, (byte)(initialHLAddress >> 8), (byte)(initialHLAddress & 0xff))
                .SetFlags(false, false, false, false)
                .SetProgramCounter(0x0001)
            )
            .WithMemory(() => new Dictionary<ushort, byte>
            {
                [0x0001] = Opcode.Ld_XHLI_A,
                [0x0002] = Opcode.Dec_HL,
                [0x0003] = Opcode.Ld_B_XHL,
                [initialHLAddress] = 0
            })
            .BuildGameBoy();

        var cycles = gameBoy.Update();
        var _ = gameBoy.Update();
        _ = gameBoy.Update();

        var processor = (ITestableProcessor)gameBoy.GetProcessor();
        Assert.Equal(expectedCycles, cycles);
        Assert.Equal(initialAValue, processor.GetValueOfRegisterA());
        Assert.Equal(initialAValue, processor.GetValueOfRegisterB());
        Assert.Equal(initialHLAddress, processor.GetValueOfRegisterHL());
    }

    [Fact]
    public static unsafe void LD_XHLD_A_AddressIsDecrementedAndValueStored()
    {
        var expectedCycles = 8;
        var initialHLAddress = (ushort)0x0111;
        var initialAValue = (byte)0xA0;

        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithProcessor(processor => processor
                .Set8BitGeneralPurposeRegisters(initialAValue, 0x00, 0x00, 0x00, 0x00, (byte)(initialHLAddress >> 8), (byte)(initialHLAddress & 0xff))
                .SetFlags(false, false, false, false)
                .SetProgramCounter(0x0001)
            )
            .WithMemory(() => new Dictionary<ushort, byte>
            {
                [0x0001] = Opcode.Ld_XHLD_A,
                [0x0002] = Opcode.Inc_HL,
                [0x0003] = Opcode.Ld_B_XHL,
                [initialHLAddress] = 0
            })
            .BuildGameBoy();

        var cycles = gameBoy.Update();
        var _ = gameBoy.Update();
        _ = gameBoy.Update();

        var processor = (ITestableProcessor)gameBoy.GetProcessor();
        Assert.Equal(expectedCycles, cycles);
        Assert.Equal(initialAValue, processor.GetValueOfRegisterA());
        Assert.Equal(initialAValue, processor.GetValueOfRegisterB());
        Assert.Equal(initialHLAddress, processor.GetValueOfRegisterHL());
    }
}