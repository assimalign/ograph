using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Assimalign.OGraph.Gdm;

using Internal;

/// <summary>
/// 
/// </summary>
[DebuggerDisplay("{Value,nq}")]
public readonly struct GdmName : IEquatable<GdmName>, IComparable<GdmName>
{
    private const string pattern = "^[a-zA-Z0-9_@.-]+$";

    private readonly string _value = string.Empty;

    /// <summary>
    /// Default constructor.
    /// </summary>
    /// <param name="value"></param>
    public GdmName(string value)
    {
        ThrowHelper.ThrowIfNullOrEmpty(value);

        if (!Regex.IsMatch(value, pattern))
        {
            ThrowHelper.ThrowInvalidName(value);
        }

        _value = value;
    }

    #region Properties

    /// <summary>
    /// The raw string value.
    /// </summary>
    public string Value => _value;

    /// <summary>
    /// Checks whether the name is empty.
    /// </summary>
    public bool IsEmpty => string.IsNullOrEmpty(_value);

    #endregion

    #region Methods

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public ReadOnlySpan<char> AsSpan()
    {
        return _value.AsSpan();
    }

    /// <summary>
    /// Converts the name to Camel case
    /// </summary>
    /// <returns></returns>
    public GdmName ToCamelCase()
    {
        return AsCamelCase(_value);
    }

    /// <summary>
    /// Converts the string to pascal case.
    /// </summary>
    /// <returns></returns>
    public GdmName ToPascalCase()
    {
        return AsPascalCase(_value);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="lowercase"></param>
    /// <returns></returns>
    public GdmName ToKebabCase(bool lowercase = true)
    {
        return AsKebabCase(_value, lowercase);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="lowercase"></param>
    /// <returns></returns>
    public GdmName ToSnakeCase(bool lowercase = true)
    {
        return AsSnakeCase(_value, lowercase);
    }

    /// <summary>
    /// Creates a name by converting the the string value to Camel case.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static GdmName AsCamelCase(string value)
    {
        return value.ConvertToCamelCase();
    }

    /// <summary>
    ///  Creates a name by converting the the string value to pascal case.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static GdmName AsPascalCase(string value)
    {
        return value.ConvertToPascalCase();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="lowercase"></param>
    /// <returns></returns>
    public static GdmName AsKebabCase(string value, bool lowercase = true)
    {
        return value.ConvertToKebabCase(lowercase);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="lowercase"></param>
    /// <returns></returns>
    public static GdmName AsSnakeCase(string value, bool lowercase = true)
    {
        return value.ConvertToSnakeCase(lowercase);
    }

    /// <inheritdoc cref="global::System.IEquatable{T}"/>
    public bool Equals(GdmName other)
    {
        return Equals(other._value, StringComparison.Ordinal);
    }

    public bool Equals (GdmName other, StringComparison comparisonType)
    {
        return (_value, other._value) switch
        {
            (null, null) => true,
            (null, _) => false,
            (_, null) => false,
            (_, _) => _value.Equals(other._value, comparisonType),
        };
    }

    /// <inheritdoc cref="global::System.IComparable{TSelf}"/>
    public int CompareTo(GdmName other)
    {
        return CompareTo(other, StringComparison.Ordinal);
        
    }

    public int CompareTo(GdmName other, StringComparison comparisonType)
    {
        return (_value, other._value) switch
        {
            (null, null) => 0,
            (null, _) => -1,
            (_, null) => 1,
            (_, _) => StringComparer.FromComparison(comparisonType).Compare(_value, other._value),
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
        return _value;
    }
    public override bool Equals(object? obj)
    {
        return obj is GdmName name ? Equals(name) : false;
    }
    public override int GetHashCode()
    {
        return _value.GetHashCode();
    }

    #endregion

    #region Operators

    public static implicit operator GdmName(string value) => new GdmName(value);

    public static implicit operator string(GdmName name) => name._value;
    public static bool operator ==(GdmName left, GdmName right) => left.Equals(right);
    public static bool operator !=(GdmName left, GdmName right) => !left.Equals(right);

    #endregion
}
