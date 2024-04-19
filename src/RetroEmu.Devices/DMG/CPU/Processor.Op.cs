using System;
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
                ALUOpType.Cp => Cp(input),
                ALUOpType.Dec => Dec(input),
                ALUOpType.Inc => Inc(input),
                ALUOpType.Or => Or(input),
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
