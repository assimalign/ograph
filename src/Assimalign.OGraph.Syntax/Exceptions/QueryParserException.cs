using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax;

using Assimalign.OGraph.Syntax.Internal;

public sealed class QueryParserException : Exception
{
    public QueryParserException(string message) : base(message) { }

    internal QueryParserException(TokenLexerException exception)
    {

    }


    
}