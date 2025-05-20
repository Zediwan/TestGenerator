using TestGenerator.Core.Common.Models;

namespace TestGenerator.Core.Scanning;

public static class FolderScanner
{
    public static void Scan(Folder folder)
    {
        // TODO: add documentation that explains this part
        if (folder.ParentFolder is { ParentFolder: not null })
        {
            folder.RootPath = folder.ParentFolder.RootPath + folder.ParentFolder.Name + "\\";
        }
        

        folder.SubFolders = folder.DirectoryInfo.GetDirectories().Select(d => new Folder(d, folder)).ToArray();
        folder.Files = folder.DirectoryInfo.GetFiles().Select(f => new File(f, folder)).ToArray();
        
        foreach (var subfolder in folder.SubFolders) Scan(subfolder);
        foreach (var file in folder.Files) FileScanner.Scan(file);
    }
}
