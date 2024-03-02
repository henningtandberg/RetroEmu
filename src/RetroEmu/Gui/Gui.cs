using ImGuiNET;
using Microsoft.Xna.Framework;

namespace RetroEmu.Gui;

public class Gui : IGui
{
    private readonly IImGuiRenderer _imGuiRenderer;
    //private readonly GraphicsDevice _graphicsDevice;

    public Gui(IImGuiRenderer imGuiRenderer/*, IWrapper<GraphicsDevice> graphicsDeviceWrapper*/)
    {
	    _imGuiRenderer = imGuiRenderer;
	    //_graphicsDevice = graphicsDeviceWrapper.Value;
    }
    
	public void Initialize()
	{
        _imGuiRenderer.RebuildFontAtlas(); 
	}

	public void LoadContent()
	{
		
	}

	public void Draw(GameTime gameTime)
    {
        _imGuiRenderer.BeforeLayout(gameTime);

        ImGuiLayout();

        _imGuiRenderer.AfterLayout();
    }
	

    protected virtual void ImGuiLayout()
    {
	    MainMenuBarBuilder
		    .Init()
		    .WithMenu("File", () =>
			{ 
				ImGui.MenuItem("Open ROM", "Ctrl+O", false, true); 
				ImGui.MenuItem("Save", "Ctrl+S", false, true); 
				ImGui.MenuItem("Save as ...", "Ctrl+Shift+S", false, true);
			})
		    .Build();
    }
}