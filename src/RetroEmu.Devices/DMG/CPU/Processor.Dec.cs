using System;

namespace RetroEmu.Devices.DMG.CPU
{
    public unsafe partial class Processor
    {
        private void SetupDecInstructions()
        {
            _ops[(int)OpType.Dec] = &Dec;

            _instructions[OPC.Dec_A] = new Instruction(WriteType.RegA, OpType.Dec, FetchType.RegA);
            _instructions[OPC.Dec_B] = new Instruction(WriteType.RegB, OpType.Dec, FetchType.RegB);
            _instructions[OPC.Dec_C] = new Instruction(WriteType.RegC, OpType.Dec, FetchType.RegC);
            _instructions[OPC.Dec_D] = new Instruction(WriteType.RegD, OpType.Dec, FetchType.RegD);
            _instructions[OPC.Dec_E] = new Instruction(WriteType.RegE, OpType.Dec, FetchType.RegE);
            _instructions[OPC.Dec_H] = new Instruction(WriteType.RegH, OpType.Dec, FetchType.RegH);
            _instructions[OPC.Dec_L] = new Instruction(WriteType.RegL, OpType.Dec, FetchType.RegL);
            _instructions[OPC.Dec_XHL] = new Instruction(WriteType.AddressHL, OpType.Dec, FetchType.AddressHL);
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