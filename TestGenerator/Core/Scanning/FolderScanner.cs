using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Shapes;
using TestGenerator.Core.Common.Models;

namespace TestGenerator.Core.Scanning;

public static class FolderScanner
{
    public static ObservableCollection<TreeItemViewModel> Scan(DirectoryInfo directoryInfo)
    {
        var rootItem = new TreeItemViewModel { Name = directoryInfo.Name, Tag = directoryInfo };

        foreach (var dir in directoryInfo.GetDirectories())
        {
            var child = ScanRecursively(dir);
            if (child == null) continue;

            child.Parent = rootItem;
            rootItem.Children.Add(child);
        }

        foreach (var file in directoryInfo.GetFiles("*.cs"))
        {
            var child = FileScanner.ScanCsFile(file);
            child.Parent = rootItem;
            rootItem.Children.Add(child);
        }

        return rootItem.Children.Any() ? [rootItem] : [];
    }


    public static TreeItemViewModel? ScanRecursively(DirectoryInfo dir)
    {
        var dirItem = new TreeItemViewModel { Name = dir.Name, Tag = dir };

        // Recursively scan subdirectories and only keep non-null results
        foreach (var subDir in dir.GetDirectories())
        {
            var child = ScanRecursively(subDir);
            if (child == null) continue;

            child.Parent = dirItem;
            dirItem.Children.Add(child);
        }

        // Add .cs files in the current directory
        foreach (var file in dir.GetFiles("*.cs"))
        {
            var child = FileScanner.ScanCsFile(file);
            child.Parent = dirItem;
            dirItem.Children.Add(child);
        }

        // Only return this directory if it has children (either subfolders or files)
        return dirItem.Children.Any() ? dirItem : null;
    }

}
