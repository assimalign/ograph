using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax;

using Assimalign.OGraph.Syntax.Lexer;

public abstract class QueryParserException : Exception
{

    public QueryParserException(string message) : base(message) { }


    internal static QueryParserException UnexpectedNode()
    {
        return new QueryParserExceptionDefault("");
    }
    internal static QueryParserException UnexpectedToken(Token Token)
    {
        return new QueryParserExceptionDefault(
            $"An unexpected token was found at location: {Token.Start}");
    }

    internal static QueryParserException InvalidSelect()
    {
        return new QueryParserExceptionDefault("");
    }

    internal static QueryParserException InvalidBinary()
    {
        return new QueryParserExceptionDefault("The ");
    }

    internal static QueryParserException InvalidPage()
    {
        return new QueryParserExceptionDefault("");
    }
}

internal class QueryParserExceptionDefault : QueryParserException
{
    public QueryParserExceptionDefault(string message) : base(message)
    {

    }
}
