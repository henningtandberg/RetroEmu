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

            _fetchOps[(int)FetchType.RegBC] = &FetchBC;
            _fetchOps[(int)FetchType.RegDE] = &FetchDE;
            _fetchOps[(int)FetchType.RegHL] = &FetchHL;
            _fetchOps[(int)FetchType.RegSP] = &FetchSP;
            _fetchOps[(int)FetchType.ImmediateValue16] = &FetchImmediateValue16;
        }

        private static (byte, ushort) FetchA(Processor processor) => processor.FetchValue(*processor.Registers.A);
        private static (byte, ushort) FetchB(Processor processor) => processor.FetchValue(*processor.Registers.B);
        private static (byte, ushort) FetchC(Processor processor) => processor.FetchValue(*processor.Registers.C);
        private static (byte, ushort) FetchD(Processor processor) => processor.FetchValue(*processor.Registers.D);
        private static (byte, ushort) FetchE(Processor processor) => processor.FetchValue(*processor.Registers.E);
        private static (byte, ushort) FetchH(Processor processor) => processor.FetchValue(*processor.Registers.H);
        private static (byte, ushort) FetchL(Processor processor) => processor.FetchValue(*processor.Registers.L);
        private static (byte, ushort) FetchFromAddressBC(Processor processor) => processor.FetchFromAddress(*processor.Registers.BC);
        private static (byte, ushort) FetchFromAddressDE(Processor processor) => processor.FetchFromAddress(*processor.Registers.DE);
        private static (byte, ushort) FetchFromAddressHL(Processor processor) => processor.FetchFromAddress(*processor.Registers.HL);
        private static (byte, ushort) FetchFromImmediateValue(Processor processor) => processor.FetchFromAddress(processor.GetNextOpcode());
        private static (byte, ushort) FetchBC(Processor processor) => processor.FetchValue16(*processor.Registers.BC);
        private static (byte, ushort) FetchDE(Processor processor) => processor.FetchValue16(*processor.Registers.DE);
        private static (byte, ushort) FetchHL(Processor processor) => processor.FetchValue16(*processor.Registers.HL);
        private static (byte, ushort) FetchSP(Processor processor) => processor.FetchValue16(*processor.Registers.SP);

        private (byte, ushort) FetchValue(byte value)
        {
            return (0, (ushort)value);
        }

        private (byte, ushort) FetchFromAddress(ushort address)
        {
            var value = _memory.Get(address);
            return (4, (ushort)value);
        }

        private static (byte, ushort) FetchFromImmediateAddress(Processor processor)
        {
            var value_hi = processor.GetNextOpcode();
            var value_lo = processor.GetNextOpcode();
            var address = (ushort)(((ushort)value_hi << 8) | ((ushort)value_lo));
            var value = processor._memory.Get(address);
            return (12, (ushort)value);
        }

        private (byte, ushort) FetchValue16(ushort value)
        {
            return (0, value);
        }

        private static (byte, ushort) FetchImmediateValue16(Processor processor)
        {
            var value_hi = processor.GetNextOpcode();
            var value_lo = processor.GetNextOpcode();
            var value = (ushort)(((ushort)value_hi << 8) | ((ushort)value_lo));
            return (8, (ushort)value);
        }
    }
}
