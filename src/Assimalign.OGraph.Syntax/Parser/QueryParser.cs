using System;
using System.Text;
using System.Collections.Generic;
using Assimalign.OGraph.Syntax.Internal;

namespace Assimalign.OGraph.Syntax;

public sealed partial class QueryParser
{
    private readonly QueryParserOptions options;

    public QueryParser() : this(new QueryParserOptions()) { }
    public QueryParser(QueryParserOptions options)
    {
        this.options = options;
    }


    public QueryDocument Parse(string query)
    {
        return Parse(Encoding.UTF8.GetBytes(query));
    }
    public QueryDocument Parse(byte[] query)
    {
        var lexer = new TokenLexer(query, new()
        {
            SkipCarriageReturn = true,
            SkipLineFeed = true,
            SkipTabs = true,
            SkipWhiteSpace = true,
            SkipComments = true
        });

        var context = new QueryParserContext();
        var errors = new List<QueryError>();
        var rootNode = new RootQueryNode();

        while (lexer.HasNext)
        {
            lexer.Next();

            if (Parse(ref lexer, context, rootNode) is not RootQueryNode node)
            {
                throw QueryParserException.UnexpectedNode();
            }

            rootNode = node;
        }

        return new QueryDocument(
            rootNode,
            errors);
    }


    #region Private Methods
    private QueryNode? Parse(ref TokenLexer lexer, QueryParserContext context, QueryNode node)
    {
        // This recursive switch statement can be interpreted as Parse node for the given token when the left node is 
        return lexer.Current.TokenType switch
        {
            TokenType.Dot                                                   => node,
            TokenType.QueryRoot         when node is RootQueryNode          => ParseRoot(ref lexer, context, node),
            TokenType.OpenBracket       when node is RootQueryNode          => ParseRootBracketBlock(ref lexer, context, node),
            TokenType.OpenParenthesis   when node is RootQueryNode          => ParseRootParanthesisBlock(ref lexer, context, node),
            // Filter Block Parsing
            TokenType.Filter                                                => ParseFilter(ref lexer, node),

            // Projection Block Parsing
            TokenType.Project           when node is RootQueryNode          => ParseProjections(ref lexer, context, node),                   // Only Parse the Project Keyword when the left node is Root
            TokenType.OpenBracket       when node is ProjectionQueryNode    => ParseProjectionsBracketBlock(ref lexer, node),
            TokenType.OpenBracket       when node is FieldQueryNode         => ParseProjectionsBracketBlock(ref lexer, node),
            TokenType.OpenParenthesis   when node is ProjectionQueryNode    => ParseProjectionsParanthesisBlock(ref lexer, node),
            TokenType.Identifier        when node is ProjectionQueryNode    => ParseProjectionsIdentifier(ref lexer, node),
            TokenType.Identifier        when node is FieldQueryNode         => ParseIdentifier(ref lexer, node),
            TokenType.Alias             when node is FieldQueryNode         => ParseProjectionsAlias(ref lexer, node),


            // Page Block Parsing
            TokenType.Page              when node is RootQueryNode          => ParsePage(ref lexer, node),
            TokenType.OpenBracket       when node is PageQueryNode          => ParsePageBracketBlock(ref lexer, node),
            TokenType.OpenParenthesis   when node is PageQueryNode          => ParsePageParanthesisBlock(ref lexer, node),
            TokenType.Skip              when node is PageQueryNode          => ParsePageSkip(ref lexer, node),
            TokenType.Take              when node is PageQueryNode          => ParsePageTake(ref lexer, node),
            TokenType.Token             when node is PageQueryNode          => ParseToken(ref lexer, node),

            // Sort Block Parsing
            TokenType.Sort                                                  => ParseSort(ref lexer, node),
            

            // Binary Parse
            TokenType.Equal                                                 => ParseBinary(ref lexer, node),
            TokenType.NotEqual                                              => ParseBinary(ref lexer, node),
            TokenType.LessThan                                              => ParseBinary(ref lexer, node),
            TokenType.LessThanOrEqual                                       => ParseBinary(ref lexer, node),
            TokenType.GreaterThan                                           => ParseBinary(ref lexer, node),
            TokenType.GreaterThanOrEqual                                    => ParseBinary(ref lexer, node),
            TokenType.And                                                   => ParseBinary(ref lexer, node),
            TokenType.Or                                                    => ParseBinary(ref lexer, node),

            // Unary Parsing (Currently only Negative numbers are supported unary)
            //TokenType.Minus when previous.TokenType != TokenType.Integer    => ParseUnary(ref lexer, node),

            // Literal Parsing
            TokenType.String                                                => ParseConstant(ref lexer, node),
            TokenType.Integer                                               => ParseConstant(ref lexer, node),
            TokenType.FloatingPoint                                         => ParseConstant(ref lexer, node),
            

            //TokenType.OpenParenthesis => ParseParnthesisBlock(ref lexer, node),
            //TokenType.OpenBracket => ParseBracketBlock(ref lexer, node),


            TokenType.CloseParenthesis => node,
            TokenType.CloseBracket => node,
            _ => default
        };
    }

