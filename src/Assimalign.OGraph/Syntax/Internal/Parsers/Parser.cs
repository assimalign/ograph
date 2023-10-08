using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax.Internal;

internal abstract class Parser
{

    internal abstract QueryNode Parse(ref TokenLexer lexer, ParserContext context, QueryNode queryNode);


    internal static Parser Create() => new VertexParser();
}
