using System.IO;

namespace TestGenerator.Core.Common.Models;

public class Folder
{
    private readonly DirectoryInfo _directoryInfo;
    public string Name => _directoryInfo.Name;
    public string FullPath => _directoryInfo.FullName;
    public Folder[] SubFolders { get; } = [];
    public File[] Files { get; } = [];

    public Folder(DirectoryInfo directoryInfo)
    {
        _directoryInfo = directoryInfo;

        foreach (var directory in _directoryInfo.GetDirectories())
        {
            SubFolders.Append(new Folder(directory));
        }

        foreach (var fileInfo in _directoryInfo.GetFiles())
        {
            Files.Append(new File(fileInfo));
        }
    }
}
