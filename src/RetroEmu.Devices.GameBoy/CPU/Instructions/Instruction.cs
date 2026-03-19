namespace RetroEmu.Devices.GameBoy.CPU.Instructions;

internal record Instruction(WriteType WriteType, OpType OpType, FetchType FetchType);