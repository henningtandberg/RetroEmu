namespace RetroEmu.Devices.Disassembly.Tokens;

public sealed record EmptyOperandToken : IOperandToken
{
    public string Value => string.Empty;
}