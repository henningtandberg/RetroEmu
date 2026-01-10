using System.Collections.Generic;
using System.Linq;
using RetroEmu.Devices.DMG.CPU.Link;

namespace RetroEmu.GB.TestSetup;

public class WireFake(bool echoData = false) : IWire
{
    private readonly Queue<Data> outgoingData = [];
    private readonly Queue<Data> incomingData = [];
    
    public bool HasData()
    {
        return incomingData.Count > 0;
    }

    public Data DequeueOutgoingData()
    {
        return outgoingData.Dequeue();
    }

    public void Write(Data data)
    {
        outgoingData.Enqueue(data);

        if (echoData)
        {
            incomingData.Enqueue(data);
        }
    }

    public void EnqueueIncomingData(Data data)
    {
        incomingData.Enqueue(data);
    }
    
    public Data Read()
    {
        return incomingData.Dequeue();
    }

    public void Flush()
    {
        incomingData.Clear();
        outgoingData.Clear();
    }

    public byte[] AllOutgoingData() =>
        outgoingData.Select(data => data.SerialByte).ToArray();
}