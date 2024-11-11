namespace RetroEmu.Devices.DMG.CPU.Instructions;

internal record Instruction(WriteType WriteType, OpType OpType, FetchType FetchType);