using Microsoft.CodeAnalysis.CSharp.Syntax;
using TestGenerator.Core.Common.Models;

namespace TestGenerator.Core.Scanning;

/// <summary>
/// Provides methods for scanning C# property declarations and constructing tree representations.
/// </summary>
public class PropertyScanner
{
    /// <summary>
    /// Scans a C# property declaration and creates a <see cref="TreeItemViewModel"/> representing the property.
    /// </summary>
    /// <param name="propertyDeclarationSyntax">The <see cref="Microsoft.CodeAnalysis.CSharp.Syntax.PropertyDeclarationSyntax"/> to scan.</param>
    /// <returns>
    /// A <see cref="TreeItemViewModel"/> representing the property.
    /// </returns>
    public static TreeItemViewModel ScanCsProperty(PropertyDeclarationSyntax propertyDeclarationSyntax)
    {
        var type = propertyDeclarationSyntax.Type?.ToFullString().Trim() ?? "object";
        var name = propertyDeclarationSyntax.Identifier.Text;
        var modifiers = string.Join(" ", propertyDeclarationSyntax.Modifiers.Select(m => m.Text));
        var propLabel = $"{modifiers} {type} {name}".Trim();

        var propNode = new TreeItemViewModel(propLabel, propertyDeclarationSyntax);

        if (propertyDeclarationSyntax.ExpressionBody != null)
        {
            propNode.Children.Add(new TreeItemViewModel("get (expression-bodied)", "get"));
            return propNode;
        }

        if (propertyDeclarationSyntax.AccessorList == null) return propNode;

        foreach (var accessorNode in propertyDeclarationSyntax.AccessorList.Accessors.Select(accessor =>
                     new TreeItemViewModel(accessor.Keyword.Text, accessor) { Parent = propNode }))
            propNode.Children.Add(accessorNode);

        return propNode;
    }
}