using Microsoft.CodeAnalysis.CSharp.Syntax;
using TestGenerator.Core.Common.Models;

namespace TestGenerator.Core.Scanning;

public class MethodScanner
{
    public static TreeItemViewModel ScanCsMethod(MethodDeclarationSyntax method)
    {
        return new TreeItemViewModel { Name = GetFormattedMethodSignature(method), Tag = method };
    }

    private static string GetFormattedMethodSignature(MethodDeclarationSyntax method)
    {
        var parameters = method.ParameterList.Parameters
            .Select(p => $"{p.Type?.ToFullString()?.Trim()} {p.Identifier.Text}");

        return $"{string.Join(" ", method.Modifiers.Select(m => m.Text))} " +
               $"{method.ReturnType.ToFullString().Trim()} " +
               $"{method.Identifier.Text}({string.Join(", ", parameters)})"; ;
    }
}
