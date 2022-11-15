using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax;

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
    Or                  = TokenType.Or,
    Any                 = TokenType.Any,
    All                 = TokenType.All,
    Alias               = TokenType.Alias,
    In                  = TokenType.In,
}
