using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public readonly struct Name : 
    IEquatable<Name>, 
    IEqualityComparer<Name>,
    IComparable<Name>
{
    // Allowed characters for name
    private const string expression = "^[a-zA-Z0-9]+$";

    public Name(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentNullException(nameof(value));
        }
        if (!Regex.IsMatch(value, expression))
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
    public string ToPascalCase()
    {
        return string.Create(Value.Length, Value, (chars, name) =>
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

    /// <summary>
    /// Converts the name to camal case
    /// </summary>
    /// <returns></returns>
    public string ToCamalCase()
    {
        return string.Create(Value.Length, Value, (chars, name) =>
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

    /// <inheritdoc />
    public override string ToString()
    {
        return Value;
    }

    /// <inheritdoc />
    public override bool Equals([NotNullWhen(true)] object? instance)
    {
        if (instance is not Name name)
        {
            return false;
        }

        return this.Equals(name);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        // TODO: Need to revisit. Not sure if I want the HashCode for the name to be the same as the instace of the string.
        return Value.ToLowerInvariant().GetHashCode();
    }

    /// <inheritdoc />
    public bool Equals(Name name)
    {
        return Value.Equals(name.Value, StringComparison.OrdinalIgnoreCase);
    }

    /// <inheritdoc />
    public bool Equals(Name left, Name right)
    {
        return left.Equals(right);
    }

    /// <inheritdoc />
    public int GetHashCode([DisallowNull] Name name)
    {
        return name.GetHashCode();
    }

    /// <inheritdoc />
    public int CompareTo(Name name)
    {
        return Value.ToLowerInvariant().CompareTo(name.Value.ToLowerInvariant());
    }

    public static implicit operator Name(string value) => new Name(value);
    public static implicit operator string(Name name) => name.Value;
    public static bool operator ==(Name left, Name right) => left.Equals(right);
    public static bool operator !=(Name left, Name right) => !left.Equals(right);
    public static bool operator <(Name left, Name right) => left.CompareTo(right) < 0;
    public static bool operator >(Name left, Name right) => left.CompareTo(right) > 0;
    public static bool operator <=(Name left, Name right) => left.CompareTo(right) <= 0;
    public static bool operator >=(Name left, Name right) => left.CompareTo(right) >= 0;
}