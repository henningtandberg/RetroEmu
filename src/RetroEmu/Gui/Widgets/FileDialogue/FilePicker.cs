using System.IO;
using ImGuiNET;
using Vector2 = System.Numerics.Vector2;
using Vector4 = System.Numerics.Vector4;

namespace RetroEmu.Gui.Widgets.FileDialogue;

// From this project: https://github.com/mellinoe/synthapp/blob/master/src/synthapp/Widgets/FilePicker.cs

public enum FilePickerResult
{
    Cancel,
    Selected,
    NotSelected
}

public class FilePicker
{
    private const string FilePickerID = "###FilePicker";
    private static readonly Vector2 DefaultFilePickerSize = new(600, 400);
    private static readonly Vector4 Yellow = new(1f, 1f, 0.0f, 1f);
    private string _currentFolder;
    
    public FilePicker(string startFolder)
    {
        _currentFolder = startFolder;
    }
    public string SelectedFile { get; set; } = "/";

    public FilePickerResult Draw(ref string selected)
    {
        ImGui.OpenPopup(FilePickerID);
        ImGui.SetNextWindowSize(DefaultFilePickerSize, ImGuiCond.FirstUseEver);
        
        var result = FilePickerResult.NotSelected;
        var popupModalIsOpen = true;

        if (!ImGui.BeginPopupModal(FilePickerID, ref popupModalIsOpen))
        {
            return result;
        }

        result = DrawFolder(ref selected, false);
        ImGui.EndPopup();

        return result;
    }

    private FilePickerResult DrawFolder(ref string selected, bool returnOnSelection = false)
    {
        var result = FilePickerResult.NotSelected;
        
        ImGui.Text("Current Folder: " + _currentFolder);
        if (ImGui.BeginChildFrame(1, new Vector2(0, 600), ImGuiWindowFlags.None))
        {
            DirectoryInfo di = new DirectoryInfo(_currentFolder);
            if (di.Exists)
            {
                if (di.Parent != null)
                {
                    ImGui.PushStyleColor(ImGuiCol.Text, Yellow);
                    if (ImGui.Selectable("../", false, ImGuiSelectableFlags.DontClosePopups))
                    {
                        _currentFolder = di.Parent.FullName;
                    }
                    ImGui.PopStyleColor();
                }
                
                foreach (var fse in Directory.EnumerateFileSystemEntries(di.FullName))
                {
                    if (Directory.Exists(fse))
                    {
                        string name = Path.GetFileName(fse);
                        ImGui.PushStyleColor(ImGuiCol.Text, Yellow);
                        if (ImGui.Selectable(name + "/", false, ImGuiSelectableFlags.DontClosePopups))
                        {
                            _currentFolder = fse;
                        }
                        ImGui.PopStyleColor();
                    }
                    else
                    {
                        string name = Path.GetFileName(fse);
                        bool isSelected = SelectedFile == fse;
                        if (ImGui.Selectable(name, isSelected, ImGuiSelectableFlags.DontClosePopups))
                        {
                            SelectedFile = fse;
                            if (returnOnSelection)
                            {
                                result = FilePickerResult.Selected;
                                selected = SelectedFile;
                            }
                        }
                        if (ImGui.IsMouseDoubleClicked(0))
                        {
                            result = FilePickerResult.Selected;
                            selected = SelectedFile;
                            ImGui.CloseCurrentPopup();
                        }
                    }
                }
            }
        }
        ImGui.EndChildFrame();

        if (ImGui.Button("Cancel"))
        {
            result = FilePickerResult.Cancel;
            ImGui.CloseCurrentPopup();
        }

        if (SelectedFile != null)
        {
            ImGui.SameLine();
            if (ImGui.Button("Open"))
            {
                result = FilePickerResult.Selected;
                selected = SelectedFile;
                ImGui.CloseCurrentPopup();
            }
        }

        return result;
    }
}