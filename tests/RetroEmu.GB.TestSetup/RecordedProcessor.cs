using RetroEmu.Devices.DMG;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.Devices.DMG.CPU.Interrupts;
using RetroEmu.Devices.DMG.CPU.Link;
using RetroEmu.Devices.DMG.CPU.PPU;
using RetroEmu.Devices.DMG.CPU.Timing;

namespace RetroEmu.GB.TestSetup
{
    public class RecordedProcessor : Processor
    {
        private IAddressBus _addressBus;
        public RecordedProcessor(IAddressBus addressBus, ITimer timer, IPixelProcessingUnit pixelProcessingUnit, IInterruptState interruptState, ISerial serial, IJoypad joypad)
            : base(addressBus, timer, pixelProcessingUnit, interruptState, serial, joypad)
        {
            _addressBus = addressBus;
        }

        private static string CreateDiffString(string prefix, byte pre_reg, byte post_reg)
        {
            if (pre_reg != post_reg)
                return prefix + "(" + (pre_reg) + " -> " + (post_reg) + ") ";
            return "";
        }
        private static string CreateDiffString(string prefix, ushort pre_reg, ushort post_reg)
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

            var opcode = _addressBus.Read(pre_regs.PC);
            //_output.WriteLine(((Opcode.OpcodeEnum)opcode).ToString());

            var regDiff = "";
            regDiff += CreateDiffString("A", pre_regs.A, post_regs.A);
            regDiff += CreateDiffString("F", pre_regs.F, post_regs.F);
            regDiff += CreateDiffString("B", pre_regs.B, post_regs.B);
            regDiff += CreateDiffString("C", pre_regs.C, post_regs.C);
            regDiff += CreateDiffString("D", pre_regs.D, post_regs.D);
            regDiff += CreateDiffString("E", pre_regs.E, post_regs.E);
            regDiff += CreateDiffString("H", pre_regs.H, post_regs.H);
            regDiff += CreateDiffString("L", pre_regs.L, post_regs.L);
            regDiff += CreateDiffString("SP", pre_regs.SP, post_regs.SP);
            regDiff += CreateDiffString("PC", pre_regs.PC, post_regs.PC);
            //_output.WriteLine(regDiff);
            
            return cycles;
        }
    }
}
