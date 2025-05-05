using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TestGenerator.Core.Common.Models;

public class Class
{
    private readonly ClassDeclarationSyntax _syntax;
    public readonly string Name;
    public string Namespace { get; set; }
    public List<Method> Methods { get; set; }
    public List<string> Modifiers;
    public List<FieldDeclarationSyntax> Fields;

    public Class(ClassDeclarationSyntax classDeclarationSyntax)
    {
        if (classDeclarationSyntax.Keyword.Text != "class")
        {
            throw new ArgumentException("ClassDeclarationSyntax must be a class declaration.");
        }

        _syntax = classDeclarationSyntax;
        Name = _syntax.Identifier.Text;
        Namespace = _syntax.Parent.ToString();
        Modifiers = _syntax.Modifiers.Select(m => m.ToString()).ToList();
        Methods = _syntax.DescendantNodes()
            .OfType<MethodDeclarationSyntax>()
            .ToList()
            .Select(m => new Method(m))
            .ToList();
        Fields = _syntax.DescendantNodes().OfType<FieldDeclarationSyntax>().ToList();
    }
    public new string ToString()
    {
        return this.Name;
    }
}
