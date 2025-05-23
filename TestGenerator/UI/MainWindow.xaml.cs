using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using TestGenerator.Core.Generation;

namespace TestGenerator.UI;

// TODO: rework the UI handling methods with bindings
public partial class MainWindow : Window, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    public bool CanGenerate =>
        !string.IsNullOrEmpty(TestsFolderPath.Text) && ProjectOverview.AnyItemSelected;

    public MainWindow()
    {
        InitializeComponent();
        TestsFolderPath.TextChanged += (s, e) => OnPropertyChanged(nameof(CanGenerate));
        ProjectOverview.AnyItemSelectedChanged += (s, e) => OnPropertyChanged(nameof(CanGenerate));
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
        var path = SrcFolderPath.Text;

        if (!Directory.Exists(path))
        {
            System.Windows.Forms.MessageBox.Show(
                $"Invalid folder path: {path}",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            return;
        }

        ProjectOverview.Load(path);

        ClearButton.IsEnabled = true;
    }

    private void ClearButton_Click(object sender, RoutedEventArgs e)
    {
        ProjectOverview.Clear();
        SrcFolderPath.Text = string.Empty;
        TestsFolderPath.Text = string.Empty;
        ScanButton.IsEnabled = false;
        ClearButton.IsEnabled = false;
    }

    private void GenerateButton_Click(object sender, RoutedEventArgs e)
    {
        var path = TestsFolderPath.Text;
        if (!Directory.Exists(path))
        {
            System.Windows.Forms.MessageBox.Show(
                $"Invalid folder path: {path}",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            return;
        }

        var generator = new Generator(path, SrcFolderPath.Text)
        {
            FileGenerator = new FileGenerator(TestSchema.FilePrefix.Text, TestSchema.FileSuffix.Text, path, SrcFolderPath.Text),
        };

        var items = ProjectOverview.GetCheckedItems(ProjectOverview.TreeItems);

        generator.Generate(items);
    }

    private void SelectTestFolder_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new FolderBrowserDialog();

        if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        {
            TestsFolderPath.Text = dialog.SelectedPath;
        }
    }
}
