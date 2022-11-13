using System;

namespace Assimalign.OGraph;

// Reference for Syntax Tokens: https://codeforwin.org/2015/05/introduction-to-programming-tokens.html#separators

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

    /// <summary>
    /// Identifiers are the name given to different programming elements. 
    /// Either name given to a variable or a function or any other programming element, all follow some basic naming conventions
    /// </summary>
    /// <remarks>
    /// The parser is responsible for identifying what 
    /// </remarks>
    Identifier,
    #endregion

    #region Separators
    Tab,
    NewLine,
    CarriageReturn,    
    Comma,
    Slash,
    Question,
    Dot,
    Colon,
    Semicolon,
    WhiteSpace,
    Exclamation,
    OpenParenthesis,
    CloseParenthesis,
    OpenBracket,
    CloseBracket,
    OpenSquareBracket,
    CloseSquareBracket,
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
    Any,
    All,
    Alias,
    In,
    #endregion

    #region Keywords
    Select,
    Filter,
    Sort,
    Page,
    #endregion
}
