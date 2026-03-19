namespace RetroEmu.UI.Desktop.Wrapper;

public interface IWrapper<T>
{
    public ref T Value { get; }
}