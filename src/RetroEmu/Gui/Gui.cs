using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace RetroEmu.Gui;

public class Gui : IGui
{
    private readonly IImGuiRenderer _imGuiRenderer;
    private readonly IEnumerable<IGuiWidget> _guiWidgets;
    //private readonly GraphicsDevice _graphicsDevice;

    public Gui(IImGuiRenderer imGuiRenderer, IEnumerable<IGuiWidget> guiWidgets/*, IWrapper<GraphicsDevice> graphicsDeviceWrapper*/)
    {
	    _imGuiRenderer = imGuiRenderer;
	    //_graphicsDevice = graphicsDeviceWrapper.Value;
	    _guiWidgets = guiWidgets;
    }
    
	public void Initialize()
	{
        _imGuiRenderer.RebuildFontAtlas();
        
        foreach (var widget in _guiWidgets)
		{
			widget.Initialize();
		}
	}

	public void LoadContent()
	{
		foreach (var widget in _guiWidgets)
		{
			widget.LoadContent();
		}
	}

	public void Draw(GameTime gameTime)
    {
        _imGuiRenderer.BeforeLayout(gameTime);
		foreach (var widget in _guiWidgets)
		{
			widget.Draw(gameTime);
		}
        _imGuiRenderer.AfterLayout();
    }
}