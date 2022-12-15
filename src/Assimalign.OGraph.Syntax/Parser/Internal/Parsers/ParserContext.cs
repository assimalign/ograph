using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax.Internal;

internal class ParserContext
{
    #region Static
    // This will cache newed up instances to prevent reallocation and deallocation of memory
    private static readonly Dictionary<Type, Parser> cache = new();
    private static readonly Dictionary<Func<Token, QueryNode, bool>, Parser> parsers = new();
    private static Func<Type, Parser> Memoise(Func<Type, Parser> method)
    {
        return input => cache.TryGetValue(input, out var output) ? 
            output : 
            method.Invoke(input);
    }

    static ParserContext()
    {
        // Root Parsers
        parsers.Add((token, node) => token.TokenType == TokenType.QueryRoot && node is RootQueryNode, Memoise(x => new RootBlockParser()).Invoke(typeof(RootBlockParser)));
        parsers.Add((token, node) => token.TokenType == TokenType.OpenBracket && node is RootQueryNode, Memoise(x => new RootBracketBlockParser()).Invoke(typeof(RootBracketBlockParser)));
        parsers.Add((token, node) => token.TokenType == TokenType.OpenParenthesis && node is RootQueryNode, Memoise(x => new RootParenthesisBlockParser()).Invoke(typeof(RootParenthesisBlockParser)));

        // Page Parsers
        parsers.Add((token, node) => token.TokenType == TokenType.Page && node is RootQueryNode, Memoise(x => new PageBlockParser()).Invoke(typeof(PageBlockParser)));
        parsers.Add((token, node) => token.TokenType == TokenType.OpenBracket && node is PageQueryNode, Memoise(x => new PageBracketBlockParser()).Invoke(typeof(PageBracketBlockParser)));
        parsers.Add((token, node) => token.TokenType == TokenType.OpenParenthesis && node is PageQueryNode, Memoise(x => new PageParenthesisBlockParser()).Invoke(typeof(PageParenthesisBlockParser)));
        parsers.Add((token, node) => token.TokenType == TokenType.Skip && node is PageQueryNode, Memoise(x => new PageSkipParser()).Invoke(typeof(PageSkipParser)));
        parsers.Add((token, node) => token.TokenType == TokenType.Take && node is PageQueryNode, Memoise(x => new PageTakeParser()).Invoke(typeof(PageTakeParser)));
        parsers.Add((token, node) => token.TokenType == TokenType.Token && node is PageQueryNode, Memoise(x => new PageTokenParser()).Invoke(typeof(PageTokenParser)));
        parsers.Add((token, node) => token.TokenType == TokenType.Integer && node is PageQueryNode, Memoise(x => new ConstantParser()).Invoke(typeof(ConstantParser)));



        // Literal Parsers
    }
    #endregion

    private readonly List<QueryDiagnostic> diagnostics = new();
    public ParserContext()
    {
        
    }
    public IList<QueryDiagnostic> Diasgnostics => this.diagnostics;

    public QueryNode Parse(ref TokenLexer lexer, QueryNode node)
    {
        foreach (var (predeicate, parser) in parsers)
        {
            if (predeicate.Invoke(lexer.Current, node))
            {
                return parser.Parse(ref lexer, this, node);
            }
        }

        return node;

        //return lexer.Current.TokenType switch
        //{
        //    // Filter Block Parsing
        //    TokenType.Filter => ParseFilter(ref lexer, node),

        //    // Projection Block Parsing
        //    TokenType.Project when node is RootQueryNode => ParseProjections(ref lexer, context, node),                   // Only Parse the Project Keyword when the left node is Root
        //    TokenType.OpenBracket when node is ProjectionQueryNode => ParseProjectionsBracketBlock(ref lexer, node),
        //    TokenType.OpenBracket when node is FieldQueryNode => ParseProjectionsBracketBlock(ref lexer, node),
        //    TokenType.OpenParenthesis when node is ProjectionQueryNode => ParseProjectionsParanthesisBlock(ref lexer, node),
        //    TokenType.Identifier when node is ProjectionQueryNode => ParseProjectionsIdentifier(ref lexer, node),
        //    TokenType.Identifier when node is FieldQueryNode => ParseIdentifier(ref lexer, node),
        //    TokenType.Alias when node is FieldQueryNode => ParseProjectionsAlias(ref lexer, node),


        //    // Page Block Parsing
        //    //TokenType.Page when node is RootQueryNode => ParsePage(ref lexer, node),
        //    //TokenType.OpenBracket when node is PageQueryNode => ParsePageBracketBlock(ref lexer, node),
        //    //TokenType.OpenParenthesis when node is PageQueryNode => ParsePageParanthesisBlock(ref lexer, node),
        //    //TokenType.Skip when node is PageQueryNode => ParsePageSkip(ref lexer, node),
        //    //TokenType.Take when node is PageQueryNode => ParsePageTake(ref lexer, node),
        //    //TokenType.Token when node is PageQueryNode => ParseToken(ref lexer, node),

        //    // Sort Block Parsing
        //    TokenType.Sort => ParseSort(ref lexer, node),


        //    // Binary Parse
        //    TokenType.Equal => ParseBinary(ref lexer, node),
        //    TokenType.NotEqual => ParseBinary(ref lexer, node),
        //    TokenType.LessThan => ParseBinary(ref lexer, node),
        //    TokenType.LessThanOrEqual => ParseBinary(ref lexer, node),
        //    TokenType.GreaterThan => ParseBinary(ref lexer, node),
        //    TokenType.GreaterThanOrEqual => ParseBinary(ref lexer, node),
        //    TokenType.And => ParseBinary(ref lexer, node),
        //    TokenType.Or => ParseBinary(ref lexer, node),

        //    // Unary Parsing (Currently only Negative numbers are supported unary)
        //    //TokenType.Minus when previous.TokenType != TokenType.Integer    => ParseUnary(ref lexer, node),

        //    // Literal Parsing
        //    TokenType.String => ParseConstant(ref lexer, node),
        //    TokenType.Integer => ParseConstant(ref lexer, node),
        //    TokenType.FloatingPoint => ParseConstant(ref lexer, node),


        //    //TokenType.OpenParenthesis => ParseParnthesisBlock(ref lexer, node),
        //    //TokenType.OpenBracket => ParseBracketBlock(ref lexer, node),


        //    TokenType.CloseParenthesis => node,
        //    TokenType.CloseBracket => node,
        //    _ => default
        //};
    }
}
