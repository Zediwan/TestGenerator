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
    public MainWindow()
    {
        InitializeComponent();
        LoadSampleDirectoryTree();
    }

    private void LoadSampleDirectoryTree()
    {
        var root = new TreeViewItem { Header = "MyProject" };

        var controllers = new TreeViewItem { Header = "Controllers" };
        controllers.Items.Add(new TreeViewItem { Header = "HomeController.cs" });
        controllers.Items.Add(new TreeViewItem { Header = "AccountController.cs" });

        var models = new TreeViewItem { Header = "Models" };
        models.Items.Add(new TreeViewItem { Header = "User.cs" });

        var services = new TreeViewItem { Header = "Services" };
        services.Items.Add(new TreeViewItem { Header = "EmailService.cs" });

        root.Items.Add(controllers);
        root.Items.Add(models);
        root.Items.Add(services);
        root.Items.Add(new TreeViewItem { Header = "Program.cs" });

        ProjectTreeView.Items.Clear();
        ProjectTreeView.Items.Add(root);
    }
}