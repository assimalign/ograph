using Assimalign.OGraph.Syntax.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax;

public sealed class QueryParser
{
    public QueryParser()
    {

    }

    public QueryTree Parse(string query) => Parse(Encoding.Default.GetBytes(query));
    public QueryTree Parse(byte[] query)
    {
        var lexer = new TokenLexer(query, new()
        {
            SkipCarriageReturn = true,
            SkipLineFeed = true,
            SkipTabs = true,
            SkipWhiteSpace = true,
        });
        var nodes = new List<QueryNode>();
        var tree = new QueryTree();

        while (lexer.HasNext)
        {
            var token = lexer.Next();

            switch (token.TokenType)
            {
                case TokenType.Sort:
                    nodes.Add(ParseSortNode(ref lexer));
                    break;
                case TokenType.Page:
                    nodes.Add(ParsePageNode(ref lexer));
                    break;
                case TokenType.Select:
                    nodes.Add(ParseSelectNode(ref lexer));
                    break;
                case TokenType.Filter:
                    nodes.Add(ParseFilterNode(ref lexer));
                    break;
                case TokenType.Dot: // Currently Dot is not considered as part of the keywords on each root expression.
                    continue;
                default:
                    // If any other token is generated then what is in switch statement above,
                    // then it is safe to assume 
                    throw new Exception();
            }
        }

        return tree;
    }




    private QueryNode ParseFilterNode(ref TokenLexer lexer)
    {
        var left = lexer.Current;

        // Translation: While the next token's binding power is less than the current (left) token's binding power continue
        while (lexer.Peek().TokenType < left.TokenType)
        {
            var right = lexer.Next();

            switch (right.TokenType)
            {
                case TokenType.OpenParenthesis:
                    {
                        if (ParseParanthesisNode(ref lexer, right) is not BinaryQueryNode binary || !binary.IsPredeicate)
                        {
                            throw new Exception();
                        }

                        return binary;
                    }
                default:
                    throw new Exception();
            }
        }
        throw new Exception();
    }
    private QueryNode ParseSelectNode(ref TokenLexer lexer)
    {
        return default;
    }
    private QueryNode ParseSortNode(ref TokenLexer lexer)
    {
        return default;
    }
    private QueryNode ParsePageNode(ref TokenLexer lexer)
    {
        return default;
    }
    private QueryNode ParseParanthesisNode(ref TokenLexer lexer, Token left)
    {
        // Translation: While the next token's binding power is less than the current (left) token's binding power continue
        while (lexer.Peek().TokenType != TokenType.CloseParenthesis)
        {
            var right = lexer.Next();

            switch (right.TokenType)
            {
                case TokenType.Identifier:
                    return ParseIdentifierNode(ref lexer, right);

                case TokenType.OpenBracket:
                    return ParseBracketNode(ref lexer, right);

                case TokenType.OpenParenthesis:
                    return ParseParanthesisNode(ref lexer, right);

                default:
                    throw new Exception();
            }
        }

        throw new Exception();
    }
    private QueryNode ParseBracketNode(ref TokenLexer lexer, Token left)
    {
        var leftNode = default(QueryNode);

        // Translation: While the next token's binding power is less than the current (left) token's binding power continue
        while (lexer.Peek().TokenType < TokenType.CloseBracket)
        {
            var right = lexer.Next();

            switch (right.TokenType)
            {
                case TokenType.OpenParenthesis:
                    return ParseParanthesisNode(ref lexer, right);

                case TokenType.Identifier:
                    {
                        if (lexer.Peek().IsOperator)
                        {
                            return ParseBinaryNode(ref lexer, right);
                        }
                        else
                        {
                            return ParseIdentifierNode(ref lexer, right);
                        }
                    }
                case TokenType.And:
                case TokenType.Or:
                    {
                        break;
                    }
                default:
                    throw new Exception();
            }
        }

        throw new Exception();
    }

    private QueryNode ParseBinaryNode(ref TokenLexer lexer, Token left)
    {
        var operandLeft = ParseIdentifierNode(ref lexer, left);
        var operat = lexer.Next().TokenType switch
        {
            TokenType.Equal => BinaryOperatorType.Equal
        };
        var operandRight = 




        return default;
    }
    private QueryNode ParseOperatorNode(ref TokenLexer lexer)
    {
        if (lexer.Peek().TokenType == TokenType.Integer)
        {

        }

        return default;
    }
    private QueryNode ParseConstantNode()
    {
        return default;
    }

    private QueryNode ParseIdentifierNode(ref TokenLexer lexer, Token left)
    {
        // An identifier node can only ever be a Member or Function
        return left.IsIdentifierFunction() ?
            ParseFunctionNode(ref lexer, left) :
            ParseMemberNode(ref lexer, left);
    }

    private QueryNode ParseMemberNode(ref TokenLexer lexer, Token left)
    {
        return default;
    }
    private FunctionCallQueryNode ParseFunctionNode(ref TokenLexer lexer, Token left)
    {


        return default;
    }

}
