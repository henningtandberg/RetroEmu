using RetroEmu.Devices;
using RetroEmu.Devices.GameBoy.CPU;
using RetroEmu.GB.TestSetup;
using Xunit;

namespace RetroEmu.GB.Tests.IsolatedOperationTests;

public class CcfTests
{
    private readonly ITestableEmulator _gameBoy = TestGameBoyBuilder.CreateBuilder().Build();

    [Theory]
    [ClassData(typeof(CcfTestData))]
    public void Ccf_CarryFlagIsComplementedAndExpectedCyclesAreCorrect(
        byte[] program, InitialState initialState, ExpectedState expectedState)
    {
        var cartridge = CartridgeBuilder.Create().WithProgram(program).Build();
        _gameBoy.Load(cartridge);
        _gameBoy.SetInitialState(initialState);

        var cycles = _gameBoy.Update();

        _gameBoy.AssertExpectedState(expectedState);
        Assert.Equal(expectedState.Cycles, cycles);
    }

    private class CcfTestData : TheoryData<byte[], InitialState, ExpectedState>
    {
        public CcfTestData()
        {
            Add([Opcode.Ccf], new InitialState { CarryFlag = true }, new ExpectedState { Cycles = 4, CarryFlag = false, HalfCarryFlag = false, ZeroFlag = false, SubtractFlag = false });
            Add([Opcode.Ccf], new InitialState { CarryFlag = false }, new ExpectedState { Cycles = 4, CarryFlag = true , HalfCarryFlag = false, ZeroFlag = false, SubtractFlag = false });
        }
    }
}