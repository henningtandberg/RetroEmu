using System;
using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU;

public partial class Processor
{
    internal bool PerformConditionOperation(ConditionType conditionType)
    {
        return conditionType switch {
            ConditionType.Always => true,
            ConditionType.Z => IsSet(Flag.Zero),
            ConditionType.C => IsSet(Flag.Carry),
            ConditionType.NZ => !IsSet(Flag.Zero),
            ConditionType.NC => !IsSet(Flag.Carry),
            _ => throw new NotImplementedException()
        };
    }
}