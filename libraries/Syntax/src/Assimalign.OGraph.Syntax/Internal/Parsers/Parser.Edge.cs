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
        Token peek;

        // Parse Parenthesis Block
        while (lexer.HasNext)
        {
            token = lexer.Next();

            if (token.TokenType != TokenType.Identifier)
            {
                // TODO: Added Diagnostics - expected edge identifier
                return queryNode;
            }

            // Try to get next
            if (!lexer.TryPeek(out peek))
            {
                context.AddDiagnostic(Diagnostic.UnexpectedEOF(
                    lexer.Current.End));

                return queryNode;
            }

            // Expected an edge path. Need to 
            if (peek.TokenType == TokenType.Slash)
            {
                var segments = new Queue<string>(new string[]
                {
                    token.Text
                });

                while (lexer.HasNext)
                {
                    token = lexer.Next();

                    if (token.TokenType != TokenType.Identifier)
                    {
                        // Expected Identifier Node
                        return queryNode;
                    }

                    if (!lexer.TryPeek(out peek))
                    {
                        context.AddDiagnostic(Diagnostic.UnexpectedEOF(
                            lexer.Current.End));
                        return queryNode;
                    }

                    segments.Enqueue(token.Text);

                    // Check if next is a separator
                    if (peek.TokenType != TokenType.Slash)
                    {
                        break;
                    }
                }

                // Lets get the root edge
                var edge = context.Root.GetNodesOfType<EdgeNode>()
                    .FirstOrDefault(p => p.Label!.Name!.Equals(segments.First(), StringComparison.OrdinalIgnoreCase));

                if (segments.Count > 2)
                {
                    for (int i = 1; i < segments.Count - 1; i++)
                    { 
                    //    edge = edge.Vertices.FirstOrDefault(p=> 
                    //        p.Label!.Name!.Equals(segments.Skip(i).First(), StringComparison.OrdinalIgnoreCase))

                    }

                }
                else
                {
                    var vertexParser = context.GetParser<VertexParser>();
                    var vertexNode = vertexParser.Parse(ref lexer, context, new VertexNode());

                    return queryNode = new()
                    {
                        Label = new LabelNode(segments.Last())
                    };
                }


            }
            if (token.TokenType == TokenType.Alias)
            {
                // TODO: Parse Edge Alias

            }

            if (token.TokenType == TokenType.CloseParenthesis)
            {

            }

            //queryNode = ParseBracketBlock(ref lexer, context, queryNode);
        }


        return queryNode;
    }
}