using System;
using RetroEmu.Devices.DMG;
using Xunit;

namespace RetroEmu.GB.TestSetup;

public static class GameBoyTestExtensions
{
    public static void SetInitialState(this IGameBoy gameBoy, InitialState initialState)
    {
        var processor = (ITestableProcessor)gameBoy.GetProcessor();

        processor.Set8BitGeneralPurposeRegisters(
            a: initialState.A,
            b: initialState.B,
            c: initialState.C,
            d: initialState.D,
            e: initialState.E,
            h: initialState.H,
            l: initialState.L);

        processor.Set16BitGeneralPurposeRegisters(
            af: initialState.AF,
            bc: initialState.BC,
            de: initialState.DE,
            hl: initialState.HL,
            sp: initialState.SP);

        processor.SetProgramCounter(initialState.PC);

        processor.SetFlags(
            zeroFlag: initialState.ZeroFlag,
            subtractFlag: initialState.SubtractFlag,
            halfCarryFlag: initialState.HalfCarryFlag,
            carryFlag: initialState.CarryFlag);

        var memory = gameBoy.GetMemory();
        foreach (var (address, value) in initialState.Memory)
        {
            memory.Write(address, value);
        }
    }

    public static void AssertExpectedState(this IGameBoy gameBoy, ExpectedState expectedState)
    {
        var processor = (ITestableProcessor)gameBoy.GetProcessor();

        // --- 8-bit registers ---
        if (expectedState.A.HasValue)
            Assert.True(processor.GetValueOfRegisterA() == expectedState.A.Value,
                $"Register A mismatch: expected {expectedState.A.Value:X2}, got {processor.GetValueOfRegisterA():X2}");

        if (expectedState.B.HasValue)
            Assert.True(processor.GetValueOfRegisterB() == expectedState.B.Value,
                $"Register B mismatch: expected {expectedState.B.Value:X2}, got {processor.GetValueOfRegisterB():X2}");

        if (expectedState.C.HasValue)
            Assert.True(processor.GetValueOfRegisterC() == expectedState.C.Value,
                $"Register C mismatch: expected {expectedState.C.Value:X2}, got {processor.GetValueOfRegisterC():X2}");

        if (expectedState.D.HasValue)
            Assert.True(processor.GetValueOfRegisterD() == expectedState.D.Value,
                $"Register D mismatch: expected {expectedState.D.Value:X2}, got {processor.GetValueOfRegisterD():X2}");

        if (expectedState.E.HasValue)
            Assert.True(processor.GetValueOfRegisterE() == expectedState.E.Value,
                $"Register E mismatch: expected {expectedState.E.Value:X2}, got {processor.GetValueOfRegisterE():X2}");

        if (expectedState.H.HasValue)
            Assert.True(processor.GetValueOfRegisterH() == expectedState.H.Value,
                $"Register H mismatch: expected {expectedState.H.Value:X2}, got {processor.GetValueOfRegisterH():X2}");

        if (expectedState.L.HasValue)
            Assert.True(processor.GetValueOfRegisterL() == expectedState.L.Value,
                $"Register L mismatch: expected {expectedState.L.Value:X2}, got {processor.GetValueOfRegisterL():X2}");

        // --- 16-bit registers ---
        if (expectedState.AF.HasValue)
            Assert.True(processor.GetValueOfRegisterAF() == expectedState.AF.Value,
                $"Register AF mismatch: expected {expectedState.AF.Value:X4}, got {processor.GetValueOfRegisterAF():X4}");

        if (expectedState.BC.HasValue)
            Assert.True(processor.GetValueOfRegisterBC() == expectedState.BC.Value,
                $"Register BC mismatch: expected {expectedState.BC.Value:X4}, got {processor.GetValueOfRegisterBC():X4}");

        if (expectedState.DE.HasValue)
            Assert.True(processor.GetValueOfRegisterDE() == expectedState.DE.Value,
                $"Register DE mismatch: expected {expectedState.DE.Value:X4}, got {processor.GetValueOfRegisterDE():X4}");

        if (expectedState.HL.HasValue)
            Assert.True(processor.GetValueOfRegisterHL() == expectedState.HL.Value,
                $"Register HL mismatch: expected {expectedState.HL.Value:X4}, got {processor.GetValueOfRegisterHL():X4}");

        if (expectedState.SP.HasValue)
            Assert.True(processor.GetValueOfRegisterSP() == expectedState.SP.Value,
                $"Register SP mismatch: expected {expectedState.SP.Value:X4}, got {processor.GetValueOfRegisterSP():X4}");

        // --- Flags ---
        if (expectedState.ZeroFlag.HasValue)
            Assert.True(processor.GetValueOfZeroFlag() == expectedState.ZeroFlag.Value,
                $"Zero flag mismatch: expected {expectedState.ZeroFlag.Value}, got {processor.GetValueOfZeroFlag()}");

        if (expectedState.SubtractFlag.HasValue)
            Assert.True(processor.GetValueOfSubtractFlag() == expectedState.SubtractFlag.Value,
                $"Carry flag mismatch: expected {expectedState.SubtractFlag.Value}, got {processor.GetValueOfZeroFlag()}");

        if (expectedState.HalfCarryFlag.HasValue)
            Assert.True(processor.GetValueOfHalfCarryFlag() == expectedState.HalfCarryFlag.Value,
                $"Half-Carry flag mismatch: expected {expectedState.HalfCarryFlag.Value}, got {processor.GetValueOfHalfCarryFlag()}");

        if (expectedState.CarryFlag.HasValue)
            Assert.True(processor.GetValueOfCarryFlag() == expectedState.CarryFlag.Value,
                $"Carry flag mismatch: expected {expectedState.CarryFlag.Value}, got {processor.GetValueOfCarryFlag()}");

        // --- Memory ---
        var memory = gameBoy.GetMemory();
        foreach (var (address, expectedValue) in expectedState.Memory)
        {
            var actualValue = memory.Read(address);
            Assert.True(actualValue == expectedValue,
                $"Memory mismatch at 0x{address:X4}: expected {expectedValue:X2}, got {actualValue:X2}");
        }
    }

    public static void RunFor(this IGameBoy gameBoy, int cycles)
    {
        for (var i = 0; i < cycles; i++)
        {
            gameBoy.Update();
        }
    }

    public static void RunWhile(this IGameBoy gameBoy, Func<bool> predicate, int maxCycles = int.MaxValue)
    {
        for (var i = 0; i < maxCycles && predicate(); i++)
        {
            gameBoy.Update();
        }
    }
}