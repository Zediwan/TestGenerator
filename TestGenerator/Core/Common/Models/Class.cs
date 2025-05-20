using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TestGenerator.Core.Common.Models;

public class Class(ClassDeclarationSyntax classDeclarationSyntax)
{
    public static readonly string Icon = "\uE8A5";
    public readonly ClassDeclarationSyntax Syntax = classDeclarationSyntax;

    public string Name => Syntax.Identifier.Text;
    public string Namespace => Syntax.Parent?.ToString() ?? string.Empty;

    public List<string> Modifiers = [];
    public List<Method> Methods { get; set; } = [];
    public List<FieldDeclarationSyntax> Fields = [];
    public List<Property> Properties = [];
    public List<Constructor> Constructors = [];

    public new string ToString() => Name;
}
