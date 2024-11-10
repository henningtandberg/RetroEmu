using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU;

public partial class Processor
{
    private void SetUpInstructions()
    {
        // Adc
        _instructions[Opcode.Adc_A_B] = new Instruction(WriteType.A, OpType.Adc, FetchType.B);
        _instructions[Opcode.Adc_A_C] = new Instruction(WriteType.A, OpType.Adc, FetchType.C);
        _instructions[Opcode.Adc_A_D] = new Instruction(WriteType.A, OpType.Adc, FetchType.D);
        _instructions[Opcode.Adc_A_E] = new Instruction(WriteType.A, OpType.Adc, FetchType.E);
        _instructions[Opcode.Adc_A_H] = new Instruction(WriteType.A, OpType.Adc, FetchType.H);
        _instructions[Opcode.Adc_A_L] = new Instruction(WriteType.A, OpType.Adc, FetchType.L);
        _instructions[Opcode.Adc_A_XHL] = new Instruction(WriteType.A, OpType.Adc, FetchType.XHL);
        _instructions[Opcode.Adc_A_A] = new Instruction(WriteType.A, OpType.Adc, FetchType.A);
        _instructions[Opcode.Adc_A_N8] = new Instruction(WriteType.A, OpType.Adc, FetchType.N8);

        // Add
        _instructions[Opcode.Add_A_B] = new Instruction(WriteType.A, OpType.Add, FetchType.B);
        _instructions[Opcode.Add_A_C] = new Instruction(WriteType.A, OpType.Add, FetchType.C);
        _instructions[Opcode.Add_A_D] = new Instruction(WriteType.A, OpType.Add, FetchType.D);
        _instructions[Opcode.Add_A_E] = new Instruction(WriteType.A, OpType.Add, FetchType.E);
        _instructions[Opcode.Add_A_H] = new Instruction(WriteType.A, OpType.Add, FetchType.H);
        _instructions[Opcode.Add_A_L] = new Instruction(WriteType.A, OpType.Add, FetchType.L);
        _instructions[Opcode.Add_A_XHL] = new Instruction(WriteType.A, OpType.Add, FetchType.XHL);
        _instructions[Opcode.Add_A_A] = new Instruction(WriteType.A, OpType.Add, FetchType.A);
        _instructions[Opcode.Add_A_N8] = new Instruction(WriteType.A, OpType.Add, FetchType.N8);
        _instructions[Opcode.Add_HL_BC] = new Instruction(WriteType.HL, OpType.Add16, FetchType.BC);
        _instructions[Opcode.Add_HL_DE] = new Instruction(WriteType.HL, OpType.Add16, FetchType.DE);
        _instructions[Opcode.Add_HL_HL] = new Instruction(WriteType.HL, OpType.Add16, FetchType.HL);
        _instructions[Opcode.Add_HL_SP] = new Instruction(WriteType.HL, OpType.Add16, FetchType.SP);
        _instructions[Opcode.Add_SP_N8] = new Instruction(WriteType.SP, OpType.AddSp, FetchType.N8);

        // And
        _instructions[Opcode.And_A_B] = new Instruction(WriteType.A, OpType.And, FetchType.B);
        _instructions[Opcode.And_A_C] = new Instruction(WriteType.A, OpType.And, FetchType.C);
        _instructions[Opcode.And_A_D] = new Instruction(WriteType.A, OpType.And, FetchType.D);
        _instructions[Opcode.And_A_E] = new Instruction(WriteType.A, OpType.And, FetchType.E);
        _instructions[Opcode.And_A_H] = new Instruction(WriteType.A, OpType.And, FetchType.H);
        _instructions[Opcode.And_A_L] = new Instruction(WriteType.A, OpType.And, FetchType.L);
        _instructions[Opcode.And_A_XHL] = new Instruction(WriteType.A, OpType.And, FetchType.XHL);
        _instructions[Opcode.And_A_A] = new Instruction(WriteType.A, OpType.And, FetchType.A);
        _instructions[Opcode.And_A_N8] = new Instruction(WriteType.A, OpType.And, FetchType.N8);

        // Call
        _instructions[Opcode.Call_N16] = new ConditionalInstruction(WriteType.PC, ConditionalOpType.Call, FetchType.N16,
            ConditionType.Always);
        _instructions[Opcode.CallNZ_N16] =
            new ConditionalInstruction(WriteType.PC, ConditionalOpType.Call, FetchType.N16, ConditionType.NZ);
        _instructions[Opcode.CallZ_N16] =
            new ConditionalInstruction(WriteType.PC, ConditionalOpType.Call, FetchType.N16, ConditionType.Z);
        _instructions[Opcode.CallNC_N16] =
            new ConditionalInstruction(WriteType.PC, ConditionalOpType.Call, FetchType.N16, ConditionType.NC);
        _instructions[Opcode.CallC_N16] =
            new ConditionalInstruction(WriteType.PC, ConditionalOpType.Call, FetchType.N16, ConditionType.C);

        // CB
        _instructions[Opcode.Pre_CB] = new CBInstruction();

        // Ccf
        _instructions[Opcode.Ccf] = new Instruction(WriteType.None, OpType.Ccf, FetchType.None);

        // Cp
        _instructions[Opcode.Cp_A_A] = new Instruction(WriteType.None, OpType.Cp, FetchType.A);
        _instructions[Opcode.Cp_A_B] = new Instruction(WriteType.None, OpType.Cp, FetchType.B);
        _instructions[Opcode.Cp_A_C] = new Instruction(WriteType.None, OpType.Cp, FetchType.C);
        _instructions[Opcode.Cp_A_D] = new Instruction(WriteType.None, OpType.Cp, FetchType.D);
        _instructions[Opcode.Cp_A_E] = new Instruction(WriteType.None, OpType.Cp, FetchType.E);
        _instructions[Opcode.Cp_A_H] = new Instruction(WriteType.None, OpType.Cp, FetchType.H);
        _instructions[Opcode.Cp_A_L] = new Instruction(WriteType.None, OpType.Cp, FetchType.L);
        _instructions[Opcode.Cp_A_XHL] = new Instruction(WriteType.None, OpType.Cp, FetchType.XHL);
        _instructions[Opcode.Cp_A_N8] = new Instruction(WriteType.None, OpType.Cp, FetchType.N8);

        // Cpl
        _instructions[Opcode.Cpl] = new Instruction(WriteType.A, OpType.Cpl, FetchType.A);

        // Daa
        _instructions[Opcode.Daa] = new Instruction(WriteType.A, OpType.Daa, FetchType.A);

        // Dec
        _instructions[Opcode.Dec_A] = new Instruction(WriteType.A, OpType.Dec, FetchType.A);
        _instructions[Opcode.Dec_B] = new Instruction(WriteType.B, OpType.Dec, FetchType.B);
        _instructions[Opcode.Dec_C] = new Instruction(WriteType.C, OpType.Dec, FetchType.C);
        _instructions[Opcode.Dec_D] = new Instruction(WriteType.D, OpType.Dec, FetchType.D);
        _instructions[Opcode.Dec_E] = new Instruction(WriteType.E, OpType.Dec, FetchType.E);
        _instructions[Opcode.Dec_H] = new Instruction(WriteType.H, OpType.Dec, FetchType.H);
        _instructions[Opcode.Dec_L] = new Instruction(WriteType.L, OpType.Dec, FetchType.L);
        _instructions[Opcode.Dec_XHL] = new Instruction(WriteType.XHL, OpType.Dec, FetchType.XHL);
        _instructions[Opcode.Dec_BC] = new Instruction(WriteType.BC, OpType.Dec16, FetchType.BC);
        _instructions[Opcode.Dec_DE] = new Instruction(WriteType.DE, OpType.Dec16, FetchType.DE);
        _instructions[Opcode.Dec_HL] = new Instruction(WriteType.HL, OpType.Dec16, FetchType.HL);
        _instructions[Opcode.Dec_SP] = new Instruction(WriteType.SP, OpType.Dec16, FetchType.SP);

        // Inc
        _instructions[Opcode.Inc_A] = new Instruction(WriteType.A, OpType.Inc, FetchType.A);
        _instructions[Opcode.Inc_B] = new Instruction(WriteType.B, OpType.Inc, FetchType.B);
        _instructions[Opcode.Inc_C] = new Instruction(WriteType.C, OpType.Inc, FetchType.C);
        _instructions[Opcode.Inc_D] = new Instruction(WriteType.D, OpType.Inc, FetchType.D);
        _instructions[Opcode.Inc_E] = new Instruction(WriteType.E, OpType.Inc, FetchType.E);
        _instructions[Opcode.Inc_H] = new Instruction(WriteType.H, OpType.Inc, FetchType.H);
        _instructions[Opcode.Inc_L] = new Instruction(WriteType.L, OpType.Inc, FetchType.L);
        _instructions[Opcode.Inc_XHL] = new Instruction(WriteType.XHL, OpType.Inc, FetchType.XHL);
        _instructions[Opcode.Inc_BC] = new Instruction(WriteType.BC, OpType.Inc16, FetchType.BC);
        _instructions[Opcode.Inc_DE] = new Instruction(WriteType.DE, OpType.Inc16, FetchType.DE);
        _instructions[Opcode.Inc_HL] = new Instruction(WriteType.HL, OpType.Inc16, FetchType.HL);
        _instructions[Opcode.Inc_SP] = new Instruction(WriteType.SP, OpType.Inc16, FetchType.SP);

        // Jp
        _instructions[Opcode.Jp_N16] =
            new ConditionalInstruction(WriteType.PC, ConditionalOpType.Jp, FetchType.N16, ConditionType.Always);
        _instructions[Opcode.JpNZ_N16] =
            new ConditionalInstruction(WriteType.PC, ConditionalOpType.Jp, FetchType.N16, ConditionType.NZ);
        _instructions[Opcode.JpZ_N16] =
            new ConditionalInstruction(WriteType.PC, ConditionalOpType.Jp, FetchType.N16, ConditionType.Z);
        _instructions[Opcode.JpNC_N16] =
            new ConditionalInstruction(WriteType.PC, ConditionalOpType.Jp, FetchType.N16, ConditionType.NC);
        _instructions[Opcode.JpC_N16] =
            new ConditionalInstruction(WriteType.PC, ConditionalOpType.Jp, FetchType.N16, ConditionType.C);
        _instructions[Opcode.Jp_XHL] =
            new ConditionalInstruction(WriteType.PC, ConditionalOpType.Jp, FetchType.XHL, ConditionType.Always);

        _instructions[Opcode.Jr_N8] =
            new ConditionalInstruction(WriteType.PC, ConditionalOpType.Jr, FetchType.N8, ConditionType.Always);
        _instructions[Opcode.JrNZ_N8] =
            new ConditionalInstruction(WriteType.PC, ConditionalOpType.Jr, FetchType.N8, ConditionType.NZ);
        _instructions[Opcode.JrZ_N8] =
            new ConditionalInstruction(WriteType.PC, ConditionalOpType.Jr, FetchType.N8, ConditionType.Z);
        _instructions[Opcode.JrNC_N8] =
            new ConditionalInstruction(WriteType.PC, ConditionalOpType.Jr, FetchType.N8, ConditionType.NC);
        _instructions[Opcode.JrC_N8] =
            new ConditionalInstruction(WriteType.PC, ConditionalOpType.Jr, FetchType.N8, ConditionType.C);

        // Ld

        _instructions[Opcode.Ld_A_B] = new Instruction(WriteType.A, OpType.Ld, FetchType.B);
        _instructions[Opcode.Ld_A_C] = new Instruction(WriteType.A, OpType.Ld, FetchType.C);
        _instructions[Opcode.Ld_A_D] = new Instruction(WriteType.A, OpType.Ld, FetchType.D);
        _instructions[Opcode.Ld_A_E] = new Instruction(WriteType.A, OpType.Ld, FetchType.E);
        _instructions[Opcode.Ld_A_H] = new Instruction(WriteType.A, OpType.Ld, FetchType.H);
        _instructions[Opcode.Ld_A_L] = new Instruction(WriteType.A, OpType.Ld, FetchType.L);
        _instructions[Opcode.Ld_A_XHL] = new Instruction(WriteType.A, OpType.Ld, FetchType.XHL);
        _instructions[Opcode.Ld_A_A] = new Instruction(WriteType.A, OpType.Ld, FetchType.A);
        _instructions[Opcode.Ld_A_N8] = new Instruction(WriteType.A, OpType.Ld, FetchType.N8);

        _instructions[Opcode.Ld_B_B] = new Instruction(WriteType.B, OpType.Ld, FetchType.B);
        _instructions[Opcode.Ld_B_C] = new Instruction(WriteType.B, OpType.Ld, FetchType.C);
        _instructions[Opcode.Ld_B_D] = new Instruction(WriteType.B, OpType.Ld, FetchType.D);
        _instructions[Opcode.Ld_B_E] = new Instruction(WriteType.B, OpType.Ld, FetchType.E);
        _instructions[Opcode.Ld_B_H] = new Instruction(WriteType.B, OpType.Ld, FetchType.H);
        _instructions[Opcode.Ld_B_L] = new Instruction(WriteType.B, OpType.Ld, FetchType.L);
        _instructions[Opcode.Ld_B_XHL] = new Instruction(WriteType.B, OpType.Ld, FetchType.XHL);
        _instructions[Opcode.Ld_B_A] = new Instruction(WriteType.B, OpType.Ld, FetchType.A);
        _instructions[Opcode.Ld_B_N8] = new Instruction(WriteType.B, OpType.Ld, FetchType.N8);

        _instructions[Opcode.Ld_C_B] = new Instruction(WriteType.C, OpType.Ld, FetchType.B);
        _instructions[Opcode.Ld_C_C] = new Instruction(WriteType.C, OpType.Ld, FetchType.C);
        _instructions[Opcode.Ld_C_D] = new Instruction(WriteType.C, OpType.Ld, FetchType.D);
        _instructions[Opcode.Ld_C_E] = new Instruction(WriteType.C, OpType.Ld, FetchType.E);
        _instructions[Opcode.Ld_C_H] = new Instruction(WriteType.C, OpType.Ld, FetchType.H);
        _instructions[Opcode.Ld_C_L] = new Instruction(WriteType.C, OpType.Ld, FetchType.L);
        _instructions[Opcode.Ld_C_XHL] = new Instruction(WriteType.C, OpType.Ld, FetchType.XHL);
        _instructions[Opcode.Ld_C_A] = new Instruction(WriteType.C, OpType.Ld, FetchType.A);
        _instructions[Opcode.Ld_C_N8] = new Instruction(WriteType.C, OpType.Ld, FetchType.N8);

        _instructions[Opcode.Ld_D_B] = new Instruction(WriteType.D, OpType.Ld, FetchType.B);
        _instructions[Opcode.Ld_D_C] = new Instruction(WriteType.D, OpType.Ld, FetchType.C);
        _instructions[Opcode.Ld_D_D] = new Instruction(WriteType.D, OpType.Ld, FetchType.D);
        _instructions[Opcode.Ld_D_E] = new Instruction(WriteType.D, OpType.Ld, FetchType.E);
        _instructions[Opcode.Ld_D_H] = new Instruction(WriteType.D, OpType.Ld, FetchType.H);
        _instructions[Opcode.Ld_D_L] = new Instruction(WriteType.D, OpType.Ld, FetchType.L);
        _instructions[Opcode.Ld_D_XHL] = new Instruction(WriteType.D, OpType.Ld, FetchType.XHL);
        _instructions[Opcode.Ld_D_A] = new Instruction(WriteType.D, OpType.Ld, FetchType.A);
        _instructions[Opcode.Ld_D_N8] = new Instruction(WriteType.D, OpType.Ld, FetchType.N8);

        _instructions[Opcode.Ld_E_B] = new Instruction(WriteType.E, OpType.Ld, FetchType.B);
        _instructions[Opcode.Ld_E_C] = new Instruction(WriteType.E, OpType.Ld, FetchType.C);
        _instructions[Opcode.Ld_E_D] = new Instruction(WriteType.E, OpType.Ld, FetchType.D);
        _instructions[Opcode.Ld_E_E] = new Instruction(WriteType.E, OpType.Ld, FetchType.E);
        _instructions[Opcode.Ld_E_H] = new Instruction(WriteType.E, OpType.Ld, FetchType.H);
        _instructions[Opcode.Ld_E_L] = new Instruction(WriteType.E, OpType.Ld, FetchType.L);
        _instructions[Opcode.Ld_E_XHL] = new Instruction(WriteType.E, OpType.Ld, FetchType.XHL);
        _instructions[Opcode.Ld_E_A] = new Instruction(WriteType.E, OpType.Ld, FetchType.A);
        _instructions[Opcode.Ld_E_N8] = new Instruction(WriteType.E, OpType.Ld, FetchType.N8);

        _instructions[Opcode.Ld_H_B] = new Instruction(WriteType.H, OpType.Ld, FetchType.B);
        _instructions[Opcode.Ld_H_C] = new Instruction(WriteType.H, OpType.Ld, FetchType.C);
        _instructions[Opcode.Ld_H_D] = new Instruction(WriteType.H, OpType.Ld, FetchType.D);
        _instructions[Opcode.Ld_H_E] = new Instruction(WriteType.H, OpType.Ld, FetchType.E);
        _instructions[Opcode.Ld_H_H] = new Instruction(WriteType.H, OpType.Ld, FetchType.H);
        _instructions[Opcode.Ld_H_L] = new Instruction(WriteType.H, OpType.Ld, FetchType.L);
        _instructions[Opcode.Ld_H_XHL] = new Instruction(WriteType.H, OpType.Ld, FetchType.XHL);
        _instructions[Opcode.Ld_H_A] = new Instruction(WriteType.H, OpType.Ld, FetchType.A);
        _instructions[Opcode.Ld_H_N8] = new Instruction(WriteType.H, OpType.Ld, FetchType.N8);

        _instructions[Opcode.Ld_L_B] = new Instruction(WriteType.L, OpType.Ld, FetchType.B);
        _instructions[Opcode.Ld_L_C] = new Instruction(WriteType.L, OpType.Ld, FetchType.C);
        _instructions[Opcode.Ld_L_D] = new Instruction(WriteType.L, OpType.Ld, FetchType.D);
        _instructions[Opcode.Ld_L_E] = new Instruction(WriteType.L, OpType.Ld, FetchType.E);
        _instructions[Opcode.Ld_L_H] = new Instruction(WriteType.L, OpType.Ld, FetchType.H);
        _instructions[Opcode.Ld_L_L] = new Instruction(WriteType.L, OpType.Ld, FetchType.L);
        _instructions[Opcode.Ld_L_XHL] = new Instruction(WriteType.L, OpType.Ld, FetchType.XHL);
        _instructions[Opcode.Ld_L_A] = new Instruction(WriteType.L, OpType.Ld, FetchType.A);
        _instructions[Opcode.Ld_L_N8] = new Instruction(WriteType.L, OpType.Ld, FetchType.N8);

        _instructions[Opcode.Ld_XHL_B] = new Instruction(WriteType.XHL, OpType.Ld, FetchType.B);
        _instructions[Opcode.Ld_XHL_C] = new Instruction(WriteType.XHL, OpType.Ld, FetchType.C);
        _instructions[Opcode.Ld_XHL_D] = new Instruction(WriteType.XHL, OpType.Ld, FetchType.D);
        _instructions[Opcode.Ld_XHL_E] = new Instruction(WriteType.XHL, OpType.Ld, FetchType.E);
        _instructions[Opcode.Ld_XHL_H] = new Instruction(WriteType.XHL, OpType.Ld, FetchType.H);
        _instructions[Opcode.Ld_XHL_L] = new Instruction(WriteType.XHL, OpType.Ld, FetchType.L);
        _instructions[Opcode.Ld_XHL_A] = new Instruction(WriteType.XHL, OpType.Ld, FetchType.A);
        _instructions[Opcode.Ld_XHL_N8] = new Instruction(WriteType.L, OpType.Ld, FetchType.N8);

        _instructions[Opcode.Ld_A_XC] = new Instruction(WriteType.A, OpType.Ld, FetchType.XC);
        _instructions[Opcode.Ld_XC_A] = new Instruction(WriteType.XC, OpType.Ld, FetchType.A);

        _instructions[Opcode.Ld_BC_N16] = new Instruction(WriteType.BC, OpType.Ld, FetchType.N16);
        _instructions[Opcode.Ld_DE_N16] = new Instruction(WriteType.DE, OpType.Ld, FetchType.N16);
        _instructions[Opcode.Ld_HL_N16] = new Instruction(WriteType.DE, OpType.Ld, FetchType.N16);
        _instructions[Opcode.Ld_SP_N16] = new Instruction(WriteType.SP, OpType.Ld, FetchType.N16);

        _instructions[Opcode.Ld_XBC_A] = new Instruction(WriteType.XBC, OpType.Ld, FetchType.A);
        _instructions[Opcode.Ld_XDE_A] = new Instruction(WriteType.XDE, OpType.Ld, FetchType.A);
        _instructions[Opcode.Ld_XHLI_A] = new Instruction(WriteType.XHLI, OpType.Ld, FetchType.A);
        _instructions[Opcode.Ld_XHLD_A] = new Instruction(WriteType.XHLD, OpType.Ld, FetchType.A);

        _instructions[Opcode.Ld_A_XN8] = new Instruction(WriteType.A, OpType.Ld, FetchType.XN8);
        _instructions[Opcode.Ld_XN8_A] = new Instruction(WriteType.XN8, OpType.Ld, FetchType.A);

        _instructions[Opcode.Ld_XN16_SP] = new Instruction(WriteType.XN16, OpType.Ld, FetchType.SP);

        _instructions[Opcode.Ld_A_XBC] = new Instruction(WriteType.A, OpType.Ld, FetchType.XBC);
        _instructions[Opcode.Ld_A_XDE] = new Instruction(WriteType.A, OpType.Ld, FetchType.XDE);
        _instructions[Opcode.Ld_A_XHLI] = new Instruction(WriteType.A, OpType.Ld, FetchType.XHLI);
        _instructions[Opcode.Ld_A_XHLD] = new Instruction(WriteType.A, OpType.Ld, FetchType.XHLD);

        _instructions[Opcode.Ld_HL_SPN8] = new Instruction(WriteType.HL, OpType.Ld, FetchType.SPN8);
        _instructions[Opcode.Ld_SP_HL] = new Instruction(WriteType.SP, OpType.Ld, FetchType.HL);

        _instructions[Opcode.Ld_XN16_A] = new Instruction(WriteType.XN16, OpType.Ld, FetchType.A);
        _instructions[Opcode.Ld_A_XN16] = new Instruction(WriteType.A, OpType.Ld, FetchType.XN16);

        // Nop
        _instructions[Opcode.Nop] = new Instruction(WriteType.None, OpType.Nop, FetchType.None);

        // Or
        _instructions[Opcode.Or_A_B] = new Instruction(WriteType.A, OpType.Or, FetchType.B);
        _instructions[Opcode.Or_A_C] = new Instruction(WriteType.A, OpType.Or, FetchType.C);
        _instructions[Opcode.Or_A_D] = new Instruction(WriteType.A, OpType.Or, FetchType.D);
        _instructions[Opcode.Or_A_E] = new Instruction(WriteType.A, OpType.Or, FetchType.E);
        _instructions[Opcode.Or_A_H] = new Instruction(WriteType.A, OpType.Or, FetchType.H);
        _instructions[Opcode.Or_A_L] = new Instruction(WriteType.A, OpType.Or, FetchType.L);
        _instructions[Opcode.Or_A_XHL] = new Instruction(WriteType.A, OpType.Or, FetchType.XHL);
        _instructions[Opcode.Or_A_A] = new Instruction(WriteType.A, OpType.Or, FetchType.A);
        _instructions[Opcode.Or_A_N8] = new Instruction(WriteType.A, OpType.Or, FetchType.N8);

        // Pop
        _instructions[Opcode.Pop_AF] = new Instruction(WriteType.AF, OpType.Ld, FetchType.Pop);
        _instructions[Opcode.Pop_BC] = new Instruction(WriteType.BC, OpType.Ld, FetchType.Pop);
        _instructions[Opcode.Pop_DE] = new Instruction(WriteType.DE, OpType.Ld, FetchType.Pop);
        _instructions[Opcode.Pop_HL] = new Instruction(WriteType.HL, OpType.Ld, FetchType.Pop);

        // Push 
        _instructions[Opcode.Push_AF] = new Instruction(WriteType.Push, OpType.Ld, FetchType.AF);
        _instructions[Opcode.Push_BC] = new Instruction(WriteType.Push, OpType.Ld, FetchType.BC);
        _instructions[Opcode.Push_DE] = new Instruction(WriteType.Push, OpType.Ld, FetchType.DE);
        _instructions[Opcode.Push_HL] = new Instruction(WriteType.Push, OpType.Ld, FetchType.HL);

        // Ret
        _instructions[Opcode.Ret] =
            new ConditionalInstruction(WriteType.PC, ConditionalOpType.Ret, FetchType.SP, ConditionType.Always);
        _instructions[Opcode.RetNZ] =
            new ConditionalInstruction(WriteType.PC, ConditionalOpType.Ret, FetchType.SP, ConditionType.NZ);
        _instructions[Opcode.RetZ] =
            new ConditionalInstruction(WriteType.PC, ConditionalOpType.Ret, FetchType.SP, ConditionType.Z);
        _instructions[Opcode.RetNC] =
            new ConditionalInstruction(WriteType.PC, ConditionalOpType.Ret, FetchType.SP, ConditionType.NC);
        _instructions[Opcode.RetC] =
            new ConditionalInstruction(WriteType.PC, ConditionalOpType.Ret, FetchType.SP, ConditionType.C);

        // Rotate
        _instructions[Opcode.Rlc_A] = new RotationInstruction(WriteType.A, RotateOpType.RotateThroughCarry, FetchType.A,
            RotationDirection.Left);
        _instructions[Opcode.Rla] =
            new RotationInstruction(WriteType.A, RotateOpType.Rotate, FetchType.A, RotationDirection.Left);
        _instructions[Opcode.Rrc_A] = new RotationInstruction(WriteType.A, RotateOpType.RotateThroughCarry, FetchType.A,
            RotationDirection.Right);
        _instructions[Opcode.Rra] =
            new RotationInstruction(WriteType.A, RotateOpType.Rotate, FetchType.A, RotationDirection.Right);

        // Rst
        _instructions[Opcode.Rst_00H] = new RestartInstruction(0x00);
        _instructions[Opcode.Rst_08H] = new RestartInstruction(0x08);
        _instructions[Opcode.Rst_10H] = new RestartInstruction(0x10);
        _instructions[Opcode.Rst_18H] = new RestartInstruction(0x18);
        _instructions[Opcode.Rst_20H] = new RestartInstruction(0x20);
        _instructions[Opcode.Rst_28H] = new RestartInstruction(0x28);
        _instructions[Opcode.Rst_30H] = new RestartInstruction(0x30);
        _instructions[Opcode.Rst_38H] = new RestartInstruction(0x38);

        // Sbc
        _instructions[Opcode.Sbc_A_B] = new Instruction(WriteType.A, OpType.Sbc, FetchType.B);
        _instructions[Opcode.Sbc_A_C] = new Instruction(WriteType.A, OpType.Sbc, FetchType.C);
        _instructions[Opcode.Sbc_A_D] = new Instruction(WriteType.A, OpType.Sbc, FetchType.D);
        _instructions[Opcode.Sbc_A_E] = new Instruction(WriteType.A, OpType.Sbc, FetchType.E);
        _instructions[Opcode.Sbc_A_H] = new Instruction(WriteType.A, OpType.Sbc, FetchType.H);
        _instructions[Opcode.Sbc_A_L] = new Instruction(WriteType.A, OpType.Sbc, FetchType.L);
        _instructions[Opcode.Sbc_A_XHL] = new Instruction(WriteType.A, OpType.Sbc, FetchType.XHL);
        _instructions[Opcode.Sbc_A_A] = new Instruction(WriteType.A, OpType.Sbc, FetchType.A);
        _instructions[Opcode.Sbc_A_N8] = new Instruction(WriteType.A, OpType.Sbc, FetchType.N8);

        // Scf
        _instructions[Opcode.Scf] = new Instruction(WriteType.None, OpType.Scf, FetchType.None);

        // Sub
        _instructions[Opcode.Sub_A_B] = new Instruction(WriteType.A, OpType.Sub, FetchType.B);
        _instructions[Opcode.Sub_A_C] = new Instruction(WriteType.A, OpType.Sub, FetchType.C);
        _instructions[Opcode.Sub_A_D] = new Instruction(WriteType.A, OpType.Sub, FetchType.D);
        _instructions[Opcode.Sub_A_E] = new Instruction(WriteType.A, OpType.Sub, FetchType.E);
        _instructions[Opcode.Sub_A_H] = new Instruction(WriteType.A, OpType.Sub, FetchType.H);
        _instructions[Opcode.Sub_A_L] = new Instruction(WriteType.A, OpType.Sub, FetchType.L);
        _instructions[Opcode.Sub_A_XHL] = new Instruction(WriteType.A, OpType.Sub, FetchType.XHL);
        _instructions[Opcode.Sub_A_A] = new Instruction(WriteType.A, OpType.Sub, FetchType.A);
        _instructions[Opcode.Sub_A_N8] = new Instruction(WriteType.A, OpType.Sub, FetchType.N8);

        // Xor
        _instructions[Opcode.Xor_A_B] = new Instruction(WriteType.A, OpType.Xor, FetchType.B);
        _instructions[Opcode.Xor_A_C] = new Instruction(WriteType.A, OpType.Xor, FetchType.C);
        _instructions[Opcode.Xor_A_D] = new Instruction(WriteType.A, OpType.Xor, FetchType.D);
        _instructions[Opcode.Xor_A_E] = new Instruction(WriteType.A, OpType.Xor, FetchType.E);
        _instructions[Opcode.Xor_A_H] = new Instruction(WriteType.A, OpType.Xor, FetchType.H);
        _instructions[Opcode.Xor_A_L] = new Instruction(WriteType.A, OpType.Xor, FetchType.L);
        _instructions[Opcode.Xor_A_XHL] = new Instruction(WriteType.A, OpType.Xor, FetchType.XHL);
        _instructions[Opcode.Xor_A_A] = new Instruction(WriteType.A, OpType.Xor, FetchType.A);
        _instructions[Opcode.Xor_A_N8] = new Instruction(WriteType.A, OpType.Xor, FetchType.N8);
    }
}