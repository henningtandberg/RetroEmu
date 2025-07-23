using System;
using System.Linq;
using Microsoft.Xna.Framework;
using RetroEmu.Devices.Disassembly;
using RetroEmu.Devices.DMG;

namespace RetroEmu.Gui.Widgets.Disassembler;

public class DisassemblerWidget(
    IApplicationStateProvider applicationStateProvider,
    IDisassembler disassembler) : IGuiWidget
{
    public void Draw(GameTime gameTime)
    {
        if (disassembler.DisassembledInstructions.Count > 0)
        {
            ///
        }
    }
}