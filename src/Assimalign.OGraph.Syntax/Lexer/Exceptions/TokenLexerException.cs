using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax;

public abstract class TokenLexerException : Exception
{
    public TokenLexerException() { }
    public TokenLexerException(string message) : base(message) { }
    public TokenLexerException(string message, Exception innerException) : base(message, innerException) { }
}
