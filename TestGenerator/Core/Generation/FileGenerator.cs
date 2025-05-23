using System.IO;
using System.Windows;
using TestGenerator.Core.Common.Models;
using TestGenerator.UI.Helpers;

namespace TestGenerator.Core.Generation;

public class FileGenerator(string prefix, string suffix, string pathToTestRootFolder, string pathToSourceRootFolder)
{
    // Paths
    // TODO: figure out the best practice to keep them consistent across Generators

    /// <summary>
    /// Gets or sets the path to the root folder of the source code.
    /// </summary>
    public string PathToSourceRootFolder = pathToSourceRootFolder;

    /// <summary>
    /// Gets or sets the path to the root folder of the test code.
    /// </summary>
    public string PathToTestRootFolder = pathToTestRootFolder;

    // File Naming

    /// <summary>
    /// Gets or sets the prefix to be added to the file name.
    /// </summary>
    public string Prefix = prefix;

    /// <summary>
    /// Gets or sets the suffix to be added to the file name.
    /// </summary>
    public string Suffix = suffix;

    /// <summary>
    /// Creates a test file based on the provided <see cref="ParsedFile"/> information.
    /// Ensures the directory and file exist, creating them if necessary.
    /// </summary>
    /// <param name="file">The <see cref="ParsedFile"/> representing the source file to generate a test for.</param>
    /// <returns>A <see cref="ParsedFile"/> representing the generated or existing test file.</returns>
    /// <exception cref="InvalidDataException">Thrown if the file is not a C# file.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the directory name cannot be determined.</exception>
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
            throw new InvalidOperationException(
                "The directory name could not be determined. Ensure the file path is not a root path.");
        if (!Directory.Exists(directoryName))
        {
            Directory.CreateDirectory(directoryName);
            MessageBoxHelper.DirectoryCreated(directoryName);
        }

        #endregion

        #region File

        if (!File.Exists(filePath))
        {
            // If the file does not exist create it
            using (File.Create(filePath))
            {
                ;
            }

            MessageBoxHelper.FileCreated(filePath);
        }
        else
            MessageBoxHelper.FileAlreadyExists(filePath);

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