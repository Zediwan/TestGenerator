using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TestGenerator.Core.Common.Models;
using TestGenerator.Core.Scanning;
using File = TestGenerator.Core.Common.Models.File;

namespace TestGenerator;

public partial class MainWindow : Window
{
    private static readonly Random Random = new();

    public MainWindow()
    {
        InitializeComponent();
        ProjectTreeView.Items.Clear();
        ProjectTreeView.Items.Add(LoadDirectoryTree(DirectoryScanner.ScanDirectory(@"D:\Repositories\EvoSim\EvoSim")));
    }

    private static TreeViewItem LoadDirectoryTree(Folder rootFolder)
    {
        return LoadFolder(rootFolder);
    }

    private static TreeViewItem LoadFolder(Folder folder)
    {
        var TreeItem = CreateTreeItem(folder.Name);

        foreach (var file in folder.Files)
        {
            TreeItem.Items.Add(LoadFile(file));
        }

        foreach (var subfolder in folder.SubFolders)
        {
            TreeItem.Items.Add(LoadFolder(subfolder));
        }

        return TreeItem;
    }

    private static TreeViewItem LoadFile(File file)
    {
        var TreeItem = CreateTreeItem(file.Name);

        foreach (var _class in file.Classes)
        {
            TreeItem.Items.Add(LoadClass(_class));
        }

        return TreeItem;
    }

    private static TreeViewItem LoadClass(Class _class)
    {
        var TreeItem = CreateTreeItem(_class.Name);

        foreach (var method in _class.Methods)
        {
            TreeItem.Items.Add(CreateTreeItem(method.Name));
        }

        return TreeItem;
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