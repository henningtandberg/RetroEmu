using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ImGuiNET;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RetroEmu.UI.Desktop.Wrapper;

namespace RetroEmu.UI.Desktop.Gui.Rendering;

/// <summary>
/// ImGui renderer for use with XNA-likes (FNA & MonoGame)
/// </summary>
internal sealed class ImGuiRenderer : IImGuiRenderer
{
    // Graphics
    private readonly GraphicsDevice _graphicsDevice;
    private readonly GameWindow _gameWindow;

    private BasicEffect _effect;
    private readonly RasterizerState _rasterizerState;

    private byte[] _vertexData;
    private VertexBuffer _vertexBuffer;
    private int _vertexBufferSize;

    private byte[] _indexData;
    private IndexBuffer _indexBuffer;
    private int _indexBufferSize;

    // Textures
    private readonly Dictionary<IntPtr, Texture2D> _loadedTextures;

    private int _textureId;
    private IntPtr? _fontTextureId;

    // Input
    private int _scrollWheelValue;
    private Keys[] _lastPressedKeys = [];
    
    
    public ImGuiRenderer(IWrapper<GraphicsDevice> graphicsDeviceWrapper, IWrapper<GameWindow> gameWindowWrapper)
    {
        var context = ImGui.CreateContext();
        ImGui.SetCurrentContext(context);
        
        var io = ImGui.GetIO();
        io.ConfigFlags |= ImGuiConfigFlags.DockingEnable;
        
        _graphicsDevice = graphicsDeviceWrapper.Value;
        _gameWindow = gameWindowWrapper.Value;

        _loadedTextures = new Dictionary<IntPtr, Texture2D>();
        _rasterizerState = new RasterizerState
        {
            CullMode = CullMode.None,
            DepthBias = 0,
            FillMode = FillMode.Solid,
            MultiSampleAntiAlias = false,
            ScissorTestEnable = true,
            SlopeScaleDepthBias = 0
        };

        SetupInput();
    }

    #region ImGuiRenderer

    /// <summary>
    /// Creates a texture and loads the font data from ImGui. Should be called when the <see cref="GraphicsDevice" /> is initialized but before any rendering is done
    /// </summary>
    public unsafe void RebuildFontAtlas()
    {
        // Get font texture from ImGui
        var io = ImGui.GetIO();
        io.Fonts.GetTexDataAsRGBA32(out byte* pixelData, out int width, out int height, out int bytesPerPixel);

        // Copy the data to a managed array
        var pixels = new byte[width * height * bytesPerPixel];
        unsafe { Marshal.Copy(new IntPtr(pixelData), pixels, 0, pixels.Length); }

        // Create and register the texture as an XNA texture
        var tex2d = new Texture2D(_graphicsDevice, width, height, false, SurfaceFormat.Color);
        tex2d.SetData(pixels);

        // Should a texture already have been build previously, unbind it first so it can be deallocated
        if (_fontTextureId.HasValue) UnbindTexture(_fontTextureId.Value);

        // Bind the new texture to an ImGui-friendly id
        _fontTextureId = BindTexture(tex2d);

        // Let ImGui know where to find the texture
        io.Fonts.SetTexID(_fontTextureId.Value);
        io.Fonts.ClearTexData(); // Clears CPU side texture data
    }

    /// <summary>
    /// Creates a pointer to a texture, which can be passed through ImGui calls such as <see cref="MediaTypeNames.Image" />. That pointer is then used by ImGui to let us know what texture to draw
    /// </summary>
    public IntPtr BindTexture(Texture2D texture)
    {
        var id = new IntPtr(_textureId++);

        _loadedTextures.Add(id, texture);

        return id;
    }

    /// <summary>
    /// Removes a previously created texture pointer, releasing its reference and allowing it to be deallocated
    /// </summary>
    public void UnbindTexture(IntPtr textureId)
    {
        _loadedTextures.Remove(textureId);
    }

    /// <summary>
    /// Sets up ImGui for a new frame, should be called at frame start
    /// </summary>
    public void BeforeLayout(GameTime gameTime)
    {
        ImGui.GetIO().DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        UpdateInput();

        ImGui.NewFrame();
    }

