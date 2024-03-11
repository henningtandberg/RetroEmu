using System;
using System.Diagnostics;
using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU
{
    public unsafe partial class Processor
    {
        internal (byte, ushort) PerformFetchOperation(FetchType fetchType)
        {
            return fetchType switch {
                FetchType.A => FetchValue(*Registers.A),
                FetchType.B => FetchValue(*Registers.B),
                FetchType.C => FetchValue(*Registers.C),
                FetchType.D => FetchValue(*Registers.D),
                FetchType.E => FetchValue(*Registers.E),
                FetchType.H => FetchValue(*Registers.H),
                FetchType.L => FetchValue(*Registers.L),
                FetchType.XBC => FetchFromAddress(*Registers.BC),
                FetchType.XDE => FetchFromAddress(*Registers.DE),
                FetchType.XHL => FetchFromAddress(*Registers.HL),
                FetchType.XHLD => FetchFromAddress(*Registers.HL--),
                FetchType.XHLI => FetchFromAddress(*Registers.HL++),
                FetchType.XN16 => FetchFromImmediateAddress(),
                FetchType.N8 => FetchFromImmediateValue(),
                FetchType.XN8 => FetchFromAddress_Immediate_0xFF00(),
                FetchType.XC => FetchFromAddress_RegC_0xFF00(),
                FetchType.BC => FetchValue16(*Registers.BC),
                FetchType.DE => FetchValue16(*Registers.DE),
                FetchType.HL => FetchValue16(*Registers.HL),
                FetchType.SP => FetchValue16(*Registers.SP),
                FetchType.N16 => FetchImmediateValue16(),
                _ => throw new NotImplementedException()
            };
        }

        private static (byte, ushort) FetchValue(byte value)
        {
            return (0, (ushort)value);
        }

        private (byte, ushort) FetchFromImmediateValue()
        {
            var value = GetNextOpcode();
            return (4, value);
        }

        private (byte, ushort) FetchFromAddress(ushort address)
        {
            var value = _memory.Read(address);
            return (4, (ushort)value);
        }

        private (byte, ushort) FetchFromImmediateAddress()
        {
            var addressLsb = GetNextOpcode();
            var addressMsb = GetNextOpcode();
            var address = (ushort)(((ushort)addressMsb << 8) | ((ushort)addressLsb));
            var value = _memory.Read(address);
            return (12, (ushort)value);
        }

        private (byte, ushort) FetchFromAddress_Immediate_0xFF00()
        {
            var im = GetNextOpcode();
            var address = 0xFF00 + im;
            var value = _memory.Read((ushort)address);
            return (8, (ushort)value);
        }
        
        private (byte, ushort) FetchFromAddress_RegC_0xFF00()
        {
            var address = 0xFF00 + *Registers.C;
            var value = _memory.Read((ushort)address);
            return (8, (ushort)value);
        }

        private static (byte, ushort) FetchValue16(ushort value)
        {
            return (0, value);
        }

        private (byte, ushort) FetchImmediateValue16()
        {
            var addressLsb = GetNextOpcode();
            var addressMsb = GetNextOpcode();
            var value = (ushort)(((ushort)addressMsb << 8) | ((ushort)addressLsb));
            return (8, (ushort)value);
        }
    }
}
