using System.Numerics;

namespace RetroEmu.Gui.Widgets.Disassembler;

internal struct GruvboxColorTheme : IDisassemblerColorTheme
{
    public Vector4 LabelColor => RgbColor.Create(0xAE, 0xB1, 0x22);
    public Vector4 AddressColor => RgbColor.Create(0xCD, 0x7B, 0x90);
    public Vector4 BytesColor => RgbColor.Create(0xCD, 0x7B, 0x90);
    public Vector4 OpcodeColor => RgbColor.Create(0xAE, 0xB1, 0x22);
    public Vector4 RegisterOperandColor => RgbColor.Create(0x78, 0x9B, 0x8D);
    public Vector4 ImmediateOperandColor => RgbColor.Create(0xCD, 0x7B, 0x90);
    public Vector4 SeparatorColor => RgbColor.Create(0x87, 0x78, 0x69);
    public Vector4 CurrentInstructionArrowColor => RgbColor.Create(0xF3, 0xB1, 0x29);
}