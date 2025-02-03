using System;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public readonly struct MetaKey : IComparable<MetaKey>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    public MetaKey(string key)
    {
        Value = key;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="decorator"></param>
    public MetaKey(string key, string decorator) : this(key)
    {
        Decorator = decorator;
    }

    #region Properties

    /// <summary>
    /// 
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Use this decorator to enhance metadata information.
    /// </remarks>
    public string? Decorator { get; }

    #endregion

    #region Overloads

    public override string ToString()
    {
        return string.Join('@', Value, Decorator);
    }

    #endregion

    #region Methods

    public int CompareTo(MetaKey other)
    {
        throw new NotImplementedException();
    }

    #endregion
}
