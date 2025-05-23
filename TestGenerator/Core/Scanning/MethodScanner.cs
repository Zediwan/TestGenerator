using Microsoft.CodeAnalysis.CSharp.Syntax;
using TestGenerator.Core.Common.Models;

namespace TestGenerator.Core.Scanning;

public class MethodScanner
{
    public static TreeItemViewModel ScanCsMethod(MethodDeclarationSyntax methodDeclarationSyntax)
    {
        return new TreeItemViewModel { Name = GetFormattedMethodSignature(methodDeclarationSyntax), Tag = methodDeclarationSyntax };
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
