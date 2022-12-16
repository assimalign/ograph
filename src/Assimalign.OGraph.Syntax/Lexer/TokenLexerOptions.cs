using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax.Lexer;

public struct TokenLexerOptions
{
    /// <summary>
    /// Skips all tab '\t' Token Types: <see cref="TokenType.Tab"/> 
    /// </summary>
    public bool SkipTabs { get; set; }
    /// <summary>
    /// Skips all tab ' ' Token Types: <see cref="TokenType.WhiteSpace"/> 
    /// </summary>
    public bool SkipWhiteSpace { get; set; }
    /// <summary>
    /// Skips all tab '\n' Token Types: <see cref="TokenType.LineFeed"/> 
    /// </summary>
    public bool SkipLineFeed { get; set; }
    /// <summary>
    /// Skips all tab '\r' Token Types: <see cref="TokenType.CarriageReturn"/> 
    /// </summary>
    public bool SkipCarriageReturn { get; set; }
    /// <summary>
    /// Skips all tab '\t' Token Types: <see cref="TokenType.Comment"/> 
    /// </summary>
    public bool SkipComments { get; set; }
}
