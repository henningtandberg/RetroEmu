using Microsoft.Xna.Framework.Content;

namespace RetroEmu.UI.Desktop.Wrapper;

public class ContentManagerWrapper : IWrapper<ContentManager>
{
    private ContentManager _contentManager;

    public ContentManagerWrapper(ContentManager contentManager)
    {
        _contentManager = contentManager;
    }

    public ref ContentManager Value => ref _contentManager;
}