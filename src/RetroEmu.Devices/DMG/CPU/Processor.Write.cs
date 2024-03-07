using System;
using System.Diagnostics;

namespace RetroEmu.Devices.DMG.CPU
{
    public unsafe partial class Processor
    {
        private void SetupWrite()
        {
            _writeOps[(int)WriteType.RegA] = &WriteA;
            _writeOps[(int)WriteType.RegB] = &WriteB;
            _writeOps[(int)WriteType.RegC] = &WriteC;
            _writeOps[(int)WriteType.RegD] = &WriteD;
            _writeOps[(int)WriteType.RegE] = &WriteE;
            _writeOps[(int)WriteType.RegH] = &WriteH;
            _writeOps[(int)WriteType.RegL] = &WriteL;
            _writeOps[(int)WriteType.AddressBC] = &WriteAddressBC;
            _writeOps[(int)WriteType.AddressDE] = &WriteAddressDE;
            _writeOps[(int)WriteType.AddressHL] = &WriteAddressHL;
            _writeOps[(int)WriteType.AddressHL_Dec] = &WriteAddressHL_Dec;
            _writeOps[(int)WriteType.AddressHL_Inc] = &WriteAddressHL_Inc;
            _writeOps[(int)WriteType.ImmediateAddress] = &WriteImmediateAddress;
            _writeOps[(int)WriteType.Address_RegC_0xFF00] = &WriteAddress_RegC_0xFF00;
            _writeOps[(int)WriteType.Address_Immediate_0xFF00] = &WriteAddress_Immediate_0xFF00;

            _writeOps[(int)WriteType.RegHL] = &WriteHL;
            _writeOps[(int)WriteType.RegSP] = &WriteSP;
            _writeOps[(int)WriteType.RegPC] = &WritePC;
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
