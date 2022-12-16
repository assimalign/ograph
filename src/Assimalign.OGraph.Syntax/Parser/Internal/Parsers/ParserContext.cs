using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax.Internal;

using Assimalign.OGraph.Syntax.Lexer;

internal class ParserContext
{
    #region Static
    // Key - When Token is match and node type matches then use 
    private static readonly Dictionary<Func<Token, QueryNode, bool>, Parser> parsers = new();
    static ParserContext()
    {
        // Root Parsers
        parsers.Add((token, node) => token.TokenType == TokenType.QueryRoot && node is RootQueryNode, new RootBlockParser());
        parsers.Add((token, node) => token.TokenType == TokenType.OpenBracket && node is RootQueryNode, new RootBracketBlockParser());
        parsers.Add((token, node) => token.TokenType == TokenType.OpenParenthesis && node is RootQueryNode, new RootParenthesisBlockParser());

        // Projection Parsers
        parsers.Add((token, node) => token.TokenType == TokenType.Identifier && node is ProjectionQueryNode, new IdentifierParser());


        // Page Parsers
        parsers.Add((token, node) => token.TokenType == TokenType.Page && node is RootQueryNode, new PageBlockParser());
        parsers.Add((token, node) => token.TokenType == TokenType.OpenBracket && node is PageQueryNode, new PageBracketBlockParser());
        parsers.Add((token, node) => token.TokenType == TokenType.OpenParenthesis && node is PageQueryNode, new PageParenthesisBlockParser());
        parsers.Add((token, node) => token.TokenType == TokenType.Skip && node is PageQueryNode, new PageSkipParser());
        parsers.Add((token, node) => token.TokenType == TokenType.Take && node is PageQueryNode, new PageTakeParser());
        parsers.Add((token, node) => token.TokenType == TokenType.Token && node is PageQueryNode, new PageTokenParser());

        // Literal Parser
        parsers.Add((token, node) =>
        {
            return  token.TokenType == TokenType.Integer || 
                    token.TokenType == TokenType.FloatingPoint || 
                    token.TokenType == TokenType.Null ||
                    token.TokenType == TokenType.String;

        }, new ConstantParser());
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
    }
}
