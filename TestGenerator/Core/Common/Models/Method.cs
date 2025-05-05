using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TestGenerator.Core.Common.Models;

public class Method
{
    private readonly MethodDeclarationSyntax _syntax;
    public string Name => _syntax.Identifier.ToString();
    public string ReturnType { get; }
    public List<string> Parameters { get; }
    public List<string> Modifiers { get; }

    public Method(MethodDeclarationSyntax syntax)
    {
        _syntax = syntax;
        Modifiers = syntax.Modifiers.Select(m => m.Text).ToList();
        Parameters = syntax.ParameterList.Parameters.Select(p => p.ToString()).ToList();
        ReturnType = syntax.ReturnType.ToString();
    }

    public new string ToString()
    {
        return $"{string.Join(" ", Modifiers)} {ReturnType} {Name}({_syntax.ParameterList})";
    }
}
