using System;
using System.ComponentModel;
using System.Globalization;

namespace Assimalign.OGraph.Gdm;

using Assimalign.OGraph.Gdm.Internal;

[TypeConverter(typeof(PropertyNameTypeConverter))]
public readonly struct PropertyName : 
    IComparable<PropertyName>, 
    IEquatable<PropertyName>
{
    public PropertyName(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            ThrowHelper.ThrowArgumentNullException(nameof(value));
        }

        Value = value;
    }
    /// <summary>
    /// The raw property name.
    /// </summary>
    public string Value { get; }

    #region Overloads
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }
        return obj is PropertyName other && Equals(other);
    }
    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
    public override string ToString()
    {
        return Value;
    }
    #endregion

    #region Operators
    public static bool operator ==(PropertyName a, PropertyName b) => a.Equals(b);
    public static bool operator !=(PropertyName a, PropertyName b) => !(a == b);
    public static bool operator >(PropertyName a, PropertyName b) => a.CompareTo(b) > 0;
    public static bool operator <(PropertyName a, PropertyName b) => a.CompareTo(b) < 0;
    public static bool operator >=(PropertyName a, PropertyName b) => a.CompareTo(b) >= 0;
    public static bool operator <=(PropertyName a, PropertyName b) => a.CompareTo(b) <= 0;

    public static implicit operator PropertyName(string value) => new PropertyName(value);
    #endregion

    /// <inheritdoc cref="global::System.IEquatable{T}"/>
    public bool Equals(PropertyName other)
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
    public int CompareTo(PropertyName other)
    {
        return (Value, other.Value) switch
        {
            (null, null) => 0,
            (null, _) => -1,
            (_, null) => 1,
            (_, _) => string.CompareOrdinal(Value, other.Value),
        };
    }


    internal partial class PropertyNameTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
        {
            if (value is string stringValue)
            {
                return new PropertyName(stringValue);
            }

            return base.ConvertFrom(context, culture, value);
        }

        public override bool CanConvertTo(ITypeDescriptorContext? context, Type? sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertTo(context, sourceType);
        }

        public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value,Type destinationType)
        {
            if (value is PropertyName idValue)
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
