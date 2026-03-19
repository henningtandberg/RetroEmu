namespace RetroEmu.Devices.GameBoy.Disassembly.Tokens;

public sealed record ConditionOperandToken(string Value) : IOperandToken
{
    public static implicit operator string(ConditionOperandToken operandToken) => operandToken.Value;
    public static implicit operator ConditionOperandToken(string value) => new(value);
    public override string ToString() => Value;
}