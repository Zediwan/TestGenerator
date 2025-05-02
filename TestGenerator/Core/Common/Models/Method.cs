namespace TestGenerator.Core.Common.Models;

public class Method
{
    public string Name { get; set; }
    public string ReturnType { get; set; }
    public string[] Parameters { get; set; } = [];
    public string[] Modifiers { get; set; } = [];

}
