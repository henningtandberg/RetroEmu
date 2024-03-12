using System;
using System.Diagnostics;
using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU
{
    public unsafe partial class Processor
    {
        // TODO: Better name for "Op"
        internal (ushort, ushort) PerformALUOpOperation(ALUOpType opType, ushort input)
        {
            return opType switch {
                ALUOpType.Add => Add(input),
                ALUOpType.Add16 => Add16(input),
                ALUOpType.AddSP => AddSP(input),
                ALUOpType.Adc => Adc(input),
                ALUOpType.And => And(input),
                ALUOpType.Dec => Dec(input),
                ALUOpType.Sbc => Sbc(input),
                ALUOpType.Sub => Sub(input),
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