    /// <summary>
    /// Asks ImGui for the generated geometry data and sends it to the graphics pipeline, should be called after the UI is drawn using ImGui.** calls
    /// </summary>
    public void AfterLayout()
    {
        ImGui.Render();

        unsafe { RenderDrawData(ImGui.GetDrawData()); }
    }

    #endregion ImGuiRenderer

    #region Setup & Update

    /// <summary>
    /// Sets up ImGui input handling
    /// </summary>
    private void SetupInput()
    {
        var io = ImGui.GetIO();

        // MonoGame-specific //////////////////////
        _gameWindow.TextInput += (s, a) =>
        {
            if (a.Character == '\t') return;

            io.AddInputCharacter(a.Character);
        };
        ///////////////////////////////////////////

        // FNA-specific ///////////////////////////
        //TextInputEXT.TextInput += c =>
        //{
        //    if (c == '\t') return;

        //    ImGui.GetIO().AddInputCharacter(c);
        //};
        ///////////////////////////////////////////

        ImGui.GetIO().Fonts.AddFontDefault();
    }

    /// <summary>
    /// Updates the <see cref="Effect" /> to the current matrices and texture
    /// </summary>
    private Effect UpdateEffect(Texture2D texture)
    {
        _effect = _effect ?? new BasicEffect(_graphicsDevice);

        var io = ImGui.GetIO();

        _effect.World = Matrix.Identity;
        _effect.View = Matrix.Identity;
        _effect.Projection = Matrix.CreateOrthographicOffCenter(0f, io.DisplaySize.X, io.DisplaySize.Y, 0f, -1f, 1f);
        _effect.TextureEnabled = true;
        _effect.Texture = texture;
        _effect.VertexColorEnabled = true;

        return _effect;
    }

    /// <summary>
    /// Sends XNA input state to ImGui
    /// </summary>
    private void UpdateInput()
    {
        // TODO: Move this out
        //if (!_game.IsActive) return;

        var io = ImGui.GetIO();

        var mouse = Mouse.GetState();
        var keyboard = Keyboard.GetState();
        var pressedKeys = keyboard.GetPressedKeys();

        // Send key events
        UpdateKeyEvent(io, pressedKeys, Keys.Tab, ImGuiKey.Tab);
        UpdateKeyEvent(io, pressedKeys, Keys.Left, ImGuiKey.LeftArrow);
        UpdateKeyEvent(io, pressedKeys, Keys.Right, ImGuiKey.RightArrow);
        UpdateKeyEvent(io, pressedKeys, Keys.Up, ImGuiKey.UpArrow);
        UpdateKeyEvent(io, pressedKeys, Keys.Down, ImGuiKey.DownArrow);
        UpdateKeyEvent(io, pressedKeys, Keys.PageUp, ImGuiKey.PageUp);
        UpdateKeyEvent(io, pressedKeys, Keys.PageDown, ImGuiKey.PageDown);
        UpdateKeyEvent(io, pressedKeys, Keys.Home, ImGuiKey.Home);
        UpdateKeyEvent(io, pressedKeys, Keys.End, ImGuiKey.End);
        UpdateKeyEvent(io, pressedKeys, Keys.Delete, ImGuiKey.Delete);
        UpdateKeyEvent(io, pressedKeys, Keys.Back, ImGuiKey.Backspace);
        UpdateKeyEvent(io, pressedKeys, Keys.Enter, ImGuiKey.Enter);
        UpdateKeyEvent(io, pressedKeys, Keys.Escape, ImGuiKey.Escape);
        UpdateKeyEvent(io, pressedKeys, Keys.Space, ImGuiKey.Space);
        UpdateKeyEvent(io, pressedKeys, Keys.A, ImGuiKey.A);
        UpdateKeyEvent(io, pressedKeys, Keys.C, ImGuiKey.C);
        UpdateKeyEvent(io, pressedKeys, Keys.V, ImGuiKey.V);
        UpdateKeyEvent(io, pressedKeys, Keys.X, ImGuiKey.X);
        UpdateKeyEvent(io, pressedKeys, Keys.Y, ImGuiKey.Y);
        UpdateKeyEvent(io, pressedKeys, Keys.Z, ImGuiKey.Z);

        _lastPressedKeys = pressedKeys;

        // Modifier keys
        io.AddKeyEvent(ImGuiKey.ModShift, keyboard.IsKeyDown(Keys.LeftShift) || keyboard.IsKeyDown(Keys.RightShift));
        io.AddKeyEvent(ImGuiKey.ModCtrl, keyboard.IsKeyDown(Keys.LeftControl) || keyboard.IsKeyDown(Keys.RightControl));
        io.AddKeyEvent(ImGuiKey.ModAlt, keyboard.IsKeyDown(Keys.LeftAlt) || keyboard.IsKeyDown(Keys.RightAlt));
        io.AddKeyEvent(ImGuiKey.ModSuper, keyboard.IsKeyDown(Keys.LeftWindows) || keyboard.IsKeyDown(Keys.RightWindows));

        io.DisplaySize = new System.Numerics.Vector2(_graphicsDevice.PresentationParameters.BackBufferWidth, _graphicsDevice.PresentationParameters.BackBufferHeight);
        io.DisplayFramebufferScale = new System.Numerics.Vector2(1f, 1f);

        io.MousePos = new System.Numerics.Vector2(mouse.X, mouse.Y);

        io.AddMouseButtonEvent(0, mouse.LeftButton == ButtonState.Pressed);
        io.AddMouseButtonEvent(1, mouse.RightButton == ButtonState.Pressed);
        io.AddMouseButtonEvent(2, mouse.MiddleButton == ButtonState.Pressed);

        var scrollDelta = mouse.ScrollWheelValue - _scrollWheelValue;
        io.AddMouseWheelEvent(0, scrollDelta / 120f);
        _scrollWheelValue = mouse.ScrollWheelValue;
    }

