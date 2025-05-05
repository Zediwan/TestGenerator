using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TestGenerator.Core.Common.Models;

public class File
{
    private readonly FileInfo _fileInfo;
    public string Name => _fileInfo.Name;
    public string FullPath => _fileInfo.FullName;
    public string Code { get; set; }
    public List<Class> Classes { get; set; }
    public SyntaxTree SyntaxTree { get; set; }
    public List<Method> Methods { get; set; }

    public File(FileInfo fileInfo)
    {
        _fileInfo = fileInfo;
        Code = System.IO.File.ReadAllText(FullPath);
        SyntaxTree = CSharpSyntaxTree.ParseText(Code);

        Classes = SyntaxTree
            .GetRoot()
            .DescendantNodes()
            .OfType<ClassDeclarationSyntax>()
            .ToList()
            .Select(c => new Class(c))
            .ToList();

        Methods = SyntaxTree
            .GetRoot()
            .DescendantNodes()
            .OfType<MethodDeclarationSyntax>()
            .ToList()
            .Select(m => new Method(m))
            .ToList();
    }

    public new string ToString()
    {
        return this.Name;
    }
}
