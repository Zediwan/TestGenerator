using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TestGenerator.Core.Common.Models;

namespace TestGenerator.Core.Scanning;

public static class FileScanner
{
    public static void Scan(File file)
    {
        file.Classes = CSharpSyntaxTree
            .ParseText(System.IO.File.ReadAllText(file.FullPath))
            .GetRoot()
            .DescendantNodes()
            .OfType<ClassDeclarationSyntax>()
            .Select(c => new Class(c))
            .ToList();

        foreach (var cls in file.Classes) ClassScanner.Scan(cls);
    }
}
