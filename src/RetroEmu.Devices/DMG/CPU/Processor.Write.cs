using System;
using System.Diagnostics;

namespace RetroEmu.Devices.DMG.CPU
{
    public unsafe partial class Processor
    {
        private void SetupWrite()
        {
            _writeOps[(int)WriteType.A] = &WriteA;
            _writeOps[(int)WriteType.B] = &WriteB;
            _writeOps[(int)WriteType.C] = &WriteC;
            _writeOps[(int)WriteType.D] = &WriteD;
            _writeOps[(int)WriteType.E] = &WriteE;
            _writeOps[(int)WriteType.H] = &WriteH;
            _writeOps[(int)WriteType.L] = &WriteL;
            _writeOps[(int)WriteType.XBC] = &WriteAddressBC;
            _writeOps[(int)WriteType.XDE] = &WriteAddressDE;
            _writeOps[(int)WriteType.XHL] = &WriteAddressHL;
            _writeOps[(int)WriteType.XHLD] = &WriteAddressHL_Dec;
            _writeOps[(int)WriteType.XHLI] = &WriteAddressHL_Inc;
            _writeOps[(int)WriteType.XN16] = &WriteImmediateAddress;
            _writeOps[(int)WriteType.XC] = &WriteAddress_RegC_0xFF00;
            _writeOps[(int)WriteType.XN8] = &WriteAddress_Immediate_0xFF00;

            _writeOps[(int)WriteType.HL] = &WriteHL;
            _writeOps[(int)WriteType.SP] = &WriteSP;
            _writeOps[(int)WriteType.PC] = &WritePC;
        }

        private static byte WriteA(Processor processor, ushort value) => processor.WriteValue(processor.Registers.A, (byte)value);
        private static byte WriteB(Processor processor, ushort value) => processor.WriteValue(processor.Registers.B, (byte)value);
        private static byte WriteC(Processor processor, ushort value) => processor.WriteValue(processor.Registers.C, (byte)value);
        private static byte WriteD(Processor processor, ushort value) => processor.WriteValue(processor.Registers.D, (byte)value);
        private static byte WriteE(Processor processor, ushort value) => processor.WriteValue(processor.Registers.E, (byte)value);
        private static byte WriteH(Processor processor, ushort value) => processor.WriteValue(processor.Registers.H, (byte)value);
        private static byte WriteL(Processor processor, ushort value) => processor.WriteValue(processor.Registers.L, (byte)value);
        private static byte WriteAddressBC(Processor processor, ushort value) => processor.WriteAtAddress(*processor.Registers.BC, (byte)value);
        private static byte WriteAddressDE(Processor processor, ushort value) => processor.WriteAtAddress(*processor.Registers.DE, (byte)value);
        private static byte WriteAddressHL(Processor processor, ushort value) => processor.WriteAtAddress(*processor.Registers.HL, (byte)value);
        private static byte WriteAddressHL_Dec(Processor processor, ushort value) => processor.WriteAtAddress(*processor.Registers.HL--, (byte)value);
        private static byte WriteAddressHL_Inc(Processor processor, ushort value) => processor.WriteAtAddress(*processor.Registers.HL++, (byte)value);
        private static byte WriteImmediateAddress(Processor processor, ushort value) => processor.WriteAtImmediateAddress((byte)value);
        private static byte WriteAddress_RegC_0xFF00(Processor processor, ushort value) => processor.WriteAtAddress((ushort)(0xFF00 + *processor.Registers.C), (byte)value);
        private static byte WriteAddress_Immediate_0xFF00(Processor processor, ushort value) => processor.WriteAtImmediateAddress_Immediate_0xFF00((byte)value);
        private static byte WriteHL(Processor processor, ushort value) => processor.WriteValue16(processor.Registers.HL, value);
        private static byte WriteSP(Processor processor, ushort value) => processor.WriteValue16(processor.Registers.SP, value);
        private static byte WritePC(Processor processor, ushort value) => processor.WriteValue16(processor.Registers.PC, value);

        private byte WriteValue(byte* dst, byte value)
        {
            *dst = value;
            return 0; // cycles
        }
        private byte WriteAtAddress(ushort address, byte value)
        {
            _memory.Write(address, value);
            return 4; // cycles
        }

        private byte WriteAtImmediateAddress(byte value)
        {
            var addressLsb = GetNextOpcode();
            var addressMsb = GetNextOpcode();
            var address = (ushort)(((ushort)addressMsb << 8) | ((ushort)addressLsb));
            _memory.Write(address, value);
            return 12; // cycles
        }

        private byte WriteAtImmediateAddress_Immediate_0xFF00(byte value)
        {
            var im = GetNextOpcode();
            var address = (int)im + (int)0xFF00;
            _memory.Write((ushort)address, value);
            return 8; // cycles
        }

        private byte WriteValue16(ushort* dst, ushort value)
        {
            *dst = value;
            return 0; // cycles
        }

    }
}
