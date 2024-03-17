namespace Assimalign.OGraph.Syntax;

using Assimalign.OGraph.Syntax.Internal;

public sealed partial class QueryParser
{
    private ConstantNode ParseConstant(ref TokenLexer lexer, ParserContext context)
    {
        var token = lexer.Current;

        return new ConstantNode(
            token.Value.ToArray(),
            token.Text,
            Location.Create(
                token.Line, 
                token.Line, 
                token.Start, 
                token.End));
    }
}
