using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax.Internal;

internal class SortParser : Parser
{
    internal override QueryNode Parse(ref TokenLexer lexer, ParserContext context, QueryNode node)
    {
        if (node is not SortQueryNode sortNode)
        {
            // TODO: Add diagnostics information. Expected RootQueryNode to follow
            return node;
        }
        if (!lexer.HasNext)
        {
            // TODO: Add diagnostics unexpected EOF
            return node;
        }

        var token = lexer.Next();

        if (token.TokenType != TokenType.OpenParenthesis)
        {
            // TODO: Add diagnostic error. Expected starting parenthesis block
            return node;
        }

        return ParseParenthesisBlock(ref lexer, context, sortNode);
    }

    private SortQueryNode ParseParenthesisBlock(ref TokenLexer lexer, ParserContext context, SortQueryNode queryNode)
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

            queryNode = new SortQueryNode()
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

                break;
            }

            queryNode = ParseBracketBlock(ref lexer, context, queryNode);
        }

        return queryNode;
    }
    private SortQueryNode ParseBracketBlock(ref TokenLexer lexer, ParserContext context, SortQueryNode queryNode)
    {
        while (lexer.HasNext)
        {
            var token = lexer.Next();

            if (token.TokenType == TokenType.CloseBracket)
            {
                break;
            }
            switch (token.TokenType)
            {
                case TokenType.Identifier when token.Value.IsFunction(out var functionType):
                    {
                        var functionNode = context.GetParser<FunctionParser>().Parse(ref lexer, context, new FunctionQueryNode()
                        {
                            FunctionType = functionType
                        });
                        if (queryNode.SortBy is null)
                        {
                            queryNode = new SortQueryNode()
                            {
                                SortBy = functionNode,
                                Direction = queryNode.Direction,
                                Edge = queryNode.Edge,
                                ThenBy = queryNode.ThenBy,
                            };
                        }
                        else
                        {

                        }
                        break;
                    }
                case TokenType.Identifier:
                    {
                        var propertyNode = context.GetParser<PropertyParser>().Parse(ref lexer, context, new PropertyQueryNode());

                        break;
                    }
                default:
                    {
                        // TODO: Add Diagnostic information. Unexpected token
                        break;
                    }
            }
        }

        return queryNode;
    }
}
