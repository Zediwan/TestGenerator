using Microsoft.CodeAnalysis.CSharp.Syntax;
using TestGenerator.Core.Common.Models;

namespace TestGenerator.Core.Scanning;

public class ConstructorScanner
{
    public static TreeItemViewModel ScanCsConstructor(ConstructorDeclarationSyntax constructorDeclarationSyntax)
    {
        return new TreeItemViewModel { Name = GetFormattedConstructorSignature(constructorDeclarationSyntax), Tag = constructorDeclarationSyntax};
    }

    private static string GetFormattedConstructorSignature(ConstructorDeclarationSyntax ctor)
    {
        var modifiers = string.Join(" ", ctor.Modifiers.Select(m => m.Text));
        var parameters = ctor.ParameterList.Parameters
            .Select(p => $"{p.Type?.ToFullString()?.Trim()} {p.Identifier.Text}");

        return $"{modifiers} {ctor.Identifier.Text}({string.Join(", ", parameters)})";
    }
}