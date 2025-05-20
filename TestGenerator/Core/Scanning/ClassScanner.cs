using Microsoft.CodeAnalysis.CSharp.Syntax;
using TestGenerator.Core.Common.Models;

namespace TestGenerator.Core.Scanning;

public class ClassScanner
{
    public static void Scan(Class cls)
    {
        cls.Modifiers = cls.Syntax.Modifiers.Select(m => m.ToString()).ToList();

        cls.Constructors = cls.Syntax.DescendantNodes()
            .OfType<ConstructorDeclarationSyntax>()
            .Select(cm => new Constructor(cm))
            .ToList();
        cls.Methods = cls.Syntax.DescendantNodes()
            .OfType<MethodDeclarationSyntax>()
            .Select(m => new Method(m))
            .ToList();
        cls.Fields = cls.Syntax.DescendantNodes().OfType<FieldDeclarationSyntax>().ToList();
        cls.Properties = cls.Syntax.DescendantNodes()
            .OfType<PropertyDeclarationSyntax>()
            .Select(p => new Property(p))
            .ToList();

        foreach (var constructor in cls.Constructors) ConstructorScanner.Scan(constructor);
        foreach (var method in cls.Methods) MethodScanner.Scan(method);
        foreach (var property in cls.Properties) PropertyScanner.Scan(property);
    }
}
