using Microsoft.CodeAnalysis.CSharp.Syntax;
using TestGenerator.Core.Common.Models;

namespace TestGenerator.Core.Scanning;

public class ClassScanner
{
    public static TreeItemViewModel ScanCsClass(ClassDeclarationSyntax cls)
    {
        var classNode = new TreeItemViewModel { Name = cls.Identifier.Text, Tag = cls };

        foreach (var method in cls.Members.OfType<MethodDeclarationSyntax>())
        {
            var child = MethodScanner.ScanCsMethod(method);
            child.Parent = classNode;
            classNode.Children.Add(child);
        }

        foreach (var property in cls.Members.OfType<PropertyDeclarationSyntax>())
        {
            var child = PropertyScanner.ScanCsProperty(property);
            child.Parent = classNode;
            classNode.Children.Add(child);
        }

        foreach (var constructor in cls.Members.OfType<ConstructorDeclarationSyntax>())
        {
            var child = ConstructorScanner.ScanCsConstructor(constructor);
            child.Parent = classNode;
            classNode.Children.Add(child);
        }

        return classNode;
    }

    // TODO: this should use the semantic model
    // TODO: test this method
    public static MethodDeclarationSyntax? FindMethod(MethodDeclarationSyntax method, ClassDeclarationSyntax cls)
    {
        return cls.DescendantNodes().OfType<MethodDeclarationSyntax>()
            .FirstOrDefault(m => m.Identifier == method.Identifier);
    }
}
