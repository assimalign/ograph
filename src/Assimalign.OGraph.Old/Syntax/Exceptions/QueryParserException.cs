using System;

namespace Assimalign.OGraph.Syntax;

using Assimalign.OGraph.Syntax.Internal;

public sealed class QueryParserException : Exception
{
    public QueryParserException(string message) : base(message) { }
    internal QueryParserException(TokenLexerException exception) { }


    internal static QueryParserException UnexpectedQueryNode(Type expected, Type actual)
    {
        throw new QueryParserException($"An invalid token was passed while paring. Expected '{expected}'. Actual '{actual}'.");
    }
}