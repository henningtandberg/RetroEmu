namespace RetroEmu.Devices.Disassembly.Tokens;

internal sealed record OpcodeToken(string Value) : IOpcodeToken
{
    public static implicit operator string(OpcodeToken operandToken) => operandToken.Value;
    public static implicit operator OpcodeToken(string value) => new(value);
    public override string ToString() => Value;
}