using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using TestGenerator.Core.Scanning;
using TestGenerator.Core.Common.Models;

namespace TestGenerator.UI;

public partial class ProjectOverview
{
    public ObservableCollection<TreeItemViewModel> TreeItems { get; set; } = new();

    public ProjectOverview()
    {
        InitializeComponent();
        DataContext = this;
    }

    public void Load(string path)
    {
        Clear();

        var directoryInfo = new DirectoryInfo(path);
        TreeItems = FolderScanner.Scan(directoryInfo);

        ProjectTreeView.ItemsSource = TreeItems;
    }

    public void Clear()
    {
        ProjectTreeView.ItemsSource = null;
        TreeItems.Clear();
    }

    /// <summary>
    /// Recursively retrieves all top-level checked items from a tree structure.
    /// <para>
    /// Only explicitly checked nodes are returned. If a node is checked, its children
    /// are not included even if they are also checked.
    /// </para>
    /// <para>
    /// If a node is unchecked but some of its children are checked, only those children are returned.
    /// </para>
    /// <para>
    /// Example tree:
    /// <code>
    /// Root
    /// ├─ A (checked)
    /// │  ├─ A1 (checked)
    /// │  └─ A2 (checked)
    /// ├─ B (unchecked)
    /// │  ├─ B1 (checked)
    /// │  └─ B2 (unchecked)
    /// └─ C (checked)
    ///     ├─ C1 (checked)
    ///     └─ C2 (checked)
    /// </code>
    /// Returns: [A, B1, C]
    /// </para>
    /// </summary>
    public static List<TreeItemViewModel> GetCheckedItems(IEnumerable<TreeItemViewModel> items)
    {
        var selectedItems = new List<TreeItemViewModel>();

        foreach (var item in items)
        {
            if (item.IsChecked == true)
                selectedItems.Add(item);
            else
            {
                // Recursively check child items
                if (item.Children.Any())
                    selectedItems.AddRange(GetCheckedItems(item.Children));
            }
        }

        return selectedItems;
    }
}