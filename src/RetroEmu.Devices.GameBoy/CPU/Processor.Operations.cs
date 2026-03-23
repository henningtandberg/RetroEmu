using System;
using RetroEmu.Devices.GameBoy.CPU.Instructions;

namespace RetroEmu.Devices.GameBoy.CPU;

public partial class Processor
{
    private OperationResult PerformOperation(OpType opType, ushort input) => opType switch
    {
        OpType.Add => Add((byte)input),
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
        OpType.Push => Push(input),
        OpType.Pop => Pop(input),
        OpType.Nop => Nop(input),
        OpType.Rst => RestartAtGivenAddress(input),
        OpType.JpAlways => Jump(input),
        OpType.JpNc => JumpConditionally(input, EvaluateCondition(ConditionType.NC)),
        OpType.JpC => JumpConditionally(input, EvaluateCondition(ConditionType.C)),
        OpType.JpNz => JumpConditionally(input, EvaluateCondition(ConditionType.NZ)),
        OpType.JpZ => JumpConditionally(input, EvaluateCondition(ConditionType.Z)),
        OpType.JrAlways => JumpRelativeConditionally(input, true),
        OpType.JrNc => JumpRelativeConditionally(input, EvaluateCondition(ConditionType.NC)),
        OpType.JrC => JumpRelativeConditionally(input, EvaluateCondition(ConditionType.C)),
        OpType.JrNz => JumpRelativeConditionally(input, EvaluateCondition(ConditionType.NZ)),
        OpType.JrZ => JumpRelativeConditionally(input, EvaluateCondition(ConditionType.Z)),
        OpType.RetI => Return(input, true),
        OpType.RetAlways => Return(input, false),
        OpType.RetNc => ReturnConditionally(input, EvaluateCondition(ConditionType.NC)),
        OpType.RetC => ReturnConditionally(input, EvaluateCondition(ConditionType.C)),
        OpType.RetNz => ReturnConditionally(input, EvaluateCondition(ConditionType.NZ)),
        OpType.RetZ => ReturnConditionally(input, EvaluateCondition(ConditionType.Z)),
        OpType.CallAlways => Call(input),
        OpType.CallNc => CallConditionally(input, EvaluateCondition(ConditionType.NC)),
        OpType.CallC => CallConditionally(input, EvaluateCondition(ConditionType.C)),
        OpType.CallNz => CallConditionally(input, EvaluateCondition(ConditionType.NZ)),
        OpType.CallZ => CallConditionally(input, EvaluateCondition(ConditionType.Z)),
        OpType.RotateLeft => RotateLeft((byte)input),
        OpType.RotateRight => RotateRight((byte)input),
        OpType.RotateLeftThroughCarry => RotateLeftThroughCarry((byte)input),
        OpType.RotateRightThroughCarry => RotateRightThroughCarry((byte)input),
        OpType.DI => DisableInterrupt(),
        OpType.EI => EnableInterrupt(),
        OpType.Halt => Halt(input),
        OpType.PreCb => throw new ArgumentException("PreCb is handled elsewhere"),
        _ => throw new NotImplementedException()
    };
    
    private bool EvaluateCondition(ConditionType conditionType) => conditionType switch
    {
        ConditionType.Always => true,
        ConditionType.Z => IsSet(Flag.Zero),
        ConditionType.C => IsSet(Flag.Carry),
        ConditionType.NZ => !IsSet(Flag.Zero),
        ConditionType.NC => !IsSet(Flag.Carry),
        _ => throw new NotImplementedException()
    };
    
    private OperationResult Add(byte input)
    {
        var registerA = Registers.A;
        var result = registerA + input;

        SetFlagToValue(Flag.Carry, result > 0xFF);
        SetFlagToValue(Flag.HalfCarry, (registerA & 0x0F) + (input & 0x0F) > 0x0F);
        ClearFlag(Flag.Subtract);
        SetFlagToValue(Flag.Zero, (byte)result == 0);

        return new OperationResult((byte)result, 4);
    }

