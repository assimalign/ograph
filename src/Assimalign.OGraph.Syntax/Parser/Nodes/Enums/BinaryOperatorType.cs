using System;

namespace Assimalign.OGraph.Syntax;

using Assimalign.OGraph.Syntax.Lexer;

public enum BinaryOperatorType
{
    None,
    Star                = TokenType.Star,
    Plus                = TokenType.Plus,
    Minus               = TokenType.Minus,
    Equal               = TokenType.Equal,
    NotEqual            = TokenType.NotEqual,
    GreaterThan         = TokenType.GreaterThan,
    GreaterThanOrEqual  = TokenType.GreaterThanOrEqual,
    LessThan            = TokenType.LessThan,
    LessThanOrEqual     = TokenType.LessThanOrEqual,
    And                 = TokenType.And,
    Or                  = TokenType.Or
}
