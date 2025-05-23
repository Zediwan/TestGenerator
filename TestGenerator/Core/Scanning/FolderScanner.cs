using System.Collections.ObjectModel;
using System.IO;
using TestGenerator.Core.Common.Models;

namespace TestGenerator.Core.Scanning;

public static class FolderScanner
{
    public static ObservableCollection<TreeItemViewModel> Scan(DirectoryInfo directoryInfo)
    {
        var rootItem = new TreeItemViewModel(directoryInfo.Name, directoryInfo);

        foreach (var subDirectoryInfo in directoryInfo.GetDirectories())
        {
            var child = ScanRecursively(subDirectoryInfo);
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


    public static TreeItemViewModel? ScanRecursively(DirectoryInfo directoryInfo)
    {
        var dirItem = new TreeItemViewModel(directoryInfo.Name, directoryInfo);

        // Recursively scan subdirectories and only keep non-null results
        foreach (var subDirectoryInfo in directoryInfo.GetDirectories())
        {
            var child = ScanRecursively(subDirectoryInfo);
            if (child == null) continue;

            child.Parent = dirItem;
            dirItem.Children.Add(child);
        }

        // Add .cs files in the current directory
        foreach (var file in directoryInfo.GetFiles("*.cs"))
        {
            var child = FileScanner.ScanCsFile(file);
            child.Parent = dirItem;
            dirItem.Children.Add(child);
        }

        // Only return this directory if it has children (either subfolders or files)
        return dirItem.Children.Any() ? dirItem : null;
    }
}