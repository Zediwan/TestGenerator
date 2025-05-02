using System.IO;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace TestGenerator.Core.Scanning;

public static class Scanner
{
    private const char OffsetChar = ' ';

    public static void LogFolderStructure(string path, TextBlock logTextBlock)
    {
        logTextBlock.Text += "Scanning started...\n";
        LogFolderStructure(new DirectoryInfo(path), logTextBlock);
    }

    private static void LogFolderStructure(DirectoryInfo dirInfo, TextBlock logTextBlock, int offset = 0)
    {
        var offsetString = new string(OffsetChar, offset * 5) + "-";
        var fileOffsetString = new string(OffsetChar, (offset + 1) * 5) + "-";

        logTextBlock.Text += offsetString + dirInfo.Name + "\n";

        var subfolders = dirInfo.GetDirectories();
        var files = dirInfo.GetFiles();


        foreach (var subfolderInfo in subfolders)
        {
            LogFolderStructure(subfolderInfo, logTextBlock, offset + 1);
        }

        foreach (var fileInfo in files)
        {
            logTextBlock.Text += fileOffsetString + fileInfo.Name + "\n";
        }
    }
}
