using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Assimalign.OGraph.Syntax;

using Assimalign.OGraph.Syntax.Internal;

public partial class TokenLexerTests
{
    [Fact]
    public void TestParameterTokenSuccess()
    {
        var query = ".page({ take @take })";
        var bytes = Encoding.UTF8.GetBytes(query);
        var lexer = new TokenLexer(bytes);
        var tokens = new List<Token>();

        while (lexer.HasNext)
        {
            tokens.Add(lexer.Next());
        }
    }
}
