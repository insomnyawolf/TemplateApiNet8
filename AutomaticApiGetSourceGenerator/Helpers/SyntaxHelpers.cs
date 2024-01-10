using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SourceGenerator;

public static class SyntaxHelpers
{
    public static bool HasToken(this SyntaxTokenList tokenList, string tokenValue)
    {
        for (int i = 0; i < tokenList.Count; i++)
        {
            var item = tokenList[i];

            if (item.Text == tokenValue)
            {
                return true;
            }
        }

        return false;
    }

    public static bool TryGetAttribute(this MemberDeclarationSyntax member, string name, out AttributeSyntax? AttributeSyntax)
    {
        foreach (var attrList in member.AttributeLists)
        {
            foreach (var attr in attrList.Attributes)
            {
                if (attr.Name.ToString() == name)
                {
                    AttributeSyntax = attr;
                    return true;
                }
            }
        }

        AttributeSyntax = null;
        return false;
    }
}
