using System.Collections.Generic;
using Xunit;

namespace Assimalign.OGraph.Syntax;

using Assimalign.OGraph.Syntax.Internal;

public partial class TokenLexerTests
{
    [Fact(DisplayName = "Sort Test: Keyword Identified Successfully")]
    public void TestSortKeywordIdentifiedSuccess()
    {
        var query = "sort({firstName})";
        var lexer = TokenLexer.Create(query, new TokenLexerOptions()
        {

        });
        var tokens = new List<Token>();

        while (lexer.HasNext)
        {
            tokens.Add(lexer.Next());
        }

        Assert.Contains(tokens, token => token.TokenType == TokenType.Sort);

        // companies/addresses x 2 identifiers
        Assert.Single(tokens, token => token.TokenType == TokenType.Identifier);

        Assert.Single(tokens, token => token.TokenType == TokenType.OpenParenthesis);
        Assert.Single(tokens, token => token.TokenType == TokenType.CloseParenthesis);

        Assert.Single(tokens, token => token.TokenType == TokenType.OpenBracket);
        Assert.Single(tokens, token => token.TokenType == TokenType.CloseBracket);
    }
}
