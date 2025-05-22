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

    private void OnCheckboxToggled(object sender, RoutedEventArgs e)
    {
        var checkbox = sender as System.Windows.Controls.CheckBox;
        if (checkbox?.DataContext is not TreeItemViewModel currentItem) return;

        foreach (var child in currentItem.Children)
            child.IsChecked = currentItem.IsChecked;
    }
}