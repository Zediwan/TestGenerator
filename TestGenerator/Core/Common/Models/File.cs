using System.IO;

namespace TestGenerator.Core.Common.Models;

public class File(FileInfo fileInfo, Folder? parentFolder)
{
    public static readonly string Icon = "\ue132";
    public readonly FileInfo FileInfo = fileInfo;
    public readonly Folder? ParentFolder = parentFolder;
    public string Name => FileInfo.Name;
    public string Extension => FileInfo.Extension;
    public string FullPath => FileInfo.FullName;
    public List<Class> Classes { get; set; } = [];

    public new string ToString() => Name;
}
