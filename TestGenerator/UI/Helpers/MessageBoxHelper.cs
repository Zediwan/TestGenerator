using System.Windows;
using MessageBox = System.Windows.MessageBox;

namespace TestGenerator.UI.Helpers;

public static class MessageBoxHelper
{
    public static bool ConfirmFileNaming()
    {
        var result = MessageBox.Show(
            "You haven't set any Prefix or Suffix for the Test File Names. Are you sure you want to continue?",
            "Warning",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);

        return result == MessageBoxResult.Yes;
    }

    public static void ShowInvalidFolderPath()
    {
        MessageBox.Show(
            "Invalid folder path. Please check the path and try again.",
            "Error",
            MessageBoxButton.OK,
            MessageBoxImage.Error);
    }

    public static void DirectoryCreated(string directoryName)
    {
        MessageBox.Show(
            "Directory has been created at: " + directoryName,
            "Directory Creation",
            MessageBoxButton.OK,
            MessageBoxImage.Information);
    }

    public static void FileCreated(string filePath)
    {
        MessageBox.Show(
            "File has been created at: " + filePath,
            "File Creation",
            MessageBoxButton.OK,
            MessageBoxImage.Information);
    }

    public static void FileAlreadyExists(string filePath)
    {
        MessageBox.Show(
            "File already exists at: " + filePath,
            "File Creation",
            MessageBoxButton.OK,
            MessageBoxImage.Warning);
    }
}
