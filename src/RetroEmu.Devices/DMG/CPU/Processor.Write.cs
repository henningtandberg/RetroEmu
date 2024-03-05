using System;

namespace RetroEmu.Devices.DMG.CPU
{
    public unsafe partial class Processor
    {
        private void SetupWrite()
        {
            _writeOps[(int)WriteType.RegA] = &WriteA;

            _writeOps[(int)WriteType.RegHL] = &WriteHL;
            _writeOps[(int)WriteType.RegSP] = &WriteSP;
        }

        private static byte WriteA(Processor processor, ushort value) => processor.WriteValue(processor.Registers.A, (byte)value);
        private static byte WriteHL(Processor processor, ushort value) => processor.WriteValue16(processor.Registers.HL, value);
        private static byte WriteSP(Processor processor, ushort value) => processor.WriteValue16(processor.Registers.SP, value);

        private byte WriteValue(byte* dst, byte value)
        {
            *dst = value;
            return 0; // cycles
        }

        private byte WriteValue16(ushort* dst, ushort value)
        {
            *dst = value;
            return 0; // cycles
        }
    }
}
