using System.IO;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TestGenerator.Core.Common.Models;

namespace TestGenerator.Core.Scanning;

public static class FileScanner
{
    public static TreeItemViewModel ScanCsFile(FileInfo file)
    {
        var sourceCode = System.IO.File.ReadAllText(file.FullName);
        var tree = CSharpSyntaxTree.ParseText(sourceCode);
        var root = tree.GetCompilationUnitRoot();

        var parsedFile = new ParsedFile
        {
            FileInfo = file,
            SyntaxTree = tree,
            SyntaxRoot = root
        };

        var fileNode = new TreeItemViewModel { Name = file.Name, Tag = parsedFile };

        foreach (var cls in parsedFile.Classes)
        {
            var child = ClassScanner.ScanCsClass(cls);
            child.Parent = fileNode;
            fileNode.Children.Add(child);
        }

        return fileNode;
    }


    // TODO: this should use the semantic model
    // TODO: test this method
    public static MethodDeclarationSyntax? FindMethod(MethodDeclarationSyntax method, ParsedFile file)
    {
        return file.Methods.FirstOrDefault(m => m.Identifier == method.Identifier); ;
    }

    // TODO: this should use the semantic model
    // TODO: test this method
    public static ClassDeclarationSyntax? FindClass(ClassDeclarationSyntax cls, ParsedFile file)
    {
        return file.Classes.FirstOrDefault(c => c.Identifier == cls.Identifier);
    }
}
