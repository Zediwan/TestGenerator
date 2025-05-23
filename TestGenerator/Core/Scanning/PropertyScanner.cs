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

        foreach (var accessorNode in prop.AccessorList.Accessors.Select(accessor => new TreeItemViewModel
                 {
                     Name = accessor.Keyword.Text,
                     Tag = accessor,
                     Parent = propNode
                 }))
        {
            propNode.Children.Add(accessorNode);
        }

        return propNode;
    }
}