using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax.Internal;

internal abstract class Parser 
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="lexer"></param>
    /// <param name="queryNode">Represents the left recursive queryNode currently being parsed.</param>
    /// <returns></returns>
    internal abstract QueryNode Parse(ref TokenLexer lexer, ParserContext context, QueryNode queryNode);


    internal static Parser Create() => new RootParser();
}
