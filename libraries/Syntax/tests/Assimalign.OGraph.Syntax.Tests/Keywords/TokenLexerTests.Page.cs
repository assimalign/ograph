using System.Linq;
using System.Collections.Generic;
using Xunit;

namespace Assimalign.OGraph.Syntax;

using Assimalign.OGraph.Syntax.Internal;

public partial class TokenLexerTests
{
    [Fact(DisplayName = "Page Test: Keyword Identified Successfully")]
    public void TestPageKeywordIdentifiedSuccess()
    {
        var query = "page({take 25 skip 32})";
        var lexer = TokenLexer.Create(query, new TokenLexerOptions()
        {

        });
        var tokens = new List<Token>();

        while (lexer.HasNext)
        {
            tokens.Add(lexer.Next());
        }

        Assert.Contains(tokens, token => token.TokenType == TokenType.Page);
        Assert.Contains(tokens, token => token.TokenType == TokenType.Skip);
        Assert.Contains(tokens, token => token.TokenType == TokenType.Take);

        Assert.True(tokens.Where(token => token.TokenType == TokenType.Integer).Count() == 2);


        Assert.Single(tokens, token => token.TokenType == TokenType.OpenParenthesis);
        Assert.Single(tokens, token => token.TokenType == TokenType.CloseParenthesis);

        Assert.Single(tokens, token => token.TokenType == TokenType.OpenBracket);
        Assert.Single(tokens, token => token.TokenType == TokenType.CloseBracket);
    }
}