using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TestGenerator.Core.Common.Models;

/// <summary>
/// Represents a parsed C# file, including its syntax tree and semantic information.
/// </summary>
public class ParsedFile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ParsedFile"/> class by parsing the specified file.
    /// </summary>
    /// <param name="fileInfo">The <see cref="FileInfo"/> of the C# file to parse.</param>
    public ParsedFile(FileInfo fileInfo)
    {
        FileInfo = fileInfo;
        SyntaxTree = CSharpSyntaxTree.ParseText(File.ReadAllText(fileInfo.FullName));
        SyntaxRoot = SyntaxTree.GetCompilationUnitRoot();
    }

    /// <summary>
    /// Gets or sets the <see cref="FileInfo"/> representing the file on disk.
    /// </summary>
    public FileInfo FileInfo { get; set; }

    /// <summary>
    /// Gets or sets the root <see cref="CompilationUnitSyntax"/> node of the syntax tree.
    /// </summary>
    public CompilationUnitSyntax SyntaxRoot { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="SyntaxTree"/> representing the parsed C# file.
    /// </summary>
    public SyntaxTree SyntaxTree { get; set; }

    /// <summary>
    /// Gets all class declarations in the file.
    /// </summary>
    public IEnumerable<ClassDeclarationSyntax> Classes => SyntaxRoot.DescendantNodes().OfType<ClassDeclarationSyntax>();

    /// <summary>
    /// Gets all method declarations in the file.
    /// </summary>
    public IEnumerable<MethodDeclarationSyntax> Methods =>
        SyntaxRoot.DescendantNodes().OfType<MethodDeclarationSyntax>();

    /// <summary>
    /// Gets all constructor declarations in the file.
    /// </summary>
    public IEnumerable<ConstructorDeclarationSyntax> Constructors =>
        SyntaxRoot.DescendantNodes().OfType<ConstructorDeclarationSyntax>();

    /// <summary>
    /// Gets all property declarations in the file.
    /// </summary>
    public IEnumerable<PropertyDeclarationSyntax> Properties =>
        SyntaxRoot.DescendantNodes().OfType<PropertyDeclarationSyntax>();

    /// <summary>
    /// Gets all field declarations in the file.
    /// </summary>
    public IEnumerable<FieldDeclarationSyntax> Fields => SyntaxRoot.DescendantNodes().OfType<FieldDeclarationSyntax>();

    /// <summary>
    /// Gets all namespace declarations in the file.
    /// </summary>
    public IEnumerable<NamespaceDeclarationSyntax> Namespaces =>
        SyntaxRoot.DescendantNodes().OfType<NamespaceDeclarationSyntax>();

    /// <summary>
    /// Gets all using directives in the file.
    /// </summary>
    public IEnumerable<UsingDirectiveSyntax> Usings => SyntaxRoot.DescendantNodes().OfType<UsingDirectiveSyntax>();

    /// <summary>
    /// Returns the file name of the parsed file.
    /// </summary>
    /// <returns>The name of the file as a string.</returns>
    public override string ToString()
    {
        return FileInfo.Name;
    }
}