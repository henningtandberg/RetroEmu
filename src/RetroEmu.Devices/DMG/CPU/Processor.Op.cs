using System;
using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU;

public partial class Processor
{
    private (ushort, ushort) PerformOperation(OpType opType, ushort input) =>
        opType switch
        {
            OpType.Add => Add(input),
            OpType.Add16 => Add16(input),
            OpType.AddSp => AddSP(input),
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
            OpType.Ld => Ld(input),
            // TODO: Add Push and Pop
            OpType.Nop => Nop(input),
            OpType.Rst => RestartAtGivenAddress(input),
            // TODO: Neater way to handle these?
            OpType.JpAlways => JumpConditionally(input, true),
            OpType.JpNc => JumpConditionally(input, EvaluateCondition(ConditionType.NC)),
            OpType.JpC => JumpConditionally(input, EvaluateCondition(ConditionType.C)),
            OpType.JpNz => JumpConditionally(input, EvaluateCondition(ConditionType.NZ)),
            OpType.JpZ => JumpConditionally(input, EvaluateCondition(ConditionType.Z)),
            OpType.JrAlways => JumpRelativeConditionally(input, true),
            OpType.JrNc => JumpRelativeConditionally(input, EvaluateCondition(ConditionType.NC)),
            OpType.JrC => JumpRelativeConditionally(input, EvaluateCondition(ConditionType.C)),
            OpType.JrNz => JumpRelativeConditionally(input, EvaluateCondition(ConditionType.NZ)),
            OpType.JrZ => JumpRelativeConditionally(input, EvaluateCondition(ConditionType.Z)),
            OpType.RetAlways => ReturnConditionally(input, true),
            OpType.RetNc => ReturnConditionally(input, EvaluateCondition(ConditionType.NC)),
            OpType.RetC => ReturnConditionally(input, EvaluateCondition(ConditionType.C)),
            OpType.RetNz => ReturnConditionally(input, EvaluateCondition(ConditionType.NZ)),
            OpType.RetZ => ReturnConditionally(input, EvaluateCondition(ConditionType.Z)),
            OpType.CallAlways => CallConditionally(input, true),
            OpType.CallNc => CallConditionally(input, EvaluateCondition(ConditionType.NC)),
            OpType.CallC => CallConditionally(input, EvaluateCondition(ConditionType.C)),
            OpType.CallNz => CallConditionally(input, EvaluateCondition(ConditionType.NZ)),
            OpType.CallZ => CallConditionally(input, EvaluateCondition(ConditionType.Z)),
            _ => throw new NotImplementedException()
        };
    
    private bool EvaluateCondition(ConditionType conditionType) =>
        conditionType switch
        {
            ConditionType.Always => true,
            ConditionType.Z => IsSet(Flag.Zero),
            ConditionType.C => IsSet(Flag.Carry),
            ConditionType.NZ => !IsSet(Flag.Zero),
            ConditionType.NC => !IsSet(Flag.Carry),
            _ => throw new NotImplementedException()
        };
        
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