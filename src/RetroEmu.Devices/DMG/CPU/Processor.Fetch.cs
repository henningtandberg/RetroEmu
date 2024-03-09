using System;
using System.Diagnostics;

namespace RetroEmu.Devices.DMG.CPU
{
    public unsafe partial class Processor
    {
        private void SetupFetch()
        {
            _fetchOps[(int)FetchType.A] = &FetchA;
            _fetchOps[(int)FetchType.B] = &FetchB;
            _fetchOps[(int)FetchType.C] = &FetchC;
            _fetchOps[(int)FetchType.D] = &FetchD;
            _fetchOps[(int)FetchType.E] = &FetchE;
            _fetchOps[(int)FetchType.H] = &FetchH;
            _fetchOps[(int)FetchType.L] = &FetchL;
            _fetchOps[(int)FetchType.XBC] = &FetchFromAddressBC;
            _fetchOps[(int)FetchType.XDE] = &FetchFromAddressDE;
            _fetchOps[(int)FetchType.XHL] = &FetchFromAddressHL;
            _fetchOps[(int)FetchType.XHLD] = &FetchFromAddressHL_Dec;
            _fetchOps[(int)FetchType.XHLI] = &FetchFromAddressHL_Inc;
            _fetchOps[(int)FetchType.XN16] = &FetchFromImmediateAddress;
            _fetchOps[(int)FetchType.N8] = &FetchFromImmediateValue;
            _fetchOps[(int)FetchType.XN8] = &FetchFromAddress_Immediate_0xFF00;
            _fetchOps[(int)FetchType.XC] = &FetchFromAddress_RegC_0xFF00; // TODO: Better name?

            _fetchOps[(int)FetchType.BC] = &FetchBC;
            _fetchOps[(int)FetchType.DE] = &FetchDE;
            _fetchOps[(int)FetchType.HL] = &FetchHL;
            _fetchOps[(int)FetchType.SP] = &FetchSP;
            _fetchOps[(int)FetchType.N16] = &FetchImmediateValue16;
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
        private static (byte, ushort) FetchFromAddressHL_Dec(Processor processor) => processor.FetchFromAddress(*processor.Registers.HL--);
        private static (byte, ushort) FetchFromAddressHL_Inc(Processor processor) => processor.FetchFromAddress(*processor.Registers.HL++);
        private static (byte, ushort) FetchFromImmediateValue(Processor processor) => processor.FetchFromImmediateValue();
        private static (byte, ushort) FetchFromAddress_Immediate_0xFF00(Processor processor) => processor.FetchFrom_Immediate_0xFF00();
        private static (byte, ushort) FetchFromAddress_RegC_0xFF00(Processor processor) => processor.FetchFrom_RegC_0xFF00();
        private static (byte, ushort) FetchBC(Processor processor) => processor.FetchValue16(*processor.Registers.BC);
        private static (byte, ushort) FetchDE(Processor processor) => processor.FetchValue16(*processor.Registers.DE);
        private static (byte, ushort) FetchHL(Processor processor) => processor.FetchValue16(*processor.Registers.HL);
        private static (byte, ushort) FetchSP(Processor processor) => processor.FetchValue16(*processor.Registers.SP);

        private (byte, ushort) FetchValue(byte value)
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

        private static (byte, ushort) FetchFromImmediateAddress(Processor processor)
        {
            var addressLsb = processor.GetNextOpcode();
            var addressMsb = processor.GetNextOpcode();
            var address = (ushort)(((ushort)addressMsb << 8) | ((ushort)addressLsb));
            var value = processor._memory.Read(address);
            return (12, (ushort)value);
        }

        private (byte, ushort) FetchFrom_Immediate_0xFF00()
        {
            var im = GetNextOpcode();
            var address = 0xFF00 + im;
            var value = _memory.Read((ushort)address);
            return (8, (ushort)value);
        }
        
        private (byte, ushort) FetchFrom_RegC_0xFF00()
        {
            var address = 0xFF00 + *Registers.C;
            var value = _memory.Read((ushort)address);
            return (8, (ushort)value);
        }

        private (byte, ushort) FetchValue16(ushort value)
        {
            return (0, value);
        }

        private static (byte, ushort) FetchImmediateValue16(Processor processor)
        {
            var addressLsb = processor.GetNextOpcode();
            var addressMsb = processor.GetNextOpcode();
            var value = (ushort)(((ushort)addressMsb << 8) | ((ushort)addressLsb));
            return (8, (ushort)value);
        }
    }
}
