using System;

namespace RetroEmu.Devices.DMG;

internal readonly record struct InternalRam() : IInternalRam, IDebugInternalRam
{
    private readonly byte[] _workRam = new byte[0xA000 - 0x8000];
    private readonly byte[] _highRam = new byte[0xFFFF - 0xFF80];

    public void Reset()
    {
        Array.Clear(_workRam, 0, _workRam.Length);
        Array.Clear(_highRam, 0, _highRam.Length);
    }

    public byte Read(ushort address) => address switch
    {
        > 0xBFFF and <= 0xDFFF => _workRam[address - 0xC000],
        > 0xDFFF and <= 0xFDFF => _workRam[address - 0xC000],
        > 0xFF7F and <= 0xFFFE => _highRam[address - 0xFF80],
        _ => throw new IndexOutOfRangeException($"No valid internal RAM at address {address}!")
    };

    public void Write(ushort address, byte value)
    {
        switch (address)
        {
            case > 0xBFFF and <= 0xDFFF:
                _workRam[address - 0xC000] = value;
                break;
            case > 0xDFFF and <= 0xFDFF:
                _workRam[address - 0xC000] = value;
                break;
            case > 0xFF7F and <= 0xFFFE:
                _highRam[address - 0xFF80] = value;
                break;
            default:
                throw new IndexOutOfRangeException($"No valid internal RAM at address {address}!");
        }
    }

    #region DebugFeatures
    public byte[] GetWorkRam() => _workRam;
    public byte[] GetHighRam() => _highRam;

    #endregion
}