    private void UpdateKeyEvent(ImGuiIOPtr io, Keys[] pressedKeys, Keys xnaKey, ImGuiKey imGuiKey)
    {
        bool wasPressed = Array.IndexOf(_lastPressedKeys, xnaKey) >= 0;
        bool isPressed = Array.IndexOf(pressedKeys, xnaKey) >= 0;

        if (wasPressed != isPressed)
        {
            io.AddKeyEvent(imGuiKey, isPressed);
        }
    }

    #endregion Setup & Update

    #region Internals

    /// <summary>
    /// Gets the geometry as set up by ImGui and sends it to the graphics device
    /// </summary>
    private void RenderDrawData(ImDrawDataPtr drawData)
    {
        // Setup render state: alpha-blending enabled, no face culling, no depth testing, scissor enabled, vertex/texcoord/color pointers
        var lastViewport = _graphicsDevice.Viewport;
        var lastScissorBox = _graphicsDevice.ScissorRectangle;

        _graphicsDevice.BlendFactor = Color.White;
        _graphicsDevice.BlendState = BlendState.NonPremultiplied;
        _graphicsDevice.RasterizerState = _rasterizerState;
        _graphicsDevice.DepthStencilState = DepthStencilState.DepthRead;

        // Setup projection
        _graphicsDevice.Viewport = new Viewport(0, 0, _graphicsDevice.PresentationParameters.BackBufferWidth, _graphicsDevice.PresentationParameters.BackBufferHeight);

        UpdateBuffers(drawData);

        RenderCommandLists(drawData);

        // Restore modified state
        _graphicsDevice.Viewport = lastViewport;
        _graphicsDevice.ScissorRectangle = lastScissorBox;
    }

