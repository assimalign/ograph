using System;
using System.Globalization;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

using Internal;

/// <summary>
/// 
/// </summary>
/// <remarks>
/// Allowed values: 
/// <list type="bullet">
/// <item>Arabic Letters: a-z, A-Z</item>
/// <item>Numeric Values: 0-9</item>
/// <item>Special Characters: - _ @</item>
/// </list>
/// </remarks>
[DebuggerDisplay("{Value}")]
[TypeConverter(typeof(LabelTypeConverter))]
public readonly struct GdmLabel : 
    IEquatable<GdmLabel>, 
    IEqualityComparer<GdmLabel>,
    IComparable<GdmLabel>
{
    // Allowed characters for name
    private const string pattern = "^[a-zA-Z0-9_@-]+$";

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="GdmException"></exception>
    public GdmLabel(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            ThrowHelper.ThrowArgumentNullException(nameof(value));
        }
        if (!Regex.IsMatch(value, pattern))
        {
            ThrowHelper.ThrowInvalidLabel(value);
        }
        Value = value;
    }

    /// <summary>
    /// The raw name value.
    /// </summary>
    public readonly string Value { get; }

    /// <summary>
    /// 
    /// </summary>
    public bool IsEmpty => string.IsNullOrEmpty(Value);

    #region Overloads

    /// <inheritdoc />
    public override string ToString()
    {
        return Value;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }
        return obj is GdmLabel other && Equals(other);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        // TODO: Need to revisit. Not sure if I want the HashCode for the name to be the same as the instance of the string.
        return Value.GetHashCode();
    }

    #endregion


    #region Methods
    /// <inheritdoc />
    public bool Equals(GdmLabel left, GdmLabel right)
    {
        return left.Equals(right);
    }

    /// <inheritdoc />
    public int GetHashCode([DisallowNull] GdmLabel name)
    {
        return name.GetHashCode();
    }


    /// <inheritdoc cref="global::System.IEquatable{T}"/>
    public bool Equals(GdmLabel other)
    {
        return (Value, other.Value) switch
        {
            (null, null) => true,
            (null, _) => false,
            (_, null) => false,
            (_, _) => Value.Equals(other.Value, StringComparison.OrdinalIgnoreCase),
        };
    }

    /// <inheritdoc cref="global::System.IComparable{TSelf}"/>
    public int CompareTo(GdmLabel other)
    {
        return (Value, other.Value) switch
        {
            (null, null) => 0,
            (null, _) => -1,
            (_, null) => 1,
            (_, _) => StringComparer.OrdinalIgnoreCase.Compare(Value, other.Value),
        };
    }

    /// <summary>
    /// Specifies whether the <paramref name="name"/> is a valid name.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static bool IsValid(string name)
    {
        return Regex.IsMatch(name, pattern);
    }

    #endregion

    #region operators

    public static implicit operator GdmLabel(string value) => new GdmLabel(value);

    public static implicit operator string(GdmLabel name) => name.Value;
    public static bool operator ==(GdmLabel left, GdmLabel right) => left.Equals(right);
    public static bool operator !=(GdmLabel left, GdmLabel right) => !left.Equals(right);

    #endregion

    internal partial class LabelTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
        {
            if (value is string stringValue)
            {
                return new GdmLabel(stringValue);
            }

            return base.ConvertFrom(context, culture, value);
        }

        public override bool CanConvertTo(ITypeDescriptorContext? context, Type? sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertTo(context, sourceType);
        }

        public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
        {
            if (value is GdmLabel idValue)
            {
                if (destinationType == typeof(string))
                {
                    return idValue.Value;
                }
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}