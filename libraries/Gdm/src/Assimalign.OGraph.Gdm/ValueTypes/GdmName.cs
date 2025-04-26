using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Assimalign.OGraph.Gdm;

using Internal;

[DebuggerDisplay("{Value}")]
public readonly struct GdmName : IEquatable<GdmName>, IComparable<GdmName>
{
    private const string pattern = "^[a-zA-Z0-9]+$";

    public GdmName(string value)
    {
        ThrowHelper.ThrowIfNullOrEmpty(value);
        
        if (Regex.IsMatch(value, pattern))
        {
            ThrowHelper.ThrowInvalidName(value);
        }

        Value = value;
    }

    #region Properties

    /// <summary>
    /// The raw string value.
    /// </summary>
    public string Value { get; }

    #endregion

    #region Methods

    /// <summary>
    /// Converts the string to pascal case.
    /// </summary>
    /// <returns></returns>
    public GdmName ToPascalCase()
    {
        return AsPascalCase(Value);
    }

    /// <summary>
    /// Converts the name to camal case
    /// </summary>
    /// <returns></returns>
    public GdmName ToCamalCase()
    {
        return AsCamalCase(Value);
    }

    /// <summary>
    /// Creates a name by converting the the string value to camal case.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static GdmName AsCamalCase(string value)
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
    ///  Creates a name by converting the the string value to pascal case.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static GdmName AsPascalCase(string value)
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

    /// <inheritdoc cref="global::System.IEquatable{T}"/>
    public bool Equals(GdmName other)
    {
        return (Value, other.Value) switch
        {
            (null, null) => true,
            (null, _) => false,
            (_, null) => false,
            (_, _) => Value.Equals(other.Value, StringComparison.Ordinal),
        };
    }

    /// <inheritdoc cref="global::System.IComparable{TSelf}"/>
    public int CompareTo(GdmName other)
    {
        return (Value, other.Value) switch
        {
            (null, null) => 0,
            (null, _) => -1,
            (_, null) => 1,
            (_, _) => StringComparer.Ordinal.Compare(Value, other.Value),
        };
    }

    public static bool IsValid(string value)
    {
        return Regex.IsMatch(value, pattern);
    }

    #endregion

    #region Overloads

    public override string ToString()
    {
        return Value;
    }

    public override bool Equals(object? obj)
    {
        return obj is GdmName name ? Equals(name) : false;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }


    #endregion

    #region Operators

    public static implicit operator GdmName (string value) => new GdmName(value);

    public static implicit operator string(GdmName name) => name.Value;

    public static bool operator ==(GdmName left, GdmName right) => left.Equals(right);
    public static bool operator !=(GdmName left, GdmName right) => !left.Equals(right);

    #endregion
}
