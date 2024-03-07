﻿using System;
using System.Diagnostics;

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
            _fetchOps[(int)FetchType.RegH] = &FetchH;
            _fetchOps[(int)FetchType.RegL] = &FetchL;
            _fetchOps[(int)FetchType.AddressBC] = &FetchFromAddressBC;
            _fetchOps[(int)FetchType.AddressDE] = &FetchFromAddressDE;
            _fetchOps[(int)FetchType.AddressHL] = &FetchFromAddressHL;
            _fetchOps[(int)FetchType.AddressHL_Dec] = &FetchFromAddressHL_Dec;
            _fetchOps[(int)FetchType.AddressHL_Inc] = &FetchFromAddressHL_Inc;
            _fetchOps[(int)FetchType.ImmediateAddress] = &FetchFromImmediateAddress;
            _fetchOps[(int)FetchType.ImmediateValue] = &FetchFromImmediateValue;
            _fetchOps[(int)FetchType.Address_Immediate_0xFF00] = &FetchFromAddress_Immediate_0xFF00;

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
        private static (byte, ushort) FetchFromAddressHL_Dec(Processor processor) => processor.FetchFromAddress(*processor.Registers.HL--);
        private static (byte, ushort) FetchFromAddressHL_Inc(Processor processor) => processor.FetchFromAddress(*processor.Registers.HL++);
        private static (byte, ushort) FetchFromImmediateValue(Processor processor) => processor.FetchFromImmediateValue();
        private static (byte, ushort) FetchFromAddress_Immediate_0xFF00(Processor processor) => processor.FetchFrom_Immediate_0xFF00();
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
