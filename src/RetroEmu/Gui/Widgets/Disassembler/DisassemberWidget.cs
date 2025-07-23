using System;
using System.Collections.Generic;
using System.Linq;
using ImGuiNET;
using Microsoft.Xna.Framework;
using RetroEmu.Devices.Disassembly;
using Vector2 = System.Numerics.Vector2;

namespace RetroEmu.Gui.Widgets.Disassembler;

public class DisassemblerWidget(
    IApplicationStateProvider applicationStateProvider,
    IDisassembler disassembler) : IGuiWidget
{
    private readonly IDisassemblerColorTheme _colorTheme = new DefaultDisassemblerColorTheme();
    
    public void Draw(GameTime gameTime)
    {
        unsafe
        {
            if (disassembler.DisassembledInstructions.Count <= 0)
            {
                return;
            }
        
            if (!ImGui.Begin("Disassembler"))
            {
                return;
            }
        
            ImGui.PushFont(ImGui.GetFont());

            var windowSize = new Vector2(ImGui.GetContentRegionAvail().X, 0);
            if (!ImGui.BeginChild("Instructions", windowSize, false, 0))
            {
                ImGui.End();
                return;
            }

            var disassemblerSize = disassembler.DisassembledInstructions.Count + disassembler.Labels.Count + 1;
            var clipper = new ImGuiListClipperPtr(ImGuiNative.ImGuiListClipper_ImGuiListClipper());
            clipper.Begin(disassemblerSize, ImGui.GetTextLineHeightWithSpacing());
            
            var addresses = disassembler.DisassembledInstructions.Keys.ToList();
            addresses.Sort();

            var labels = new Dictionary<ushort, string>(disassembler.Labels);

            while (clipper.Step())
            {
                var i = 0;
                for (var item = clipper.DisplayStart; item < clipper.DisplayEnd && i < addresses.Count; item++)
                {
                    var address = addresses[i];
                    if (labels.Remove(address, out var label))
                    {
                        ImGui.TextColored(_colorTheme.LabelColor, $"{label}:");
                        continue;
                    }

                    var di = disassembler.DisassembledInstructions[address];
                
                    ImGui.PushID(item);
                
                    ImGui.TextColored(_colorTheme.AddressColor, $"0x{di.Address:X4}");

                    var bytesString = di.Bytes.Aggregate("", (bs, b) => bs + $" {b:X2}");
                    ImGui.SameLine();
                    ImGui.TextColored(_colorTheme.BytesColor, $"{bytesString,-9}");
                
                    ImGui.SameLine();
                    ImGui.TextColored(_colorTheme.OpcodeColor, $" {di.OpcodeToken}");
                
                    ImGui.SameLine();
                    ImGui.TextColored(_colorTheme.OperandColor, $" {di.Operand1Token}");
                
                    ImGui.SameLine();
                    ImGui.TextColored(_colorTheme.OperandColor, $" {di.Operand2Token}");

                    ImGui.PopID();
                    i++;
                }
            }
        
        
            clipper.End();
            ImGui.PopFont();
            ImGui.EndChild();
            ImGui.End();
        }
    }
}