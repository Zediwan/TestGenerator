using System.IO;

namespace TestGenerator.Core.Common.Models;

public class Folder
{
    private readonly DirectoryInfo _directoryInfo;
    public string Name => _directoryInfo.Name;
    public string FullPath => _directoryInfo.FullName;
    public Folder[] SubFolders { get; }
    public File[] Files { get; }

    public Folder(DirectoryInfo directoryInfo)
    {
        _directoryInfo = directoryInfo;

        SubFolders = _directoryInfo.GetDirectories().Select(d => new Folder(d)).ToArray();
        Files = _directoryInfo.GetFiles().Select(f => new File(f)).ToArray();
    }

    public new string ToString()
    {
        return this.Name;
    }
}
