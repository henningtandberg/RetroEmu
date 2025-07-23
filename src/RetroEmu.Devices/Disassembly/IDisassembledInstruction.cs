using System.Collections.Generic;
using RetroEmu.Devices.Disassembly.Tokens;

namespace RetroEmu.Devices.Disassembly;

public interface IDisassembledInstruction
{
    public ushort Address { get; }
    public IReadOnlyList<byte> Bytes { get; }
    public IOpcodeToken OpcodeToken { get; }
    public IOperandToken Operand1Token { get; }
    public IOperandToken Operand2Token { get; }
}