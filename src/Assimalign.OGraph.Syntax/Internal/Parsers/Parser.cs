using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax.Internal;

internal abstract class Parser 
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="lexer"></param>
    /// <param name="node">Represents the left recursive node currently being parsed.</param>
    /// <returns></returns>
    internal abstract QueryNode Parse(ref TokenLexer lexer, ParserContext context, QueryNode node);


    internal static Parser Create() => new RootParser();
}
