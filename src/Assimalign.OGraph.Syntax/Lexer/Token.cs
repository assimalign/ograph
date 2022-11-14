using System;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax;

/// <summary>
/// 
/// </summary>
public readonly struct Token
{
    /// <summary>
    /// Specifies the start position for the given token within a sequence.
    /// </summary>
    public long Start { get; init; }
    /// <summary>
    /// Specifies the end position for the given token within a sequence.
    /// </summary>
    public long End { get; init; }
    /// <summary>
    /// The raw value as bytes.
    /// </summary>
    public byte[] Value { get; init; }
    /// <summary>
    /// The Value in bytes parsed as raw text with UTF8 encoding.
    /// </summary>
    public string ValueAsText => Encoding.UTF8.GetString(Value);
    /// <summary>
    /// Represents the token kind.
    /// </summary>
    public TokenType TokenType { get; init; }

    public bool IsKeyword
    {
        get
        {
            switch (TokenType)
            {
                case TokenType.Filter:
                case TokenType.Select:
                case TokenType.Sort:
                case TokenType.Page:
                case TokenType.Null:
                case TokenType.And:
                case TokenType.Or:
                    return true;
                default:
                    return false;
            }
        }
    }
    public bool IsLiteral
    {
        get
        {
            switch (TokenType)
            {
                case TokenType.String: 
                    return true;
                default:
                    return false;
            }
        }
    }
    public bool IsOperator
    {
        get
        {
            switch (TokenType)
            {
                case TokenType.Equal:
                case TokenType.NotEqual:
                case TokenType.GreaterThan:
                case TokenType.GreaterThanOrEqual:
                case TokenType.LessThan:
                case TokenType.LessThanOrEqual:
                case TokenType.And:
                case TokenType.Or:
                    return true;
                default: 
                    return false;
            }
        }
    }

    /// <summary>
    /// Parses the given token data as <see cref="DateOnly"/>.
    /// </summary>
    /// <returns></returns>
    public DateOnly GetDate() => DateOnly.Parse(ValueAsText);
    public DateTime GetDateTime() => DateTime.Parse(ValueAsText);
    public DateTimeOffset GetDateTimeOffset() => DateTimeOffset.Parse(ValueAsText);

}
