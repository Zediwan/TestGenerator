using System.IO;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TestGenerator.Core.Common.Models;

namespace TestGenerator.Core.Scanning;

public static class FileScanner
{
    public static TreeItemViewModel ScanCsFile(FileInfo fileInfo)
    {
        var parsedFile = new ParsedFile(fileInfo);

        var fileNode = new TreeItemViewModel(fileInfo.Name, parsedFile);

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
    public static MethodDeclarationSyntax? FindMethod(MethodDeclarationSyntax methodDeclarationSyntax,
        ParsedFile parsedFile)
    {
        return parsedFile.Methods.FirstOrDefault(m => m.Identifier.Text == methodDeclarationSyntax.Identifier.Text);
        ;
    }

    // TODO: this should use the semantic model
    // TODO: test this method
    public static ClassDeclarationSyntax? FindClass(ClassDeclarationSyntax classDeclarationSyntax,
        ParsedFile parsedFile)
    {
        return parsedFile.Classes.FirstOrDefault(c => c.Identifier.Text == classDeclarationSyntax.Identifier.Text);
    }
}