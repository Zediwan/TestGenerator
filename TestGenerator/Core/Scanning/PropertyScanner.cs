using Microsoft.CodeAnalysis.CSharp.Syntax;
using TestGenerator.Core.Common.Models;

namespace TestGenerator.Core.Scanning;

public class PropertyScanner
{
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