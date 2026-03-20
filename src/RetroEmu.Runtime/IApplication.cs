using System;

namespace RetroEmu.Runtime;

public interface IApplication
{
    public void Initialize();
    public void Update(TimeSpan deltaTime);
}