using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU
{
    public partial class Processor
    {
        private void SetupPopInstructions()
        {
            _instructions[Opcode.Pop_AF] = new LoadInstruction(WriteType.AF, FetchType.Pop);
            _instructions[Opcode.Pop_BC] = new LoadInstruction(WriteType.BC, FetchType.Pop);
            _instructions[Opcode.Pop_DE] = new LoadInstruction(WriteType.DE, FetchType.Pop);
            _instructions[Opcode.Pop_HL] = new LoadInstruction(WriteType.HL, FetchType.Pop);
        }
    }
}