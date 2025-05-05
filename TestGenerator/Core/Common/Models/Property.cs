using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TestGenerator.Core.Common.Models;

public class Property
{
    private readonly PropertyDeclarationSyntax _syntax;
    public string Name { get; set; }
    public string Type { get; set; }
    public string Modifier { get; }
    public Method? Getter { get; set; }
    public Method? Setter { get; set; }

    public Property(PropertyDeclarationSyntax syntax)
    {
        _syntax = syntax;
        Name = _syntax.Identifier.Text;
        Type = _syntax.Type.ToString();
        Modifier = _syntax.Modifiers.ToString();

        if (syntax.AccessorList == null) return;

        foreach (var accessor in syntax.AccessorList.Accessors)
        {
            var methodName = accessor.Keyword.Text; // "get" or "set"
            var modifiers = accessor.Modifiers.Select(m => m.Text).ToList();
            var returnType = Type; // Same as property type

            // Construct a Method-like object (you may want a separate class like AccessorMethod for this)
            var method = new Method(methodName, returnType, [], modifiers);

            switch (methodName)
            {
                case "get":
                    Getter = method;
                    break;
                case "set":
                    Setter = method;
                    break;
            }
        }


    }
    public new string ToString()
    {
        return $"{Modifier} {Type} {Name}";
    }
}

