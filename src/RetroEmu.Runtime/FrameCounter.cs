using System;
using System.Collections.Generic;
using System.Linq;

namespace RetroEmu.Runtime;

public class FrameCounter : IFrameCounter
{
    public long TotalFrames { get; private set; }
    public float TotalSeconds { get; private set; }
    public float AverageFramesPerSecond { get; private set; }
    public float CurrentFramesPerSecond { get; private set; }

    public const int MaximumSamples = 100;

    private Queue<float> _sampleBuffer = new();

    public void Update(TimeSpan deltaTime)
    {
        var deltaSeconds = (float)deltaTime.TotalSeconds;
        CurrentFramesPerSecond = 1.0f / deltaSeconds;

        _sampleBuffer.Enqueue(CurrentFramesPerSecond);

        if (_sampleBuffer.Count > MaximumSamples)
        {
            _sampleBuffer.Dequeue();
            AverageFramesPerSecond = _sampleBuffer.Average(i => i);
        }
        else
        {
            AverageFramesPerSecond = CurrentFramesPerSecond;
        }

        TotalFrames++;
        TotalSeconds += deltaSeconds;
    }
}

public interface IFrameCounter
{
    public float AverageFramesPerSecond { get; }
    public float CurrentFramesPerSecond { get; }
}
