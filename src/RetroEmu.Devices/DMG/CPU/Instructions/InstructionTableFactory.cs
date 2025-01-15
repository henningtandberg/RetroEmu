using System.Collections.Generic;

namespace RetroEmu.Devices.DMG.CPU.Instructions;

internal static class InstructionTableFactory
{
    public static Instruction[] Create()
    {
        var instructions = new Instruction[256];
        var table = _create();

        foreach (var (opcode, instruction) in table)
        {
            instructions[opcode] = instruction;
        }

        return instructions;
    }
    
    private static Dictionary<byte, Instruction> _create() => new()
    {
        // Adc
        [Opcode.Adc_A_B] = new Instruction(WriteType.A, OpType.Adc, FetchType.B),
        [Opcode.Adc_A_C] = new Instruction(WriteType.A, OpType.Adc, FetchType.C),
        [Opcode.Adc_A_D] = new Instruction(WriteType.A, OpType.Adc, FetchType.D),
        [Opcode.Adc_A_E] = new Instruction(WriteType.A, OpType.Adc, FetchType.E),
        [Opcode.Adc_A_H] = new Instruction(WriteType.A, OpType.Adc, FetchType.H),
        [Opcode.Adc_A_L] = new Instruction(WriteType.A, OpType.Adc, FetchType.L),
        [Opcode.Adc_A_XHL] = new Instruction(WriteType.A, OpType.Adc, FetchType.XHL),
        [Opcode.Adc_A_A] = new Instruction(WriteType.A, OpType.Adc, FetchType.A),
        [Opcode.Adc_A_N8] = new Instruction(WriteType.A, OpType.Adc, FetchType.N8),

        // Add
        [Opcode.Add_A_B] = new Instruction(WriteType.A, OpType.Add, FetchType.B),
        [Opcode.Add_A_C] = new Instruction(WriteType.A, OpType.Add, FetchType.C),
        [Opcode.Add_A_D] = new Instruction(WriteType.A, OpType.Add, FetchType.D),
        [Opcode.Add_A_E] = new Instruction(WriteType.A, OpType.Add, FetchType.E),
        [Opcode.Add_A_H] = new Instruction(WriteType.A, OpType.Add, FetchType.H),
        [Opcode.Add_A_L] = new Instruction(WriteType.A, OpType.Add, FetchType.L),
        [Opcode.Add_A_XHL] = new Instruction(WriteType.A, OpType.Add, FetchType.XHL),
        [Opcode.Add_A_A] = new Instruction(WriteType.A, OpType.Add, FetchType.A),
        [Opcode.Add_A_N8] = new Instruction(WriteType.A, OpType.Add, FetchType.N8),
        [Opcode.Add_HL_BC] = new Instruction(WriteType.HL, OpType.Add16, FetchType.BC),
        [Opcode.Add_HL_DE] = new Instruction(WriteType.HL, OpType.Add16, FetchType.DE),
        [Opcode.Add_HL_HL] = new Instruction(WriteType.HL, OpType.Add16, FetchType.HL),
        [Opcode.Add_HL_SP] = new Instruction(WriteType.HL, OpType.Add16, FetchType.SP),
        [Opcode.Add_SP_N8] = new Instruction(WriteType.SP, OpType.AddSp, FetchType.N8),

        // And
        [Opcode.And_A_B] = new Instruction(WriteType.A, OpType.And, FetchType.B),
        [Opcode.And_A_C] = new Instruction(WriteType.A, OpType.And, FetchType.C),
        [Opcode.And_A_D] = new Instruction(WriteType.A, OpType.And, FetchType.D),
        [Opcode.And_A_E] = new Instruction(WriteType.A, OpType.And, FetchType.E),
        [Opcode.And_A_H] = new Instruction(WriteType.A, OpType.And, FetchType.H),
        [Opcode.And_A_L] = new Instruction(WriteType.A, OpType.And, FetchType.L),
        [Opcode.And_A_XHL] = new Instruction(WriteType.A, OpType.And, FetchType.XHL),
        [Opcode.And_A_A] = new Instruction(WriteType.A, OpType.And, FetchType.A),
        [Opcode.And_A_N8] = new Instruction(WriteType.A, OpType.And, FetchType.N8),

        // Call
        [Opcode.Call_N16] = new Instruction(WriteType.PC, OpType.CallAlways, FetchType.N16),
        [Opcode.CallNZ_N16] = new Instruction(WriteType.PC, OpType.CallNz, FetchType.N16),
        [Opcode.CallZ_N16] = new Instruction(WriteType.PC, OpType.CallZ, FetchType.N16),
        [Opcode.CallNC_N16] = new Instruction(WriteType.PC, OpType.CallNc, FetchType.N16),
        [Opcode.CallC_N16] = new Instruction(WriteType.PC, OpType.CallC, FetchType.N16),

        // CB
        [Opcode.Pre_CB] = new Instruction(WriteType.None, OpType.PreCb, FetchType.None),

        // Ccf
        [Opcode.Ccf] = new Instruction(WriteType.None, OpType.Ccf, FetchType.None),

        // Cp
        [Opcode.Cp_A_A] = new Instruction(WriteType.None, OpType.Cp, FetchType.A),
        [Opcode.Cp_A_B] = new Instruction(WriteType.None, OpType.Cp, FetchType.B),
        [Opcode.Cp_A_C] = new Instruction(WriteType.None, OpType.Cp, FetchType.C),
        [Opcode.Cp_A_D] = new Instruction(WriteType.None, OpType.Cp, FetchType.D),
        [Opcode.Cp_A_E] = new Instruction(WriteType.None, OpType.Cp, FetchType.E),
        [Opcode.Cp_A_H] = new Instruction(WriteType.None, OpType.Cp, FetchType.H),
        [Opcode.Cp_A_L] = new Instruction(WriteType.None, OpType.Cp, FetchType.L),
        [Opcode.Cp_A_XHL] = new Instruction(WriteType.None, OpType.Cp, FetchType.XHL),
        [Opcode.Cp_A_N8] = new Instruction(WriteType.None, OpType.Cp, FetchType.N8),

        // Cpl
        [Opcode.Cpl] = new Instruction(WriteType.A, OpType.Cpl, FetchType.A),

        // Daa
        [Opcode.Daa] = new Instruction(WriteType.A, OpType.Daa, FetchType.A),

        // Dec
        [Opcode.Dec_A] = new Instruction(WriteType.A, OpType.Dec, FetchType.A),
        [Opcode.Dec_B] = new Instruction(WriteType.B, OpType.Dec, FetchType.B),
        [Opcode.Dec_C] = new Instruction(WriteType.C, OpType.Dec, FetchType.C),
        [Opcode.Dec_D] = new Instruction(WriteType.D, OpType.Dec, FetchType.D),
        [Opcode.Dec_E] = new Instruction(WriteType.E, OpType.Dec, FetchType.E),
        [Opcode.Dec_H] = new Instruction(WriteType.H, OpType.Dec, FetchType.H),
        [Opcode.Dec_L] = new Instruction(WriteType.L, OpType.Dec, FetchType.L),
        [Opcode.Dec_XHL] = new Instruction(WriteType.XHL, OpType.Dec, FetchType.XHL),
        [Opcode.Dec_BC] = new Instruction(WriteType.BC, OpType.Dec16, FetchType.BC),
        [Opcode.Dec_DE] = new Instruction(WriteType.DE, OpType.Dec16, FetchType.DE),
        [Opcode.Dec_HL] = new Instruction(WriteType.HL, OpType.Dec16, FetchType.HL),
        [Opcode.Dec_SP] = new Instruction(WriteType.SP, OpType.Dec16, FetchType.SP),

        // Inc
        [Opcode.Inc_A] = new Instruction(WriteType.A, OpType.Inc, FetchType.A),
        [Opcode.Inc_B] = new Instruction(WriteType.B, OpType.Inc, FetchType.B),
        [Opcode.Inc_C] = new Instruction(WriteType.C, OpType.Inc, FetchType.C),
        [Opcode.Inc_D] = new Instruction(WriteType.D, OpType.Inc, FetchType.D),
        [Opcode.Inc_E] = new Instruction(WriteType.E, OpType.Inc, FetchType.E),
        [Opcode.Inc_H] = new Instruction(WriteType.H, OpType.Inc, FetchType.H),
        [Opcode.Inc_L] = new Instruction(WriteType.L, OpType.Inc, FetchType.L),
        [Opcode.Inc_XHL] = new Instruction(WriteType.XHL, OpType.Inc, FetchType.XHL),
        [Opcode.Inc_BC] = new Instruction(WriteType.BC, OpType.Inc16, FetchType.BC),
        [Opcode.Inc_DE] = new Instruction(WriteType.DE, OpType.Inc16, FetchType.DE),
        [Opcode.Inc_HL] = new Instruction(WriteType.HL, OpType.Inc16, FetchType.HL),
        [Opcode.Inc_SP] = new Instruction(WriteType.SP, OpType.Inc16, FetchType.SP),

        // Jp
        [Opcode.Jp_N16] = new Instruction(WriteType.PC, OpType.JpAlways, FetchType.N16),
        [Opcode.JpNZ_N16] = new Instruction(WriteType.PC, OpType.JpNz, FetchType.N16),
        [Opcode.JpZ_N16] = new Instruction(WriteType.PC, OpType.JpZ, FetchType.N16),
        [Opcode.JpNC_N16] = new Instruction(WriteType.PC, OpType.JpNc, FetchType.N16),
        [Opcode.JpC_N16] = new Instruction(WriteType.PC, OpType.JpC, FetchType.N16),
        [Opcode.Jp_XHL] = new Instruction(WriteType.PC, OpType.JpAlways, FetchType.XHL),

        // Jr
        [Opcode.Jr_N8] = new Instruction(WriteType.PC, OpType.JrAlways, FetchType.N8),
        [Opcode.JrNZ_N8] = new Instruction(WriteType.PC, OpType.JrNz, FetchType.N8),
        [Opcode.JrZ_N8] = new Instruction(WriteType.PC, OpType.JrZ, FetchType.N8),
        [Opcode.JrNC_N8] = new Instruction(WriteType.PC, OpType.JrNc, FetchType.N8),
        [Opcode.JrC_N8] = new Instruction(WriteType.PC, OpType.JrC, FetchType.N8),

        // Ld
        [Opcode.Ld_A_B] = new Instruction(WriteType.A, OpType.Ld, FetchType.B),
        [Opcode.Ld_A_C] = new Instruction(WriteType.A, OpType.Ld, FetchType.C),
        [Opcode.Ld_A_D] = new Instruction(WriteType.A, OpType.Ld, FetchType.D),
        [Opcode.Ld_A_E] = new Instruction(WriteType.A, OpType.Ld, FetchType.E),
        [Opcode.Ld_A_H] = new Instruction(WriteType.A, OpType.Ld, FetchType.H),
        [Opcode.Ld_A_L] = new Instruction(WriteType.A, OpType.Ld, FetchType.L),
        [Opcode.Ld_A_XHL] = new Instruction(WriteType.A, OpType.Ld, FetchType.XHL),
        [Opcode.Ld_A_A] = new Instruction(WriteType.A, OpType.Ld, FetchType.A),
        [Opcode.Ld_A_N8] = new Instruction(WriteType.A, OpType.Ld, FetchType.N8),

        [Opcode.Ld_B_B] = new Instruction(WriteType.B, OpType.Ld, FetchType.B),
        [Opcode.Ld_B_C] = new Instruction(WriteType.B, OpType.Ld, FetchType.C),
        [Opcode.Ld_B_D] = new Instruction(WriteType.B, OpType.Ld, FetchType.D),
        [Opcode.Ld_B_E] = new Instruction(WriteType.B, OpType.Ld, FetchType.E),
        [Opcode.Ld_B_H] = new Instruction(WriteType.B, OpType.Ld, FetchType.H),
        [Opcode.Ld_B_L] = new Instruction(WriteType.B, OpType.Ld, FetchType.L),
        [Opcode.Ld_B_XHL] = new Instruction(WriteType.B, OpType.Ld, FetchType.XHL),
        [Opcode.Ld_B_A] = new Instruction(WriteType.B, OpType.Ld, FetchType.A),
        [Opcode.Ld_B_N8] = new Instruction(WriteType.B, OpType.Ld, FetchType.N8),

        [Opcode.Ld_C_B] = new Instruction(WriteType.C, OpType.Ld, FetchType.B),
        [Opcode.Ld_C_C] = new Instruction(WriteType.C, OpType.Ld, FetchType.C),
        [Opcode.Ld_C_D] = new Instruction(WriteType.C, OpType.Ld, FetchType.D),
        [Opcode.Ld_C_E] = new Instruction(WriteType.C, OpType.Ld, FetchType.E),
        [Opcode.Ld_C_H] = new Instruction(WriteType.C, OpType.Ld, FetchType.H),
        [Opcode.Ld_C_L] = new Instruction(WriteType.C, OpType.Ld, FetchType.L),
        [Opcode.Ld_C_XHL] = new Instruction(WriteType.C, OpType.Ld, FetchType.XHL),
        [Opcode.Ld_C_A] = new Instruction(WriteType.C, OpType.Ld, FetchType.A),
        [Opcode.Ld_C_N8] = new Instruction(WriteType.C, OpType.Ld, FetchType.N8),

        [Opcode.Ld_D_B] = new Instruction(WriteType.D, OpType.Ld, FetchType.B),
        [Opcode.Ld_D_C] = new Instruction(WriteType.D, OpType.Ld, FetchType.C),
        [Opcode.Ld_D_D] = new Instruction(WriteType.D, OpType.Ld, FetchType.D),
        [Opcode.Ld_D_E] = new Instruction(WriteType.D, OpType.Ld, FetchType.E),
        [Opcode.Ld_D_H] = new Instruction(WriteType.D, OpType.Ld, FetchType.H),
        [Opcode.Ld_D_L] = new Instruction(WriteType.D, OpType.Ld, FetchType.L),
        [Opcode.Ld_D_XHL] = new Instruction(WriteType.D, OpType.Ld, FetchType.XHL),
        [Opcode.Ld_D_A] = new Instruction(WriteType.D, OpType.Ld, FetchType.A),
        [Opcode.Ld_D_N8] = new Instruction(WriteType.D, OpType.Ld, FetchType.N8),

        [Opcode.Ld_E_B] = new Instruction(WriteType.E, OpType.Ld, FetchType.B),
        [Opcode.Ld_E_C] = new Instruction(WriteType.E, OpType.Ld, FetchType.C),
        [Opcode.Ld_E_D] = new Instruction(WriteType.E, OpType.Ld, FetchType.D),
        [Opcode.Ld_E_E] = new Instruction(WriteType.E, OpType.Ld, FetchType.E),
        [Opcode.Ld_E_H] = new Instruction(WriteType.E, OpType.Ld, FetchType.H),
        [Opcode.Ld_E_L] = new Instruction(WriteType.E, OpType.Ld, FetchType.L),
        [Opcode.Ld_E_XHL] = new Instruction(WriteType.E, OpType.Ld, FetchType.XHL),
        [Opcode.Ld_E_A] = new Instruction(WriteType.E, OpType.Ld, FetchType.A),
        [Opcode.Ld_E_N8] = new Instruction(WriteType.E, OpType.Ld, FetchType.N8),

        [Opcode.Ld_H_B] = new Instruction(WriteType.H, OpType.Ld, FetchType.B),
        [Opcode.Ld_H_C] = new Instruction(WriteType.H, OpType.Ld, FetchType.C),
        [Opcode.Ld_H_D] = new Instruction(WriteType.H, OpType.Ld, FetchType.D),
        [Opcode.Ld_H_E] = new Instruction(WriteType.H, OpType.Ld, FetchType.E),
        [Opcode.Ld_H_H] = new Instruction(WriteType.H, OpType.Ld, FetchType.H),
        [Opcode.Ld_H_L] = new Instruction(WriteType.H, OpType.Ld, FetchType.L),
        [Opcode.Ld_H_XHL] = new Instruction(WriteType.H, OpType.Ld, FetchType.XHL),
        [Opcode.Ld_H_A] = new Instruction(WriteType.H, OpType.Ld, FetchType.A),
        [Opcode.Ld_H_N8] = new Instruction(WriteType.H, OpType.Ld, FetchType.N8),

        [Opcode.Ld_L_B] = new Instruction(WriteType.L, OpType.Ld, FetchType.B),
        [Opcode.Ld_L_C] = new Instruction(WriteType.L, OpType.Ld, FetchType.C),
        [Opcode.Ld_L_D] = new Instruction(WriteType.L, OpType.Ld, FetchType.D),
        [Opcode.Ld_L_E] = new Instruction(WriteType.L, OpType.Ld, FetchType.E),
        [Opcode.Ld_L_H] = new Instruction(WriteType.L, OpType.Ld, FetchType.H),
        [Opcode.Ld_L_L] = new Instruction(WriteType.L, OpType.Ld, FetchType.L),
        [Opcode.Ld_L_XHL] = new Instruction(WriteType.L, OpType.Ld, FetchType.XHL),
        [Opcode.Ld_L_A] = new Instruction(WriteType.L, OpType.Ld, FetchType.A),
        [Opcode.Ld_L_N8] = new Instruction(WriteType.L, OpType.Ld, FetchType.N8),

        [Opcode.Ld_XHL_B] = new Instruction(WriteType.XHL, OpType.Ld, FetchType.B),
        [Opcode.Ld_XHL_C] = new Instruction(WriteType.XHL, OpType.Ld, FetchType.C),
        [Opcode.Ld_XHL_D] = new Instruction(WriteType.XHL, OpType.Ld, FetchType.D),
        [Opcode.Ld_XHL_E] = new Instruction(WriteType.XHL, OpType.Ld, FetchType.E),
        [Opcode.Ld_XHL_H] = new Instruction(WriteType.XHL, OpType.Ld, FetchType.H),
        [Opcode.Ld_XHL_L] = new Instruction(WriteType.XHL, OpType.Ld, FetchType.L),
        [Opcode.Ld_XHL_A] = new Instruction(WriteType.XHL, OpType.Ld, FetchType.A),
        [Opcode.Ld_XHL_N8] = new Instruction(WriteType.L, OpType.Ld, FetchType.N8),

        [Opcode.Ld_A_XC] = new Instruction(WriteType.A, OpType.Ld, FetchType.XC),
        [Opcode.Ld_XC_A] = new Instruction(WriteType.XC, OpType.Ld, FetchType.A),

        [Opcode.Ld_BC_N16] = new Instruction(WriteType.BC, OpType.Ld, FetchType.N16),
        [Opcode.Ld_DE_N16] = new Instruction(WriteType.DE, OpType.Ld, FetchType.N16),
        [Opcode.Ld_HL_N16] = new Instruction(WriteType.DE, OpType.Ld, FetchType.N16),
        [Opcode.Ld_SP_N16] = new Instruction(WriteType.SP, OpType.Ld, FetchType.N16),

        [Opcode.Ld_XBC_A] = new Instruction(WriteType.XBC, OpType.Ld, FetchType.A),
        [Opcode.Ld_XDE_A] = new Instruction(WriteType.XDE, OpType.Ld, FetchType.A),
        [Opcode.Ld_XHLI_A] = new Instruction(WriteType.XHLI, OpType.Ld, FetchType.A),
        [Opcode.Ld_XHLD_A] = new Instruction(WriteType.XHLD, OpType.Ld, FetchType.A),

        [Opcode.Ld_A_XN8] = new Instruction(WriteType.A, OpType.Ld, FetchType.XN8),
        [Opcode.Ld_XN8_A] = new Instruction(WriteType.XN8, OpType.Ld, FetchType.A),

        [Opcode.Ld_XN16_SP] = new Instruction(WriteType.XN16, OpType.Ld, FetchType.SP),

        [Opcode.Ld_A_XBC] = new Instruction(WriteType.A, OpType.Ld, FetchType.XBC),
        [Opcode.Ld_A_XDE] = new Instruction(WriteType.A, OpType.Ld, FetchType.XDE),
        [Opcode.Ld_A_XHLI] = new Instruction(WriteType.A, OpType.Ld, FetchType.XHLI),
        [Opcode.Ld_A_XHLD] = new Instruction(WriteType.A, OpType.Ld, FetchType.XHLD),

        [Opcode.Ld_HL_SPN8] = new Instruction(WriteType.HL, OpType.Ld, FetchType.SPN8),
        [Opcode.Ld_SP_HL] = new Instruction(WriteType.SP, OpType.Ld, FetchType.HL),

        [Opcode.Ld_XN16_A] = new Instruction(WriteType.XN16, OpType.Ld, FetchType.A),
        [Opcode.Ld_A_XN16] = new Instruction(WriteType.A, OpType.Ld, FetchType.XN16),

        // Nop
        [Opcode.Nop] = new Instruction(WriteType.None, OpType.Nop, FetchType.None),

        // Or
        [Opcode.Or_A_B] = new Instruction(WriteType.A, OpType.Or, FetchType.B),
        [Opcode.Or_A_C] = new Instruction(WriteType.A, OpType.Or, FetchType.C),
        [Opcode.Or_A_D] = new Instruction(WriteType.A, OpType.Or, FetchType.D),
        [Opcode.Or_A_E] = new Instruction(WriteType.A, OpType.Or, FetchType.E),
        [Opcode.Or_A_H] = new Instruction(WriteType.A, OpType.Or, FetchType.H),
        [Opcode.Or_A_L] = new Instruction(WriteType.A, OpType.Or, FetchType.L),
        [Opcode.Or_A_XHL] = new Instruction(WriteType.A, OpType.Or, FetchType.XHL),
        [Opcode.Or_A_A] = new Instruction(WriteType.A, OpType.Or, FetchType.A),
        [Opcode.Or_A_N8] = new Instruction(WriteType.A, OpType.Or, FetchType.N8),

        // Pop
        [Opcode.Pop_AF] = new Instruction(WriteType.AF, OpType.Pop, FetchType.Pop),
        [Opcode.Pop_BC] = new Instruction(WriteType.BC, OpType.Pop, FetchType.Pop),
        [Opcode.Pop_DE] = new Instruction(WriteType.DE, OpType.Pop, FetchType.Pop),
        [Opcode.Pop_HL] = new Instruction(WriteType.HL, OpType.Pop, FetchType.Pop),

        // Push 
        [Opcode.Push_AF] = new Instruction(WriteType.Push, OpType.Push, FetchType.AF),
        [Opcode.Push_BC] = new Instruction(WriteType.Push, OpType.Push, FetchType.BC),
        [Opcode.Push_DE] = new Instruction(WriteType.Push, OpType.Push, FetchType.DE),
        [Opcode.Push_HL] = new Instruction(WriteType.Push, OpType.Push, FetchType.HL),

        // Ret
        [Opcode.Ret] = new Instruction(WriteType.PC, OpType.RetAlways, FetchType.SP),
        [Opcode.RetNZ] = new Instruction(WriteType.PC, OpType.RetNz, FetchType.SP),
        [Opcode.RetZ] = new Instruction(WriteType.PC, OpType.RetZ, FetchType.SP),
        [Opcode.RetNC] = new Instruction(WriteType.PC, OpType.RetNc, FetchType.SP),
        [Opcode.RetC] = new Instruction(WriteType.PC, OpType.RetC, FetchType.SP),

        // Rotate
        [Opcode.Rlc_A] = new Instruction(WriteType.A, OpType.RotateLeft, FetchType.A),
        [Opcode.Rla] = new Instruction(WriteType.A, OpType.RotateLeftThroughCarry, FetchType.A),
        [Opcode.Rrc_A] = new Instruction(WriteType.A, OpType.RotateRight, FetchType.A),
        [Opcode.Rra] = new Instruction(WriteType.A, OpType.RotateRightThroughCarry, FetchType.A),

        // Rst
        [Opcode.Rst_00H] = new Instruction(WriteType.PC, OpType.Rst, FetchType.Address00H),
        [Opcode.Rst_08H] = new Instruction(WriteType.PC, OpType.Rst, FetchType.Address08H),
        [Opcode.Rst_10H] = new Instruction(WriteType.PC, OpType.Rst, FetchType.Address10H),
        [Opcode.Rst_18H] = new Instruction(WriteType.PC, OpType.Rst, FetchType.Address18H),
        [Opcode.Rst_20H] = new Instruction(WriteType.PC, OpType.Rst, FetchType.Address20H),
        [Opcode.Rst_28H] = new Instruction(WriteType.PC, OpType.Rst, FetchType.Address28H),
        [Opcode.Rst_30H] = new Instruction(WriteType.PC, OpType.Rst, FetchType.Address30H),
        [Opcode.Rst_38H] = new Instruction(WriteType.PC, OpType.Rst, FetchType.Address38H),

        // Sbc
        [Opcode.Sbc_A_B] = new Instruction(WriteType.A, OpType.Sbc, FetchType.B),
        [Opcode.Sbc_A_C] = new Instruction(WriteType.A, OpType.Sbc, FetchType.C),
        [Opcode.Sbc_A_D] = new Instruction(WriteType.A, OpType.Sbc, FetchType.D),
        [Opcode.Sbc_A_E] = new Instruction(WriteType.A, OpType.Sbc, FetchType.E),
        [Opcode.Sbc_A_H] = new Instruction(WriteType.A, OpType.Sbc, FetchType.H),
        [Opcode.Sbc_A_L] = new Instruction(WriteType.A, OpType.Sbc, FetchType.L),
        [Opcode.Sbc_A_XHL] = new Instruction(WriteType.A, OpType.Sbc, FetchType.XHL),
        [Opcode.Sbc_A_A] = new Instruction(WriteType.A, OpType.Sbc, FetchType.A),
        [Opcode.Sbc_A_N8] = new Instruction(WriteType.A, OpType.Sbc, FetchType.N8),

        // Scf
        [Opcode.Scf] = new Instruction(WriteType.None, OpType.Scf, FetchType.None),

        // Sub
        [Opcode.Sub_A_B] = new Instruction(WriteType.A, OpType.Sub, FetchType.B),
        [Opcode.Sub_A_C] = new Instruction(WriteType.A, OpType.Sub, FetchType.C),
        [Opcode.Sub_A_D] = new Instruction(WriteType.A, OpType.Sub, FetchType.D),
        [Opcode.Sub_A_E] = new Instruction(WriteType.A, OpType.Sub, FetchType.E),
        [Opcode.Sub_A_H] = new Instruction(WriteType.A, OpType.Sub, FetchType.H),
        [Opcode.Sub_A_L] = new Instruction(WriteType.A, OpType.Sub, FetchType.L),
        [Opcode.Sub_A_XHL] = new Instruction(WriteType.A, OpType.Sub, FetchType.XHL),
        [Opcode.Sub_A_A] = new Instruction(WriteType.A, OpType.Sub, FetchType.A),
        [Opcode.Sub_A_N8] = new Instruction(WriteType.A, OpType.Sub, FetchType.N8),

        // Xor
        [Opcode.Xor_A_B] = new Instruction(WriteType.A, OpType.Xor, FetchType.B),
        [Opcode.Xor_A_C] = new Instruction(WriteType.A, OpType.Xor, FetchType.C),
        [Opcode.Xor_A_D] = new Instruction(WriteType.A, OpType.Xor, FetchType.D),
        [Opcode.Xor_A_E] = new Instruction(WriteType.A, OpType.Xor, FetchType.E),
        [Opcode.Xor_A_H] = new Instruction(WriteType.A, OpType.Xor, FetchType.H),
        [Opcode.Xor_A_L] = new Instruction(WriteType.A, OpType.Xor, FetchType.L),
        [Opcode.Xor_A_XHL] = new Instruction(WriteType.A, OpType.Xor, FetchType.XHL),
        [Opcode.Xor_A_A] = new Instruction(WriteType.A, OpType.Xor, FetchType.A),
        [Opcode.Xor_A_N8] = new Instruction(WriteType.A, OpType.Xor, FetchType.N8),
    };
}