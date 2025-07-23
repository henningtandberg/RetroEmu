using System;
using System.Collections.Generic;
using System.Linq;
using ImGuiNET;
using Microsoft.Xna.Framework;
using RetroEmu.Devices.Disassembly;
using RetroEmu.Devices.Disassembly.Tokens;
using Vector2 = System.Numerics.Vector2;

namespace RetroEmu.Gui.Widgets.Disassembler;

public class DisassemblerWidget(
    IApplicationStateProvider applicationStateProvider,
    IDisassembler disassembler) : IGuiWidget
{
    private readonly IDisassemblerColorTheme _colorTheme = new DefaultDisassemblerColorTheme();

    private static void DrawButton(string title, string toolTip, Action action)
    {
        if (ImGui.Button(title))
        {
            action();
        }
        if (ImGui.IsItemHovered())
        {
            ImGui.SetTooltip(toolTip);
        }
    }
    
    public unsafe void Draw(GameTime gameTime)
    {
        if (!ImGui.Begin("Disassembler"))
        {
            return;
        }

        DrawButton("Continue", "This will resume execution of the curren program.", () => {});
        ImGui.SameLine();
        DrawButton("Step", "This will step to the next instruction.", applicationStateProvider.Step);
        ImGui.SameLine();
        DrawButton("Step Over", "This will step over the next instruction.", () => {});
        ImGui.SameLine();
        DrawButton("Step Out", "This will step out of the current frame.", () => {});
        
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
                ImGui.TextColored(_colorTheme.OpcodeColor, $" {di.OpcodeToken,-4}");

                if (di.Operand1Token is not EmptyOperandToken)
                {
                    ImGui.SameLine();
                    var delimiter = ",".PadRight(7 - di.Operand1Token.Value.Length);
                    var color = di.Operand1Token is RegisterOperandToken
                        ? _colorTheme.RegisterOperandColor
                        : _colorTheme.ImmediateOperandColor;
                    ImGui.TextColored(color, $" {di.Operand1Token}{delimiter}");
                }

                if (di.Operand2Token is not EmptyOperandToken)
                {
                    ImGui.SameLine();
                    var color = di.Operand2Token is RegisterOperandToken
                        ? _colorTheme.RegisterOperandColor
                        : _colorTheme.ImmediateOperandColor;
                    ImGui.TextColored(color, $" {di.Operand2Token,-7}");
                }

                if (di.IsJump() || di.IsReturn())
                {
                    ImGui.PushStyleColor(ImGuiCol.Separator, _colorTheme.SeparatorColor);
                    ImGui.Separator();
                    ImGui.PopStyleColor();
                }

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