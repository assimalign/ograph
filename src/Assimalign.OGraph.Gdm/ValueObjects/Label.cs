using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
[DebuggerDisplay("{Value}")]
public readonly struct Label : 
    IEquatable<Label>, 
    IEqualityComparer<Label>,
    IComparable<Label>
{
    // Allowed characters for name
    private const string pattern = "^[a-zA-Z0-9_-]+$";

    public Label(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentNullException(nameof(value));
        }
        if (!Regex.IsMatch(value, pattern))
        {
            throw new ArgumentException($"The following name: '{value}' contains invalid characters. Only the following characters are: [A-Z, a-z, 0-9]");
        }
        Value = value;
    }

    /// <summary>
    /// The raw name value.
    /// </summary>
    public readonly string Value { get; }

    /// <summary>
    /// Converts the string to pascal case.
    /// </summary>
    /// <returns></returns>
    public Label ToPascalCase()
    {
        return AsPascalCase(Value);
    }

    /// <summary>
    /// Converts the name to camal case
    /// </summary>
    /// <returns></returns>
    public Label ToCamalCase()
    {
        return AsCamalCase(Value);
    }

    /// <summary>
    /// Creates a label by converting the the string value to camal case.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static Label AsCamalCase(string value)
    {
        return string.Create(value.Length, value, (chars, name) =>
        {
            name.CopyTo(chars);

            for (int i = 0; i < chars.Length && (i != 1 || char.IsUpper(chars[i])); i++)
            {
                bool flag = i + 1 < chars.Length;
                if (i > 0 && flag && !char.IsUpper(chars[i + 1]))
                {
                    if (chars[i + 1] == ' ')
                    {
                        chars[i] = char.ToLowerInvariant(chars[i]);
                    }
                    break;
                }
                chars[i] = char.ToLowerInvariant(chars[i]);
            }
        });
    }

    /// <summary>
    ///  Creates a label by converting the the string value to pascal case.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static Label AsPascalCase(string value)
    {
        return string.Create(value.Length, value, (chars, name) =>
        {
            name.CopyTo(chars);

            for (int i = 0; i < chars.Length && (i != 1 || char.IsUpper(chars[i])); i++)
            {
                bool flag = i + 1 < chars.Length;
                if (i > 0 && flag && !char.IsUpper(chars[i + 1]))
                {
                    if (chars[i + 1] == ' ')
                    {
                        chars[i] = char.ToUpperInvariant(chars[i]);
                    }
                    break;
                }
                chars[i] = char.ToUpperInvariant(chars[i]);
            }
        });
    }


    /// <inheritdoc />
    public override string ToString()
    {
        return Value;
    }

    /// <inheritdoc />
    public override bool Equals([NotNullWhen(true)] object? instance)
    {
        if (instance is not Label name)
        {
            return false;
        }

        return this.Equals(name);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        // TODO: Need to revisit. Not sure if I want the HashCode for the name to be the same as the instance of the string.
        return Value.GetHashCode();
    }

    /// <inheritdoc />
    public bool Equals(Label name)
    {
        return Value.Equals(name.Value, StringComparison.Ordinal);
    }

    /// <inheritdoc />
    public bool Equals(Label left, Label right)
    {
        return left.Equals(right);
    }

    /// <inheritdoc />
    public int GetHashCode([DisallowNull] Label name)
    {
        return name.GetHashCode();
    }

    /// <inheritdoc />
    public int CompareTo(Label name)
    {
        return Value.CompareTo(name.Value);
    }

    public static implicit operator Label(string value) => new Label(value);
    public static implicit operator string(Label name) => name.Value;
    public static bool operator ==(Label left, Label right) => left.Equals(right);
    public static bool operator !=(Label left, Label right) => !left.Equals(right);
    public static bool operator <(Label left, Label right) => left.CompareTo(right) < 0;
    public static bool operator >(Label left, Label right) => left.CompareTo(right) > 0;
    public static bool operator <=(Label left, Label right) => left.CompareTo(right) <= 0;
    public static bool operator >=(Label left, Label right) => left.CompareTo(right) >= 0;


    /// <summary>
    /// Specifies whether the <paramref name="name"/> is a valid name.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static bool IsValid(string name) => Regex.IsMatch(name, pattern);
}