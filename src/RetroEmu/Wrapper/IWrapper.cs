namespace RetroEmu.Wrapper;

public interface IWrapper<T>
{
    public ref T Value { get; }
}