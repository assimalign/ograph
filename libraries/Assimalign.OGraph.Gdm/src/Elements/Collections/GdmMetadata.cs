using System;
using System.Collections.Generic;
using System.Collections;
using System.Runtime.InteropServices;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.OGraph.Gdm.Elements;

using Internal;

public sealed class GdmMetadata : IOGraphGdmMetaCollection
{
    private readonly Dictionary<GdmMetaKey, object> _meta;
    private readonly Dictionary<string, object>.AlternateLookup<ReadOnlySpan<char>> _lookup;

    public GdmMetadata()
    {
        _meta = new Dictionary<GdmMetaKey, object>();
        //_lookup = _meta.GetAlternateLookup<ReadOnlySpan<char>>();
    }

    public int Count => _meta.Count;
    public bool IsReadOnly => throw new System.NotImplementedException();
    public ICollection<GdmMetaKey> Keys => _meta.Keys;
    public ICollection<object> Values => _meta.Values;
    public object this[GdmMetaKey key]
    {
        get
        {
            return _lookup[key.AsSpan()];
        }
        set
        {
            _lookup[key.AsSpan()] = value;
        }
    }

    public T GetValue<T>(GdmMetaKey key)
    {
        if (!_lookup.TryGetValue(key.AsSpan(), out var obj))
        {
            throw new KeyNotFoundException();
        }

        if (obj is not T value)
        {
            throw new InvalidCastException();
        }

        return value;
    }

    public void Add(GdmMetaKey key, object value)
    {
        ThrowHelper.ThrowIfNull(value);

        _meta.Add(key, value);
    }

    public bool ContainsKey(GdmMetaKey key)
    {
        throw new NotImplementedException();
    }

    public bool Remove(GdmMetaKey key)
    {
        throw new NotImplementedException();
    }

    public bool TryGetValue(GdmMetaKey key, [MaybeNullWhen(false)] out object value)
    {
        throw new NotImplementedException();
    }

    public void Add(KeyValuePair<GdmMetaKey, object> item)
    {
        throw new NotImplementedException();
    }

    public void Clear()
    {
        throw new NotImplementedException();
    }

    public bool Contains(KeyValuePair<GdmMetaKey, object> item)
    {
        throw new NotImplementedException();
    }

    public void CopyTo(KeyValuePair<GdmMetaKey, object>[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }

    public bool Remove(KeyValuePair<GdmMetaKey, object> item)
    {
        throw new NotImplementedException();
    }

    public IEnumerator<KeyValuePair<GdmMetaKey, object>> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
