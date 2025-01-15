namespace RetroEmu.Devices.DMG.CPU;

public partial class Processor {
    // Push works the same way as Ld, it is handled by the ReadType and WriteType
    private (ushort, ushort) Push(ushort input) {
        return (input, 4);
    }
}