    private OperationResult Add16(ushort input)
    {
        var registerHL = Registers.HL;
        var result = registerHL + input;

        var halfCarry = 0xF000 & ((registerHL & 0x0FFF) + (input & 0x0FFF));

        SetFlagToValue(Flag.Carry, result > 0xFFFF);
        SetFlagToValue(Flag.HalfCarry, halfCarry > 0x0FFF);
        ClearFlag(Flag.Subtract);

        return new OperationResult((ushort)result, 8);
    }

    private OperationResult AddSP(ushort input)
    {
        var registerSP = Registers.SP;
        var result1 = (0x000F & registerSP) + (0x000F & (byte)input);
        var result2 = (0x00FF & registerSP) + (0x00FF & (byte)input);
        var halfCarry = result1 > 0x0F;
        var carry = result2 > 0xFF;

        var resultLSB = (byte)result2;
        var resultMSB = registerSP & 0xFF00;

        var isNegative = (sbyte)input < 0;
        if (!isNegative && carry)
        {
            resultMSB += 0x0100;
        }
        else if(isNegative && !carry)
        {
            resultMSB -= 0x0100;
        }
        var result = resultMSB | resultLSB;

        SetFlagToValue(Flag.Carry, carry); // Set or reset according to operation?
        SetFlagToValue(Flag.HalfCarry, halfCarry); // Set or reset according to operation?
        ClearFlag(Flag.Subtract);
        ClearFlag(Flag.Zero);

        return new OperationResult((ushort)result, 12); // cycles (Not sure why this one is more expensive)
    }

    private OperationResult Adc(ushort input)
    {
        var carry = IsSet(Flag.Carry) ? 1 : 0;
        var registerA = Registers.A;
        var result = registerA + input + carry;

        SetFlagToValue(Flag.Carry, result > 0xFF);
        SetFlagToValue(Flag.HalfCarry, (registerA & 0x0F) + (input & 0x0F) + carry > 0x0F);
        ClearFlag(Flag.Subtract);
        SetFlagToValue(Flag.Zero, (byte)result == 0);

        Registers.A = (byte)result;
        return new OperationResult((ushort)result, 4);
    }

    private OperationResult And(ushort input)
    {
        var registerA = Registers.A;
        var result = registerA & input;

        SetFlagToValue(Flag.Zero, result == 0);
        ClearFlag(Flag.Subtract);
        SetFlag(Flag.HalfCarry);
        ClearFlag(Flag.Carry);

        return new OperationResult((ushort)result, 4);
    }

    private OperationResult Cp(ushort input)
    {
        var registerA = Registers.A;

        SetFlagToValue(Flag.Zero, registerA == input);
        SetFlag(Flag.Subtract);
        SetFlagToValue(Flag.HalfCarry, (registerA & 0x0F) < (input & 0x0F)); // TODO: Doublecheck if this is correct
        SetFlagToValue(Flag.Carry, registerA < input);

        return new OperationResult(0, 4);
    }

    private OperationResult Dec(ushort input)
    {
        var result = input - 1;

        SetFlagToValue(Flag.Zero, result == 0);
        SetFlag(Flag.Subtract);
        SetFlagToValue(Flag.HalfCarry, (result & 0x0F) == 0x0F);

        return new OperationResult((byte)result, 4);
    }

    private OperationResult Dec16(ushort input)
    {
        var result = input - 1;

        return new OperationResult((ushort)result, 8);
    }

    private OperationResult Inc(ushort input)
    {
        var result = input + 1;

        SetFlagToValue(Flag.Zero, (byte)result == 0);
        ClearFlag(Flag.Subtract);
        SetFlagToValue(Flag.HalfCarry, ((input & 0x0F) + 1) > 0x0F);

        return new OperationResult((ushort)result, 4);
    }

    private OperationResult Inc16(ushort input)
    {
        var result = input + 1;

        return new OperationResult((ushort)result, 8);
    }

