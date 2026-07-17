using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax;

using Assimalign.OGraph.Syntax.Internal;
using static System.Net.Mime.MediaTypeNames;

public sealed partial class QueryParser
{
    private VertexNode? ParseVertex(ref TokenLexer lexer, ParserContext context)
    {
        Token token;
        Int32 start = lexer.Current.Start;
        Int32 startLine = lexer.Current.Line;
        Int32 end;
        Int32 endLine;

        VertexNode vertex;
        ConstantNode? argument = null;

        // Ensure next token is an Open Parenthesis Block
        if (!lexer.TryNext(out token))
        {
            AddEofDiagnostic(ref lexer, context);
            return null;
        }
        if (token.TokenType != TokenType.OpenParenthesis || 
            lexer.Previous.TokenType != TokenType.Vertex)   // <-- This checks ensures that there is no invalid character
        {                                                   // between the key word and the open parenthesis
            AddExpectedOpenParenDiagnostic(ref lexer, context);
            return null;
        }
        // Get Vertex Identifier
        if (!lexer.TryNext(out token) || token.TokenType != TokenType.Identifier)
        {
            // TODO: Add Diagnostics
            return null;
        }

        var hasNext = lexer.TryNext(out token);
        if (!hasNext || (token.TokenType != TokenType.CloseParenthesis && token.TokenType != TokenType.Comma))
        {
            AddExpectedClosingParenDiagnostic(ref lexer, context);
            return null;
        }

        // Check for Literal Argument 
        if (token.TokenType == TokenType.Comma)
        {
            if (lexer.TryNext(out token) && (
                token.TokenType == TokenType.String ||
                token.TokenType == TokenType.FloatingPoint ||
                token.TokenType == TokenType.Integer))
            {
                argument = ParseConstant(ref lexer, context);

                lexer.TryNext(out token);
            }
        }
        if (token.TokenType != TokenType.CloseParenthesis)
        {
            AddExpectedClosingParenDiagnostic(ref lexer, context);
            return null;
        }

        vertex = argument is null
            ? new VertexNode()
            : new VertexNode(argument, []);

        if (lexer.TryNext(out token) && token.TokenType != TokenType.Dot)
        {
            AddExpectedDotSeparatorDiagnostic(ref lexer, context);
            return null;
        }

        while (lexer.TryNext(out token))
        {
            if (token.TokenType == TokenType.Edge)
            {
                if (!ParseEdge(ref lexer))
                {
                    return null;
                }
            }
            else
            {
                QueryNode? node = token.TokenType switch
                {
                    TokenType.Page => ParsePage(ref lexer, context),
                    TokenType.Sort => ParseSort(ref lexer, context),
                    TokenType.Filter => ParseFilter(ref lexer, context),
                    TokenType.Project => ParseProject(ref lexer, context),
                    _ => null
                };

                if (node is null)
                {
                    // TODO: Add Diagnostics
                    continue;
                }

                vertex.AddNode(node);
            }
        }

        return vertex;

        bool ParseEdge(ref TokenLexer lexer )
        {
            LabelNode? label = null;
            LabelNode? alias = null;
            VertexNode? target = null;
            VertexNode? source = vertex;
            string path = "";

            Token token = lexer.Current;

            // Capture the dot notation if the previous node was chained.
            if (lexer.Previous.TokenType == TokenType.Dot)
            {
                token = lexer.Previous;
            }

            Int32 start = token.Start;
            Int32 startLine = token.Line;
            Int32 end;
            Int32 endLine;
            String text;
            Location location;

            // Ensure next token is an Open Parenthesis Block
            if (!lexer.TryNext(out token))
            {
                AddEofDiagnostic(ref lexer, context);
                return false;
            }
            if (token.TokenType != TokenType.OpenParenthesis || lexer.Previous.TokenType != TokenType.Edge)
            {
                AddExpectedOpenParenDiagnostic(ref lexer, context);
                return false;
            }

            string? current = null;

            while (lexer.TryNext(out token))
            {
                if (token.TokenType != TokenType.Identifier)
                {
                    // TODO: Add Diagnostics
                    return false;
                }
                else
                {
                    current = token.Text;
                    path += "/" + current;
                }
                if (!lexer.TryNext(out token))
                {
                    // TODO: Expected slash or comma
                    return false;
                }
                if (token.TokenType == TokenType.Alias)
                {
                    if (!lexer.TryNext(out token) || token.TokenType != TokenType.Identifier)
                    {
                        // TODO: Add Diagnostics
                    }

                    alias = new LabelNode(token.Text);

                    if (!lexer.TryNext(out token) || token.TokenType != TokenType.CloseParenthesis)
                    {
                        // TODO: Add Diagnostics
                        return false;
                    }
                }
                if (token.TokenType == TokenType.Slash)
                {
                    if (lexer.Previous.TokenType != TokenType.Identifier)
                    {
                        //TODO: Add Diagnostic - There can't be any other token between the slash and identifier
                        return false;
                    }
                    var item = source!.Nodes.FirstOrDefault(p =>
                    {
                        return p is EdgeNode edge && edge?.Label?.Name == current;
                    }) as EdgeNode;

                    if (item is null)
                    {
                        // Invalid Path
                        return false;
                    }

                    source = item.Target;

                    continue;
                }
                else if (token.TokenType != TokenType.CloseParenthesis)
                {
                    AddExpectedClosingParenDiagnostic(ref lexer, context);
                    return false;
                }
                else
                {
                    break;
                }
            }

            target = new VertexNode();
            label = new LabelNode(current!);

            // 
            if (!lexer.TryNext(out token))
            {
                return false;
            }
            if (token.TokenType != TokenType.Dot)
            {
                AddExpectedDotSeparatorDiagnostic(ref lexer, context);
                return false;
            }

            while (lexer.TryNext(out token))
            {
                QueryNode? node = token.TokenType switch
                {
                    TokenType.Page => ParsePage(ref lexer, context),
                    TokenType.Sort => ParseSort(ref lexer, context),
                    TokenType.Filter => ParseFilter(ref lexer, context),
                    TokenType.Project => ParseProject(ref lexer, context),
                    _ => null
                };

                if (node is null)
                {
                    continue;
                }

                target.AddNode(node);

                if (!lexer.TryPeek(out token) || token.TokenType == TokenType.Edge)
                {
                    end = lexer.Position;
                    endLine = lexer.Line;
                    text = lexer.GetText(start, end);
                    location = Location.Create(startLine, endLine, start, end);


                    source!.AddNode(new EdgeNode(
                        label,
                        source!,
                        target,
                        text,
                        location,
                        alias,
                        path));
                    return true;
                }
            }

            return false;
        }
    }
}
