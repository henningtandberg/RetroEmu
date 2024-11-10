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

        _instructions[Opcode.Ld_A_B] = new LoadInstruction(WriteType.A, FetchType.B);
        _instructions[Opcode.Ld_A_C] = new LoadInstruction(WriteType.A, FetchType.C);
        _instructions[Opcode.Ld_A_D] = new LoadInstruction(WriteType.A, FetchType.D);
        _instructions[Opcode.Ld_A_E] = new LoadInstruction(WriteType.A, FetchType.E);
        _instructions[Opcode.Ld_A_H] = new LoadInstruction(WriteType.A, FetchType.H);
        _instructions[Opcode.Ld_A_L] = new LoadInstruction(WriteType.A, FetchType.L);
        _instructions[Opcode.Ld_A_XHL] = new LoadInstruction(WriteType.A, FetchType.XHL);
        _instructions[Opcode.Ld_A_A] = new LoadInstruction(WriteType.A, FetchType.A);
        _instructions[Opcode.Ld_A_N8] = new LoadInstruction(WriteType.A, FetchType.N8);

        _instructions[Opcode.Ld_B_B] = new LoadInstruction(WriteType.B, FetchType.B);
        _instructions[Opcode.Ld_B_C] = new LoadInstruction(WriteType.B, FetchType.C);
        _instructions[Opcode.Ld_B_D] = new LoadInstruction(WriteType.B, FetchType.D);
        _instructions[Opcode.Ld_B_E] = new LoadInstruction(WriteType.B, FetchType.E);
        _instructions[Opcode.Ld_B_H] = new LoadInstruction(WriteType.B, FetchType.H);
        _instructions[Opcode.Ld_B_L] = new LoadInstruction(WriteType.B, FetchType.L);
        _instructions[Opcode.Ld_B_XHL] = new LoadInstruction(WriteType.B, FetchType.XHL);
        _instructions[Opcode.Ld_B_A] = new LoadInstruction(WriteType.B, FetchType.A);
        _instructions[Opcode.Ld_B_N8] = new LoadInstruction(WriteType.B, FetchType.N8);

        _instructions[Opcode.Ld_C_B] = new LoadInstruction(WriteType.C, FetchType.B);
        _instructions[Opcode.Ld_C_C] = new LoadInstruction(WriteType.C, FetchType.C);
        _instructions[Opcode.Ld_C_D] = new LoadInstruction(WriteType.C, FetchType.D);
        _instructions[Opcode.Ld_C_E] = new LoadInstruction(WriteType.C, FetchType.E);
        _instructions[Opcode.Ld_C_H] = new LoadInstruction(WriteType.C, FetchType.H);
        _instructions[Opcode.Ld_C_L] = new LoadInstruction(WriteType.C, FetchType.L);
        _instructions[Opcode.Ld_C_XHL] = new LoadInstruction(WriteType.C, FetchType.XHL);
        _instructions[Opcode.Ld_C_A] = new LoadInstruction(WriteType.C, FetchType.A);
        _instructions[Opcode.Ld_C_N8] = new LoadInstruction(WriteType.C, FetchType.N8);

        _instructions[Opcode.Ld_D_B] = new LoadInstruction(WriteType.D, FetchType.B);
        _instructions[Opcode.Ld_D_C] = new LoadInstruction(WriteType.D, FetchType.C);
        _instructions[Opcode.Ld_D_D] = new LoadInstruction(WriteType.D, FetchType.D);
        _instructions[Opcode.Ld_D_E] = new LoadInstruction(WriteType.D, FetchType.E);
        _instructions[Opcode.Ld_D_H] = new LoadInstruction(WriteType.D, FetchType.H);
        _instructions[Opcode.Ld_D_L] = new LoadInstruction(WriteType.D, FetchType.L);
        _instructions[Opcode.Ld_D_XHL] = new LoadInstruction(WriteType.D, FetchType.XHL);
        _instructions[Opcode.Ld_D_A] = new LoadInstruction(WriteType.D, FetchType.A);
        _instructions[Opcode.Ld_D_N8] = new LoadInstruction(WriteType.D, FetchType.N8);

        _instructions[Opcode.Ld_E_B] = new LoadInstruction(WriteType.E, FetchType.B);
        _instructions[Opcode.Ld_E_C] = new LoadInstruction(WriteType.E, FetchType.C);
        _instructions[Opcode.Ld_E_D] = new LoadInstruction(WriteType.E, FetchType.D);
        _instructions[Opcode.Ld_E_E] = new LoadInstruction(WriteType.E, FetchType.E);
        _instructions[Opcode.Ld_E_H] = new LoadInstruction(WriteType.E, FetchType.H);
        _instructions[Opcode.Ld_E_L] = new LoadInstruction(WriteType.E, FetchType.L);
        _instructions[Opcode.Ld_E_XHL] = new LoadInstruction(WriteType.E, FetchType.XHL);
        _instructions[Opcode.Ld_E_A] = new LoadInstruction(WriteType.E, FetchType.A);
        _instructions[Opcode.Ld_E_N8] = new LoadInstruction(WriteType.E, FetchType.N8);

        _instructions[Opcode.Ld_H_B] = new LoadInstruction(WriteType.H, FetchType.B);
        _instructions[Opcode.Ld_H_C] = new LoadInstruction(WriteType.H, FetchType.C);
        _instructions[Opcode.Ld_H_D] = new LoadInstruction(WriteType.H, FetchType.D);
        _instructions[Opcode.Ld_H_E] = new LoadInstruction(WriteType.H, FetchType.E);
        _instructions[Opcode.Ld_H_H] = new LoadInstruction(WriteType.H, FetchType.H);
        _instructions[Opcode.Ld_H_L] = new LoadInstruction(WriteType.H, FetchType.L);
        _instructions[Opcode.Ld_H_XHL] = new LoadInstruction(WriteType.H, FetchType.XHL);
        _instructions[Opcode.Ld_H_A] = new LoadInstruction(WriteType.H, FetchType.A);
        _instructions[Opcode.Ld_H_N8] = new LoadInstruction(WriteType.H, FetchType.N8);

        _instructions[Opcode.Ld_L_B] = new LoadInstruction(WriteType.L, FetchType.B);
        _instructions[Opcode.Ld_L_C] = new LoadInstruction(WriteType.L, FetchType.C);
        _instructions[Opcode.Ld_L_D] = new LoadInstruction(WriteType.L, FetchType.D);
        _instructions[Opcode.Ld_L_E] = new LoadInstruction(WriteType.L, FetchType.E);
        _instructions[Opcode.Ld_L_H] = new LoadInstruction(WriteType.L, FetchType.H);
        _instructions[Opcode.Ld_L_L] = new LoadInstruction(WriteType.L, FetchType.L);
        _instructions[Opcode.Ld_L_XHL] = new LoadInstruction(WriteType.L, FetchType.XHL);
        _instructions[Opcode.Ld_L_A] = new LoadInstruction(WriteType.L, FetchType.A);
        _instructions[Opcode.Ld_L_N8] = new LoadInstruction(WriteType.L, FetchType.N8);

        _instructions[Opcode.Ld_XHL_B] = new LoadInstruction(WriteType.XHL, FetchType.B);
        _instructions[Opcode.Ld_XHL_C] = new LoadInstruction(WriteType.XHL, FetchType.C);
        _instructions[Opcode.Ld_XHL_D] = new LoadInstruction(WriteType.XHL, FetchType.D);
        _instructions[Opcode.Ld_XHL_E] = new LoadInstruction(WriteType.XHL, FetchType.E);
        _instructions[Opcode.Ld_XHL_H] = new LoadInstruction(WriteType.XHL, FetchType.H);
        _instructions[Opcode.Ld_XHL_L] = new LoadInstruction(WriteType.XHL, FetchType.L);
        _instructions[Opcode.Ld_XHL_A] = new LoadInstruction(WriteType.XHL, FetchType.A);
        _instructions[Opcode.Ld_XHL_N8] = new LoadInstruction(WriteType.L, FetchType.N8);

        _instructions[Opcode.Ld_A_XC] = new LoadInstruction(WriteType.A, FetchType.XC);
        _instructions[Opcode.Ld_XC_A] = new LoadInstruction(WriteType.XC, FetchType.A);

        _instructions[Opcode.Ld_BC_N16] = new LoadInstruction(WriteType.BC, FetchType.N16);
        _instructions[Opcode.Ld_DE_N16] = new LoadInstruction(WriteType.DE, FetchType.N16);
        _instructions[Opcode.Ld_HL_N16] = new LoadInstruction(WriteType.DE, FetchType.N16);
        _instructions[Opcode.Ld_SP_N16] = new LoadInstruction(WriteType.SP, FetchType.N16);

        _instructions[Opcode.Ld_XBC_A] = new LoadInstruction(WriteType.XBC, FetchType.A);
        _instructions[Opcode.Ld_XDE_A] = new LoadInstruction(WriteType.XDE, FetchType.A);
        _instructions[Opcode.Ld_XHLI_A] = new LoadInstruction(WriteType.XHLI, FetchType.A);
        _instructions[Opcode.Ld_XHLD_A] = new LoadInstruction(WriteType.XHLD, FetchType.A);

        _instructions[Opcode.Ld_A_XN8] = new LoadInstruction(WriteType.A, FetchType.XN8);
        _instructions[Opcode.Ld_XN8_A] = new LoadInstruction(WriteType.XN8, FetchType.A);

        _instructions[Opcode.Ld_XN16_SP] = new LoadInstruction(WriteType.XN16, FetchType.SP);

        _instructions[Opcode.Ld_A_XBC] = new LoadInstruction(WriteType.A, FetchType.XBC);
        _instructions[Opcode.Ld_A_XDE] = new LoadInstruction(WriteType.A, FetchType.XDE);
        _instructions[Opcode.Ld_A_XHLI] = new LoadInstruction(WriteType.A, FetchType.XHLI);
        _instructions[Opcode.Ld_A_XHLD] = new LoadInstruction(WriteType.A, FetchType.XHLD);

        _instructions[Opcode.Ld_HL_SPN8] = new LoadInstruction(WriteType.HL, FetchType.SPN8);
        _instructions[Opcode.Ld_SP_HL] = new LoadInstruction(WriteType.SP, FetchType.HL);

        _instructions[Opcode.Ld_XN16_A] = new LoadInstruction(WriteType.XN16, FetchType.A);
        _instructions[Opcode.Ld_A_XN16] = new LoadInstruction(WriteType.A, FetchType.XN16);

        // Nop
        _instructions[Opcode.Nop] = new NopInstruction();

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
        _instructions[Opcode.Pop_AF] = new LoadInstruction(WriteType.AF, FetchType.Pop);
        _instructions[Opcode.Pop_BC] = new LoadInstruction(WriteType.BC, FetchType.Pop);
        _instructions[Opcode.Pop_DE] = new LoadInstruction(WriteType.DE, FetchType.Pop);
        _instructions[Opcode.Pop_HL] = new LoadInstruction(WriteType.HL, FetchType.Pop);

        // Push 
        _instructions[Opcode.Push_AF] = new LoadInstruction(WriteType.Push, FetchType.AF);
        _instructions[Opcode.Push_BC] = new LoadInstruction(WriteType.Push, FetchType.BC);
        _instructions[Opcode.Push_DE] = new LoadInstruction(WriteType.Push, FetchType.DE);
        _instructions[Opcode.Push_HL] = new LoadInstruction(WriteType.Push, FetchType.HL);

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