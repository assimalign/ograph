using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

public readonly struct MetaKey
{
    public MetaKey(string value)
    {
        Value = value;
    }

    public MetaKey(string value, string decorator) : this(value)
    {
        Decorator = decorator;
    }
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
}
