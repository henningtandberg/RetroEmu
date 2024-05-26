using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU
{
    public unsafe partial class Processor
    {
        private void SetupRestartInstructions()
        {
            _instructions[Opcode.Rst_00H] = new RestartInstruction(0x00);
            _instructions[Opcode.Rst_08H] = new RestartInstruction(0x08);
            _instructions[Opcode.Rst_10H] = new RestartInstruction(0x10);
            _instructions[Opcode.Rst_18H] = new RestartInstruction(0x18);
            _instructions[Opcode.Rst_20H] = new RestartInstruction(0x20);
            _instructions[Opcode.Rst_28H] = new RestartInstruction(0x28);
            _instructions[Opcode.Rst_30H] = new RestartInstruction(0x30);
            _instructions[Opcode.Rst_38H] = new RestartInstruction(0x38);
        }

        public (ushort, ushort) RestartAtGivenAddress(ushort address)
        {
            Push16ToStack(*Registers.PC);
            return new(address, 32);
        }
    }
}