    private unsafe void UpdateBuffers(ImDrawDataPtr drawData)
    {
        if (drawData.TotalVtxCount == 0)
        {
            return;
        }

        // Expand buffers if we need more room
        if (drawData.TotalVtxCount > _vertexBufferSize)
        {
            _vertexBuffer?.Dispose();

            _vertexBufferSize = (int)(drawData.TotalVtxCount * 1.5f);
            _vertexBuffer = new VertexBuffer(_graphicsDevice, DrawVertDeclaration.Declaration, _vertexBufferSize, BufferUsage.None);
            _vertexData = new byte[_vertexBufferSize * DrawVertDeclaration.Size];
        }

        if (drawData.TotalIdxCount > _indexBufferSize)
        {
            _indexBuffer?.Dispose();

            _indexBufferSize = (int)(drawData.TotalIdxCount * 1.5f);
            _indexBuffer = new IndexBuffer(_graphicsDevice, IndexElementSize.SixteenBits, _indexBufferSize, BufferUsage.None);
            _indexData = new byte[_indexBufferSize * sizeof(ushort)];
        }

        // Copy ImGui's vertices and indices to a set of managed byte arrays
        int vtxOffset = 0;
        int idxOffset = 0;

        for (int n = 0; n < drawData.CmdListsCount; n++)
        {
            ImDrawListPtr cmdList = drawData.CmdLists[n];

            fixed (void* vtxDstPtr = &_vertexData[vtxOffset * DrawVertDeclaration.Size])
            fixed (void* idxDstPtr = &_indexData[idxOffset * sizeof(ushort)])
            {
                Buffer.MemoryCopy((void*)cmdList.VtxBuffer.Data, vtxDstPtr, _vertexData.Length, cmdList.VtxBuffer.Size * DrawVertDeclaration.Size);
                Buffer.MemoryCopy((void*)cmdList.IdxBuffer.Data, idxDstPtr, _indexData.Length, cmdList.IdxBuffer.Size * sizeof(ushort));
            }

            vtxOffset += cmdList.VtxBuffer.Size;
            idxOffset += cmdList.IdxBuffer.Size;
        }

        // Copy the managed byte arrays to the gpu vertex- and index buffers
        _vertexBuffer.SetData(_vertexData, 0, drawData.TotalVtxCount * DrawVertDeclaration.Size);
        _indexBuffer.SetData(_indexData, 0, drawData.TotalIdxCount * sizeof(ushort));
    }

    private unsafe void RenderCommandLists(ImDrawDataPtr drawData)
    {
        _graphicsDevice.SetVertexBuffer(_vertexBuffer);
        _graphicsDevice.Indices = _indexBuffer;

        int vtxOffset = 0;
        int idxOffset = 0;

        // Handle cases of screen coordinates != from framebuffer coordinates (e.g. retina displays)
        var fbScale = drawData.FramebufferScale;

        for (int n = 0; n < drawData.CmdListsCount; n++)
        {
            ImDrawListPtr cmdList = drawData.CmdLists[n];

            for (int cmdi = 0; cmdi < cmdList.CmdBuffer.Size; cmdi++)
            {
                ImDrawCmdPtr drawCmd = cmdList.CmdBuffer[cmdi];

                if (drawCmd.ElemCount == 0)
                {
                    continue;
                }

                if (!_loadedTextures.ContainsKey(drawCmd.TextureId))
                {
                    throw new InvalidOperationException($"Could not find a texture with id '{drawCmd.TextureId}', please check your bindings");
                }

                // Apply framebuffer scale to clip rectangles
                _graphicsDevice.ScissorRectangle = new Rectangle(
                    (int)(drawCmd.ClipRect.X * fbScale.X),
                    (int)(drawCmd.ClipRect.Y * fbScale.Y),
                    (int)((drawCmd.ClipRect.Z - drawCmd.ClipRect.X) * fbScale.X),
                    (int)((drawCmd.ClipRect.W - drawCmd.ClipRect.Y) * fbScale.Y)
                );

                var effect = UpdateEffect(_loadedTextures[drawCmd.TextureId]);

                foreach (var pass in effect.CurrentTechnique.Passes)
                {
                    pass.Apply();

#pragma warning disable CS0618 // // FNA does not expose an alternative method.
                    _graphicsDevice.DrawIndexedPrimitives(
                        primitiveType: PrimitiveType.TriangleList,
                        baseVertex: (int)drawCmd.VtxOffset + vtxOffset,
                        minVertexIndex: 0,
                        numVertices: cmdList.VtxBuffer.Size,
                        startIndex: (int)drawCmd.IdxOffset + idxOffset,
                        primitiveCount: (int)drawCmd.ElemCount / 3
                    );
#pragma warning restore CS0618
                }
            }

            vtxOffset += cmdList.VtxBuffer.Size;
            idxOffset += cmdList.IdxBuffer.Size;
        }
    }

    #endregion Internals
}