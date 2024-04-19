using System;
using System.Diagnostics;
using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU
{
    public unsafe partial class Processor
    {
        internal byte PerformWriteOperation(WriteType writeType, ushort value)
        {
            return writeType switch {
                WriteType.A => WriteValue(Registers.A, (byte)value),
                WriteType.B => WriteValue(Registers.B, (byte)value),
                WriteType.C => WriteValue(Registers.C, (byte)value),
                WriteType.D => WriteValue(Registers.D, (byte)value),
                WriteType.E => WriteValue(Registers.E, (byte)value),
                WriteType.H => WriteValue(Registers.H, (byte)value),
                WriteType.L => WriteValue(Registers.L, (byte)value),
                WriteType.XBC => WriteAtAddress(*Registers.BC, (byte)value),
                WriteType.XDE => WriteAtAddress(*Registers.DE, (byte)value),
                WriteType.XHL => WriteAtAddress(*Registers.HL, (byte)value),
                WriteType.XHLD => WriteAtAddress(*Registers.HL--, (byte)value),
                WriteType.XHLI => WriteAtAddress(*Registers.HL++, (byte)value),
                WriteType.XN16 => WriteAtImmediateAddress((byte)value),
                WriteType.XC => WriteAtAddress_RegC_0xFF00((byte)value),
                WriteType.XN8 => WriteAtImmediateAddress_Immediate_0xFF00((byte)value),
                WriteType.HL => WriteValue16(Registers.HL, value),
                WriteType.SP => WriteValue16(Registers.SP, value),
                WriteType.PC => WriteValue16(Registers.PC, value),
                WriteType.None => 0,
                _ => throw new NotImplementedException()
            };
        }

        private static byte WriteValue(byte* dst, byte value)
        {
            *dst = value;
            return 0;
        }
        
        private byte WriteAtAddress(ushort address, byte value)
        {
            _memory.Write(address, value);
            return 4;
        }

        private byte WriteAtImmediateAddress(byte value)
        {
            var addressLsb = GetNextOpcode();
            var addressMsb = GetNextOpcode();
            var address = (ushort)(((ushort)addressMsb << 8) | ((ushort)addressLsb));
            _memory.Write(address, value);
            return 12;
        }

        private byte WriteAtImmediateAddress_Immediate_0xFF00(byte value)
        {
            var im = GetNextOpcode();
            var address = (int)im + (int)0xFF00;
            _memory.Write((ushort)address, value);
            return 8;
        }
        
        private byte WriteAtAddress_RegC_0xFF00(byte value)
        { 
            return WriteAtAddress((ushort)(0xFF00 + *Registers.C), value);
        }

        private static byte WriteValue16(ushort* dst, ushort value)
        {
            *dst = value;
            return 0;
        }

    }
}
