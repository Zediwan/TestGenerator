using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TestGenerator.Core.Common.Models;

public class File
{
    public static readonly string Icon = "\ue132";
    private readonly FileInfo _fileInfo;
    public string Name => _fileInfo.Name;
    public string FullPath => _fileInfo.FullName;
    public List<Class> Classes { get; set; }

    public File(FileInfo fileInfo)
    {
        _fileInfo = fileInfo;
        Classes = CSharpSyntaxTree
            .ParseText(System.IO.File.ReadAllText(FullPath))
            .GetRoot()
            .DescendantNodes()
            .OfType<ClassDeclarationSyntax>()
            .Select(c => new Class(c))
            .ToList();
    }

    public new string ToString()
    {
        return this.Name;
    }
}
