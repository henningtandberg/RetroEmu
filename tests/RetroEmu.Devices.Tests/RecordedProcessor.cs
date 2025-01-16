using RetroEmu.Devices.DMG;
using RetroEmu.Devices.DMG.CPU;
using Xunit.Abstractions;

namespace RetroEmu.Devices.Tests
{
    public class RecordedProcessor : Processor
    {
        private IMemory _memory;
        private ITestOutputHelper _output;
        public RecordedProcessor(IMemory memory, ITimer timer, ITestOutputHelper output) : base(memory, timer)
        {
            _memory = memory;
            _output = output;
        }

        private static string CreateDiffString(string prefix, ref byte pre_reg, ref byte post_reg)
        {
            if (pre_reg != post_reg)
                return prefix + "(" + (pre_reg) + " -> " + (post_reg) + ") ";
            return "";
        }
        private static string CreateDiffString(string prefix, ref ushort pre_reg, ref ushort post_reg)
        {
            if (pre_reg != post_reg)
                return prefix + "(" + (pre_reg) + " -> " + (post_reg) + ") ";
            return "";
        }

        public new int Update()
        {
            var pre_regs = Registers;
            var cycles = base.Update();
            var post_regs = Registers;

            var opcode = _memory.Read(pre_regs.PC);
            _output.WriteLine(((Opcode.OpcodeEnum)opcode).ToString());

            var regDiff = "";
            regDiff += CreateDiffString("A", ref pre_regs.A, ref post_regs.A);
            regDiff += CreateDiffString("F", ref pre_regs.F, ref post_regs.F);
            regDiff += CreateDiffString("B", ref pre_regs.B, ref post_regs.B);
            regDiff += CreateDiffString("C", ref pre_regs.C, ref post_regs.C);
            regDiff += CreateDiffString("D", ref pre_regs.D, ref post_regs.D);
            regDiff += CreateDiffString("E", ref pre_regs.E, ref post_regs.E);
            regDiff += CreateDiffString("H", ref pre_regs.H, ref post_regs.H);
            regDiff += CreateDiffString("L", ref pre_regs.L, ref post_regs.L);
            regDiff += CreateDiffString("SP", ref pre_regs.SP, ref post_regs.SP);
            regDiff += CreateDiffString("PC", ref pre_regs.PC, ref post_regs.PC);
            _output.WriteLine(regDiff);
            
            return cycles;
        }
    }
}
