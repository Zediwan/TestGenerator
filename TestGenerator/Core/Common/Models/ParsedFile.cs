using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TestGenerator.Core.Common.Models;

public class ParsedFile
{
    public ParsedFile(FileInfo fileInfo)
    {
        FileInfo = fileInfo;
        SyntaxTree = CSharpSyntaxTree.ParseText(File.ReadAllText(fileInfo.FullName));
        SyntaxRoot = SyntaxTree.GetCompilationUnitRoot();
    }

    public FileInfo FileInfo { get; set; }
    public CompilationUnitSyntax SyntaxRoot { get; set; }
    public SyntaxTree SyntaxTree { get; set; }

    public IEnumerable<ClassDeclarationSyntax> Classes => SyntaxRoot.DescendantNodes().OfType<ClassDeclarationSyntax>();

    public IEnumerable<MethodDeclarationSyntax> Methods =>
        SyntaxRoot.DescendantNodes().OfType<MethodDeclarationSyntax>();

    public IEnumerable<ConstructorDeclarationSyntax> Constructors =>
        SyntaxRoot.DescendantNodes().OfType<ConstructorDeclarationSyntax>();

    public IEnumerable<PropertyDeclarationSyntax> Properties =>
        SyntaxRoot.DescendantNodes().OfType<PropertyDeclarationSyntax>();

    public IEnumerable<FieldDeclarationSyntax> Fields => SyntaxRoot.DescendantNodes().OfType<FieldDeclarationSyntax>();

    public IEnumerable<NamespaceDeclarationSyntax> Namespaces =>
        SyntaxRoot.DescendantNodes().OfType<NamespaceDeclarationSyntax>();

    public IEnumerable<UsingDirectiveSyntax> Usings => SyntaxRoot.DescendantNodes().OfType<UsingDirectiveSyntax>();

    public override string ToString()
    {
        return FileInfo.Name;
    }
}