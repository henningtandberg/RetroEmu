namespace RetroEmu.GB.TestSetup;

public static class ProcessorTestExtensions
{
    public static ITestableProcessor Set8BitGeneralPurposeRegisters(this ITestableProcessor processor, byte a, byte b, byte c, byte d, byte e, byte h, byte l)
    {
        processor.GetRegisters().A = a;
        processor.GetRegisters().B = b;
        processor.GetRegisters().C = c;
        processor.GetRegisters().D = d;
        processor.GetRegisters().E = e;
        processor.GetRegisters().H = h;
        processor.GetRegisters().L = l;

        return processor;
    }
    
    public static ITestableProcessor Set16BitGeneralPurposeRegisters(this ITestableProcessor processor, ushort af, ushort bc, ushort de, ushort hl, ushort sp)
    {
        processor.GetRegisters().AF = af;
        processor.GetRegisters().BC = bc;
        processor.GetRegisters().DE = de;
        processor.GetRegisters().HL = hl;
        processor.GetRegisters().SP = sp;

        return processor;
    }
    
    public static ITestableProcessor SetStackPointer(this ITestableProcessor processor, ushort sp)
    {
        processor.GetRegisters().SP = sp;

        return processor;
    }
    
    public static ITestableProcessor SetProgramCounter(this ITestableProcessor processor, ushort pc)
    {
        processor.GetRegisters().PC = pc;

        return processor;
    }
    
    public static byte GetValueOfRegisterA(this ITestableProcessor processor) => processor.GetRegisters().A;
    public static byte GetValueOfRegisterB(this ITestableProcessor processor) => processor.GetRegisters().B;
    public static byte GetValueOfRegisterC(this ITestableProcessor processor) => processor.GetRegisters().C;
    public static byte GetValueOfRegisterD(this ITestableProcessor processor) => processor.GetRegisters().D;
    public static byte GetValueOfRegisterE(this ITestableProcessor processor) => processor.GetRegisters().E;
    public static byte GetValueOfRegisterH(this ITestableProcessor processor) => processor.GetRegisters().H;
    public static byte GetValueOfRegisterL(this ITestableProcessor processor) => processor.GetRegisters().L;
    
    public static ushort GetValueOfRegisterAF(this ITestableProcessor processor) => processor.GetRegisters().AF;
    public static ushort GetValueOfRegisterBC(this ITestableProcessor processor) => processor.GetRegisters().BC;
    public static ushort GetValueOfRegisterDE(this ITestableProcessor processor) => processor.GetRegisters().DE;
    public static ushort GetValueOfRegisterPC(this ITestableProcessor processor) => processor.GetRegisters().PC;
    public static ushort GetValueOfRegisterSP(this ITestableProcessor processor) => processor.GetRegisters().SP;
    public static ushort GetValueOfRegisterHL(this ITestableProcessor processor) => processor.GetRegisters().HL;
    
    public static ITestableProcessor SetFlags(this ITestableProcessor processor, bool zeroFlag, bool subtractFlag, bool halfCarryFlag, bool carryFlag)
    {
        processor.SetZeroFlagToValue(zeroFlag);
        processor.SetSubtractFlagToValue(subtractFlag);
        processor.SetHalfCarryFlagToValue(halfCarryFlag);
        processor.SetCarryFlagToValue(carryFlag);
        
        return processor;
    }
}