using System;
using System.Diagnostics;
using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU
{
    public unsafe partial class Processor
    {
        // TODO: Better name for "Op"
        internal (ushort, ushort) PerformOpOperation(OpType opType, ushort input)
        {
            return opType switch {
                OpType.Add => Add(input),
                OpType.Add16 => Add16(input),
                OpType.AddSP => AddSP(input),
                OpType.Adc => Adc(input),
                OpType.And => And(input),
                OpType.Dec => Dec(input),
                OpType.Sbc => Sbc(input),
                OpType.Sub => Sub(input),
                _ => throw new NotImplementedException()
            };
        }

        internal (ushort, ushort) PerformConditionalOpOperation(ConditionalOpType opType, ushort input, bool condition)
        {
            return opType switch
            {
                ConditionalOpType.Jp => JumpConditionally(input, condition),
                _ => throw new NotImplementedException()
            };
        }
    }
}
