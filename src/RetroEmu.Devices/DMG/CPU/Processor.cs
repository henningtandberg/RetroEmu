using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU;

public unsafe partial class Processor(IMemory memory) : IProcessor
{
    private readonly Instruction[] _instructions = InstructionTableFactory.Create();

    public Registers Registers { get; } = new();
    public int Cycles { get; set; }

    public void Reset()
    {
    }

    public int Update()
    {
        var opcode = GetNextOpcode();
        var instr = _instructions[opcode];

        return instr.OpType == OpType.PreCb
            ? ExecuteCbInstruction()
            : ExecuteInstruction(instr);
    }

    private int ExecuteInstruction(Instruction instruction)
    {
        var (fetchCycles, fetchResult) = PerformFetchOperation(instruction.FetchType);
        var (opResult, opCycles) = PerformOperation(instruction.OpType, fetchResult);
        var writeCycles = PerformWriteOperation(instruction.WriteType, opResult);

        return fetchCycles + opCycles + writeCycles;
    }

    private int ExecuteCbInstruction()
    {
        var cbOpCode = GetNextOpcode();
        var cbType = DecodeCbType(cbOpCode);
        var fetchType = DecodeFetchType(cbOpCode);
        var writeType = DecodeWriteType(cbOpCode);
        var (fetchCycles, fetchResult) = PerformFetchOperation(fetchType);
        var (opCycles, opResult) = PerformCbOperation(cbType, fetchResult);
        var writeCycles = PerformWriteOperation(writeType, opResult);

        const int cbCycles = 4;
        return cbCycles + fetchCycles + opCycles + writeCycles;
    }

    private byte GetNextOpcode()
    {
        var opcode = memory.Read(*Registers.PC);
        (*Registers.PC)++;
        return opcode;
    }
}