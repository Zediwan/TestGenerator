using Microsoft.CodeAnalysis.CSharp.Syntax;
using TestGenerator.Core.Common.Models;

namespace TestGenerator.Core.Scanning;

public class PropertyScanner
{
    public static TreeItemViewModel ScanCsProperty(PropertyDeclarationSyntax prop)
    {
        var type = prop.Type?.ToFullString().Trim() ?? "object";
        var name = prop.Identifier.Text;
        var modifiers = string.Join(" ", prop.Modifiers.Select(m => m.Text));
        var propLabel = $"{modifiers} {type} {name}".Trim();

        var propNode = new TreeItemViewModel
        {
            Name = propLabel,
            Tag = prop // full property
        };

        if (prop.ExpressionBody != null)
        {
            propNode.Children.Add(new TreeItemViewModel
            {
                Name = "get (expression-bodied)",
                Tag = "get"
            });
            return propNode;
        }

        if (prop.AccessorList == null) return propNode;

        foreach (var accessor in prop.AccessorList.Accessors)
        {
            var accessorNode = new TreeItemViewModel
            {
                Name = accessor.Keyword.Text,
                Tag = accessor // could be string like "get" or actual Syntax
            };

            propNode.Children.Add(accessorNode);
        }

        return propNode;
    }
}