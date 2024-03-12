using System;
using System.Diagnostics;
using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU
{
    public unsafe partial class Processor
    {
        // TODO: Better name for "Op"
        internal OperationOutput PerformOpOperation(OpType opType, IOperationInput operationInput)
        {
            return opType switch {
                OpType.Add => Add(operationInput),
                OpType.Add16 => Add16(operationInput),
                OpType.AddSP => AddSP(operationInput),
                OpType.Adc => Adc(operationInput),
                OpType.And => And(operationInput),
                OpType.Dec => Dec(operationInput),
                OpType.Ld => Load(operationInput),
                OpType.Jp => Jump(operationInput),
                OpType.JpConditionally => JumpConditionally(operationInput),
                OpType.Sbc => Sbc(operationInput),
                OpType.Sub => Sub(operationInput),
                _ => throw new NotImplementedException()
            };
        }

        internal OperationOutput PerformConditionalOpOperation(ConditionalOpType opType, IOperationInput operationInput)
        {
            return opType switch
            {
                ConditionalOpType.JpConditionally => JumpConditionally(operationInput),
                _ => throw new NotImplementedException()
            };
        }
    }
}
