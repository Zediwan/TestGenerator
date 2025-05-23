using Microsoft.CodeAnalysis.CSharp.Syntax;
using TestGenerator.Core.Common.Models;

namespace TestGenerator.Core.Scanning;

/// <summary>
/// Provides methods for scanning C# method declarations and constructing tree representations.
/// </summary>
public class MethodScanner
{
    /// <summary>
    /// Scans a C# method declaration and creates a <see cref="TreeItemViewModel"/> representing the method.
    /// </summary>
    /// <param name="methodDeclarationSyntax">The <see cref="Microsoft.CodeAnalysis.CSharp.Syntax.MethodDeclarationSyntax"/> to scan.</param>
    /// <returns>
    /// A <see cref="TreeItemViewModel"/> representing the method.
    /// </returns>
    public static TreeItemViewModel ScanCsMethod(MethodDeclarationSyntax methodDeclarationSyntax)
    {
        return new TreeItemViewModel(GetFormattedMethodSignature(methodDeclarationSyntax), methodDeclarationSyntax);
    }

    private static string GetFormattedMethodSignature(MethodDeclarationSyntax methodDeclarationSyntax)
    {
        var parameters = methodDeclarationSyntax.ParameterList.Parameters
            .Select(p => $"{p.Type?.ToFullString()?.Trim()} {p.Identifier.Text}");

        return $"{string.Join(" ", methodDeclarationSyntax.Modifiers.Select(m => m.Text))} " +
               $"{methodDeclarationSyntax.ReturnType.ToFullString().Trim()} " +
               $"{methodDeclarationSyntax.Identifier.Text}({string.Join(", ", parameters)})";
    }
}