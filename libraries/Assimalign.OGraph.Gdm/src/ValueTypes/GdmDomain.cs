using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

using Internal;

[DebuggerDisplay("_value")]
public readonly struct GdmDomain : IEquatable<GdmDomain>
{
    public readonly string _value = string.Empty;

    public GdmDomain(string domain)
    {
        ThrowHelper.ThrowIfNullOrEmpty(domain);

        _value = domain;
    }



    public bool IsEmpty => string.IsNullOrEmpty(_value);


    public bool Equals(GdmDomain other)
    {
        throw new NotImplementedException();
    }

    #region Overloads

    public override string ToString()
    {
        return _value;
    }
    public override bool Equals(object? obj)
    {
        return obj is GdmDomain domain ? Equals(domain) : false;
    }
    public override int GetHashCode()
    {
        return _value.GetHashCode();
    }

    #endregion

    #region Operators

    public static implicit operator GdmDomain(string domain)
    {
        return new GdmDomain(domain);
    }
    public static bool operator ==(GdmDomain left, GdmDomain right)
    {
        return left._value == right._value;
    }
    public static bool operator !=(GdmDomain left, GdmDomain right)
    {
        return left._value != right._value;
    }
    #endregion
}
