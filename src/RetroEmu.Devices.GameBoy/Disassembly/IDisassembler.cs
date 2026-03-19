using System.Collections.Generic;

namespace RetroEmu.Devices.GameBoy.Disassembly;

public interface IDisassembler
{
    public Dictionary<ushort, string> Labels { get; }
    public Dictionary<ushort, IDisassembledInstruction> DisassembledInstructions { get; }
    public IDisassembledInstruction DisassembleNextInstruction();
}