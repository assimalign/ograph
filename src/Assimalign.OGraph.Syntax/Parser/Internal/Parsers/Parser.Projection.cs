using System;

namespace Assimalign.OGraph.Syntax.Internal;

using Assimalign.OGraph.Syntax.Lexer;

internal class ProjectionParser : Parser
{
    internal override QueryNode Parse(ref TokenLexer lexer, ParserContext context, QueryNode node)
    {
        if (node is not RootQueryNode root)
        {
            throw QueryParserException.InvalidPage();
        }

        var token = lexer.Next();

        if (token.TokenType != TokenType.OpenParenthesis) // The projection clause MUST follow an open parenthesis token
        {
            
        }
        if (context.Parse(ref lexer, new ProjectionQueryNode()) is not ProjectionQueryNode projectionNode)
        {
            throw QueryParserException.UnexpectedNode();
        }

        root.AddNode(projectionNode);

        return root;
    }
}
