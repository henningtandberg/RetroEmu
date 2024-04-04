using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU
{
    public partial class Processor
    {
        private void SetupNopInstruction()
        {
            _instructions[Opcode.Nop] = new NopInstruction();
        }
    }
}
