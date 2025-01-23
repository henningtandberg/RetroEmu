using Microsoft.Xna.Framework;
using RetroEmu.Devices.DMG;

namespace RetroEmu.Gui.Widgets.Debug;

public class MemoryEditorWidget(
    IDebugState debugState,
    IMemory memory)
        : IGuiWidget
{
    private readonly MemoryEditor _memoryEditor = new();
    public void Initialize()
    {
    }

    public void LoadContent()
    {
    }

    public void Draw(GameTime gameTime)
    {
        if (!debugState.DisplayMemoryEditor)
        {
            return;
        }
        
        //var memoryData = memory.GetMemory();
        //var memorySize = memoryData.Length;
        //_memoryEditor.Draw("Memory Editor", memoryData, memorySize);
    }
}