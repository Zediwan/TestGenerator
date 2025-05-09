using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TestGenerator.Core.Common.Models;

public class Constructor
{
    public static readonly string Icon = "\uE822";
    private readonly ConstructorDeclarationSyntax _syntax;
    public string Name { get; }
    public List<string> Parameters { get; }
    public List<string> Modifiers { get; }

    public Constructor(ConstructorDeclarationSyntax syntax)
    {
        _syntax = syntax;
        Name = _syntax.Identifier.ToString();
        Modifiers = syntax.Modifiers.Select(m => m.Text).ToList();
        Parameters = syntax.ParameterList.Parameters.Select(p => p.ToString()).ToList();
    }

    public new string ToString()
    {
        return $"{string.Join(" ", Modifiers)} {Name}({string.Join(", ", Parameters)})";
    }
}
