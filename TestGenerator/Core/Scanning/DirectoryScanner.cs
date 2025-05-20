using System.IO;
using TestGenerator.Core.Common.Models;

namespace TestGenerator.Core.Scanning;

public static class DirectoryScanner
{
    public static Folder ScanDirectory(string path)
    {
        var directoryInfo = new DirectoryInfo(path);
        var folder = new Folder(directoryInfo, null);
        FolderScanner.Scan(folder);
        return folder;
    }
}
