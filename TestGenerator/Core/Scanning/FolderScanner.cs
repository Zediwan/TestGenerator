using System.Collections.ObjectModel;
using System.IO;
using TestGenerator.Core.Common.Models;

namespace TestGenerator.Core.Scanning;

/// <summary>
/// Provides methods for scanning directories (folders) and constructing tree representations
/// of their structure, including subdirectories and C# files.
/// </summary>
public static class FolderScanner
{
    /// <summary>
    /// Scans the specified directory and returns a collection containing a <see cref="TreeItemViewModel"/>
    /// representing the directory and its children (subdirectories and C# files).
    /// </summary>
    /// <param name="directoryInfo">The <see cref="DirectoryInfo"/> of the directory to scan.</param>
    /// <returns>
    /// An <see cref="ObservableCollection{TreeItemViewModel}"/> containing the root directory node,
    /// or an empty collection if the directory contains no relevant children.
    /// </returns>
    public static ObservableCollection<TreeItemViewModel> Scan(DirectoryInfo directoryInfo)
    {
        var rootItem = new TreeItemViewModel(directoryInfo.Name, directoryInfo);

        #region Folders

        foreach (var subDirectoryInfo in directoryInfo.GetDirectories())
        {
            var child = ScanRecursively(subDirectoryInfo);
            if (child == null) continue;

            child.Parent = rootItem;
            rootItem.Children.Add(child);
        }

        #endregion

        #region Files

        foreach (var file in directoryInfo.GetFiles("*.cs"))
        {
            var child = FileScanner.ScanCsFile(file);
            child.Parent = rootItem;
            rootItem.Children.Add(child);
        }

        #endregion

        return rootItem.Children.Any() ? [rootItem] : [];
    }

    /// <summary>
    /// Recursively scans the specified directory and its subdirectories, returning a tree node
    /// representing the directory and its children (subdirectories and C# files).
    /// </summary>
    /// <param name="directoryInfo">The <see cref="DirectoryInfo"/> of the directory to scan.</param>
    /// <returns>
    /// A <see cref="TreeItemViewModel"/> representing the directory and its children,
    /// or <c>null</c> if the directory contains no relevant children.
    /// </returns>
    public static TreeItemViewModel? ScanRecursively(DirectoryInfo directoryInfo)
    {
        var dirItem = new TreeItemViewModel(directoryInfo.Name, directoryInfo);

        #region Folders

        // Recursively scan subdirectories and only keep non-null results
        foreach (var subDirectoryInfo in directoryInfo.GetDirectories())
        {
            var child = ScanRecursively(subDirectoryInfo);
            if (child == null) continue;

            child.Parent = dirItem;
            dirItem.Children.Add(child);
        }

        #endregion

        #region Files

        // Add .cs files in the current directory
        foreach (var file in directoryInfo.GetFiles("*.cs"))
        {
            var child = FileScanner.ScanCsFile(file);
            child.Parent = dirItem;
            dirItem.Children.Add(child);
        }

        #endregion

        // Only return this directory if it has children (either subfolders or files)
        return dirItem.Children.Any() ? dirItem : null;
    }
}