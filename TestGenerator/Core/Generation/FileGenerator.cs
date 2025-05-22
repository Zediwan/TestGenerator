using System.IO;

namespace TestGenerator.Core.Generation;

//public static class FileGenerator
//{
//    public static File Create(string pathToTestRootFolder, File file, string prefix, string suffix)
//    {
//        if (file.Extension != ".cs") throw new InvalidDataException("File is not a C# file.");

//        var fileName = prefix + file.Name;

//        if (!string.IsNullOrEmpty(suffix)) fileName = fileName.Replace(file.Extension, suffix + file.Extension);

//        // Gather the relative path inside the src folder
//        var relativePath = file.ParentFolder?.ProjectPath + file.ParentFolder?.Name + "\\";
//        var filePath = pathToTestRootFolder + "\\" + relativePath + fileName;

//        // Ensure the directory exists
//        Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);

//        // Properly dispose the FileStream
//        if (!System.IO.File.Exists(filePath)) using (System.IO.File.Create(filePath)){}

//        var testFileInfo = new FileInfo(filePath);
//        var testFile = new File(testFileInfo, file.ParentFolder);

//        System.Windows.MessageBox.Show("File has been created at: " + filePath);

//        return testFile;
//    }
//}