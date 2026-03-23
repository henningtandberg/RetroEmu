using System;

namespace RetroEmu.Runtime;

public interface IEmulatorOrchestrator
{
    public void Initialize();
    public void Update();
}