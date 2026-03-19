namespace RetroEmu.Devices.DMG.CPU.Instructions;

internal readonly record struct Instruction(WriteType WriteType, OpType OpType, FetchType FetchType);