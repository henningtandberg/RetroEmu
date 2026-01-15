using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using RetroEmu.Devices.Disassembly.Tokens;
using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.Disassembly;

internal sealed record DisassembledCbInstruction(
    ushort Address,
    byte Opcode,
    IReadOnlyList<byte> ImmediateBytes,
    CBType CbType,
    WriteType WriteType,
    FetchType FetchType)
    : IDisassembledInstruction
{
    public IReadOnlyList<byte> Bytes => GetBytes();

    private ImmutableList<byte> GetBytes() => new[] { Opcode }
        .Concat(ImmediateBytes)
        .ToImmutableList();

    public IOpcodeToken OpcodeToken => CbType switch
    {
        CBType.RLC => new OpcodeToken("RLC"),
        CBType.RRC => new OpcodeToken("RRC"),
        CBType.RL => new OpcodeToken("RL"),
        CBType.RR => new OpcodeToken("RR"),
        CBType.SLA => new OpcodeToken("SLA"),
        CBType.SRA => new OpcodeToken("SRA"),
        CBType.SWAP => new OpcodeToken("SWAP"),
        CBType.SRL => new OpcodeToken("SRL"),
        CBType.BIT0 => new OpcodeToken("BIT0"),
        CBType.BIT1 => new OpcodeToken("BIT1"),
        CBType.BIT2 => new OpcodeToken("BIT2"),
        CBType.BIT3 => new OpcodeToken("BIT3"),
        CBType.BIT4 => new OpcodeToken("BIT4"),
        CBType.BIT5 => new OpcodeToken("BIT5"),
        CBType.BIT6 => new OpcodeToken("BIT6"),
        CBType.BIT7 => new OpcodeToken("BIT7"),
        CBType.RES0 => new OpcodeToken("RES0"),
        CBType.RES1 => new OpcodeToken("RES1"),
        CBType.RES2 => new OpcodeToken("RES2"),
        CBType.RES3 => new OpcodeToken("RES3"),
        CBType.RES4 => new OpcodeToken("RES4"),
        CBType.RES5 => new OpcodeToken("RES5"),
        CBType.RES6 => new OpcodeToken("RES6"),
        CBType.RES7 => new OpcodeToken("RES7"),
        CBType.SET0 => new OpcodeToken("SET0"),
        CBType.SET1 => new OpcodeToken("SET1"),
        CBType.SET2 => new OpcodeToken("SET2"),
        CBType.SET3 => new OpcodeToken("SET3"),
        CBType.SET4 => new OpcodeToken("SET4"),
        CBType.SET5 => new OpcodeToken("SET5"),
        CBType.SET6 => new OpcodeToken("SET6"),
        CBType.SET7 => new OpcodeToken("SET7"),
        _ => throw new ArgumentOutOfRangeException()
    };

    public IOperandToken Operand1Token => WriteType switch
    {
        WriteType.A => new RegisterOperandToken("A"),
        WriteType.B => new RegisterOperandToken("B"),
        WriteType.C => new RegisterOperandToken("C"),
        WriteType.D => new RegisterOperandToken("D"),
        WriteType.E => new RegisterOperandToken("E"),
        WriteType.H => new RegisterOperandToken("H"),
        WriteType.L => new RegisterOperandToken("L"),
        WriteType.XHL => new RegisterOperandToken("(HL)"),
        _ => throw new ArgumentOutOfRangeException()
    };

    public IOperandToken Operand2Token => FetchType switch
    {
        FetchType.A => new RegisterOperandToken("A"),
        FetchType.B => new RegisterOperandToken("B"),
        FetchType.C => new RegisterOperandToken("C"),
        FetchType.D => new RegisterOperandToken("D"),
        FetchType.E => new RegisterOperandToken("E"),
        FetchType.H => new RegisterOperandToken("H"),
        FetchType.L => new RegisterOperandToken("L"),
        FetchType.XHL => new RegisterOperandToken("(HL)"),
        _ => throw new ArgumentOutOfRangeException()
    };

    public bool IsJump() => false;

    public bool IsReturn() => false;
}