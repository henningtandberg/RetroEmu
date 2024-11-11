using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU;

public unsafe partial class Processor : IProcessor
{
    private readonly IMemory _memory;
    private readonly Instruction[] _instructions;

    public Registers Registers { get; }
    public int Cycles { get; set; }

    public Processor(IMemory memory)
    {
        Registers = new Registers();
        _memory = memory;
        _instructions = new Instruction[256];
        SetUpInstructions();
    }

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
        var opcode = _memory.Read(*Registers.PC);
        (*Registers.PC)++;
        return opcode;
    }
}