using System;
using ImGuiNET;
using Microsoft.Xna.Framework;
using RetroEmu.Devices.DMG;

namespace RetroEmu.Gui.Widgets.ProcessorInfo;

public class IORegistersWidget(IDebugProcessor processor) : IGuiWidget
{
    private const string Id = "###ProcessorInfo";
    private static readonly Vector2 DefaultSize = new(100, 200);
    
    public void Draw(GameTime gameTime)
    {
        if (!ImGui.Begin("I/O Registers", ImGuiWindowFlags.None))
            return;
        
        DrawInterruptMasterEnableTable();
        
        if (ImGui.BeginTabBar("Registers Tab Bar"))
        {
            if (ImGui.BeginTabItem("Interrupt State Detailed"))
            {
                DrawInterruptRegistersDetailedTable();
                ImGui.EndTabItem();
            }
            
            if (ImGui.BeginTabItem("Interrupt State"))
            {
                DrawInterruptRegistersTable();
                ImGui.EndTabItem();
            }
            
            ImGui.EndTabBar();
        }

        ImGui.End();
    }

    private void DrawInterruptMasterEnableTable()
    {
        ImGui.Text($"Interrupt Master Enable (IME): {processor.GetInterruptMasterEnable()}");
    }

    private void DrawInterruptRegistersDetailedTable()
    {
        if (!ImGui.BeginTable("Interrupt Registers Details", 6))
            return;
        
        ImGui.TableSetupColumn(" ");
        ImGui.TableSetupColumn("Joypad");
        ImGui.TableSetupColumn("Serial");
        ImGui.TableSetupColumn("Timer");
        ImGui.TableSetupColumn("LCD");
        ImGui.TableSetupColumn("VBlank");
        ImGui.TableHeadersRow();

        var interruptEnable = processor.GetInterruptEnable();
        
        ImGui.TableNextRow();
        ImGui.TableSetColumnIndex(0);
        ImGui.Text("Interrupt Enable (0xFFFF)");
        ImGui.TableSetColumnIndex(1);
        ImGui.Text((interruptEnable & 0x10) > 0 ? "1" : "0");
        ImGui.TableSetColumnIndex(2);
        ImGui.Text((interruptEnable & 0x08) > 0 ? "1" : "0");
        ImGui.TableSetColumnIndex(3);
        ImGui.Text((interruptEnable & 0x04) > 0 ? "1" : "0");
        ImGui.TableSetColumnIndex(4);
        ImGui.Text((interruptEnable & 0x02) > 0 ? "1" : "0");
        ImGui.TableSetColumnIndex(5);
        ImGui.Text((interruptEnable & 0x01) > 0 ? "1" : "0");

        var interruptFlag = processor.GetInterruptFlag();
        
        ImGui.TableNextRow();
        ImGui.TableSetColumnIndex(0);
        ImGui.Text("Interrupt Flag (0xFF0F)");
        ImGui.TableSetColumnIndex(1);
        ImGui.Text((interruptFlag & 0x10) > 0 ? "1" : "0");
        ImGui.TableSetColumnIndex(2);
        ImGui.Text((interruptFlag & 0x08) > 0 ? "1" : "0");
        ImGui.TableSetColumnIndex(3);
        ImGui.Text((interruptFlag & 0x04) > 0 ? "1" : "0");
        ImGui.TableSetColumnIndex(4);
        ImGui.Text((interruptFlag & 0x02) > 0 ? "1" : "0");
        ImGui.TableSetColumnIndex(5);
        ImGui.Text((interruptFlag & 0x01) > 0 ? "1" : "0");

        ImGui.EndTable();

        if (!ImGui.BeginTable("LCDC Register Details", 9))
            return;

        ImGui.TableSetupColumn(" ");
        ImGui.TableSetupColumn("PPU Enable");
        ImGui.TableSetupColumn("Window tile map");
        ImGui.TableSetupColumn("Window enable");
        ImGui.TableSetupColumn("BG & Window tiles");
        ImGui.TableSetupColumn("BG tile map");
        ImGui.TableSetupColumn("OBJ size");
        ImGui.TableSetupColumn("OBJ enable");
        ImGui.TableSetupColumn("BG & Window enable");
        ImGui.TableHeadersRow();

        var LCDC = processor.GetLCDC();

        ImGui.TableNextRow();
        ImGui.TableSetColumnIndex(0);
        ImGui.Text("LCDC (0xFF40)");
        ImGui.TableSetColumnIndex(1);
        ImGui.Text((LCDC & 0x80) > 0 ? "1" : "0");
        ImGui.TableSetColumnIndex(2);
        ImGui.Text((LCDC & 0x40) > 0 ? "1" : "0");
        ImGui.TableSetColumnIndex(3);
        ImGui.Text((LCDC & 0x20) > 0 ? "1" : "0");
        ImGui.TableSetColumnIndex(4);
        ImGui.Text((LCDC & 0x10) > 0 ? "1" : "0");
        ImGui.TableSetColumnIndex(5);
        ImGui.Text((LCDC & 0x08) > 0 ? "1" : "0");
        ImGui.TableSetColumnIndex(6);
        ImGui.Text((LCDC & 0x04) > 0 ? "1" : "0");
        ImGui.TableSetColumnIndex(7);
        ImGui.Text((LCDC & 0x02) > 0 ? "1" : "0");
        ImGui.TableSetColumnIndex(8);
        ImGui.Text((LCDC & 0x01) > 0 ? "1" : "0");

        ImGui.EndTable();
    }

    private void DrawInterruptRegistersTable()
    {
        if (!ImGui.BeginTable("Interrupt Registers", 4))
            return;
            
        DrawRegistersTableHeaders();

        var interruptEnable = processor.GetInterruptEnable();
        var interruptFlag = processor.GetInterruptFlag();
            
        DrawRegisterTableRow("Interrupt Enable (0xFFFF)", interruptEnable);
        DrawRegisterTableRow("Interrupt Flag (0xFF0F)", interruptFlag);
            
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
}
