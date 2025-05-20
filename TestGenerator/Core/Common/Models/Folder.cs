using System.IO;

namespace TestGenerator.Core.Common.Models;

public class Folder(DirectoryInfo directoryInfo, Folder? parentFolder)
{
    public static readonly string Icon = "\ue188";
    public readonly DirectoryInfo DirectoryInfo = directoryInfo;
    public readonly Folder? ParentFolder = parentFolder;
    /// <summary>
    /// The path to this folder inside the project.
    /// </summary>
    public string? ProjectPath;
    public string Name => DirectoryInfo.Name;
    public string FullPath => DirectoryInfo.FullName;
    public Folder[] SubFolders { get; set; } = [];
    public File[] Files { get; set; } = [];

    public new string ToString() => Name;
}
