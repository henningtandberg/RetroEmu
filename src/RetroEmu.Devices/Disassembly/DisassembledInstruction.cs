using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using RetroEmu.Devices.Disassembly.Tokens;
using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.Disassembly;

internal sealed record DisassembledInstruction(
    ushort Address,
    byte Opcode,
    List<byte> ImmediateBytes,
    OpType OpType,
    WriteType WriteType,
    FetchType FetchType)
    : IDisassembledInstruction
{
    public IReadOnlyList<byte> Bytes => GetBytes();

    private ImmutableList<byte> GetBytes() => new[] { Opcode }
        .Concat(ImmediateBytes)
        .ToImmutableList();

    public IOpcodeToken OpcodeToken => OpType switch
    {
        OpType.Add or OpType.Add16 or OpType.AddSp => new OpcodeToken("ADD"),
        OpType.Adc => new OpcodeToken("ADC"),
        OpType.And => new OpcodeToken("AND"),
        OpType.Dec or OpType.Dec16 => new OpcodeToken("DEC"),
        OpType.Sbc => new OpcodeToken("SBC"),
        OpType.Sub => new OpcodeToken("SUB"),
        OpType.Inc or OpType.Inc16 => new OpcodeToken("INC"),
        OpType.Cp => new OpcodeToken("CP"),
        OpType.Or => new OpcodeToken("OR"),
        OpType.Xor => new OpcodeToken("XOR"),
        OpType.Cpl => new OpcodeToken("CPL"),
        OpType.Ccf => new OpcodeToken("CCF"),
        OpType.Daa => new OpcodeToken("DAA"),
        OpType.Scf => new OpcodeToken("SCF"),
        OpType.Ld => new OpcodeToken("LD"),
        OpType.JpAlways or OpType.JpNz or OpType.JpZ or OpType.JpNc or OpType.JpC => new OpcodeToken("JP"),
        OpType.Rst => new OpcodeToken("RST"),
        OpType.Nop => new OpcodeToken("NOP"),
        OpType.JrAlways or OpType.JrNz or OpType.JrZ or OpType.JrNc or OpType.JrC => new OpcodeToken("JR"),
        OpType.RetI => new OpcodeToken("RETI"),
        OpType.RetAlways or OpType.RetNc or OpType.RetC or OpType.RetNz or OpType.RetZ => new OpcodeToken("RET"),
        OpType.CallAlways or OpType.CallNz or OpType.CallZ or OpType.CallNc or OpType.CallC => new OpcodeToken("CALL"),
        OpType.RotateLeftThroughCarry => new OpcodeToken("RLA"),
        OpType.RotateLeft => new OpcodeToken("RLCA"),
        OpType.RotateRightThroughCarry => new OpcodeToken("RRA"),
        OpType.RotateRight => new OpcodeToken("RRCA"),
        OpType.Push => new OpcodeToken("PUSH"),
        OpType.Pop => new OpcodeToken("POP"),
        OpType.DI => new OpcodeToken("DI"),
        OpType.EI => new OpcodeToken("EI"),
        OpType.Halt => new OpcodeToken("HALT"),
        _ => throw new ArgumentOutOfRangeException($"Unknown opcode type: {OpType}")
    };
    
    public IOperandToken Operand1Token => WriteType switch
    {
        WriteType.A => new OperandToken("A"),
        WriteType.B => new OperandToken("B"),
        WriteType.C => new OperandToken("C"),
        WriteType.D => new OperandToken("D"),
        WriteType.E => new OperandToken("E"),
        WriteType.H => new OperandToken("H"),
        WriteType.L => new OperandToken("L"),
        WriteType.XBC => new OperandToken("(BC)"),
        WriteType.XDE => new OperandToken("(DE)"),
        WriteType.XHL => new OperandToken("(HL)"),
        WriteType.XHLD => new OperandToken("(HL-)"),
        WriteType.XHLI => new OperandToken("(HL+)"),
        WriteType.XN16 => new OperandToken($"(${GetImmediate16Bit():X4})"),
        WriteType.XN16x2 => new OperandToken($"(${GetImmediate16Bit():X4})"),
        WriteType.XC => new EmptyOperandToken(),
        WriteType.XN8 => new OperandToken($"(${GetImmediate8Bit():X2})"),
        WriteType.AF => new OperandToken("AF"),
        WriteType.BC => new OperandToken("BC"),
        WriteType.DE => new OperandToken("DE"),
        WriteType.HL => new OperandToken("HL"),
        WriteType.SP => new OperandToken("SP"),
        WriteType.PC => new OperandToken("PC"),
        WriteType.Push => new EmptyOperandToken(),
        WriteType.None => new EmptyOperandToken(),
        _ => throw new ArgumentOutOfRangeException($"Unknown write type: {WriteType}")
    };

    public IOperandToken Operand2Token => (WriteType, FetchType).Equals()
        ? new EmptyOperandToken()
        : FetchType switch
    {
        FetchType.A => new OperandToken("A"),
        FetchType.B => new OperandToken("B"),
        FetchType.C => new OperandToken("C"),
        FetchType.D => new OperandToken("D"),
        FetchType.E => new OperandToken("E"),
        FetchType.H => new OperandToken("H"),
        FetchType.L => new OperandToken("L"),
        FetchType.XBC => new OperandToken("(BC)"),
        FetchType.XDE => new OperandToken("(DE)"),
        FetchType.XHL => new OperandToken("(HL)"),
        FetchType.XHLD => new OperandToken("(HL-)"),
        FetchType.XHLI => new OperandToken("(HL+)"),
        FetchType.XN16 => new OperandToken($"(${GetImmediate16Bit():X4})"),
        FetchType.N8 => new OperandToken($"${GetImmediate8Bit():X2}"),
        FetchType.XN8 => new OperandToken($"(${GetImmediate8Bit():X2})"),
        FetchType.XC => new EmptyOperandToken(),
        FetchType.SPN8 => new EmptyOperandToken(),
        FetchType.AF => new OperandToken("AF"),
        FetchType.BC => new OperandToken("BC"),
        FetchType.DE => new OperandToken("DE"),
        FetchType.HL => new OperandToken("HL"),
        FetchType.PC => new OperandToken("PC"),
        FetchType.SP => new OperandToken("SP"),
        FetchType.N16 => new OperandToken($"${GetImmediate16Bit():X4}"),
        FetchType.Pop => new EmptyOperandToken(),
        FetchType.Address00H => new EmptyOperandToken(),
        FetchType.Address08H => new EmptyOperandToken(),
        FetchType.Address10H => new EmptyOperandToken(),
        FetchType.Address18H => new EmptyOperandToken(),
        FetchType.Address20H => new EmptyOperandToken(),
        FetchType.Address28H => new EmptyOperandToken(),
        FetchType.Address30H => new EmptyOperandToken(),
        FetchType.Address38H => new EmptyOperandToken(),
        FetchType.None => new EmptyOperandToken(),
        _ => throw new ArgumentOutOfRangeException($"Unknown fetch type: {FetchType}")
    };

    private ushort GetImmediate8Bit() => ImmediateBytes[0];

    private ushort GetImmediate16Bit() => (ushort)((ushort)(ImmediateBytes[0] << 8) | ImmediateBytes[1]);

    internal bool IsJumpKind() => OpType switch
    {
        OpType.JpAlways or OpType.JpNz or OpType.JpZ or OpType.JpNc or OpType.JpC or
        OpType.JrAlways or OpType.JrNz or OpType.JrZ or OpType.JrNc or OpType.JrC or
        OpType.CallAlways or OpType.CallNz or OpType.CallZ or OpType.CallNc or OpType.CallC => true,
        _ => false
    };
}