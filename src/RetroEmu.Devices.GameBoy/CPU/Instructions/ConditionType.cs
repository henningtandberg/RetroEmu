namespace RetroEmu.Devices.GameBoy.CPU.Instructions;

internal enum ConditionType : byte
{
    Always,
    Z,
    NZ,
    C,
    NC,
}
