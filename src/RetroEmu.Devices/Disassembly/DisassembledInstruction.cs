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
    
    public IOperandToken Operand1Token => IsJump() || IsReturn()
        ? new EmptyOperandToken()
        : WriteType switch
    {
        WriteType.A => new RegisterOperandToken("A"),
        WriteType.B => new RegisterOperandToken("B"),
        WriteType.C => new RegisterOperandToken("C"),
        WriteType.D => new RegisterOperandToken("D"),
        WriteType.E => new RegisterOperandToken("E"),
        WriteType.H => new RegisterOperandToken("H"),
        WriteType.L => new RegisterOperandToken("L"),
        WriteType.XBC => new RegisterOperandToken("(BC)"),
        WriteType.XDE => new RegisterOperandToken("(DE)"),
        WriteType.XHL => new RegisterOperandToken("(HL)"),
        WriteType.XHLD => new RegisterOperandToken("(HL-)"),
        WriteType.XHLI => new RegisterOperandToken("(HL+)"),
        WriteType.XN16 => new ImmediateOperandToken($"(${GetImmediate16Bit():X4})"),
        WriteType.XN16x2 => new ImmediateOperandToken($"(${GetImmediate16Bit():X4})"),
        WriteType.XC => new EmptyOperandToken(),
        WriteType.XN8 => new ImmediateOperandToken($"(${GetImmediate8Bit():X2})"),
        WriteType.AF => new RegisterOperandToken("AF"),
        WriteType.BC => new RegisterOperandToken("BC"),
        WriteType.DE => new RegisterOperandToken("DE"),
        WriteType.HL => new RegisterOperandToken("HL"),
        WriteType.SP => new RegisterOperandToken("SP"),
        WriteType.PC => new RegisterOperandToken("PC"),
        WriteType.Push => new EmptyOperandToken(),
        WriteType.None => new EmptyOperandToken(),
        _ => throw new ArgumentOutOfRangeException($"Unknown write type: {WriteType}")
    };

    public IOperandToken Operand2Token => (WriteType, FetchType).Equals()
        ? new EmptyOperandToken()
        : FetchType switch
    {
        FetchType.A => new RegisterOperandToken("A"),
        FetchType.B => new RegisterOperandToken("B"),
        FetchType.C => new RegisterOperandToken("C"),
        FetchType.D => new RegisterOperandToken("D"),
        FetchType.E => new RegisterOperandToken("E"),
        FetchType.H => new RegisterOperandToken("H"),
        FetchType.L => new RegisterOperandToken("L"),
        FetchType.XBC => new RegisterOperandToken("(BC)"),
        FetchType.XDE => new RegisterOperandToken("(DE)"),
        FetchType.XHL => new RegisterOperandToken("(HL)"),
        FetchType.XHLD => new RegisterOperandToken("(HL-)"),
        FetchType.XHLI => new RegisterOperandToken("(HL+)"),
        FetchType.XN16 => new ImmediateOperandToken($"(${GetImmediate16Bit():X4})"),
        FetchType.N8 => new ImmediateOperandToken($"${GetImmediate8Bit():X2}"),
        FetchType.XN8 => new ImmediateOperandToken($"(${GetImmediate8Bit():X2})"),
        FetchType.XC => new EmptyOperandToken(),
        FetchType.SPN8 => new EmptyOperandToken(),
        FetchType.AF => new RegisterOperandToken("AF"),
        FetchType.BC => new RegisterOperandToken("BC"),
        FetchType.DE => new RegisterOperandToken("DE"),
        FetchType.HL => new RegisterOperandToken("HL"),
        FetchType.PC => new RegisterOperandToken("PC"),
        FetchType.SP => new RegisterOperandToken("SP"),
        FetchType.N16 => new ImmediateOperandToken($"${GetImmediate16Bit():X4}"),
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

    private ushort GetImmediate16Bit() => (ushort)((ushort)(ImmediateBytes[1] << 8) | ImmediateBytes[0]);

    public bool IsJump() => OpType switch
    {
        OpType.JpAlways or OpType.JpNz or OpType.JpZ or OpType.JpNc or OpType.JpC or
        OpType.JrAlways or OpType.JrNz or OpType.JrZ or OpType.JrNc or OpType.JrC or
        OpType.CallAlways or OpType.CallNz or OpType.CallZ or OpType.CallNc or OpType.CallC => true,
        _ => false
    };

    public bool IsReturn() => OpType switch
    {
        OpType.RetAlways or OpType.RetC or OpType.RetNc or OpType.RetZ or OpType.RetNz or OpType.Rst => true,
        _ => false
    };
}