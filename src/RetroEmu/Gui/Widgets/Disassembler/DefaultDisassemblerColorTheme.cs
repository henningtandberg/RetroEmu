using System.Numerics;

namespace RetroEmu.Gui.Widgets.Disassembler;

internal struct DefaultDisassemblerColorTheme : IDisassemblerColorTheme
{
    public Vector4 LabelColor => new(0, 255, 0, 100);
    public Vector4 AddressColor => new(255, 255, 0, 100);
    public Vector4 BytesColor => new(244, 244, 244, 100);
    public Vector4 OpcodeColor => new(244, 244, 244, 100);
    public Vector4 OperandColor => new(244, 244, 244, 100);
}