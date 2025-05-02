using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestGenerator;

public partial class MainWindow : Window
{
    public static Random random = new Random();

    public MainWindow()
    {
        InitializeComponent();
        ProjectTreeView.Items.Clear();
        ProjectTreeView.Items.Add(LoadSampleDirectoryTree());
    }

    private TreeViewItem LoadSampleDirectoryTree(int depth = 0, int maxDepth = 5, int maxPerLevel = 3)
    {
        var numSubElements = 0;
        var name = $"File {depth}:{random.Next(100)}.cs";
        
        if (depth < maxDepth)
        {
            numSubElements = random.Next(2) == 1 ? random.Next(maxPerLevel) : 0;
            name = numSubElements > 0 ? $"Folder {depth}:{random.Next(10)}" : $"File {depth}:{random.Next(100)}.cs";
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

    private TreeViewItem CreateTreeItem(string name)
    {
        return new TreeViewItem() { Header = new CheckBox() { Content = name } }; ;
    }
}