using System.Numerics;

namespace RetroEmu.Gui.Widgets.Disassembler;

internal struct DefaultDisassemblerColorTheme : IDisassemblerColorTheme
{
    public Vector4 LabelColor => RgbColor.Create(0, 255, 0);
    public Vector4 AddressColor => RgbColor.Create(255, 255, 0);
    public Vector4 BytesColor => RgbColor.Create(244, 244, 244);
    public Vector4 OpcodeColor => RgbColor.Create(244, 112, 102);
    public Vector4 RegisterOperandColor => RgbColor.Create(220, 189, 251);
    public Vector4 ImmediateOperandColor => RgbColor.Create(107, 182, 255);
    public Vector4 SeparatorColor => RgbColor.Create(75, 75, 75);
    public Vector4 CurrentInstructionArrowColor => RgbColor.Create(0, 255, 0);
}