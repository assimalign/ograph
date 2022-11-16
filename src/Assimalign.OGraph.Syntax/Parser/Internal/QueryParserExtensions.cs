using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax.Internal;


internal static class QueryParserExtensions
{
    private static ReadOnlySpan<string> functions => new string[]
    {
        "startswith",
        "endswith",
        "any"
    };
    internal static bool IsIdentifierFunction(this Token token)
    {
        if (token.TokenType != TokenType.Identifier)
        {
            throw new Exception();
        }

        return functions.Contains(token.ValueAsText.ToLower());
    }
}
