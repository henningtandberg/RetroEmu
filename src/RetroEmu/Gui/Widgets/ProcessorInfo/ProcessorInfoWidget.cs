using System;
using ImGuiNET;
using Microsoft.Xna.Framework;
using RetroEmu.Devices.DMG;
using Vector2 = System.Numerics.Vector2;

namespace RetroEmu.Gui.Widgets.ProcessorInfo;

public class ProcessorInfoWidget(IDebugProcessor processor) : IGuiWidget
{
    private const string Id = "###ProcessorInfo";
    private static readonly Vector2 DefaultSize = new(100, 200);
    
    public void Draw(GameTime gameTime)
    {
        ImGui.ShowDemoWindow();

        if (!ImGui.Begin("Processor", ImGuiWindowFlags.None))
            return;
        
        ImGui.Text($"PC = {processor.GetRegisters().PC:X4}");
        ImGui.Text($"SP = {processor.GetRegisters().SP:X4}");
        
        DrawFlagsTable();
        
        if (ImGui.BeginTabBar("Registers Tab Bar"))
        {
            if (ImGui.BeginTabItem("8-Bit GP Registers"))
            {
                Draw8BitRegisterTable();
                ImGui.EndTabItem();
            }
            
            if (ImGui.BeginTabItem("16-Bit GP Registers"))
            {
                Draw16RegisterTable();
                ImGui.EndTabItem();
            }
            
            ImGui.EndTabBar();
        }

        ImGui.End();
    }

    private void DrawFlagsTable()
    {
        if (!ImGui.BeginTable("flags", 4))
            return;
        
        ImGui.TableSetupColumn("Z");
        ImGui.TableSetupColumn("S");
        ImGui.TableSetupColumn("C");
        ImGui.TableSetupColumn("H");
        ImGui.TableHeadersRow();
        
        ImGui.TableNextRow();
        ImGui.TableSetColumnIndex(0);
        ImGui.Text(processor.ZeroFlagIsSet() ? "1" : "0");
        ImGui.TableSetColumnIndex(1);
        ImGui.Text(processor.SubtractFlagIsSet() ? "1" : "0");
        ImGui.TableSetColumnIndex(2);
        ImGui.Text(processor.CarryFlagIsSet() ? "1" : "0");
        ImGui.TableSetColumnIndex(3);
        ImGui.Text(processor.HalfCarryFlagIsSet() ? "1" : "0");
        
        ImGui.EndTable();
    }

    private void Draw16RegisterTable()
    {
        if (!ImGui.BeginTable("16-bit-regs", 4))
            return;
            
        DrawRegistersTableHeaders();

        var registers = processor.GetRegisters();
            
        DrawRegisterTableRow("AF", registers.AF);
        DrawRegisterTableRow("BC", registers.BC);
        DrawRegisterTableRow("DE", registers.DE);
        DrawRegisterTableRow("HL", registers.HL);
            
        ImGui.EndTable();
    }

    private void Draw8BitRegisterTable()
    {
        if (!ImGui.BeginTable("8-bit-regs", 4))
            return;
        
        DrawRegistersTableHeaders();

        var registers = processor.GetRegisters();
                
        DrawRegisterTableRow("A", registers.A);
        DrawRegisterTableRow("F", registers.F);
        
        DrawRegisterTableRow("B", registers.B);
        DrawRegisterTableRow("C", registers.C);
        
        DrawRegisterTableRow("D", registers.D);
        DrawRegisterTableRow("E", registers.E);
        
        DrawRegisterTableRow("H", registers.H);
        DrawRegisterTableRow("L", registers.L);

        ImGui.EndTable();
    }
    
    private static void DrawRegistersTableHeaders()
    {
        ImGui.TableSetupColumn("Register");
        ImGui.TableSetupColumn("Dec");
        ImGui.TableSetupColumn("Hex");
        ImGui.TableSetupColumn("Binary");
        ImGui.TableHeadersRow();
    }

    private static void DrawRegisterTableRow(string registerName, ushort registerValue)
    {
        ImGui.TableNextRow();
        ImGui.TableSetColumnIndex(0);
        ImGui.Text(registerName);
        ImGui.TableSetColumnIndex(1);
        ImGui.Text($"{registerValue}");
        ImGui.TableSetColumnIndex(2);
        ImGui.Text($"0x{registerValue:X4}");
        ImGui.TableSetColumnIndex(3);
        ImGui.Text($"{ToBinary(registerValue)}");
    }

    private static void DrawRegisterTableRow(string registerName, byte registerValue)
    {
        ImGui.TableNextRow();
        ImGui.TableSetColumnIndex(0);
        ImGui.Text(registerName);
        ImGui.TableSetColumnIndex(1);
        ImGui.Text($"{registerValue}");
        ImGui.TableSetColumnIndex(2);
        ImGui.Text($"0x{registerValue:X2}");
        ImGui.TableSetColumnIndex(3);
        ImGui.Text($"{ToBinary(registerValue)}");
    }

    private static string ToBinary(byte registerValue)
    {
        Span<char> digits = stackalloc char[8];
        var digitsLength = digits.Length;
        
        for (var i = 0; i < digitsLength; ++i)
        {
            digits[digitsLength - 1 - i] = (registerValue >> i & 0x01) == 1 ? '1' : '0';
        }
        
        Span<char> binaryString = stackalloc char[9];
        
        binaryString.Fill(' ');
        digits[..4].CopyTo(binaryString[..4]);
        digits[4..].CopyTo(binaryString[5..]);

        return binaryString.ToString();
    }

    private static string ToBinary(ushort registerValue)
    {
        Span<char> digits = stackalloc char[16];
        var digitsLength = digits.Length;
        
        for (var i = 0; i < digitsLength; ++i)
        {
            digits[digitsLength - 1 - i] = (registerValue >> i & 0x01) == 1 ? '1' : '0';
        }
        
        Span<char> binaryString = stackalloc char[19];
        binaryString.Fill(' ');
        
        digits[..4].CopyTo(binaryString[..4]);
        digits[4..8].CopyTo(binaryString[5..9]);
        digits[8..12].CopyTo(binaryString[10..14]);
        digits[12..].CopyTo(binaryString[15..]);

        return binaryString.ToString();
    }
}