    private OperationResult Or(ushort input)
    {
        var registerA = Registers.A;
        var result = registerA | input;

        SetFlagToValue(Flag.Zero, result == 0);
        ClearFlag(Flag.Subtract);
        ClearFlag(Flag.HalfCarry);
        ClearFlag(Flag.Carry);

        return new OperationResult((ushort)result, 4);
    }

    private OperationResult Sbc(ushort input)
    {
        var carry = IsSet(Flag.Carry) ? 1 : 0;
        var result = (Registers.A - input - carry) & 0xFF;

        SetFlagToValue(Flag.Zero, result == 0);
        SetFlag(Flag.Subtract);
        SetFlagToValue(Flag.HalfCarry, (Registers.A & 0x0F) < (input & 0x0F) + carry);
        SetFlagToValue(Flag.Carry, Registers.A < input + carry);

        return new OperationResult((ushort)result, 4);
    }

    private OperationResult Sub(ushort input)
    {
        var result = Registers.A - input;

        SetFlagToValue(Flag.Zero, result == 0);
        SetFlag(Flag.Subtract);
        SetFlagToValue(Flag.HalfCarry, (Registers.A & 0x0F) < (input & 0x0F));
        SetFlagToValue(Flag.Carry, result < 0);

        return new OperationResult((ushort)result, 4);
    }

    private OperationResult Xor(ushort input)
    {
        var registerA = Registers.A;
        var result = registerA ^ input;

        SetFlagToValue(Flag.Zero, result == 0);
        ClearFlag(Flag.Subtract);
        ClearFlag(Flag.HalfCarry);
        ClearFlag(Flag.Carry);

        return new OperationResult((ushort)result, 4);
    }

    private OperationResult Cpl(ushort input)
    {
        var result = ~input & 0xFF;

        SetFlag(Flag.Subtract);
        SetFlag(Flag.HalfCarry);

        return new OperationResult((ushort)result, 4);
    }

    private OperationResult Ccf(ushort _)
    {
        ClearFlag(Flag.Subtract);
        ClearFlag(Flag.HalfCarry);
        ToggleFlag(Flag.Carry);

        return new OperationResult(_, 4);
    }

    private OperationResult Scf(ushort _)
    {
        ClearFlag(Flag.Subtract);
        ClearFlag(Flag.HalfCarry);
        SetFlag(Flag.Carry);

        return new OperationResult(_, 4);
    }

    private OperationResult Daa(ushort input)
    {
        var subtractIsNotSet = !IsSet(Flag.Subtract);
        var result = (byte)input;
        var correction = 0;

        if (subtractIsNotSet && (result & 0x0F) > 0x09 || IsSet(Flag.HalfCarry))
        {
            correction |= 0x06;
        }

        if (subtractIsNotSet && result > 0x99 || IsSet(Flag.Carry))
        {
            correction |= 0x60;
            SetFlag(Flag.Carry);
        }

        result += IsSet(Flag.Subtract) ? (byte)-correction : (byte)correction;
        result &= 0xFF;

        SetFlagToValue(Flag.Zero, result == 0);
        ClearFlag(Flag.HalfCarry);

        return new OperationResult(result, 4);
    }

    private OperationResult Ld(ushort input)
    {
        return new OperationResult(input, 4);
    }

    private OperationResult Push(ushort input)
    {
        return new OperationResult(input, 4);
    }

    private OperationResult Pop(ushort input)
    {
        return new OperationResult(input, 4);
    }

    private OperationResult Nop(ushort _)
    {
        // Here we trust that the programmes have set the Fetch and Write to None!
        return new OperationResult(0, 4);
    }

    private OperationResult RestartAtGivenAddress(ushort address)
    {
        Push16ToStack(Registers.PC);
        return new OperationResult(address, 32);
    }

    private OperationResult Jump(ushort input)
    {
        return new OperationResult(input, 8);
    }

