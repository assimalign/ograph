using System;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax.Internal;

internal class ProjectionParser : Parser
{
    internal override QueryNode Parse(ref TokenLexer lexer, ParserContext context, QueryNode queryNode)
    {
        if (queryNode is not ProjectionQueryNode projectionNode)
        {
            // This is internal error. Some dumbass messed with the code.
            return queryNode;
        }
        if (!lexer.HasNext)
        {
            context.AddUnexpectedEOFError(ref lexer);
            return queryNode;
        }

        var token = lexer.Next();

        if (token.TokenType != TokenType.OpenParenthesis)
        {
            // TODO: Add diagnostic error. Expected starting parenthesis block
            return queryNode;
        }

        return ParseParenthesisBlock(ref lexer, context, projectionNode);
    }
    private ProjectionQueryNode ParseParenthesisBlock(ref TokenLexer lexer, ParserContext context, ProjectionQueryNode queryNode)
    {
        var next = default(Token);

        if (!lexer.TryPeek(out next))
        {
            // TODO: Add Diagnostic error. Unexpected EOF
            return queryNode;
        }
        // Check if projection is followed by an edge identifier
        if (next.TokenType == TokenType.Identifier)
        {
            var edgeParser = context.GetParser<EdgeParser>();
            var edgeNode = edgeParser.Parse<EdgeQueryNode>(ref lexer, context);

            queryNode = new ProjectionQueryNode()
            {
                Edge = edgeNode
            };

            if (!lexer.TryPeek(out next))
            {
                // TODO: Add Diagnostic error. Unexpected EOF
                return queryNode;
            }
        }
        if (next.TokenType != TokenType.OpenBracket)
        {
            // TODO: Add diagnostic error. Expected starting bracket block
            return queryNode;
        }
        while (lexer.HasNext)
        {
            var token = lexer.Next();
            
            if (token.TokenType == TokenType.CloseParenthesis)
            {
                if (lexer.TryPeek(out var peek) && peek.TokenType != TokenType.Dot)
                {
                    // TODO: Diagnostics error dot notation is required
                }

                return queryNode;
            }

            queryNode = ParseBracketBlock(ref lexer, context, queryNode);
        }

        // TODO: Add diagnostics error. Missing Closing Parenthesis


        return queryNode;
    }
    private ProjectionQueryNode ParseBracketBlock(ref TokenLexer lexer, ParserContext context, ProjectionQueryNode queryNode)
    {
        var token = default(Token);
        var properties = new List<PropertyQueryNode>();

        while (lexer.HasNext)
        {
            if (!lexer.TryNext(out token))
            {
                context.AddUnexpectedEOFError(ref lexer);
                return queryNode;
            }
            if (token.TokenType == TokenType.CloseBracket)
            {
                break;
            }
            switch (token.TokenType)
            {
                case TokenType.Identifier:
                    {
                        var parser = context.GetParser<PropertyParser>();

                        if (parser.Parse(ref lexer, context, new PropertyQueryNode()) is not PropertyQueryNode propertyNode)
                        {
                            // TODO: Add Diagnostic Error
                            continue;
                        }
                        properties.Add(propertyNode);
                    }
                    break;
                default:
                    {
                        // TODO: Add Diagnostic information. Unexpected next
                        break;
                    }
            }
        }
        return queryNode = new ProjectionQueryNode()
        {
            Edge = queryNode.Edge,
            Properties = properties
        };
    }
}
