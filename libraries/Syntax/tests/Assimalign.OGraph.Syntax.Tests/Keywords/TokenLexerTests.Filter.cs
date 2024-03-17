using Xunit;
using System.Linq;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax;

using Assimalign.OGraph.Syntax.Internal;

public partial class TokenLexerTests
{
    [Fact(DisplayName = "Filter Test: Keyword Identified Successfully")]
    public void TestFilterKeywordIdentifiedSuccess()
    {
        var query = "filter({startsWith(firstName, 'c')})";
        var lexer = TokenLexer.Create(query, new TokenLexerOptions()
        {

        });
        var tokens = new List<Token>();

        while (lexer.HasNext)
        {
            tokens.Add(lexer.Next());
        }

        Assert.Contains(tokens, token => token.TokenType == TokenType.Filter);

        Assert.True(tokens.Where(token => token.TokenType == TokenType.OpenParenthesis).Count() == 2);
        Assert.True(tokens.Where(token => token.TokenType == TokenType.CloseParenthesis).Count() == 2);
        Assert.True(tokens.Where(token => token.TokenType == TokenType.Identifier).Count() == 2); // startsWith, firstName
        Assert.Single(tokens, token => token.TokenType == TokenType.String); // 'c'

        Assert.Single(tokens, token => token.TokenType == TokenType.OpenBracket);
        Assert.Single(tokens, token => token.TokenType == TokenType.CloseBracket);
    }
}