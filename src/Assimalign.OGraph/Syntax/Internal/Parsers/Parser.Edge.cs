using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;

namespace Assimalign.OGraph.Syntax.Internal;

internal class EdgeParser : Parser<EdgeNode>
{
    internal override EdgeNode Parse(ref TokenLexer lexer, ParserContext context, EdgeNode queryNode)
    {
        var edgePath = string.Empty;

        Token token;

        if (!lexer.TryPeek(out token))
        {
            context.AddDiagnostic(Diagnostic.UnexpectedEOF(
                lexer.Current.End));
        }
        if (token.TokenType != TokenType.OpenParenthesis)
        {
            
            //context.AddDiagnostic(Diagnostic.(
            //    lexer.Current.End));

            return queryNode;
        }

        while (lexer.HasNext)
        {
            //var token = lexer.Next();

            //if (token.TokenType == TokenType.Comma)
            //{
            //    return queryNode;
            //}
            //if (token.TokenType == TokenType.Slash)
            //{
            //    continue;
            //}
            //if (token.TokenType == TokenType.Identifier)
            //{
            //    queryNode = new EdgeNode()
            //    {
            //        Name = edgePath = string.IsNullOrEmpty(edgePath) ? token.Text : string.Join('/', edgePath, token.Text)
            //    };
            //    if (!lexer.TryPeek(out var peek))
            //    {
            //        context.AddDiagnostic(Diagnostic.UnexpectedEOF(
            //            lexer.Current.End));

            //        return queryNode;
            //    }
            //    if (peek.TokenType != TokenType.Slash && peek.TokenType != TokenType.Comma)
            //    {
            //        context.AddDiagnostic(Diagnostic.ExpectedCommaSeparator(
            //            peek.Start,
            //            peek.End));
            //    }
            //}
            //else
            //{
            //    context.AddDiagnostic(Diagnostic.InvalidToken(ref token));
            //}
        }

        return queryNode;
    }

    internal EdgeNode ParseParenthesisBlock(ref TokenLexer lexer, ParserContext context, EdgeNode queryNode)
    {
        Token token;
        EdgeNode edge = null;

        // Parse Parenthesis Block
        while (lexer.HasNext)
        {
            token = lexer.Next();

            if (token.TokenType != TokenType.Identifier)
            {
                // Expected edge identifier
                return queryNode;
            }
            Token peek;
            if (!lexer.TryPeek(out peek))
            {
                context.AddDiagnostic(Diagnostic.UnexpectedEOF(
                    lexer.Current.End));

                return queryNode;
            }
            // Expected an edge path. Need to 
            if (peek.TokenType == TokenType.Slash)
            {
                IEnumerable<EdgeNode> edges = null;

                if (edge is null)
                {
                    if (!context.GetRoot().TryGetEdges(out edges))
                    {
                        // TODO: Unexpected error. For example., if there is an edge path 'companies/address' and we are parsing addresses then the 'companies' edge should already exist.
                        return queryNode;
                    }
                }
                else
                {
                    if (!edge.TryGetEdges(out edges))
                    {
                        // TODO: Unexpected error. For example., if there is an edge path 'companies/address' and we are parsing addresses then the 'companies' edge should already exist.
                        return queryNode;
                    }
                }
                edge = edges.FirstOrDefault(p => p.Identifier!.Name!.Equals(token.Text, StringComparison.OrdinalIgnoreCase))!;
            }
            if (token.TokenType == TokenType.Alias)
            {
                // TODO: Parse Edge Alias
            }

            if (token.TokenType == TokenType.CloseParenthesis)
            {
                
            }

            queryNode = ParseBracketBlock(ref lexer, context, queryNode);
        }


        return queryNode;
    }
}