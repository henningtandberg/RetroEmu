using RetroEmu.Devices.DMG.CPU;

namespace RetroEmu.Devices.Tests;

public static class ProcessorTestExtensions
{
    public static unsafe IProcessor Set8BitGeneralPurposeRegisters(this IProcessor processor, byte a, byte b, byte c, byte d, byte e, byte h, byte l)
    {
        *processor.Registers.A = a;
        *processor.Registers.B = b;
        *processor.Registers.C = c;
        *processor.Registers.D = d;
        *processor.Registers.E = e;
        *processor.Registers.H = h;
        *processor.Registers.L = l;

        return processor;
    }
    
    public static unsafe IProcessor Set16BitGeneralPurposeRegisters(this IProcessor processor, ushort af, ushort bc, ushort de, ushort hl, ushort sp)
    {
        *processor.Registers.AF = af;
        *processor.Registers.BC = bc;
        *processor.Registers.DE = de;
        *processor.Registers.HL = hl;
        *processor.Registers.SP = sp;

        return processor;
    }
    
    public static unsafe IProcessor SetProgramCounter(this IProcessor processor, ushort pc)
    {
        *processor.Registers.PC = pc;

        return processor;
    }
    
    public static unsafe byte GetValueOfRegisterA(this IProcessor processor)
    {
        return *processor.Registers.A;
    }

    public static unsafe ushort GetValueOfRegisterPC(this IProcessor processor)
    {
        return *processor.Registers.SP;
    }
}