using Microsoft.CodeAnalysis.CSharp.Syntax;
using TestGenerator.Core.Common.Models;

namespace TestGenerator.Core.Scanning;

public class ClassScanner
{
    public static TreeItemViewModel ScanCsClass(ClassDeclarationSyntax classDeclarationSyntax)
    {
        var classNode = new TreeItemViewModel { Name = classDeclarationSyntax.Identifier.Text, Tag = classDeclarationSyntax };

        foreach (var method in classDeclarationSyntax.Members.OfType<MethodDeclarationSyntax>())
        {
            var child = MethodScanner.ScanCsMethod(method);
            child.Parent = classNode;
            classNode.Children.Add(child);
        }

        foreach (var property in classDeclarationSyntax.Members.OfType<PropertyDeclarationSyntax>())
        {
            var child = PropertyScanner.ScanCsProperty(property);
            child.Parent = classNode;
            classNode.Children.Add(child);
        }

        foreach (var constructor in classDeclarationSyntax.Members.OfType<ConstructorDeclarationSyntax>())
        {
            var child = ConstructorScanner.ScanCsConstructor(constructor);
            child.Parent = classNode;
            classNode.Children.Add(child);
        }

        return classNode;
    }

    // TODO: this should use the semantic model
    // TODO: test this method
    public static MethodDeclarationSyntax? FindMethod(MethodDeclarationSyntax methodDeclarationSyntax, ClassDeclarationSyntax classDeclarationSyntax)
    {
        return classDeclarationSyntax.DescendantNodes().OfType<MethodDeclarationSyntax>()
            .FirstOrDefault(m => m.Identifier == methodDeclarationSyntax.Identifier);
    }
}
