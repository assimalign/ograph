using System;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax.Internal;

/// <summary>
/// 
/// </summary>
internal readonly struct Token
{
    /// <summary>
    /// Specifies the start position for the given token within a sequence.
    /// </summary>
    public int Start { get; init; }
    /// <summary>
    /// Specifies the end position for the given token within a sequence.
    /// </summary>
    public int End { get; init; }
    /// <summary>
    /// The raw value as bytes.
    /// </summary>
    public ReadOnlyMemory<byte> Value { get; init; }
    /// <summary>
    /// The Value in bytes parsed as raw text with UTF8 encoding.
    /// </summary>
    public string ValueAsText => Encoding.UTF8.GetString(Value.ToArray());
    /// <summary>
    /// Represents the token kind.
    /// </summary>
    public TokenType TokenType { get; init; }
    /// <summary>
    /// 
    /// </summary>
    public bool IsKeyword
    {
        get
        {
            switch (TokenType)
            {
                case TokenType.Filter:
                case TokenType.Project:
                case TokenType.Sort:
                case TokenType.Page:
                case TokenType.Null:
                case TokenType.And:
                case TokenType.Or:
                case TokenType.Boolean:
                case TokenType.Alias:
                case TokenType.Ascending:
                case TokenType.Descending:
                case TokenType.Take:
                case TokenType.Skip:
                case TokenType.Token:
                    return true;
                default:
                    return false;
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public bool IsLiteral
    {
        get
        {
            switch (TokenType)
            {
                case TokenType.String:
                case TokenType.FloatingPoint:
                case TokenType.Boolean:
                case TokenType.Integer:
                    return true;
                default:
                    return false;
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public bool IsOperator
    {
        get
        {
            switch (TokenType)
            {
                case TokenType.Plus: 
                case TokenType.Minus:
                case TokenType.Star:
                case TokenType.Slash:
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

    #region Overloads
    public override string ToString()
    {
        return $"{TokenType} - {ValueAsText}";
    }
    #endregion
}