    private OperationResult JumpConditionally(ushort input, bool condition)
    {
        return condition
            ? Jump(input)
            : new OperationResult(Registers.PC, 4);
    }

    private OperationResult JumpRelative(ushort input)
    {
        var target = Registers.PC + (sbyte)input;
        return new OperationResult((ushort)target, 8);
    }

    private OperationResult JumpRelativeConditionally(ushort input, bool condition)
    {
        return condition
            ? JumpRelative(input)
            : new OperationResult(Registers.PC, 4);
    }

    private OperationResult Return(ushort _, bool enableInterrupts)
    {
        var (_, nextInstruction) = Pop16FromStack();

        if (enableInterrupts)
        {
            interruptState.SetInterruptMasterEnable(true);
        }

        return new OperationResult(nextInstruction, 8);
    }

    private OperationResult ReturnConditionally(ushort input, bool condition)
    {
        if (!condition)
            return new OperationResult(input, 8);

        var (popCycles, nextInstruction) = Pop16FromStack();

        return new OperationResult(nextInstruction, popCycles + 8);
    }

    private OperationResult Call(ushort input)
    {
        var nextInstruction = Registers.PC;
        Push16ToStack(nextInstruction);

        return new OperationResult(input, 16);
    }

    private OperationResult CallConditionally(ushort input, bool condition)
    {
        return condition
            ? Call(input)
            : new OperationResult(Registers.PC, 4);
    }

    private OperationResult RotateLeft(byte input)
    {
        var newCarry = (input & 0x80) > 0;
        var lsbMask = newCarry ? 0x01 : 0x00;

        var result = (byte)((input << 1) | lsbMask);

        ClearFlag(Flag.Zero);
        ClearFlag(Flag.Subtract);
        ClearFlag(Flag.HalfCarry);
        SetFlagToValue(Flag.Carry, newCarry);

        return new OperationResult(result, 4);
    }

    private OperationResult RotateLeftThroughCarry(byte input)
    {
        var newCarry = (input & 0x80) > 0;
        var lsbMask = IsSet(Flag.Carry) ? 0x01 : 0x00;

        var result = (byte)((input << 1) | lsbMask);

        ClearFlag(Flag.Zero);
        ClearFlag(Flag.Subtract);
        ClearFlag(Flag.HalfCarry);
        SetFlagToValue(Flag.Carry, newCarry);

        return new OperationResult(result, 4);
    }

    private OperationResult RotateRight(byte input)
    {
        var newCarry = (input & 0x01) > 0;
        var msbMask = newCarry ? 0x80 : 0x00;

        var result = (input >> 1) | msbMask;

        ClearFlag(Flag.Zero);
        ClearFlag(Flag.Subtract);
        ClearFlag(Flag.HalfCarry);
        SetFlagToValue(Flag.Carry, newCarry);


        return new OperationResult((ushort)result, 4);
    }

    private OperationResult RotateRightThroughCarry(byte input)
    {
        var newCarry = (input & 0x01) > 0;
        var msbMask = IsSet(Flag.Carry) ? 0x80 : 0x00;

        var result = (input >> 1) | msbMask;

        ClearFlag(Flag.Zero);
        ClearFlag(Flag.Subtract);
        ClearFlag(Flag.HalfCarry);
        SetFlagToValue(Flag.Carry, newCarry);


        return new OperationResult((ushort)result, 4);
    }

    private OperationResult DisableInterrupt()
    {
        interruptState.ResetDisableInterruptCounter();
        return new OperationResult(0, 4);
    }

    private OperationResult EnableInterrupt()
    {
        interruptState.ResetEnableInterruptCounter();
        return new OperationResult(0, 4);
    }

    private OperationResult Halt(ushort value)
    {
        var result = (ushort)(value - 1);

        // IME == 0 and there is a pending interrupt (Halt Bug)
        if (interruptState.InterruptMasterEnableIsDisabledAndThereIsAPendingInterrupt())
        {
            result++;
        }

        return new OperationResult(result, 4);
    }
}