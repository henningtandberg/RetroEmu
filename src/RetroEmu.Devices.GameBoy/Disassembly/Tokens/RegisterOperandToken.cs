namespace RetroEmu.Devices.GameBoy.Disassembly.Tokens;

public sealed record RegisterOperandToken(string Value) : IOperandToken
{
    public static implicit operator string(RegisterOperandToken registerOperandToken) => registerOperandToken.Value;
    public static implicit operator RegisterOperandToken(string value) => new(value);
    public override string ToString() => Value;
}