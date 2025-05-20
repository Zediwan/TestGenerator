using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using TestGenerator.Core.Common.Models;

namespace TestGenerator.Core.Generation;

public static class MethodGenerator
{
    public static void Generate(string path, Method method, string prefix, string suffix)
    {
        var className = string.Empty;
        var namespaceName = string.Empty;
        var content = string.Empty;

        // Create the method name from the prefix and suffix
        var testMethodName = string.Empty;
        if (!string.IsNullOrEmpty(prefix)) testMethodName += prefix;
        testMethodName += method.Name;
        if (!string.IsNullOrEmpty(suffix)) testMethodName += suffix;

        // Define the method to be added
        var methodDeclarationSyntax = SyntaxFactory.MethodDeclaration(SyntaxFactory.ParseTypeName("void"), testMethodName)
            .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
            .WithBody(SyntaxFactory.Block(SyntaxFactory.ParseStatement(content)));

        // Define the class the method should be added to
        var classDeclarationSyntax = SyntaxFactory.ClassDeclaration(className)
            .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
            .AddMembers(methodDeclarationSyntax);

        // Create a namespace and add the class
        var namespaceDeclarationSyntax = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName(namespaceName))
            .AddMembers(classDeclarationSyntax);

        // Create a compilation unit and add the namespace
        var compilationUnit = SyntaxFactory.CompilationUnit()
            .AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System")))
            .AddMembers(namespaceDeclarationSyntax);

        // Normalize the whitespace and convert to string
        var code = compilationUnit.NormalizeWhitespace().ToFullString();

        // Append the generated code to the file
        System.IO.File.AppendAllText(path, code);

        // Optionally, you can also display a message box to inform the user
        System.Windows.MessageBox.Show($"Method '{testMethodName}' has been generated in {path}");
    }
}