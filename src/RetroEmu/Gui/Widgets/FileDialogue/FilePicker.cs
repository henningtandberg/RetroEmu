using System.IO;
using ImGuiNET;
using Vector2 = System.Numerics.Vector2;
using Vector4 = System.Numerics.Vector4;

namespace RetroEmu.Gui.Widgets.FileDialogue;

// FilePicker originally from this project:
// https://github.com/mellinoe/synthapp/blob/master/src/synthapp/Widgets/FilePicker.cs

public enum FilePickerResult
{
    Cancel,
    Selected,
    NotSelected
}

public class FilePicker
{
    private const string FilePickerId = "###FilePicker";
    private static readonly Vector2 DefaultFilePickerSize = new(600, 400);
    private static readonly Vector4 Yellow = new(1f, 1f, 0.0f, 1f);
    private string _currentFolder;
    
    public FilePicker(string startFolder)
    {
        _currentFolder = startFolder;
    }
    public string SelectedFile { get; set; } = "/";

    public FilePickerResult Draw()
    {
        ImGui.OpenPopup(FilePickerId);
        ImGui.SetNextWindowSize(DefaultFilePickerSize, ImGuiCond.FirstUseEver);
        
        var result = FilePickerResult.NotSelected;
        var popupModalIsOpen = true;

        if (!ImGui.BeginPopupModal(FilePickerId, ref popupModalIsOpen))
        {
            return result;
        }

        result = DrawFolder();
        ImGui.EndPopup();

        return result;
    }

    private FilePickerResult DrawFolder(bool returnOnSelection = false)
    {
        var result = FilePickerResult.NotSelected;
        
        ImGui.Text("Current Folder: " + _currentFolder);
        if (!ImGui.BeginChildFrame(1, new Vector2(0, 600), ImGuiWindowFlags.None))
        {
            return result;
        }

        var di = new DirectoryInfo(_currentFolder);
        if (!di.Exists)
        {
            return result;
        }

        DrawParentDirectory(di);
        
        foreach (var fse in Directory.EnumerateFileSystemEntries(di.FullName))
        {
            if (Directory.Exists(fse))
            {
                DrawDirectory(fse);
            }
            else if (fse.Contains(".gb"))
            {
                result = DrawSelectableFile(returnOnSelection, fse);
            }
        }
        ImGui.EndChildFrame();

        if (ImGui.Button("Cancel"))
        {
            result = FilePickerResult.Cancel;
            SelectedFile = string.Empty;
            ImGui.CloseCurrentPopup();
        }

        if (string.IsNullOrEmpty(SelectedFile))
        {
            return result;
        }
        
        ImGui.SameLine();
        if (!ImGui.Button("Open"))
        {
            return result;
        }
        
        result = FilePickerResult.Selected;
        ImGui.CloseCurrentPopup();

        return result;
    }

    private FilePickerResult DrawSelectableFile(bool returnOnSelection, string fse)
    {
        var name = Path.GetFileName(fse);
        var isSelected = SelectedFile == fse;
        
        if (ImGui.Selectable(name, isSelected, ImGuiSelectableFlags.DontClosePopups))
        {
            SelectedFile = fse;
            if (returnOnSelection)
            {
                return FilePickerResult.Selected;
            }
        }

        if (!ImGui.IsMouseDoubleClicked(0))
        {
            return FilePickerResult.NotSelected;
        }
        
        ImGui.CloseCurrentPopup();
        return FilePickerResult.Selected;
    }

    private void DrawDirectory(string fse)
    {
        var name = Path.GetFileName(fse);
        ImGui.PushStyleColor(ImGuiCol.Text, Yellow);
        if (ImGui.Selectable(name + "/", false, ImGuiSelectableFlags.DontClosePopups))
        {
            _currentFolder = fse;
        }
        ImGui.PopStyleColor();
    }

    private void DrawParentDirectory(DirectoryInfo di)
    {
        if (di.Parent == null)
        {
            return;
        }
        
        ImGui.PushStyleColor(ImGuiCol.Text, Yellow);
        if (ImGui.Selectable("../", false, ImGuiSelectableFlags.DontClosePopups))
        {
            _currentFolder = di.Parent.FullName;
        }
        ImGui.PopStyleColor();
    }
}