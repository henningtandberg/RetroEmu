namespace RetroEmu.Devices.Disassembly.Tokens;

public sealed record ImmediateOperandToken(string Value) : IOperandToken
{
    public static implicit operator string(ImmediateOperandToken registerOperandToken) => registerOperandToken.Value;
    public static implicit operator ImmediateOperandToken(string value) => new(value);
    public override string ToString() => Value;
}