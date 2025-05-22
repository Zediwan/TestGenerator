using System.IO;
using System.Windows;
using TestGenerator.Core.Generation;

namespace TestGenerator.UI;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
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
            System.Windows.MessageBox.Show("Invalid folder path.");
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
        GenerateButton.IsEnabled = false;
    }

    private void GenerateButton_Click(object sender, RoutedEventArgs e)
    {
        var path = TestsFolderPath.Text;
        if (!Directory.Exists(path))
        {
            System.Windows.MessageBox.Show("Invalid folder path.");
            return;
        }
        // Call your generator
        System.Windows.MessageBox.Show("Test generation is not properly implemented yet.");

        //var exampleFile = ProjectOverview.rootFolder.SubFolders[2].SubFolders[3].Files[0];
        //var exampleMethod = exampleFile.Classes[0].Methods[0];

        //var testFile = FileGenerator.Create(TestsFolderPath.Text, exampleFile, TestSchema.FilePrefix.Text, TestSchema.FileSuffix.Text);
        //MethodGenerator.Generate(testFile, exampleMethod, TestSchema.MethodPrefix.Text, TestSchema.MethodSuffix.Text);
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