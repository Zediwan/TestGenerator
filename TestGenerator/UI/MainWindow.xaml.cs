using System.IO;
using System.Windows;
using System.Windows.Controls;
using TestGenerator.Core.Common.Models;
using TestGenerator.Core.Scanning;
using File = TestGenerator.Core.Common.Models.File;
using CheckBox = System.Windows.Controls.CheckBox;
using Orientation = System.Windows.Controls.Orientation;

namespace TestGenerator.UI;

public partial class MainWindow : Window
{
    private static readonly Random Random = new();

    public MainWindow()
    {
        InitializeComponent();
    }

    private void LoadProjectTreeView(string path)
    {
        ProjectTreeView.Items.Clear();
        ProjectTreeView.Items.Add(LoadDirectoryTree(DirectoryScanner.ScanDirectory(path)));
    }

    private static TreeViewItem LoadDirectoryTree(Folder rootFolder)
    {
        return LoadFolder(rootFolder);
    }

    private static TreeViewItem LoadFolder(Folder folder)
    {
        var TreeItem = CreateTreeItem(folder.ToString(), Folder.Icon);

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
        var TreeItem = CreateTreeItem(file.ToString(), File.Icon);

        foreach (var _class in file.Classes)
        {
            TreeItem.Items.Add(LoadClass(_class));
        }

        return TreeItem;
    }

    private static TreeViewItem LoadClass(Class _class)
    {
        var TreeItem = CreateTreeItem(_class.ToString(), Class.Icon);

        foreach (var constructor in _class.Constructors)
        {
            TreeItem.Items.Add(CreateTreeItem(constructor.ToString(), Constructor.Icon));
        }

        foreach (var property in _class.Properties)
        {
            TreeItem.Items.Add(LoadProperty(property));
        }

        foreach (var method in _class.Methods)
        {
            TreeItem.Items.Add(CreateTreeItem(method.ToString(), Method.Icon));
        }

        return TreeItem;
    }

    private static TreeViewItem LoadProperty(Property _property)
    {
        var TreeItem = CreateTreeItem(_property.ToString(), Property.Icon);

        if (_property.Getter != null)
        {
            TreeItem.Items.Add(CreateTreeItem(_property.Getter.ToString(), Method.Icon));
        }

        if (_property.Setter != null)
        {
            TreeItem.Items.Add(CreateTreeItem(_property.Setter.ToString(), Method.Icon));
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

        var root = CreateTreeItem(name, "");

        if (numSubElements <= 0) return root;

        for (var i = 0; i < numSubElements; i++)
        {
            root.Items.Add(LoadSampleDirectoryTree(depth + 1, maxDepth, maxPerLevel));
        }

        return root;
    }

    private static TreeViewItem CreateTreeItem(string name, string symbol)
    {
        var stackPanel = new StackPanel(){Orientation = Orientation.Horizontal};
        stackPanel.Children.Add(new TextBlock() { FontFamily = new System.Windows.Media.FontFamily("Segoe MDL2 Assets"), Text = symbol, Margin = new Thickness(0, 2.5, 5, 0) });
        stackPanel.Children.Add(new TextBlock() { Text = name });

        var checkBox = new CheckBox() { Content = stackPanel };
        checkBox.Checked += OnCheckboxToggled;
        checkBox.Unchecked += OnCheckboxToggled;

        var treeViewItem = new TreeViewItem() { Header = checkBox };

        checkBox.Tag = treeViewItem;

        return treeViewItem;
    }

    // TODO: if all children AND sub-children of an item are unchecked / checked then the same should count for the parent.
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

    private void SelectSourceFolder_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new FolderBrowserDialog();

        if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        {
            SrcFolderPath.Text = dialog.SelectedPath;
        }

        ScanButton.IsEnabled = !string.IsNullOrEmpty(SrcFolderPath.Text);
    }

    private void ScanButton_Click(object sender, RoutedEventArgs e)
    {
        string path = SrcFolderPath.Text;

        if (!Directory.Exists(path))
        {
            System.Windows.MessageBox.Show("Invalid folder path.");
            return;
        }

        // Call your scanner
        LoadProjectTreeView(path);

        ClearButton.IsEnabled = true;
    }

    private void ClearButton_Click(object sender, RoutedEventArgs e)
    {
        ProjectTreeView.Items.Clear();
        SrcFolderPath.Text = string.Empty;
        TestsFolderPath.Text = string.Empty;
        ScanButton.IsEnabled = false;
        ClearButton.IsEnabled = false;
        GenerateButton.IsEnabled = false;
    }

    private void GenerateButton_Click(object sender, RoutedEventArgs e)
    {
        string path = TestsFolderPath.Text;
        if (!Directory.Exists(path))
        {
            System.Windows.MessageBox.Show("Invalid folder path.");
            return;
        }
        // Call your generator
        System.Windows.MessageBox.Show("Test generation is not implemented yet.");
    }

    private void SelectTestFolder_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new FolderBrowserDialog();

        if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        {
            TestsFolderPath.Text = dialog.SelectedPath;
        }

        // TODO: this should only be enabled if the source folder and target folder are set and some items are selected
        GenerateButton.IsEnabled = !string.IsNullOrEmpty(TestsFolderPath.Text);
    }
}