using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public readonly struct QueryKey :
    IEquatable<QueryKey>,
    IEqualityComparer<QueryKey>,
    IComparable<QueryKey>
{
    public QueryKey(string value)
    {
        Value = value;
    }
    /// <summary>
    /// 
    /// </summary>
    public string Value { get; }

    public bool Equals(QueryKey other)
    {
        throw new NotImplementedException();
    }

    int IComparable<QueryKey>.CompareTo(QueryKey other)
    {
        throw new NotImplementedException();
    }

    bool IEqualityComparer<QueryKey>.Equals(QueryKey x, QueryKey y)
    {
        throw new NotImplementedException();
    }

    int IEqualityComparer<QueryKey>.GetHashCode(QueryKey obj)
    {
        throw new NotImplementedException();
    }


    public static implicit operator string(QueryKey key) => key.Value;
    public static implicit operator QueryKey(string value) => new QueryKey(value);
}