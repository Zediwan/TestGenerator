using System.IO;
using File = TestGenerator.Core.Common.Models.File;

namespace TestGenerator.Core.Generation;

public static class FileCreator
{
    public static void Create(string pathToTestRootFolder, File file)
    {
        if (file.Extension != ".cs") return;

        const string prefix = ""; // TODO: make this a UI setting
        const string suffix = "_test"; // TODO: make this a UI setting
        var fileName = prefix + file.Name.Replace(file.Extension, suffix + file.Extension);
        // Gather the relative path inside the src folder
        var relativePath = file.ParentFolder?.ProjectPath + file.ParentFolder?.Name + "\\";
        var filePath = pathToTestRootFolder + "\\" + relativePath + fileName;

        // Ensure the directory exists
        Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);

        // Properly dispose the FileStream
        if (!System.IO.File.Exists(filePath)) using (System.IO.File.Create(filePath)){}

        System.Windows.MessageBox.Show("File has been created at: " + filePath);
    }
}