namespace RetroEmu.Devices.GameBoy.Serial;

public class Wire : IWire
{
    public bool HasData()
    {
        return false;
    }

    public void Write(Data data)
    {
    }

    public Data Read()
    {
        return new Data(1, 1);
    }

    public void Flush()
    {
    }
}