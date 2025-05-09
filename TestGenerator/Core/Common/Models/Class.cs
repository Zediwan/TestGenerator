using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TestGenerator.Core.Common.Models;

public class Class
{
    public static readonly string Icon = "\uE8A5";
    private readonly ClassDeclarationSyntax _syntax;

    public readonly List<string> Modifiers;

    public readonly string Name;
    public string Namespace { get; set; }
    public List<Method> Methods { get; set; }
    
    public List<FieldDeclarationSyntax> Fields;
    public List<Property> Properties;
    public List<Constructor> Constructors;

    public Class(ClassDeclarationSyntax classDeclarationSyntax)
    {
        if (classDeclarationSyntax.Keyword.Text != "class")
        {
            throw new ArgumentException("ClassDeclarationSyntax must be a class declaration.");
        }

        _syntax = classDeclarationSyntax;
        Modifiers = _syntax.Modifiers.Select(m => m.ToString()).ToList();

        Name = _syntax.Identifier.Text;
        Namespace = _syntax.Parent.ToString();
        Constructors = _syntax.DescendantNodes()
            .OfType<ConstructorDeclarationSyntax>()
            .Select(cm => new Constructor(cm))
            .ToList();
        Methods = _syntax.DescendantNodes()
            .OfType<MethodDeclarationSyntax>()
            .Select(m => new Method(m))
            .ToList();
        Fields = _syntax.DescendantNodes().OfType<FieldDeclarationSyntax>().ToList();
        Properties = _syntax.DescendantNodes()
            .OfType<PropertyDeclarationSyntax>()
            .Select(p => new Property(p))
            .ToList();
    }
    public new string ToString()
    {
        return this.Name;
    }
}
