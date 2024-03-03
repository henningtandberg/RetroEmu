using System;
using ImGuiNET;
using MediatR;
using Microsoft.Xna.Framework;

namespace RetroEmu.Gui.Widgets.MainMenu;

public class MainMenuWidget : IGuiWidget
{
    private readonly IMediator _mediator;
    private readonly IApplicationStateProvider _applicationStateProvider;

    public MainMenuWidget(IMediator mediator, IApplicationStateProvider applicationStateProvider)
    {
        _mediator = mediator;
        _applicationStateProvider = applicationStateProvider;
    }

    public void Initialize()
    {
        // Nothing to be done here yet
    }

    public void LoadContent()
    {
        // Nothing to be done here yet
    }

    public void Draw(GameTime gameTime)
    {
        if (!ImGui.BeginMainMenuBar())
        {
            return;
        }

        if (!ImGui.BeginMenu("File"))
        {
            return;
        }
        
        // TODO: Add keyboard shortcuts
        // TODO: Add file picker dialog. Inspo: https://github.com/mellinoe/synthapp/blob/master/src/synthapp/Widgets/FilePicker.cs#L58

        if (ImGui.MenuItem("Open", "Ctrl+O", false))
        {
            Console.WriteLine("Open");
        }
        if (ImGui.MenuItem("Save", "Ctrl+S", false, true))
        {
            Console.WriteLine("Save");
        }
        if (ImGui.MenuItem("Save As..", "Ctrl+Shift+S", false, true))
        {
            Console.WriteLine("Save As..");
        }

        if (_applicationStateProvider.ApplicationState == ApplicationState.Running)
        {
            if (ImGui.MenuItem("Pause", "", false, true))
            {
                _mediator.Send(new ApplicationStateRequest { State = ApplicationState.Paused });
            }
        }
        else
        {
            if (ImGui.MenuItem("Resume", "", false, true))
            {
                _mediator.Send(new ApplicationStateRequest { State = ApplicationState.Running });
            }
        }
        
        ImGui.EndMenu();
        ImGui.EndMainMenuBar();
    }
}
