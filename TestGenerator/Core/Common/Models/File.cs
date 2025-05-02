using System.IO;

namespace TestGenerator.Core.Common.Models;

public class File
{
    private readonly FileInfo _fileInfo;
    public string Name => _fileInfo.Name;
    public string FullPath => _fileInfo.FullName;
    public string Code { get; set; }
    public Class[] ScannedClasses { get; set; } = [];

    public File(FileInfo fileInfo)
    {
        _fileInfo = fileInfo;
        Code = System.IO.File.ReadAllText(FullPath);
    }
}
