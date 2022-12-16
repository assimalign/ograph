using System;

namespace Assimalign.OGraph.Syntax.Lexer;

// Reference for Syntax Tokens: https://codeforwin.org/2015/05/introduction-to-programming-tokens.html#Separators

/// <summary>
/// 
/// </summary>
/// <remarks>
/// Tokens can be grouped into multiple categorization. 
/// For example the 'and' operator is also a keyword as it is reserved.
/// </remarks>
public enum TokenType 
{
    #region Other
    Comment,
    /// <summary>
    /// Identifier is the name given to different programming elements. 
    /// It can be either a name given to a variable or a function or any other programming element which follow some basic naming conventions
    /// </summary>
    /// <remarks>
    /// The parser is responsible for identifying what 
    /// </remarks>
    Identifier,
    Variable,
    #endregion

    #region Literals (are constant values that are used for performing various operations and calculations)
    Null, // Also a keyword
    Boolean, // Also a keyword
    /// <summary>
    /// A integer is a literal which represents types such as:
    /// <see cref="short"/>,
    /// <see cref="int"/>,
    /// <see cref="long"/>,
    /// <see cref="ushort"/>,
    /// <see cref="uint"/>,
    /// <see cref="ulong"/>
    /// </summary>
    Integer,
    /// <summary>
    /// A floating point is a literal which represents types such as:
    /// <see cref="System.Single"/>,
    /// <see cref="decimal"/>,
    /// <see cref="double"/>
    /// </summary>
    FloatingPoint,
    /// <summary>
    /// A string is a literal that represents many types such as: 
    /// <see cref="String"/>,
    /// <see cref="Guid"/>,
    /// <see cref="TimeSpan"/>,
    /// <see cref="TimeOnly"/>,
    /// <see cref="DateOnly"/>,
    /// <see cref="DateTime"/>,
    /// <see cref="DateTimeOffset"/>
    /// </summary>
    String,
    #endregion

    #region Operators (A operator returns a binary expression)
    Star, //
    Slash,
    Plus,
    Minus,
    Equal,
    NotEqual,
    GreaterThan,
    GreaterThanOrEqual,
    LessThan,
    LessThanOrEqual,
    And,
    Or,
    
    #endregion

    #region Separators
    Tab,
    LineFeed,
    CarriageReturn,
    Comma,    
    Question,
    Dot,
    Colon,
    Semicolon,
    WhiteSpace,
    Exclamation,
    OpenBracket,
    CloseBracket,
    OpenParenthesis,
    CloseParenthesis,
    #endregion

    #region Keywords
    Alias,
    QueryRoot,
    Project,
    Filter,
    Sort,
    Page,
    Descending,
    Ascending,
    Take,
    Skip,
    Token
    #endregion
}