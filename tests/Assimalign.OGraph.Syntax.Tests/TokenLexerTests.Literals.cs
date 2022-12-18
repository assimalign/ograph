using System;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax.Tests;

using Assimalign.OGraph.Syntax.Internal;


public partial class TokenLexerTests
{

    #region String Literals
    [Fact(DisplayName = "Literal Test (String): Bad formatted string value")]
    public void TestBadStringFormatException()
    {
        var exception = Assert.Throws<TokenLexerException>(() =>
        {
            var value = "'This is a bad formatted string";
            var lexer = new TokenLexer(Encoding.UTF8.GetBytes(value));
            lexer.Next();
        });
    }

    [Fact(DisplayName = "Literal Test (String): False value parsed successfully")]
    public void TestStringValueWithEscapeCharacter()
    {
        var content = "'This is test\'s'";


    }
    #endregion

    #region Keyword Literals
    [Fact(DisplayName = "Literal Test (Boolean): 'Null' value parsed successfully")]
    public void TestNullLiteralParsedSuccesfully()
    {
        var value = "null";
        var lexer = new TokenLexer(Encoding.UTF8.GetBytes(value));
        var token = lexer.Next();

        Assert.Equal(TokenType.Null, token.TokenType);
    }

    [Fact(DisplayName = "Literal Test (Boolean): 'False' value parsed successfully")]
    public void TestBooleanFalseLiteralParsedSuccesfully()
    {
        var value = "false";
        var lexer = new TokenLexer(Encoding.UTF8.GetBytes(value));
        var token = lexer.Next();

        Assert.Equal(TokenType.Boolean, token.TokenType);
        Assert.True(token.Value.Span.SequenceEqual(new ReadOnlySpan<byte>(Encoding.UTF8.GetBytes("false"))));
    }

    [Fact(DisplayName = "Literal Test (Boolean): 'True' value parsed successfully")]
    public void TestBooleanTrueLiteralParsedSuccesfully()
    {
        var value = "true";
        var lexer = new TokenLexer(Encoding.UTF8.GetBytes(value));
        var token = lexer.Next();

        Assert.Equal(TokenType.Boolean, token.TokenType);
        Assert.True(token.Value.Span.SequenceEqual(new ReadOnlySpan<byte>(Encoding.UTF8.GetBytes("true"))));
    }

    #endregion
}
