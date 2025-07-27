using System.Numerics;

namespace RetroEmu.Gui.Widgets.Disassembler;

internal interface IDisassemblerColorTheme
{
    Vector4 LabelColor { get; }
    Vector4 AddressColor { get; }
    Vector4 BytesColor { get; }
    Vector4 OpcodeColor { get; }
    Vector4 RegisterOperandColor { get; }
    Vector4 ImmediateOperandColor { get; }
    Vector4 SeparatorColor { get; }
    Vector4 CurrentInstructionArrowColor { get; }
}