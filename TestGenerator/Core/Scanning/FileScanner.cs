using System.IO;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TestGenerator.Core.Common.Models;

namespace TestGenerator.Core.Scanning;

/// <summary>
/// Provides methods for scanning C# files and constructing tree representations.
/// </summary>
public static class FileScanner
{
    /// <summary>
    /// Scans a C# file and creates a <see cref="TreeItemViewModel"/> representing the file and its classes.
    /// </summary>
    /// <param name="fileInfo">The <see cref="FileInfo"/> of the C# file to scan.</param>
    /// <returns>
    /// A <see cref="TreeItemViewModel"/> representing the file, with children for its classes.
    /// </returns>
    public static TreeItemViewModel ScanCsFile(FileInfo fileInfo)
    {
        var parsedFile = new ParsedFile(fileInfo);

        var fileNode = new TreeItemViewModel(fileInfo.Name, parsedFile);

        #region Classes

        foreach (var cls in parsedFile.Classes)
        {
            var child = ClassScanner.ScanCsClass(cls);
            child.Parent = fileNode;
            fileNode.Children.Add(child);
        }

        #endregion

        return fileNode;
    }

    // TODO: this should use the semantic model
    // TODO: test this method
    /// <summary>
    /// Finds a method in the specified parsed file that matches the given method declaration.
    /// </summary>
    /// <param name="methodDeclarationSyntax">The method declaration to find.</param>
    /// <param name="parsedFile">The <see cref="ParsedFile"/> in which to search for the method.</param>
    /// <returns>
    /// The matching <see cref="MethodDeclarationSyntax"/> if found; otherwise, <c>null</c>.
    /// </returns>
    public static MethodDeclarationSyntax? FindMethod(MethodDeclarationSyntax methodDeclarationSyntax,
        ParsedFile parsedFile)
    {
        return parsedFile.Methods.FirstOrDefault(m => m.Identifier.Text == methodDeclarationSyntax.Identifier.Text);
        ;
    }
}