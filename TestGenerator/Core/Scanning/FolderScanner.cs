using TestGenerator.Core.Common.Models;

namespace TestGenerator.Core.Scanning;

public static class FolderScanner
{
    public static void Scan(Folder folder)
    {
        folder.SubFolders = folder.DirectoryInfo.GetDirectories().Select(d => new Folder(d)).ToArray();
        folder.Files = folder.DirectoryInfo.GetFiles().Select(f => new File(f)).ToArray();
        
        foreach (var subfolder in folder.SubFolders) Scan(subfolder);
    }
}
