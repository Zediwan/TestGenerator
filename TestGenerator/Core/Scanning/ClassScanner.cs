using Microsoft.CodeAnalysis.CSharp.Syntax;
using TestGenerator.Core.Common.Models;

namespace TestGenerator.Core.Scanning;

/// <summary>
/// Provides methods for scanning C# class declarations and constructing tree representations.
/// </summary>
public class ClassScanner
{
    /// <summary>
    /// Scans a C# class declaration and creates a <see cref="TreeItemViewModel"/> representing the class and its members.
    /// </summary>
    /// <param name="classDeclarationSyntax">The <see cref="ClassDeclarationSyntax"/> to scan.</param>
    /// <returns>
    /// A <see cref="TreeItemViewModel"/> representing the class, with children for its methods, properties, and constructors.
    /// </returns>
    public static TreeItemViewModel ScanCsClass(ClassDeclarationSyntax classDeclarationSyntax)
    {
        var classNode = new TreeItemViewModel(classDeclarationSyntax.Identifier.Text, classDeclarationSyntax);

        #region Methods

        foreach (var method in classDeclarationSyntax.Members.OfType<MethodDeclarationSyntax>())
        {
            var child = MethodScanner.ScanCsMethod(method);
            child.Parent = classNode;
            classNode.Children.Add(child);
        }

        #endregion

        #region Properties

        foreach (var property in classDeclarationSyntax.Members.OfType<PropertyDeclarationSyntax>())
        {
            var child = PropertyScanner.ScanCsProperty(property);
            child.Parent = classNode;
            classNode.Children.Add(child);
        }

        #endregion

        #region Constructors

        foreach (var constructor in classDeclarationSyntax.Members.OfType<ConstructorDeclarationSyntax>())
        {
            var child = ConstructorScanner.ScanCsConstructor(constructor);
            child.Parent = classNode;
            classNode.Children.Add(child);
        }

        #endregion

        return classNode;
    }

    // TODO: this should use the semantic model
    // TODO: test this method
    /// <summary>
    /// Finds a method in the specified class that matches the given method declaration.
    /// </summary>
    /// <param name="methodDeclarationSyntax">The method declaration to find.</param>
    /// <param name="classDeclarationSyntax">The class in which to search for the method.</param>
    /// <returns>
    /// The matching <see cref="MethodDeclarationSyntax"/> if found; otherwise, <c>null</c>.
    /// </returns>
    public static MethodDeclarationSyntax? FindMethod(MethodDeclarationSyntax methodDeclarationSyntax,
        ClassDeclarationSyntax classDeclarationSyntax)
    {
        return classDeclarationSyntax.DescendantNodes().OfType<MethodDeclarationSyntax>()
            .FirstOrDefault(m => m.Identifier == methodDeclarationSyntax.Identifier);
    }
}