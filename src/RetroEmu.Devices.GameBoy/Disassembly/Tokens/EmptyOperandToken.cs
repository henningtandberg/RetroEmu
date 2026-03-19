namespace RetroEmu.Devices.GameBoy.Disassembly.Tokens;

public sealed record EmptyOperandToken : IOperandToken
{
    public string Value => string.Empty;
    public static implicit operator string(EmptyOperandToken operandToken) => operandToken.Value;
    public override string ToString() => Value;
}