using System;
using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.Disassembly;

internal static class FetchWriteComparer
{
    internal static bool Equals(this (WriteType, FetchType) combination) => combination switch
    {
        (WriteType.A, FetchType.A) => true,
        (WriteType.B, FetchType.B) => true,
        (WriteType.C, FetchType.C) => true,
        (WriteType.D, FetchType.D) => true,
        (WriteType.E, FetchType.E) => true,
        (WriteType.H, FetchType.H) => true,
        (WriteType.L, FetchType.L) => true,
        (WriteType.AF, FetchType.AF) => true,
        (WriteType.BC, FetchType.BC) => true,
        (WriteType.DE, FetchType.DE) => true,
        (WriteType.HL, FetchType.HL) => true,
        (WriteType.PC, FetchType.PC) => true,
        (WriteType.SP, FetchType.SP) => true,
        (WriteType.XBC, FetchType.XBC) => true,
        (WriteType.XDE, FetchType.XDE) => true,
        (WriteType.XHL, FetchType.XHL) => true,
        (WriteType.XHLD, FetchType.XHLD) => true,
        (WriteType.XHLI, FetchType.XHLI) => true,
        (WriteType.XN8, FetchType.XN8) => true,
        (WriteType.XN16, FetchType.XN16) => true,
        _ => false
    };
}