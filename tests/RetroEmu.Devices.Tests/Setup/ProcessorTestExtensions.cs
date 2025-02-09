using RetroEmu.Devices.DMG.CPU;

namespace RetroEmu.Devices.Tests.Setup;

public static class ProcessorTestExtensions
{
    public static ITestableProcessor Set8BitGeneralPurposeRegisters(this ITestableProcessor processor, byte a, byte b, byte c, byte d, byte e, byte h, byte l)
    {
        processor.Registers.A = a;
        processor.Registers.B = b;
        processor.Registers.C = c;
        processor.Registers.D = d;
        processor.Registers.E = e;
        processor.Registers.H = h;
        processor.Registers.L = l;

        return processor;
    }
    
    public static ITestableProcessor Set16BitGeneralPurposeRegisters(this ITestableProcessor processor, ushort af, ushort bc, ushort de, ushort hl, ushort sp)
    {
        processor.Registers.AF = af;
        processor.Registers.BC = bc;
        processor.Registers.DE = de;
        processor.Registers.HL = hl;
        processor.Registers.SP = sp;

        return processor;
    }
    
    public static ITestableProcessor SetStackPointer(this ITestableProcessor processor, ushort sp)
    {
        processor.Registers.SP = sp;

        return processor;
    }
    
    public static ITestableProcessor SetProgramCounter(this ITestableProcessor processor, ushort pc)
    {
        processor.Registers.PC = pc;

        return processor;
    }
    
    public static byte GetValueOfRegisterA(this ITestableProcessor processor) => processor.Registers.A;
    public static byte GetValueOfRegisterB(this ITestableProcessor processor) => processor.Registers.B;
    public static byte GetValueOfRegisterC(this ITestableProcessor processor) => processor.Registers.C;
    public static byte GetValueOfRegisterD(this ITestableProcessor processor) => processor.Registers.D;
    public static byte GetValueOfRegisterE(this ITestableProcessor processor) => processor.Registers.E;
    public static byte GetValueOfRegisterH(this ITestableProcessor processor) => processor.Registers.H;
    public static byte GetValueOfRegisterL(this ITestableProcessor processor) => processor.Registers.L;
    public static ushort GetValueOfRegisterPC(this ITestableProcessor processor) => processor.Registers.PC;
    public static ushort GetValueOfRegisterSP(this ITestableProcessor processor) => processor.Registers.SP;
    
    public static ITestableProcessor SetFlags(this ITestableProcessor processor, bool zeroFlag, bool subtractFlag, bool halfCarryFlag, bool carryFlag)
    {
        if (zeroFlag)
        {
            processor.SetZeroFlag();
        }
        else
        {
            processor.ClearZeroFlag();
        }

        if (subtractFlag)
        {
            processor.SetSubtractFlag();
        }
        else
        {
            processor.ClearSubtractFlag();
        }

        if (halfCarryFlag)
        {
            processor.SetHalfCarryFlag();
        }
        else
        {
            processor.ClearHalfCarryFlag();
        }

        if (carryFlag)
        {
            processor.SetCarryFlag();
        }
        else
        {
            processor.ClearCarryFlag();
        }

        return processor;
    }
}