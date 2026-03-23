namespace RetroEmu.Abstractions;

public interface IReadOnlyFrameCounter
{
    public float AverageFramesPerSecond { get; }
    public float CurrentFramesPerSecond { get; }
}