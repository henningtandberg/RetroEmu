using System.Numerics;

namespace RetroEmu.UI.Desktop.Gui.Widgets.Disassembler;

internal sealed class RgbColor
{
    public static Vector4 Create(byte r, byte g, byte b) => new(r / 255f, g / 255f, b / 255f, 1.0f);
}