namespace TestGenerator.Core.Common.Models;

public class Folder
{
    public string Name { get; set; }
    public string FullPath { get; set; }
    public Folder[] Subfolders { get; set; }
    public File[] Files { get; set; }
}
