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
    public QueryParser() { }

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
                    throw QueryParserException.UnexpectedToken(token);
            }
        }

        return new QueryTree()
        {
            Nodes = nodes
        };
    }


    #region Keyword Parse Methods

    private QueryNode ParseFilterNode(ref TokenLexer lexer)
    {
        var current = default(QueryNode);

        while (lexer.HasNext)
        {
            switch (lexer.Next().TokenType)
            {
                case TokenType.OpenParenthesis:
                case TokenType.OpenBracket:
                    continue;

                case TokenType.Identifier:
                    current = ParseIdentifierNode(ref lexer, current);
                    break;

                case TokenType.Equal:
                case TokenType.NotEqual:
                case TokenType.LessThan:
                case TokenType.LessThanOrEqual:
                case TokenType.GreaterThan:
                case TokenType.GreaterThanOrEqual:
                case TokenType.And:
                case TokenType.Or:
                    current = ParseBinaryNode(ref lexer, current);
                    break;

                default:
                    throw QueryParserException.UnexpectedToken(default);

            }
        }

        return current;
    }
    private QueryNode ParseSelectNode(ref TokenLexer lexer)
    {
        var current = default(QueryNode);

        while (lexer.HasNext)
        {
            switch (lexer.Next().TokenType)
            {
                case TokenType.OpenParenthesis:
                case TokenType.OpenBracket:
                    continue;

                case TokenType.Identifier:
                    current = ParseIdentifierNode(ref lexer, current);
                    break;

                case TokenType.Equal:
                case TokenType.NotEqual:
                case TokenType.LessThan:
                case TokenType.LessThanOrEqual:
                case TokenType.GreaterThan:
                case TokenType.GreaterThanOrEqual:
                case TokenType.And:
                case TokenType.Or:
                    current = ParseBinaryNode(ref lexer, current);
                    break;

                default:
                    throw QueryParserException.UnexpectedToken(default);

            }
        }

        return current;
    }
    private QueryNode ParseSortNode(ref TokenLexer lexer)
    {
       
        return default;
    }
    private QueryNode ParsePageNode(ref TokenLexer lexer)
    {
        return default;
    }
    #endregion

    private QueryNode ParseParanthesisNode(ref TokenLexer lexer, QueryNode left)
    {
        var current = default(QueryNode);

        // Translation: While the next token's binding power is less than the current (left) token's binding power continue
        while (lexer.Peek().TokenType < TokenType.CloseParenthesis)
        {

            switch (lexer.Next().TokenType)
            {
                case TokenType.Identifier:
                    {
                        current = ParseIdentifierNode(ref lexer, current);
                        break;
                    }

                case TokenType.OpenBracket:
                    left = ParseBracketNode(ref lexer, left);
                    break;

                case TokenType.OpenParenthesis:
                    left = ParseParanthesisNode(ref lexer, left);
                    break;

                // Parse Operator
                case TokenType.Equal:
                case TokenType.NotEqual:
                case TokenType.LessThan:
                case TokenType.LessThanOrEqual:
                case TokenType.GreaterThan:
                case TokenType.GreaterThanOrEqual:
                case TokenType.And:
                case TokenType.Or:
                    left = ParseBinaryNode(ref lexer, left);
                    break;

                // Parse Literal or Constant
                case TokenType.String:
                case TokenType.Boolean:
                case TokenType.Null:
                case TokenType.Integer:
                    left = ParseConstantNode(ref lexer);
                    break;


                default:
                    throw new Exception();
            }
        }

        return left;
    }
    private QueryNode ParseBracketNode(ref TokenLexer lexer, QueryNode left)
    {
        var current = default(QueryNode);

        // Translation: While the next token's binding power is less than the current (left) token's binding power continue
        while (lexer.Peek().TokenType < TokenType.CloseBracket)
        {
            switch (lexer.Next().TokenType)
            {
                case TokenType.Identifier:
                    current = ParseIdentifierNode(ref lexer, current);
                    break;

                case TokenType.OpenBracket:
                    left = ParseBracketNode(ref lexer, left);
                    break;

                case TokenType.OpenParenthesis:
                    left = ParseParanthesisNode(ref lexer, left);
                    break;

                // Parse Operator
                case TokenType.Equal:
                case TokenType.NotEqual:
                case TokenType.LessThan:
                case TokenType.LessThanOrEqual:
                case TokenType.GreaterThan:
                case TokenType.GreaterThanOrEqual:
                case TokenType.And:
                case TokenType.Or:
                    left = ParseBinaryNode(ref lexer, left);
                    break;

                // Parse Literal or Constant
                case TokenType.String:
                case TokenType.Boolean:
                case TokenType.Null:
                case TokenType.Integer:
                    left = ParseConstantNode(ref lexer);
                    break;


                default:
                    throw new Exception();
            }
        }

        return left;
    }

    private QueryNode ParseBinaryNode(ref TokenLexer lexer, QueryNode left)
    {
        if (left is not MemberQueryNode || left is not FunctionCallQueryNode)
        {
            throw new Exception();
        }

        var operat = lexer.Next().TokenType switch
        {
            TokenType.Equal => BinaryOperatorType.Equal
        };

        while (lexer.HasNext)
        {
            switch (lexer.Next().TokenType)
            {

            }
        }
        //var operandRight = 


        return default;
    }
    private QueryNode ParseOperatorNode(ref TokenLexer lexer)
    {
        if (lexer.Peek().TokenType == TokenType.Integer)
        {

        }

        return default;
    }
    private QueryNode ParseConstantNode(ref TokenLexer lexer)
    {
        var token = lexer.Current;


        return default;
    }


    private QueryNode ParseIdentifierNode(ref TokenLexer lexer, QueryNode left)
    {
        var currentToken = lexer.Current;
        var currentNode = default(QueryNode);

        // Translation: While the next token's binding power is less than the current (left) token's binding power continue
        while (lexer.Peek().TokenType < currentToken.TokenType)
        {
            switch (lexer.Next().TokenType)
            {

            }
        }

        if (currentToken.IsIdentifierFunction())
        {
            return ParseFunctionNode(ref lexer);
        }
        else
        {
            return ParseMemberNode(ref lexer, left);
        }
    }







    private QueryNode ParseMemberNode(ref TokenLexer lexer, QueryNode left)
    {
        var node = new MemberQueryNode()
        {
            Name = lexer.Current.ValueAsText
        };

        if (left is MemberQueryNode member)
        {
            left = new MemberQueryNode()
            {
                Children = member.Children.Concat(new[]
                {
                    node
                })
            };
        }

        return left = node;
    }


    private FunctionCallQueryNode ParseFunctionNode(ref TokenLexer lexer)
    {


        return default;
    }

}
