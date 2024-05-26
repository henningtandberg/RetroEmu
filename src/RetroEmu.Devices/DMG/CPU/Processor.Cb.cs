using System;
using System.Diagnostics;
using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU
{
    public unsafe partial class Processor
    {
        private void SetupCbInstruction()
        {
            _instructions[Opcode.Pre_CB] = new CBInstruction();
        }

        internal (byte, ushort) PerformCBOperation(CBType cbType, ushort fetchValue)
        {
            return cbType switch {
                CBType.BIT0 => Bit(fetchValue, 0),
                CBType.BIT1 => Bit(fetchValue, 1),
                CBType.BIT2 => Bit(fetchValue, 2),
                CBType.BIT3 => Bit(fetchValue, 3),
                CBType.BIT4 => Bit(fetchValue, 4),
                CBType.BIT5 => Bit(fetchValue, 5),
                CBType.BIT6 => Bit(fetchValue, 6),
                CBType.BIT7 => Bit(fetchValue, 7),
                _ => throw new NotImplementedException()
            };
        }

        private (byte, ushort) Bit(ushort fetchValue, byte bit)
        {
            byte b = (byte)((fetchValue >> bit) & 0x01);

            if (b == 0)
            {
                SetFlag(Flag.Zero);
            }
            else
            {
                ClearFlag(Flag.Zero);
            }
            ClearFlag(Flag.Subtract);
            SetFlag(Flag.HalfCarry);

            return (4, fetchValue);
        }
    }
}
