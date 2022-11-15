namespace Assimalign.OGraph.Syntax.Tests;

public class TokenLexerTests
{
    private const string query = " .filter({}).select({}).sort({ firstName desc lastName asc}).page({take 23 skip 1});";

    [Fact]
    public void Test1()
    {
        var tokens = new Queue<Token>();
        var lexer = new TokenLexer(query.Select(x => (byte)x).ToArray());

        while (lexer.HasNext)
        {
            var token = lexer.Next();

            tokens.Enqueue(token);
        }


        Assert.Contains(tokens, token => token.TokenType == TokenType.Ascending);


        
    }




}