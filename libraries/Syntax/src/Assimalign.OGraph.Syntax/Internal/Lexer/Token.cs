using System;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;

namespace Assimalign.OGraph.Syntax.Internal;

/// <summary>
/// 
/// </summary>
[DebuggerDisplay("{TokenType}: {Text} ")]
internal readonly struct Token : IEquatable<Token>
{
    /// <summary>
    /// Specifies the start position for the given token within a sequence.
    /// </summary>
    internal int Start { get; init; }
    /// <summary>
    /// Specifies the end position for the given token within a sequence.
    /// </summary>
    internal int End { get; init; }
    /// <summary>
    /// The line number the token is on.
    /// </summary>
    internal int Line { get; init; }
    /// <summary>
    /// The raw value as bytes.
    /// </summary>
    internal ReadOnlyMemory<byte> Value { get; init; }
    /// <summary>
    /// The raw text in the set encoding.
    /// </summary>
    internal string Text { get; init; }
    /// <summary>
    /// Represents the token kind.
    /// </summary>
    internal TokenType TokenType { get; init; }
    /// <summary>
    /// 
    /// </summary>
    internal bool IsKeyword
    {
        get
        {
            switch (TokenType)
            {
                case TokenType.Filter:
                case TokenType.Project:
                case TokenType.Sort:
                case TokenType.Page:
                case TokenType.Edge:
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
    internal bool IsLiteral
    {
        get
        {
            switch (TokenType)
            {
                case TokenType.String:
                case TokenType.FloatingPoint:
                case TokenType.Boolean:
                case TokenType.Integer:
                case TokenType.Null:
                    return true;
                default:
                    return false;
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    internal bool IsOperator
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

    internal bool IsIdentifier => TokenType == TokenType.Identifier;

    public override string ToString()
    {
        return $"{TokenType} - {Text}";
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(typeof(Token), Value);
    }

    public override bool Equals(object? obj)
    {
        if (obj is Token token)
        {
            return Equals(token);
        }
        return false;
    }

    public bool Equals(Token other)
    {
        var left = Value.Span;
        var right = other.Value.Span;

        return left.SequenceEqual(right);
    }
}