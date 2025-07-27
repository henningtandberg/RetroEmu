using System;
using System.Collections.Generic;
using System.Linq;
using ImGuiNET;
using Microsoft.Xna.Framework;
using RetroEmu.Devices.Disassembly;
using RetroEmu.Devices.Disassembly.Tokens;
using RetroEmu.Devices.DMG;
using Vector2 = System.Numerics.Vector2;

namespace RetroEmu.Gui.Widgets.Disassembler;

public class DisassemblerWidget(
    IApplicationStateProvider applicationStateProvider,
    IDisassembler disassembler,
    IDebugProcessor debugProcessor) : IGuiWidget
{
    private readonly IDisassemblerColorTheme _colorTheme = new DefaultDisassemblerColorTheme();

    private bool _follow = true;

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
        ImGui.SetNextWindowPos(new Vector2(200, 50), ImGuiCond.FirstUseEver);
        ImGui.SetNextWindowSize(new Vector2(500, 350), ImGuiCond.FirstUseEver);
        
        if (!ImGui.Begin("Disassembler"))
        {
            return;
        }
        
        ImGui.PushFont(ImGui.GetFont());

        /*
         * Top bar
         */
        DrawButton("Continue", "This will resume execution of the curren program.", () => {});
        ImGui.SameLine();
        DrawButton("Step", "This will step to the next instruction.", applicationStateProvider.Step);
        ImGui.SameLine();
        DrawButton("Step Over", "This will step over the next instruction.", () => {});
        ImGui.SameLine();
        DrawButton("Step Out", "This will step out of the current frame.", () => {});
        ImGui.SameLine();
        ImGui.Checkbox("Follow", ref _follow);
        ImGui.Separator();

        var windowSize = new Vector2(ImGui.GetContentRegionAvail().X, 0);
        if (!ImGui.BeginChild("Instructions", windowSize, false, 0))
        {
            ImGui.End();
            return;
        }

        var disassemblerSize = disassembler.DisassembledInstructions.Count;// + disassembler.Labels.Count;

        var clipper = new ImGuiListClipperPtr(ImGuiNative.ImGuiListClipper_ImGuiListClipper());
        clipper.Begin(disassemblerSize, ImGui.GetTextLineHeightWithSpacing());
            
        var addresses = disassembler.DisassembledInstructions.Keys.ToList();
        addresses.Sort();
        
        if (_follow)
        {
            var programCounterPosition = addresses.IndexOf(debugProcessor.GetRegisters().PC);
            var windowOffset = ImGui.GetWindowHeight() / 2.0f;
            var offset = windowOffset - (ImGui.GetTextLineHeightWithSpacing() - 2.0f);
            ImGui.SetScrollY(programCounterPosition * ImGui.GetTextLineHeightWithSpacing() - offset);
        }

        var labels = new Dictionary<ushort, string>(disassembler.Labels);

        while (clipper.Step())
        {
            for (var item = clipper.DisplayStart; item < clipper.DisplayEnd; item++)
            {
                var address = addresses[item];
                if (labels.Remove(address, out var label))
                {
                    ImGui.TextColored(_colorTheme.LabelColor, $"{label}:");
                }

                var di = disassembler.DisassembledInstructions[address];
                
                ImGui.PushID(item);
                
                ImGui.TextColored(_colorTheme.AddressColor, $" 0x{di.Address:X4}");

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
                
                if (di.Operand1Token is EmptyOperandToken)
                {
                    var padding = string.Empty.PadRight(8);
                    ImGui.SameLine();
                    ImGui.Text(padding);
                }
                
                if (di.Address == debugProcessor.GetRegisters().PC)
                {
                    ImGui.SameLine();
                    ImGui.TextColored(_colorTheme.CurrentInstructionArrowColor, "<- Current");
                }
                
                if (di.IsJump() || di.IsReturn())
                {
                    ImGui.PushStyleColor(ImGuiCol.Separator, _colorTheme.SeparatorColor);
                    ImGui.Separator();
                    ImGui.PopStyleColor();
                }

                ImGui.PopID();
            }
        }
        
        
        clipper.End();
        ImGui.PopFont();
        ImGui.EndChild();
        ImGui.End();
    }
}