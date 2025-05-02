using System.IO;
using System.Windows;
using System.Windows.Controls;
using TestGenerator.Core.Scanning;

namespace TestGenerator;

public partial class MainWindow : Window
{
    private static readonly Random Random = new();

    public MainWindow()
    {
        InitializeComponent();
        ProjectTreeView.Items.Clear();
        ProjectTreeView.Items.Add(LoadSampleDirectoryTree());
        Scanner.LogFolderStructure(@"D:\Repositories\EvoSim\EvoSim", logTextBlock: LogText);
    }

    private static TreeViewItem LoadSampleDirectoryTree(int depth = 0, int maxDepth = 5, int maxPerLevel = 3)
    {
        var numSubElements = 0;
        var name = $"File {depth}:{Random.Next(100)}.cs";
        
        if (depth < maxDepth)
        {
            numSubElements = Random.Next(2) == 1 ? Random.Next(maxPerLevel) : 0;
            name = numSubElements > 0 ? $"Folder {depth}:{Random.Next(10)}" : $"File {depth}:{Random.Next(100)}.cs";
        }
        if (depth == 0)
        {
            name = "Project";
            numSubElements = 5;
        }

        var root = CreateTreeItem(name);

        if (numSubElements <= 0) return root;

        for (var i = 0; i < numSubElements; i++)
        {
            root.Items.Add(LoadSampleDirectoryTree(depth + 1, maxDepth, maxPerLevel));
        }

        return root;
    }

    private static TreeViewItem CreateTreeItem(string name)
    {
        var checkBox = new CheckBox() { Content = name };
        checkBox.Checked += OnCheckboxToggled;
        checkBox.Unchecked += OnCheckboxToggled;

        var treeViewItem = new TreeViewItem() { Header = checkBox };

        checkBox.Tag = treeViewItem;

        return treeViewItem;
    }

    private static void OnCheckboxToggled(object sender, RoutedEventArgs e)
    {
        var checkbox = (CheckBox) sender;

        if (checkbox.Tag is not TreeViewItem treeViewItem) return;
        if (treeViewItem.Items.IsEmpty) return;

        foreach (TreeViewItem item in treeViewItem.Items)
        {
            if (item.Header is not CheckBox childCheckbox) continue;
            childCheckbox.IsChecked = checkbox.IsChecked;
        }
    }
}