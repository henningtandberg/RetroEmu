using System;
using RetroEmu.Devices.DMG;
using RetroEmu.Devices.DMG.CPU;
using Xunit;
using Xunit.Abstractions;

namespace RetroEmu.Devices.Tests
{
    public unsafe class RecordedProcessor : Processor
    {
        private IMemory _memory;
        private ITestOutputHelper _output;
        public RecordedProcessor(IMemory memory, ITestOutputHelper output) : base(memory)
        {
            _memory = memory;
            _output = output;
        }

        static private String CreateDiffString(String prefix, byte* pre_reg, byte* post_reg)
        {
            if (*pre_reg != *post_reg)
                return prefix + "(" + (*pre_reg).ToString() + " -> " + (*post_reg).ToString() + ") ";
            return "";
        }
        static private String CreateDiffString(String prefix, ushort* pre_reg, ushort* post_reg)
        {
            if (*pre_reg != *post_reg)
                return prefix + "(" + (*pre_reg).ToString() + " -> " + (*post_reg).ToString() + ") ";
            return "";
        }

        new public int Update()
        {
            Registers pre_regs = Registers;
            var cycles = base.Update();
            Registers post_regs = Registers;

            var opcode = _memory.Read(*pre_regs.PC);
            _output.WriteLine(((OPC.OPC_Enum)opcode).ToString());

            String reg_diff = "";
            reg_diff += CreateDiffString("A", pre_regs.A, post_regs.A);
            reg_diff += CreateDiffString("F", pre_regs.F, post_regs.F);
            reg_diff += CreateDiffString("B", pre_regs.B, post_regs.B);
            reg_diff += CreateDiffString("C", pre_regs.C, post_regs.C);
            reg_diff += CreateDiffString("D", pre_regs.D, post_regs.D);
            reg_diff += CreateDiffString("E", pre_regs.E, post_regs.E);
            reg_diff += CreateDiffString("H", pre_regs.H, post_regs.H);
            reg_diff += CreateDiffString("L", pre_regs.L, post_regs.L);
            reg_diff += CreateDiffString("SP", pre_regs.SP, post_regs.SP);
            reg_diff += CreateDiffString("PC", pre_regs.PC, post_regs.PC);
            _output.WriteLine(reg_diff);
            
            return cycles;
        }
    }
}
