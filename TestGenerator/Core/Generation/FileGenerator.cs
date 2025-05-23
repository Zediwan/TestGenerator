using System.IO;
using TestGenerator.Core.Common.Models;

namespace TestGenerator.Core.Generation;

public class FileGenerator(string prefix, string suffix, string pathToTestRootFolder, string pathToSourceRootFolder)
{
    // Paths
    public string PathToSourceRootFolder = pathToSourceRootFolder;
    public string PathToTestRootFolder = pathToTestRootFolder;

    // File Naming
    public string Prefix = prefix;
    public string Suffix = suffix;

    public ParsedFile Create(ParsedFile file)
    {
        #region FileName

        if (file.FileInfo.Extension != ".cs") throw new InvalidDataException("File is not a C# file.");

        var fileName = Prefix + file.FileInfo.Name;

        if (!string.IsNullOrEmpty(Suffix))
            fileName = fileName.Replace(file.FileInfo.Extension, Suffix + file.FileInfo.Extension);

        #endregion

        #region FilePath

        var filePath = GetFullPathForTest(file.FileInfo, fileName);

        #endregion

        #region Directory

        // Ensure the directory exists
        var directoryName = Path.GetDirectoryName(filePath);
        if (directoryName == null)
            throw new InvalidOperationException("The directory name could not be determined. Ensure the file path is not a root path.");
        if (!Directory.Exists(directoryName))
        {
            MessageBox.Show("Directory has been created at: " + directoryName);
            Directory.CreateDirectory(directoryName);
        }

        #endregion

        #region File

        if (!File.Exists(filePath))
        {
            // If the file does not exist create it
            using (File.Create(filePath));
            System.Windows.MessageBox.Show("File has been created at: " + filePath);
        }
        else
            System.Windows.MessageBox.Show("File already exists: " + filePath);

        #endregion

        return new ParsedFile(new FileInfo(filePath));
    }

    private string GetFullPathForTest(FileInfo fileInfo, string testFileName)
    {
        var fullTestPath = fileInfo.FullName;
        fullTestPath = fullTestPath.Replace(fileInfo.Name, testFileName);
        fullTestPath = fullTestPath.Replace(PathToSourceRootFolder, PathToTestRootFolder);
        return fullTestPath;
    }
}