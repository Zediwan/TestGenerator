using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using TestGenerator.Core.Generation;
using TestGenerator.UI.Helpers;

namespace TestGenerator.UI;

public partial class MainWindow : Window, INotifyPropertyChanged
{
    public MainWindow()
    {
        InitializeComponent();
        TestsFolderPath.TextChanged += (s, e) => OnPropertyChanged(nameof(CanGenerate));
        ProjectOverview.AnyItemSelectedChanged += (s, e) => OnPropertyChanged(nameof(CanGenerate));
        SrcFolderPath.TextChanged += (s, e) => OnPropertyChanged(nameof(CanScan));
    }

    #region Binding Properties

    public bool CanGenerate =>
        !string.IsNullOrEmpty(TestsFolderPath.Text) && ProjectOverview.AnyItemSelected;

    public bool CanScan => !string.IsNullOrEmpty(SrcFolderPath.Text);

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    #endregion

    #region Buttons

    private void ScanButton_Click(object sender, RoutedEventArgs e)
    {
        var path = SrcFolderPath.Text;

        if (!Directory.Exists(path))
        {
            MessageBoxHelper.ShowInvalidFolderPath();
            return;
        }

        ProjectOverview.Load(path);
    }

    private void GenerateButton_Click(object sender, RoutedEventArgs e)
    {
        var path = TestsFolderPath.Text;
        if (!Directory.Exists(path))
        {
            MessageBoxHelper.ShowInvalidFolderPath();
            return;
        }

        if (!TestSchema.CheckSchema())
            return;

        #region Generators

        var generator = new Generator(path, SrcFolderPath.Text);

        var fileGenerator = new FileGenerator(TestSchema.FilePrefix.Text, TestSchema.FileSuffix.Text, path,
            SrcFolderPath.Text);
        generator.FileGenerator = fileGenerator;

        #endregion

        var items = ProjectOverview.GetCheckedItems(ProjectOverview.TreeItems);

        generator.Generate(items);
    }

    #endregion

    #region FolderSelection

    private void SelectSourceFolder_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new FolderBrowserDialog();
        if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) SrcFolderPath.Text = dialog.SelectedPath;
    }

    private void SelectTestFolder_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new FolderBrowserDialog();
        if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) TestsFolderPath.Text = dialog.SelectedPath;
    }

    #endregion
}