using System;

namespace RetroEmu.Devices.DMG.CPU
{
    public unsafe partial class Processor
    {
        private void SetupFetch()
        {
            _fetchOps[(int)FetchType.RegA] = &FetchA;
            _fetchOps[(int)FetchType.RegB] = &FetchB;
            _fetchOps[(int)FetchType.RegC] = &FetchC;
            _fetchOps[(int)FetchType.RegD] = &FetchD;
            _fetchOps[(int)FetchType.RegE] = &FetchE;
            _fetchOps[(int)FetchType.RegH] = &FetchL;
            _fetchOps[(int)FetchType.AddressBC] = &FetchFromAddressBC;
            _fetchOps[(int)FetchType.AddressDE] = &FetchFromAddressDE;
            _fetchOps[(int)FetchType.AddressHL] = &FetchFromAddressHL;
            _fetchOps[(int)FetchType.ImmediateAddress] = &FetchFromImmediateAddress;
            _fetchOps[(int)FetchType.ImmediateValue] = &FetchFromImmediateValue;
        }

        private static (byte, byte) FetchA(Processor processor) => processor.FetchValue(*processor.Registers.A);
        private static (byte, byte) FetchB(Processor processor) => processor.FetchValue(*processor.Registers.B);
        private static (byte, byte) FetchC(Processor processor) => processor.FetchValue(*processor.Registers.C);
        private static (byte, byte) FetchD(Processor processor) => processor.FetchValue(*processor.Registers.D);
        private static (byte, byte) FetchE(Processor processor) => processor.FetchValue(*processor.Registers.E);
        private static (byte, byte) FetchH(Processor processor) => processor.FetchValue(*processor.Registers.H);
        private static (byte, byte) FetchL(Processor processor) => processor.FetchValue(*processor.Registers.L);
        private static (byte, byte) FetchFromAddressBC(Processor processor) => processor.FetchFromAddress(*processor.Registers.BC);
        private static (byte, byte) FetchFromAddressDE(Processor processor) => processor.FetchFromAddress(*processor.Registers.DE);
        private static (byte, byte) FetchFromAddressHL(Processor processor) => processor.FetchFromAddress(*processor.Registers.HL);
        private static (byte, byte) FetchFromImmediateValue(Processor processor) => processor.FetchFromAddress(processor.GetNextOpcode());

        private (byte, byte) FetchValue(byte value)
        {
            return (0, value);
        }

        private (byte, byte) FetchFromAddress(ushort address)
        {
            var value = _memory.Get(address);
            return (4, value);
        }

        private static (byte, byte) FetchFromImmediateAddress(Processor processor)
        {
            var value_hi = processor.GetNextOpcode();
            var value_lo = processor.GetNextOpcode();
            var address = (ushort)(((ushort)value_hi << 8) | ((ushort)value_lo));
            var value = processor._memory.Get(address);
            return (12, value);
        }
    }
}
