using Microsoft.CodeAnalysis.CSharp.Syntax;
using TestGenerator.Core.Common.Models;

namespace TestGenerator.Core.Scanning;

/// <summary>
/// Provides methods for scanning C# constructor declarations and constructing tree representations.
/// </summary>
public class ConstructorScanner
{
    /// <summary>
    /// Scans a C# constructor declaration and creates a <see cref="TreeItemViewModel"/> representing the constructor.
    /// </summary>
    /// <param name="constructorDeclarationSyntax">The <see cref="Microsoft.CodeAnalysis.CSharp.Syntax.ConstructorDeclarationSyntax"/> to scan.</param>
    /// <returns>
    /// A <see cref="TreeItemViewModel"/> representing the constructor.
    /// </returns>
    public static TreeItemViewModel ScanCsConstructor(ConstructorDeclarationSyntax constructorDeclarationSyntax)
    {
        return new TreeItemViewModel(GetFormattedConstructorSignature(constructorDeclarationSyntax),
            constructorDeclarationSyntax);
    }

    private static string GetFormattedConstructorSignature(ConstructorDeclarationSyntax ctor)
    {
        var modifiers = string.Join(" ", ctor.Modifiers.Select(m => m.Text));
        var parameters = ctor.ParameterList.Parameters
            .Select(p => $"{p.Type?.ToFullString()?.Trim()} {p.Identifier.Text}");

        return $"{modifiers} {ctor.Identifier.Text}({string.Join(", ", parameters)})";
    }
}