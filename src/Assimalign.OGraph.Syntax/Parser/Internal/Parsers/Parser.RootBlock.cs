using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax.Internal;

internal class RootBlockParser : Parser
{
    internal override QueryNode Parse(ref TokenLexer lexer, ParserContext context, QueryNode node)
    {
        if (node is not RootQueryNode root)
        {
            return node;
        }
        if (lexer.TryPeek(out var peek))
        {
            // G0001 Error: An open parenthesis is expected
            if (peek.TokenType != TokenType.OpenParenthesis)
            {
                context.Diasgnostics.Add(new QueryDiagnostic(
                    code: QueryDiagnosticCode.G0001,
                    message: "An open parenthesis is expected: '('",
                    start: peek.Start,
                    end: peek.End,
                    severity: QueryDiagnosticSeverity.Error));
            }

            var token = lexer.Next();

            // G0000 Error: 
            if (context.Parse(ref lexer, node) is not RootQueryNode)
            {
                context.Diasgnostics.Add(new QueryDiagnostic(
                    code: QueryDiagnosticCode.G0000,
                    message: "An invalid query was parsed",
                    start: peek.Start,
                    end: peek.End,
                    severity: QueryDiagnosticSeverity.Error));
            }
        }
        else
        {
            context.Diasgnostics.Add(new QueryDiagnostic(
                code: QueryDiagnosticCode.G0005,
                message: "Unexpected EOF (End of File) near query root.",
                start: peek.Start,
                end: peek.End,
                severity: QueryDiagnosticSeverity.Error));

        }
        return node;
    }
}
