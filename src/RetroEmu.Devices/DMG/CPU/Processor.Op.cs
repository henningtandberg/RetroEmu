using System;
using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU
{
    public partial class Processor
    {
        private (ushort, ushort) PerformOperation(OpType opType, ushort input) =>
            opType switch
            {
                OpType.Add => Add(input),
                OpType.Add16 => Add16(input),
                OpType.AddSP => AddSP(input),
                OpType.Adc => Adc(input),
                OpType.And => And(input),
                OpType.Cp => Cp(input),
                OpType.Dec => Dec(input),
                OpType.Dec16 => Dec16(input),
                OpType.Inc => Inc(input),
                OpType.Inc16 => Inc16(input),
                OpType.Or => Or(input),
                OpType.Sbc => Sbc(input),
                OpType.Sub => Sub(input),
                OpType.Xor => Xor(input),
                OpType.Cpl => Cpl(input),
                OpType.Ccf => Ccf(input),
                OpType.Scf => Scf(input),
                OpType.Daa => Daa(input),
                _ => throw new NotImplementedException()
            };
        
        // TODO: Better name for "Op"
        internal (ushort, ushort) PerformALUOpOperation(ALUOpType opType, ushort input)
        {
            return opType switch {
                ALUOpType.Add => Add(input),
                ALUOpType.Add16 => Add16(input),
                ALUOpType.AddSP => AddSP(input),
                ALUOpType.Adc => Adc(input),
                ALUOpType.And => And(input),
                ALUOpType.Cp => Cp(input),
                ALUOpType.Dec => Dec(input),
                ALUOpType.Dec16 => Dec16(input),
                ALUOpType.Inc => Inc(input),
                ALUOpType.Inc16 => Inc16(input),
                ALUOpType.Or => Or(input),
                ALUOpType.Sbc => Sbc(input),
                ALUOpType.Sub => Sub(input),
                ALUOpType.Xor => Xor(input),
                ALUOpType.Cpl => Cpl(input),
                ALUOpType.Ccf => Ccf(input),
                ALUOpType.Scf => Scf(input),
                ALUOpType.Daa => Daa(input),
                _ => throw new NotImplementedException()
            };
        }

        internal (ushort, ushort) PerformConditionalOpOperation(ConditionalOpType opType, ushort input, bool condition)
        {
            return opType switch
            {
                ConditionalOpType.Jp => JumpConditionally(input, condition),
                ConditionalOpType.Jr => JumpRelativeConditionally(input, condition),
                ConditionalOpType.Call => CallConditionally(input, condition),
                ConditionalOpType.Ret => ReturnConditionally(input, condition),
                _ => throw new NotImplementedException()
            };
        }
        
        internal (ushort, ushort) PerformRotateOpOperation(RotateOpType opType, ushort input, RotationDirection direction)
        {
            return (opType, direction) switch
            {
                (RotateOpType.Rotate, RotationDirection.Left) => throw new NotImplementedException("RotateOpType.Rotate is not implemented."),
                (RotateOpType.Rotate, RotationDirection.Right) => throw new NotImplementedException("RotateOpType.Rotate is not implemented."),
                (RotateOpType.RotateThroughCarry, RotationDirection.Left) => RotateLeft((byte)input),
                (RotateOpType.RotateThroughCarry, RotationDirection.Right) => RotateRightThroughCarry((byte)input),
                _ => throw new NotImplementedException()
            };
        }
    }
}
