using System;

namespace RetroEmu.UI.Desktop.Rendering;

public interface IFrameCounter
{
    public float AverageFramesPerSecond { get; }
    public float CurrentFramesPerSecond { get; }

    public void Update(TimeSpan deltaTime);
}