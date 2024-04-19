using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU
{
    public partial class Processor
    {
        private void SetupPushInstructions()
        {
            _instructions[Opcode.Push_AF] = new LoadInstruction(WriteType.Push, FetchType.AF);
            _instructions[Opcode.Push_BC] = new LoadInstruction(WriteType.Push, FetchType.BC);
            _instructions[Opcode.Push_DE] = new LoadInstruction(WriteType.Push, FetchType.DE);
            _instructions[Opcode.Push_HL] = new LoadInstruction(WriteType.Push, FetchType.HL);
        }
    }
}