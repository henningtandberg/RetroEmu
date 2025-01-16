using RetroEmu.Devices.DMG.CPU;

namespace RetroEmu.Devices.Tests.Setup;

public static class ProcessorTestExtensions
{
    public static unsafe IProcessor Set8BitGeneralPurposeRegisters(this IProcessor processor, byte a, byte b, byte c, byte d, byte e, byte h, byte l)
    {
        *processor.Registers.A = a;
        processor.Registers.B = b;
        processor.Registers.C = c;
        *processor.Registers.D = d;
        *processor.Registers.E = e;
        *processor.Registers.H = h;
        *processor.Registers.L = l;

        return processor;
    }
    
    public static unsafe IProcessor Set16BitGeneralPurposeRegisters(this IProcessor processor, ushort af, ushort bc, ushort de, ushort hl, ushort sp)
    {
        *processor.Registers.AF = af;
        processor.Registers.BC = bc;
        *processor.Registers.DE = de;
        *processor.Registers.HL = hl;
        *processor.Registers.SP = sp;

        return processor;
    }
    
    public static unsafe IProcessor SetStackPointer(this IProcessor processor, ushort sp)
    {
        *processor.Registers.SP = sp;

        return processor;
    }
    
    public static unsafe IProcessor SetProgramCounter(this IProcessor processor, ushort pc)
    {
        *processor.Registers.PC = pc;

        return processor;
    }
    
    public static unsafe byte GetValueOfRegisterA(this IProcessor processor) => *processor.Registers.A;
    public static unsafe byte GetValueOfRegisterB(this IProcessor processor) => processor.Registers.B;
    public static unsafe byte GetValueOfRegisterC(this IProcessor processor) => processor.Registers.C;
    public static unsafe byte GetValueOfRegisterD(this IProcessor processor) => *processor.Registers.D;
    public static unsafe byte GetValueOfRegisterE(this IProcessor processor) => *processor.Registers.E;
    public static unsafe byte GetValueOfRegisterH(this IProcessor processor) => *processor.Registers.H;
    public static unsafe byte GetValueOfRegisterL(this IProcessor processor) => *processor.Registers.L;
    public static unsafe ushort GetValueOfRegisterPC(this IProcessor processor) => *processor.Registers.PC;
    public static unsafe ushort GetValueOfRegisterSP(this IProcessor processor) => *processor.Registers.SP;
    
    public static IProcessor SetFlags(this IProcessor processor, bool zeroFlag, bool subtractFlag, bool halfCarryFlag, bool carryFlag)
    {
        if (zeroFlag)
        {
            processor.SetFlag(Flag.Zero);
        }
        else
        {
            processor.ClearFlag(Flag.Zero);
        }

        if (subtractFlag)
        {
            processor.SetFlag(Flag.Subtract);
        }
        else
        {
            processor.ClearFlag(Flag.Subtract);
        }

        if (halfCarryFlag)
        {
            processor.SetFlag(Flag.HalfCarry);
        }
        else
        {
            processor.ClearFlag(Flag.HalfCarry);
        }

        if (carryFlag)
        {
            processor.SetFlag(Flag.Carry);
        }
        else
        {
            processor.ClearFlag(Flag.Carry);
        }

        return processor;
    }
}