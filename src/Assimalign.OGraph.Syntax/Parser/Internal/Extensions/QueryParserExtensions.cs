using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax.Internal;

using Assimalign.OGraph.Syntax.Lexer;


internal static class QueryParserExtensions
{
    private static ReadOnlySpan<string> functions => new string[]
    {
        // string functions
        "startswith",
        "endswith",
        "tolower",
        "toupper",
        "concat",
        "contains", // contains(identifier arg1, string arg2)

        "any",
        "in",

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
