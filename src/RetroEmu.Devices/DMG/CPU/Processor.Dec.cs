using System;

namespace RetroEmu.Devices.DMG.CPU
{
    public unsafe partial class Processor
    {
        private void SetupDecInstructions()
        {
            _ops[(int)OpType.Dec] = &Dec;

            _instructions[OPC.Dec_A] = new Instruction(FetchType.RegA, OpType.Dec, WriteType.RegA);
            _instructions[OPC.Dec_B] = new Instruction(FetchType.RegB, OpType.Dec, WriteType.RegB);
            _instructions[OPC.Dec_C] = new Instruction(FetchType.RegC, OpType.Dec, WriteType.RegC);
            _instructions[OPC.Dec_D] = new Instruction(FetchType.RegD, OpType.Dec, WriteType.RegD);
            _instructions[OPC.Dec_E] = new Instruction(FetchType.RegE, OpType.Dec, WriteType.RegE);
            _instructions[OPC.Dec_H] = new Instruction(FetchType.RegH, OpType.Dec, WriteType.RegH);
            _instructions[OPC.Dec_L] = new Instruction(FetchType.RegL, OpType.Dec, WriteType.RegL);
            _instructions[OPC.Dec_XHL] = new Instruction(FetchType.AddressHL, OpType.Dec, WriteType.AddressHL);
        }

        private static (byte, ushort) Dec(Processor processor, ushort value)
        {
            var result = (int)value - 1;

            if (result > 0xFF)
            {
                processor.SetFlag(Flag.Carry);
            }
            else
            {
                processor.ClearFlag(Flag.Carry);
            }

            if (result == 0x0F)
            {
                processor.SetFlag(Flag.HalfCarry);
            }
            else
            {
                processor.ClearFlag(Flag.HalfCarry);
            }

            processor.SetFlag(Flag.Subtract);

            if (result == 0)
            {
                processor.SetFlag(Flag.Zero);
            }
            else
            {
                processor.ClearFlag(Flag.Zero);
            }

            return (4, (ushort)result); // cycles
        }
    }
}