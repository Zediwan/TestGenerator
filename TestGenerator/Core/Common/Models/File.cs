using System.IO;

namespace TestGenerator.Core.Common.Models;

public class File(FileInfo fileInfo)
{
    public static readonly string Icon = "\ue132";
    public string Name => fileInfo.Name;
    public string FullPath => fileInfo.FullName;
    public List<Class> Classes { get; set; } = [];

    public new string ToString() => Name;
}
