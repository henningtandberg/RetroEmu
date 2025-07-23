using System.Numerics;

namespace RetroEmu.Gui.Widgets.Disassembler;

internal interface IDisassemblerColorTheme
{
    Vector4 LabelColor { get; }
    Vector4 AddressColor { get; }
    Vector4 BytesColor { get; }
    Vector4 OpcodeColor { get; }
    Vector4 OperandColor { get; }
}