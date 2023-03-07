using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax.Internal;

internal class FilterParser : Parser
{
    internal override QueryNode Parse(ref TokenLexer lexer, ParserContext context, QueryNode queryNode)
    {
        if (queryNode is not FilterQueryNode filterNode)
        {
            // This is internal error. Some dumbass messed with the code.
            return queryNode;
        }
        if (!lexer.HasNext)
        {
            context.AddUnexpectedEOFDiagnosticError(lexer.Current.End);
            return queryNode;
        }

        var token = lexer.Next();

        if (token.TokenType != TokenType.OpenParenthesis)
        {
            // TODO: Add diagnostic error. Expected starting parenthesis block
            return queryNode;
        }

        return ParseParenthesisBlock(ref lexer, context, filterNode);
    }

    private FilterQueryNode ParseParenthesisBlock(ref TokenLexer lexer, ParserContext context, FilterQueryNode queryNode)
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

            if (edgeParser.Parse(ref lexer, context, new EdgeQueryNode()) is not EdgeQueryNode edge)
            {
                // TODO: 
            }
            else
            {
                queryNode = new FilterQueryNode()
                {
                    Edge = edge,
                };
            }
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

        // TODO: Add diagnostics error. Missing Closing Parentheisis


        return queryNode;
    }
    private FilterQueryNode ParseBracketBlock(ref TokenLexer lexer, ParserContext context, FilterQueryNode queryNode)
    {
        var leftOperand = default(QueryNode);

        while (lexer.HasNext)
        {
            var token = lexer.Next();

            if (token.TokenType == TokenType.CloseBracket)
            {
                break;
            }
            switch (token.TokenType)
            {
                case TokenType.Identifier:
                    {
                        leftOperand = ParseIdentifierNode(ref lexer, context, new FilterQueryNode());
                    }
                    break;
                case TokenType.String:
                case TokenType.Integer:
                case TokenType.FloatingPoint:
                case TokenType.Boolean:
                    {
                        //attributes.Add(ParseConstantAsIdentifier(ref lexer, context, new AttributeQueryNode()));
                        break;
                    }
                default:
                    {
                        // TODO: Add Diagnostic information. Unexpected token
                        break;
                    }
            }
        }

        queryNode = new FilterQueryNode()
        {
            Edge = queryNode.Edge,
            //Attributes = attributes
        };

        return queryNode;
    }

    private FilterQueryNode ParseBinaryNode(ref TokenLexer lexer, ParserContext context, QueryNode queryNode)
    {



        return queryNode as FilterQueryNode;
    }

    private FilterQueryNode ParseIdentifierNode(ref TokenLexer lexer, ParserContext context, FilterQueryNode queryNode)
    {
        var next = default(Token);
        var identifier = context.GetParser<IdentifierParser>().Parse(ref lexer, context, queryNode);

        // Comma for Binary Expression
        if (!lexer.TryNext(out next))
        {
            // TODO: Diagnostic 
        }

        // Identifiers in the Filter block should always follow a binary expression
        switch (next.TokenType)
        {
            case TokenType.Equal:
            case TokenType.NotEqual:
            case TokenType.GreaterThan:
            case TokenType.GreaterThanOrEqual:
            case TokenType.LessThan:
            case TokenType.LessThanOrEqual:
            case TokenType.And:
            case TokenType.Or:
                {

                    break;
                }
            default:
                {
                    break;
                }
        }

        //// Alias Check
        //if (lexer.TryPeek(out next) && next.TokenType == TokenType.Alias)
        //{
        //    lexer.Skip();
        //    queryNode = ParseIdentifierAlias(ref lexer, context, queryNode);
        //}
        //// Nested Project Check
        //if (lexer.TryPeek(out next) && next.TokenType == TokenType.OpenBracket)
        //{
        //    queryNode = ParseNestedAttributeNode(ref lexer, context, queryNode);
        //}

        return queryNode;
    }
}