    #region Root Parsing
    private QueryNode ParseRoot(ref TokenLexer lexer, QueryParserContext context, QueryNode node)
    {
        if (node is RootQueryNode root)
        {
            throw QueryParserException.UnexpectedNode();
        }

        if (lexer.TryPeek(out var peek))
        {
            if (peek.TokenType != TokenType.OpenParenthesis)
            {
                context.AddError(new QueryError()
            }

            var token = lexer.Next();

            if (Parse(ref lexer, context, node) is not RootQueryNode)
            {
                context.AddError(new QueryError()
                {

                });
            }
        }

        return node;
    }
    private QueryNode ParseRootParanthesisBlock(ref TokenLexer lexer, QueryParserContext context, QueryNode node)
    {
        while (lexer.HasNext)
        {
            var token = lexer.Next();

            if (token.TokenType == TokenType.CloseParenthesis)
            {
                break;
            }

            node = Parse(ref lexer, context, node);
        }

        return node;
    }
    private QueryNode ParseRootBracketBlock(ref TokenLexer lexer, QueryParserContext context, QueryNode node)
    {
        while (lexer.HasNext)
        {
            var token = lexer.Next();

            if (token.TokenType == TokenType.CloseBracket)
            {
                break;
            }

            node = Parse(ref lexer, context, node);
        }

        return node;
    }
    #endregion

    #region Sort Parsing
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

    #endregion

    #region Page Parsing
    private QueryNode ParsePage(ref TokenLexer lexer, QueryNode node)
    {
        if (node is not RootQueryNode root)
        {
            throw QueryParserException.UnexpectedNode();
        }
        var token = lexer.Next();

        if (Parse(ref lexer, new PageQueryNode()) is not PageQueryNode pageNode)
        {
            throw QueryParserException.InvalidPage();
        }

        root.AddNode(pageNode);

        return root;
    }
    private QueryNode ParsePageParanthesisBlock(ref TokenLexer lexer, QueryNode node)
    {
        while (true)
        {
            var token = lexer.Next();

            if (token.TokenType == TokenType.CloseParenthesis)
            {
                break;
            }

            node = Parse(ref lexer, node);   
        }

        return node;
    }
    private QueryNode ParsePageBracketBlock(ref TokenLexer lexer, QueryNode node)
    {
        while (true)
        {
            var token = lexer.Next();

            if (token.TokenType == TokenType.CloseBracket)
            {
                break;
            }

            node = Parse(ref lexer, node);
        }

        return node;
    }
    private QueryNode ParsePageTake(ref TokenLexer lexer, QueryNode node)
    {
        if (node is not PageQueryNode page)
        {
            throw QueryParserException.UnexpectedNode();
        }

        var token = lexer.Next();

        if (Parse(ref lexer, default) is not ConstantQueryNode constant)
        {
            throw QueryParserException.InvalidPage();
        }

        page.SetTake(constant);

        return page;
    }
    private QueryNode ParsePageSkip(ref TokenLexer lexer, QueryNode node)
    {
        if (node is not PageQueryNode page)
        {
            throw QueryParserException.UnexpectedNode();
        }

        var token = lexer.Next();

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

        var token = lexer.Next();

        if (Parse(ref lexer, default) is not ConstantQueryNode constant)
        {
            throw QueryParserException.InvalidPage();
        }

        page.SetToken(constant);

        return page;
    }
    #endregion

    #region Filter Parsing
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

    #endregion

    #region Projection Parsing
    private QueryNode ParseProjections(ref TokenLexer lexer, QueryParserContext context, QueryNode node)
    {
        if (node is not RootQueryNode root)
        {
            throw QueryParserException.InvalidPage();
        }

        var token = lexer.Next();

        if (token.TokenType != TokenType.OpenParenthesis) // The projection clause MUST follow an open parenthesis token
        {
            context.AddError(new QueryError()
            {
                Message = ""
            });
        }
        if (Parse(ref lexer, context, new ProjectionQueryNode()) is not ProjectionQueryNode projectionNode)
        {
            throw QueryParserException.UnexpectedNode();
        }

        root.AddNode(projectionNode);

        return root;
    }    
    private QueryNode ParseProjectionsBracketBlock(ref TokenLexer lexer, QueryNode node)
    {
        // Project should be the current left node
        
        while (lexer.HasNext)
        {
            var token = lexer.Next();

            if (node is ProjectionQueryNode projection) // Parsing Root
            {
                if (token.TokenType == TokenType.CloseBracket)
                {
                    return projection;
                }
                if (Parse(ref lexer, projection) is not ProjectionQueryNode)
                {
                    throw QueryParserException.UnexpectedNode();
                }
            }
            if (node is FieldQueryNode field) // Nested select
            {
                if (token.TokenType == TokenType.CloseBracket)
                {
                    return field;
                }
                if (Parse(ref lexer, new FieldQueryNode()) is not FieldQueryNode projectionField)
                {
                    throw QueryParserException.UnexpectedNode();
                }
            
                field.AddChild(projectionField);
            }
        }

        throw QueryParserException.UnexpectedNode();
    }
    private QueryNode ParseProjectionsParanthesisBlock(ref TokenLexer lexer, QueryNode node)
    {
        while (true)
        {
            var token = lexer.Next();

            if (token.TokenType == TokenType.CloseParenthesis)
            {
                break;
            }

            node = Parse(ref lexer, node);
        }

        return node;
    }
    private QueryNode ParseProjectionsIdentifier(ref TokenLexer lexer, QueryNode node)
    {
        if (node is not ProjectionQueryNode projectionNode)
        {
            throw QueryParserException.UnexpectedNode();
        }
        if (Parse(ref lexer, new FieldQueryNode()) is not FieldQueryNode fieldNode)
        {
            throw QueryParserException.UnexpectedNode();
        }

        // Peek for Alias node
        var token = lexer.Peek();

        // Peek for Nested select
        if (token.TokenType == TokenType.OpenBracket)
        {
            token = lexer.Next();

            if (Parse(ref lexer, fieldNode) is not FieldQueryNode)
            {
                throw QueryParserException.UnexpectedNode();
            }

            token = lexer.Peek();
        }
        // Validate next token is closing bracket or next identifier
        if (token.TokenType != TokenType.Identifier && token.TokenType != TokenType.CloseBracket)
        {
            throw QueryParserException.UnexpectedToken(token);
        }

        projectionNode.AddProjection(fieldNode);

        return projectionNode;
    }

    private QueryNode ParseProjectionsAlias(ref TokenLexer lexer, QueryNode node)
    {
        // Alias should always follow Fields Nodes
        if (node is not FieldQueryNode field)
        {
            throw QueryParserException.UnexpectedNode();
        }
        // Should expect an identifier following the alias operator
        if (lexer.TryPeek(out var next) && next.TokenType != TokenType.Identifier)
        {
            throw QueryParserException.UnexpectedToken(next);
        }

        lexer.Next();

        field.SetAlias(lexer.Current.ValueAsText);

        return field;
    }

    #endregion

    #region Other Parsing
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
        var identifier = lexer.Current.GetString().ToLower() switch
        {
            "any"           => ParseFunctionAny(ref lexer, node),
            "startswith"    => ParseFunctionStartsWtih(ref lexer, node),
            "endswith"      => ParseFunctionEndsWith(ref lexer, node),

            _ => ParseMember(ref lexer, node),
        };

        // Projection Logic 
        if (identifier is FieldQueryNode fieldNode)
        {
            // Alias's always follow identifiers. Let's check to see if alias is next
            var token = lexer.Peek();

            if (token.TokenType == TokenType.Alias)
            {
                token = lexer.Next();

                if (Parse(ref lexer, fieldNode) is not FieldQueryNode)
                {
                    throw QueryParserException.UnexpectedNode();
                }
            }

            return fieldNode;
        }



        return default;
    }
    private QueryNode ParseVariables(ref TokenLexer lexer, QueryNode node)
    {


        return default;
    }

    #region Function Parsing
    private QueryNode ParseFunctionAny(ref TokenLexer lexer, QueryNode node)
    {
        return default;
    }

    private QueryNode ParseFunctionEndsWith(ref TokenLexer lexer, QueryNode node)
    {

        return default;
    }
    private QueryNode ParseFunctionStartsWtih(ref TokenLexer lexer, QueryNode node)
    {

        return default;
    } 

    #endregion

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
        if (node is FieldQueryNode fieldNode)
        {
            fieldNode.SetValue(new MemberQueryNode(lexer.Current.GetString()));
            return fieldNode;
        }

        return default;
    }
    private QueryNode ParseAlias(ref TokenLexer lexer, QueryNode node)
    {
        if (node is not FieldQueryNode fieldNode)
        {
            throw QueryParserException.UnexpectedNode();
        }

        fieldNode.SetAlias(lexer.Current.GetString());

        return node;
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
    #endregion

    private class QueryParserContext
    {
        private readonly IList<QueryError> errors;

        public QueryParserContext()
        {
            this.errors = new List<QueryError>();
        }

        public IEnumerable<QueryError> Errors => this.errors;

        public void AddError(QueryError error) => this.errors.Add(error);
    }
}