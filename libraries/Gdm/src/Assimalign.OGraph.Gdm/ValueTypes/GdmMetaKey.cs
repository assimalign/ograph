using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Assimalign.OGraph.Gdm;

[DebuggerDisplay("{ToString(), n}")]
public readonly struct GdmMetaKey : IEquatable<GdmMetaKey>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public GdmMetaKey(string value)
    {
        Value = value;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="provider"></param>
    public GdmMetaKey(string value, string provider) : this(value)
    {
        Provider = provider;
    }

    #region Properties

    /// <summary>
    /// The raw key value.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// 
    /// </summary>
    public string? Provider { get; }

    #endregion

    #region Overloads

    public override string ToString()
    {
        return string.Join('@', Value, Provider);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }


    #endregion

    #region Methods

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public ReadOnlySpan<char> AsSpan()
    {
        return Value.AsSpan();
    }

    public static GdmMetaKey Parse(string value)
    {
        var parts = value.Split('@');
        
        if (parts.Length == 1)
        {
            return new GdmMetaKey(parts[0]);
        }
        else if (parts.Length == 2)
        {
            return new GdmMetaKey(parts[0], parts[1]);
        }
        else
        {
            throw new FormatException("The format of the meta key is invalid.");
        }
    }

    public bool Equals(GdmMetaKey other)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region Operators

    public static implicit operator GdmMetaKey(string value)
    {
        return Parse(value);
    }


    #endregion
}
