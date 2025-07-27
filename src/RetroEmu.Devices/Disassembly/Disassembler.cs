using System;
using System.Collections.Generic;
using RetroEmu.Devices.DMG;
using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.Disassembly;

internal sealed class Disassembler(IReadOnlyAddressBus addressBus, IDebugProcessor debugProcessor) : IDisassembler
{
    private static readonly Instruction[] Instructions = InstructionTableFactory.Create();
    private static int _labelCounter = 0;
    
    public Dictionary<ushort, string> Labels { get; } = new()
    {
        [0x0000] = "Restart_00",
        [0x0008] = "Restart_08",
        [0x0010] = "Restart_10",
        [0x0018] = "Restart_18",
        [0x0020] = "Restart_20",
        [0x0028] = "Restart_28",
        [0x0030] = "Restart_30",
        [0x0038] = "Restart_38",
        [0x0040] = "VBlank_Interrupt",
        [0x0048] = "STAT_Interrupt",
        [0x0050] = "Timer_Interrupt",
        [0x0058] = "Serial_Interrupt",
        [0x0060] = "Joypad_Interrupt",
        [0x0100] = "Start"
    };
    
    public Dictionary<ushort, IDisassembledInstruction> DisassembledInstructions { get; } = [];
    
    public IDisassembledInstruction DisassembleNextInstruction()
    {
        var programCounter = debugProcessor.GetRegisters().PC;
        var opcode = addressBus.Read(programCounter);

        const byte preCbOpcode = 0xCB;
        IDisassembledInstruction disassembledInstruction = opcode == preCbOpcode
            ? DisassembleCbInstruction(programCounter, 1)
            : DisassembleInstruction(opcode, programCounter, 1);

        DisassembledInstructions[programCounter] = disassembledInstruction;
        UpdateLabels(disassembledInstruction);
        
        return disassembledInstruction;
    }

    private void UpdateLabels(IDisassembledInstruction disassembledInstruction)
    {
        if (disassembledInstruction is not DisassembledInstruction { WriteType: WriteType.PC } instruction)
        {
            return;
        }

        if (!instruction.IsJump() && !instruction.IsCall())
        {
            return;
        }
        
        var nextProgramCounter = (instruction.OpType, instruction.FetchType) switch
        {
            (OpType.JpAlways, FetchType.HL) => debugProcessor.GetRegisters().HL,
            (OpType.JpAlways or OpType.JpNz or OpType.JpZ or OpType.JpNc or OpType.JpC, FetchType.N16) =>
                (ushort)(instruction.Bytes[2] << 8 | instruction.Bytes[1]),
            (OpType.JrAlways or OpType.JrNz or OpType.JrZ or OpType.JrNc or OpType.JrC, FetchType.N8) =>
                (ushort)(instruction.Address + (sbyte)instruction.Bytes[1] + 2), // Compensate for opcode and immediate byte
            (OpType.CallAlways or OpType.CallNz or OpType.CallZ or OpType.CallNc or OpType.CallC, FetchType.N16) =>
                (ushort)(instruction.Bytes[2] << 8 | instruction.Bytes[1]),
            _ => throw new ArgumentOutOfRangeException()
        };
            
        if (Labels.ContainsKey(nextProgramCounter))
        {
            return;
        }

        Labels[nextProgramCounter] = $"Label_{_labelCounter++}";
    }

    private DisassembledInstruction DisassembleInstruction(byte opcode, ushort programCounter, int programCounterOffset)
    {
        var instruction = Instructions[opcode];
        var immediateBytes = GetImmediateBytes(programCounter, programCounterOffset, instruction.WriteType, instruction.FetchType);
        
        return new DisassembledInstruction(programCounter, opcode, immediateBytes, instruction.OpType, instruction.WriteType, instruction.FetchType);
    }

    private DisassembledCbInstruction DisassembleCbInstruction(ushort programCounter, int programCounterOffset)
    {
        var cbOpCode = addressBus.Read((ushort)(programCounter + programCounterOffset++));
        var cbType = cbOpCode.DecodeCbType();
        var fetchType = cbOpCode.DecodeFetchType();
        var writeType = cbOpCode.DecodeWriteType();
        var immediateBytes = GetImmediateBytes(programCounter, programCounterOffset, writeType, fetchType);

        return new DisassembledCbInstruction(programCounter, cbOpCode, immediateBytes, cbType, writeType, fetchType);
    }
    
    private List<byte> GetImmediateBytes(ushort programCounter, int programCounterOffset, WriteType writeType, FetchType fetchType)
    {
        var immediateBytesFetch = GetImmediateBytes(fetchType, programCounter, programCounterOffset);
        var immediateBytesWrite = GetImmediateBytes(writeType, programCounter, programCounterOffset);
        
        return (immediateBytesFetch.Count, immediateBytesWrite.Count) switch
        {
            (> 0, 0) => immediateBytesFetch,
            (0, > 0) => immediateBytesWrite,
            (0, 0) => [],
            _ => throw new Exception("An instruction cannot fetch and write to immediate bytes!")
        };
    }

    private List<byte> GetImmediateBytes(FetchType fetchType, ushort programCounter, int programCounterOffset) => fetchType switch
    {
        FetchType.SPN8 or FetchType.XN8 or FetchType.N8 =>
        [
            addressBus.Read((ushort)(programCounter + programCounterOffset))
        ],
        FetchType.N16 or FetchType.XN16 =>
        [
            addressBus.Read((ushort)(programCounter + programCounterOffset)),
            addressBus.Read((ushort)(programCounter + programCounterOffset + 1))
        ],
        _ => []
    };
    
    private List<byte> GetImmediateBytes(WriteType writeType, ushort programCounter, int programCounterOffset) => writeType switch
    {
        WriteType.XN8 =>
        [
            addressBus.Read((ushort)(programCounter + programCounterOffset))
        ],
        WriteType.XN16x2 or WriteType.XN16 =>
        [
            addressBus.Read((ushort)(programCounter + programCounterOffset)),
            addressBus.Read((ushort)(programCounter + programCounterOffset + 1))
        ],
        _ => []
    };
}