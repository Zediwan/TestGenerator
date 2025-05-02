using System.IO;
using TestGenerator.Core.Common.Models;

namespace TestGenerator.Core.Scanning;

public static class DirectoryScanner
{
    public static Folder ScanDirectory(string path)
    {
        return new Folder(new DirectoryInfo(path));
    }
}
