using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TestGenerator.Core.Common.Models;

public class Method
{
    public static readonly string Icon = "\uE945";
    private readonly MethodDeclarationSyntax _syntax;
    public string Name { get; }
    public string ReturnType { get; }
    public List<string> Parameters { get; }
    public List<string> Modifiers { get; }

    public Method(MethodDeclarationSyntax syntax)
    {
        _syntax = syntax;
        Name = _syntax.Identifier.ToString();
        Modifiers = syntax.Modifiers.Select(m => m.Text).ToList();
        Parameters = syntax.ParameterList.Parameters.Select(p => p.ToString()).ToList();
        ReturnType = syntax.ReturnType.ToString();
    }

    public Method(string name, string returnType, List<string> parameters, List<string> modifiers)
    {
        Name = name;
        ReturnType = returnType;
        Parameters = parameters;
        Modifiers = modifiers;
    }

    public new string ToString()
    {
        return $"{string.Join(" ", Modifiers)} {ReturnType} {Name}({string.Join(", ", Parameters)})";
    }
}
