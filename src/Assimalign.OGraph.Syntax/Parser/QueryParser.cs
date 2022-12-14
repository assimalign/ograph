using System;
using System.Text;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax;

public sealed partial class QueryParser
{
    private readonly QueryParserOptions options;

    public QueryParser() : this(new QueryParserOptions()) { }
    public QueryParser(QueryParserOptions options)
    {
        this.options = options;
    }


    public QueryNode Parse(string query)
    {
        return Parse(Encoding.UTF8.GetBytes(query));
    }
    public QueryNode Parse(byte[] query)
    {
        var lexer = new TokenLexer(query, new()
        {
            SkipCarriageReturn = true,
            SkipLineFeed = true,
            SkipTabs = true,
            SkipWhiteSpace = true,
        });

        var rootNode = default(QueryNode);

        while (lexer.HasNext)
        {
            rootNode = Parse(ref lexer, new RootQueryNode());
        }

        return rootNode;
    }


    #region Private Methods
    private QueryNode Parse(ref TokenLexer lexer, QueryNode node)
    {
        var previous = lexer.Current;

        return lexer.Next().TokenType switch
        {
            TokenType.Dot => Parse(ref lexer, node),
            TokenType.QueryRoot => ParseRoot(ref lexer, node),
            TokenType.Filter => ParseFilter(ref lexer, node),
            TokenType.Project => ParseProject(ref lexer, node),
            TokenType.Page => ParsePage(ref lexer, node),
            TokenType.Sort => ParseSort(ref lexer, node),
            TokenType.Skip => ParseSkip(ref lexer, node),
            TokenType.Take => ParseTake(ref lexer, node),
            TokenType.Token => ParseToken(ref lexer, node),
            TokenType.Identifier => ParseIdentifier(ref lexer, node),
            // Binary Parse
            TokenType.Equal => ParseBinary(ref lexer, node),
            TokenType.NotEqual => ParseBinary(ref lexer, node),
            TokenType.LessThan => ParseBinary(ref lexer, node),
            TokenType.LessThanOrEqual => ParseBinary(ref lexer, node),
            TokenType.GreaterThan => ParseBinary(ref lexer, node),
            TokenType.GreaterThanOrEqual => ParseBinary(ref lexer, node),
            TokenType.And => ParseBinary(ref lexer, node),
            TokenType.Or => ParseBinary(ref lexer, node),

            // Unary Parsing (Currently only Negative numbers are supported unary)
            TokenType.Minus when previous.TokenType != TokenType.Integer => ParseUnary(ref lexer, node),

            // Literal Parsing
            TokenType.String => ParseConstant(ref lexer, node),
            TokenType.Integer => ParseConstant(ref lexer, node),
            TokenType.FloatingPoint => ParseConstant(ref lexer, node),
            TokenType.OpenParenthesis => ParseParnthesisBlock(ref lexer, node),
            TokenType.OpenBracket => ParseBracketBlock(ref lexer, node),


            TokenType.CloseParenthesis => node,
            TokenType.CloseBracket => node
        };
    }
    private QueryNode ParseRoot(ref TokenLexer lexer, QueryNode node)
    {
        if (node is not RootQueryNode root)
        {
            throw QueryParserException.UnexpectedNode();
        }

        while (lexer.Next().TokenType != TokenType.CloseParenthesis)
        {
            switch (lexer.Current.TokenType)
            {
                case TokenType.OpenParenthesis:
                case TokenType.OpenBracket:
                case TokenType.CloseBracket:
                    continue;
                default:
                    throw QueryParserException.UnexpectedToken(default);
            }
        }

        return node;
    }
    private QueryNode ParseSort(ref TokenLexer lexer, QueryNode node)
    {
        if (node is not RootQueryNode root)
        {
            throw QueryParserException.InvalidPage();
        }

        var sortNode = new SortQueryNode();


        // TODO: Add Sort Parsing logic

        root.AddNode(sortNode);

        return root;
    }
    private QueryNode ParsePage(ref TokenLexer lexer, QueryNode node)
    {
        if (node is not RootQueryNode root)
        {
            throw QueryParserException.InvalidPage();
        }
        if (Parse(ref lexer, new PageQueryNode()) is not PageQueryNode pageNode)
        {
            throw QueryParserException.InvalidPage();
        }

        root.AddNode(pageNode);

        return root;
    }
    private QueryNode ParseTake(ref TokenLexer lexer, QueryNode node)
    {
        if (node is not PageQueryNode page)
        {
            throw QueryParserException.UnexpectedNode();
        }
        if (Parse(ref lexer, default) is not ConstantQueryNode constant)
        {
            throw QueryParserException.InvalidPage();
        }

        page.SetTake(constant);

        return page;
    }
    private QueryNode ParseSkip(ref TokenLexer lexer, QueryNode node)
    {
        if (node is not PageQueryNode page)
        {
            throw QueryParserException.UnexpectedNode();
        }
        if (Parse(ref lexer, default) is not ConstantQueryNode constant)
        {
            throw QueryParserException.InvalidPage();
        }

        page.SetSkip(constant);

        return page;
    }
    private QueryNode ParseToken(ref TokenLexer lexer, QueryNode node)
    {
        if (node is not PageQueryNode page)
        {
            throw QueryParserException.UnexpectedNode();
        }
        if (Parse(ref lexer, default) is not ConstantQueryNode constant)
        {
            throw QueryParserException.InvalidPage();
        }

        page.SetToken(constant);

        return page;
    }
    private QueryNode ParseFilter(ref TokenLexer lexer, QueryNode node)
    {
        if (node is not RootQueryNode root)
        {
            throw QueryParserException.InvalidPage();
        }

        var binaryNode = Parse(ref lexer, default) as BinaryQueryNode;


        //var filterNode = new FilterQueryNode()
        //{
        //    Predicate = binaryNode,
        //};

        //root.AddNode(filterNode);


        return root;
    }
    private QueryNode ParseProject(ref TokenLexer lexer, QueryNode node)
    {
        if (node is not RootQueryNode root)
        {
            throw QueryParserException.InvalidPage();
        }

        var selectNode = new ProjectionQueryNode();

        root.AddNode(selectNode);

        return Parse(ref lexer, selectNode);
    }
    private QueryNode ParseParnthesisBlock(ref TokenLexer lexer, QueryNode node)
    {
        while (lexer.Current.TokenType != TokenType.CloseParenthesis)
        {
            switch (node)
            {
                case ProjectionQueryNode:
                    node = Parse(ref lexer, node);
                    break;

                case PageQueryNode:
                    node = Parse(ref lexer, node);
                    break;
            }
        }

        return node;
    }
    private QueryNode ParseBracketBlock(ref TokenLexer lexer, QueryNode node)
    {
        while (lexer.Current.TokenType != TokenType.CloseBracket)
        {
            switch (node)
            {
                case FieldQueryNode member:
                    {
                        member.AddChild(Parse(ref lexer, member));

                        break;
                    }
                case PageQueryNode:
                    node = Parse(ref lexer, node);
                    break;
            }
        }

        return node;
    }
    private QueryNode ParseBinary(ref TokenLexer lexer, QueryNode node)
    {
        var token = lexer.Current;

        // Parse Conjunction Binary
        if (token.TokenType == TokenType.And || token.TokenType == TokenType.Or)
        {
            var operatorType = (BinaryOperatorType)token.TokenType;

            // Conjunctions must not be followed by conjunctions (Duplicate binary statement)
            if (node is not BinaryQueryNode leftOperand)
            {
                throw QueryParserException.InvalidBinary();
            }
            if (leftOperand.OperatorType == BinaryOperatorType.And || leftOperand.OperatorType == BinaryOperatorType.Or)
            {
                throw QueryParserException.InvalidBinary();
            }

            var rightOperand = Parse(ref lexer, default);

            return new BinaryQueryNode(
                leftOperand,
                rightOperand,
                operatorType);
        }

        // Parse Predicate Binary
        if (token.TokenType == TokenType.Equal ||
            token.TokenType == TokenType.NotEqual ||
            token.TokenType == TokenType.GreaterThan ||
            token.TokenType == TokenType.GreaterThanOrEqual ||
            token.TokenType == TokenType.LessThan ||
            token.TokenType == TokenType.LessThanOrEqual)
        {
            var operatorType = (BinaryOperatorType)token.TokenType;

            if (node is not MemberQueryNode && node is not FunctionQueryNode && node is not ConstantQueryNode)
            {
                throw QueryParserException.InvalidBinary();
            }

            var rightOperand = Parse(ref lexer, default);

            return new BinaryQueryNode(
                node,
                rightOperand,
                operatorType);

        }


        throw QueryParserException.InvalidBinary();
    }
    private QueryNode ParseUnary(ref TokenLexer lexer, QueryNode node)
    {
        return default;
    }
    private QueryNode ParseIdentifier(ref TokenLexer lexer, QueryNode node)
    {
        if (node is RootQueryNode && lexer.Current.GetString().Equals("variables", StringComparison.InvariantCultureIgnoreCase))
        {
            return ParseVariables(ref lexer, node);
        }
        return default;
    }
    private QueryNode ParseVariables(ref TokenLexer lexer, QueryNode node)
    {


        return default;
    }
    private QueryNode ParseField(ref TokenLexer lexer, QueryNode node)
    {
        return default;
    }
    private QueryNode ParseFunction(ref TokenLexer lexer, QueryNode node)
    {


        if (!lexer.TryPeek(out var token) || token.TokenType != TokenType.OpenParenthesis)
        {
            throw QueryParserException.InvalidBinary();
        }

        var parameters = new Queue<ParameterQueryNode>();

        while (lexer.Current.TokenType != TokenType.CloseParenthesis)
        {
            switch (lexer.Current.TokenType)
            {
                default:
                    {
                        if (Parse(ref lexer, new ParameterQueryNode()) is not ParameterQueryNode parameter)
                        {
                            throw QueryParserException.UnexpectedNode();
                        }

                        parameters.Enqueue(parameter);

                        break;
                    }
            }
        }

        // After parsing function check if there is an alias
        if (lexer.TryPeek(out var next) && next.TokenType == TokenType.Alias)
        {

        }

        return default;
    }
    private QueryNode ParseFunctionArgument(ref TokenLexer lexer, QueryNode node)
    {


        return default;
    }
    private QueryNode ParseMember(ref TokenLexer lexer, QueryNode node)
    {


        if (node is ProjectionQueryNode select)
        {



        }
        // After parsing function check if there is an alias
        if (lexer.TryPeek(out var next) && next.TokenType == TokenType.Alias)
        {
            return ParseAlias(ref lexer, default);
        }

        return default;
    }
    private QueryNode ParseAlias(ref TokenLexer lexer, QueryNode node)
    {

        return default;
    }
    private QueryNode ParseConstant(ref TokenLexer lexer, QueryNode node)
    {
        var token = lexer.Current;
        var constantNode = new ConstantQueryNode();

        if (token.TokenType == TokenType.String)
        {
            if (DateOnly.TryParse(token.GetString(), out var dateTime))
            {

            }
        }

        if (token.TokenType == TokenType.Integer)
        {
            var value = token.GetLong();

            constantNode.SetValue(value);
        }


        return constantNode;
    }
    #endregion
}