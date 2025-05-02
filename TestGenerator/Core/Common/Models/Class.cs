namespace TestGenerator.Core.Common.Models;

public class Class
{
    public string Name { get; set; }
    public string Namespace { get; set; }
    public Method[] ScannedMethods { get; set; } = [];
}
