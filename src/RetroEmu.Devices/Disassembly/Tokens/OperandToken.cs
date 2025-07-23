namespace RetroEmu.Devices.Disassembly.Tokens;

public sealed record OperandToken(string Value) : IOperandToken
{
    public static implicit operator string(OperandToken operandToken) => operandToken.Value;
    public static implicit operator OperandToken(string value) => new(value);
    public override string ToString() => Value;
}