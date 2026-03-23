using System;
using System.Collections.Generic;
using System.Linq;
using RetroEmu.Abstractions;

namespace RetroEmu.UI.Desktop.Rendering;

public class FrameCounter : IFrameCounter, IReadOnlyFrameCounter
{
    private const int MaximumSamples = 100;
    
    private long TotalFrames { get; set; }
    private float TotalSeconds { get; set; }
    
    public float AverageFramesPerSecond { get; private set; }
    public float CurrentFramesPerSecond { get; private set; }

    private readonly Queue<float> _sampleBuffer = new();

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