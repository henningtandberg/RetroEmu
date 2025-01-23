using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using ImGuiNET;

namespace RetroEmu.Gui.Widgets.IO;

// From this project: https://github.com/mellinoe/synthapp/blob/master/src/synthapp/Widgets/FilePicker.cs

// FilePicker fp = FilePicker.GetFilePicker(ws, ws.WaveFilePath);
// string file = ws.WaveFilePath;
// if (fp.Draw(ref file))
// {
//     ws.WaveFilePath = file;
// }

public class FilePicker
{
    private const string FilePickerID = "###FilePicker";
    private static readonly Dictionary<object, FilePicker> s_filePickers = new Dictionary<object, FilePicker>();
    private static readonly Vector2 DefaultFilePickerSize = new Vector2(600, 400);
    private static readonly Vector4 Yellow = new(1f, 1f, 0.0f, 1f);

    public string CurrentFolder { get; set; } = "/";
    public string SelectedFile { get; set; } = "/";

    public static FilePicker GetFilePicker(object o, string startingPath)
    {
        startingPath = File.Exists(startingPath)
            ? new FileInfo(startingPath).DirectoryName
            : "/";

        if (s_filePickers.TryGetValue(o, out var fp))
        {
            return fp;
        }
        
        fp = new FilePicker
        {
            CurrentFolder = startingPath
        };
        
        s_filePickers.Add(o, fp);
        return fp;
    }

    public bool Draw(ref string selected)
    {
        string label = null;
        if (selected != null)
        {
            if (File.Exists(selected))
            {
                var realFile = new FileInfo(selected);
                label = realFile.Name;
            }
            else
            {
                label = "<Select File>";
            }
        }
        if (ImGui.Button(label))
        {
            ImGui.OpenPopup(FilePickerID);
        }

        bool result = false;
        ImGui.SetNextWindowSize(DefaultFilePickerSize, ImGuiCond.FirstUseEver);
        bool popupModalIsOpen = true;
        if (ImGui.BeginPopupModal(FilePickerID, ref popupModalIsOpen))
        {
            result = DrawFolder(ref selected, true);
            ImGui.EndPopup();
        }

        return result;
    }

    private bool DrawFolder(ref string selected, bool returnOnSelection = false)
    {
        ImGui.Text("Current Folder: " + CurrentFolder);
        bool result = false;
        
        if (ImGui.BeginChildFrame(1, new Vector2(0, 600), ImGuiWindowFlags.None))
        {
            DirectoryInfo di = new DirectoryInfo(CurrentFolder);
            if (di.Exists)
            {
                if (di.Parent != null)
                {
                    ImGui.PushStyleColor(ImGuiCol.Text, Yellow);
                    if (ImGui.Selectable("../", false, ImGuiSelectableFlags.DontClosePopups))
                    {
                        CurrentFolder = di.Parent.FullName;
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
                            CurrentFolder = fse;
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
                                result = true;
                                selected = SelectedFile;
                            }
                        }
                        if (ImGui.IsMouseDoubleClicked(0))
                        {
                            result = true;
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
            result = false;
            ImGui.CloseCurrentPopup();
        }

        if (SelectedFile != null)
        {
            ImGui.SameLine();
            if (ImGui.Button("Open"))
            {
                result = true;
                selected = SelectedFile;
                ImGui.CloseCurrentPopup();
            }
        }

        return result;
    }
}