using System.Windows;
using System.Windows.Controls;
using TestGenerator.Core.Common.Models;
using TestGenerator.Core.Scanning;
using CheckBox = System.Windows.Controls.CheckBox;
using Orientation = System.Windows.Controls.Orientation;
using FontFamily = System.Windows.Media.FontFamily;

namespace TestGenerator.UI
{
    /// <summary>
    /// Interaction logic for ProjectOverview.xaml
    /// </summary>
    public partial class ProjectOverview
    {
        // Symbols
        private static readonly FontFamily SymbolFontFamily = new("Segoe MDL2 Assets");
        private static readonly Thickness SymbolMargin = new(0, 2.5, 5, 0);

        public ProjectOverview()
        {
            InitializeComponent();
        }

        public void LoadProjectTreeView(string path)
        {
            Clear();
            ProjectTreeView.Items.Add(LoadFolder(DirectoryScanner.ScanDirectory(path)));
        }

        public void Clear()
        {
            ProjectTreeView.Items.Clear();
        }
        
        private static TreeViewItem LoadFolder(Folder folder)
        {
            var treeItem = CreateTreeItem(folder.ToString(), Folder.Icon);

            foreach (var file in folder.Files)
            {
                treeItem.Items.Add(LoadFile(file));
            }

            foreach (var subfolder in folder.SubFolders)
            {
                treeItem.Items.Add(LoadFolder(subfolder));
            }

            return treeItem;
        }

        private static TreeViewItem LoadFile(File file)
        {
            var treeItem = CreateTreeItem(file.ToString(), File.Icon);

            foreach (var cls in file.Classes)
            {
                treeItem.Items.Add(LoadClass(cls));
            }

            return treeItem;
        }

        private static TreeViewItem LoadClass(Class cls)
        {
            var treeItem = CreateTreeItem(cls.ToString(), Class.Icon);

            foreach (var constructor in cls.Constructors)
            {
                treeItem.Items.Add(CreateTreeItem(constructor.ToString(), Constructor.Icon));
            }

            foreach (var property in cls.Properties)
            {
                treeItem.Items.Add(LoadProperty(property));
            }

            foreach (var method in cls.Methods)
            {
                treeItem.Items.Add(CreateTreeItem(method.ToString(), Method.Icon));
            }

            return treeItem;
        }

        private static TreeViewItem LoadProperty(Property prop)
        {
            var treeItem = CreateTreeItem(prop.ToString(), Property.Icon);

            if (prop.Getter != null)
            {
                treeItem.Items.Add(CreateTreeItem(prop.Getter.ToString(), Method.Icon));
            }

            if (prop.Setter != null)
            {
                treeItem.Items.Add(CreateTreeItem(prop.Setter.ToString(), Method.Icon));
            }

            return treeItem;
        }

        private static TreeViewItem CreateTreeItem(string name, string symbol)
        {
            var stackPanel = new StackPanel() { Orientation = Orientation.Horizontal };
            stackPanel.Children.Add(new TextBlock() { FontFamily = SymbolFontFamily, Text = symbol, Margin = SymbolMargin });
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
            var checkbox = (CheckBox)sender;

            if (checkbox.Tag is not TreeViewItem treeViewItem) return;
            if (treeViewItem.Items.IsEmpty) return;

            foreach (TreeViewItem item in treeViewItem.Items)
            {
                if (item.Header is not CheckBox childCheckbox) continue;
                childCheckbox.IsChecked = checkbox.IsChecked;
            }
        }
    }
}
