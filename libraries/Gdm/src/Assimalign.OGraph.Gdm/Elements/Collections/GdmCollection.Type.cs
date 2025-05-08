using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Assimalign.OGraph.Gdm.Elements;

using Internal;

[DebuggerDisplay("Count = {Count}")]
public class GdmTypeCollection : IOGraphGdmTypeCollection, IEnumerable<GdmType>
{
    private bool _isReadOnly;
    private readonly List<GdmType> _types;

    public GdmTypeCollection()
    {
        _types = new List<GdmType>();
    }

    internal GdmTypeCollection(List<GdmType> types)
    {
        _types = types;
        _isReadOnly = true;
    }

    public GdmType this[GdmName name]
    {
        get
        {
            for (int i = 0; i < Count; i++)
            {
                var type = _types[i]; 

                if (type.Name == name)
                {
                    return type;
                }
            }

            throw new KeyNotFoundException();
        }
    }
    public int Count => _types.Count;
    public bool IsReadOnly => _isReadOnly;
    public void Add(GdmType type)
    {
        ThrowHelper.ThrowIfNull(type);

        for (int i = 0; i < _types.Count; i++)
        {
            var existingType = _types[i];

            // No need to add a type that is already in the collection.
            if (object.ReferenceEquals(existingType, type))
            {
                return;
            }

            if (existingType.Name == type.Name)
            {
                Remove(existingType);
            }
        }

        _types.Add(type);
    }
    public void Remove(GdmType type)
    {
        ThrowHelper.ThrowIfNull(type);

        bool isRemoved = _types.Remove(type);

        // If not removed by reference, then force remove by name
        if (!isRemoved)
        {
            for (int i = 0; i < _types.Count; i++)
            {
                var existingType = _types[i];

                if (existingType.Name == type.Name)
                {
                    _types.RemoveAt(i);
                }
            }
        }
    }
    public IEnumerator<GdmType> GetEnumerator()
    {
        return _types.GetEnumerator();
    }

    private void AssertType(GdmType type)
    {
        switch (type)
        {
            case GdmComplexType complex when complex.Members.Count == 0:
                throw new ArgumentException("");
            case GdmComplexType complex when complex.Graph is null:
                throw new ArgumentException("");
        }
    }
    //public GdmType GetOrAdd(GdmName name, Type runtimeType, Func<GdmName, Type, GdmType> factory)
    //{
    //    GdmType? gdmType = default;

    //    for (int i = 0; i < _types.Count; i++)
    //    {
    //        gdmType = _types[i];

    //        if (gdmType.IsOfType(runtimeType) && gdmType.Name == name)
    //        {
    //            return gdmType;
    //        }
    //    }

    //    gdmType = factory.Invoke(name, runtimeType);

    //    if (gdmType is null)
    //    {
    //        ThrowHelper.ThrowInvalidOperationException("The type cannot be null");
    //    }

    //    _types.Add(gdmType);

    //    return gdmType;
    //}


    IOGraphGdmType IOGraphGdmTypeCollection.this[GdmName name] => this[name];
    void IOGraphGdmTypeCollection.Add(IOGraphGdmType type)
    {
        Add(ThrowHelper.ThrowIfNotType<GdmType>(type));
    }
    void IOGraphGdmTypeCollection.Remove(IOGraphGdmType type)
    {
        Remove(ThrowHelper.ThrowIfNotType<GdmType>(type));
    }
    IEnumerator<IOGraphGdmType> IEnumerable<IOGraphGdmType>.GetEnumerator()
    {
        return GetEnumerator();
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }


    //#if NET9_0_OR_GREATER
    //    private readonly Dictionary<GdmName, GdmType> _types;
    //    private readonly Dictionary<GdmName, GdmType>.AlternateLookup<ReadOnlySpan<char>> _lookup;

    //    public GdmTypeCollection()
    //    {
    //        _types = new Dictionary<GdmName, GdmType>();
    //        _lookup = _types.GetAlternateLookup<ReadOnlySpan<char>>();
    //    }

    //    public GdmType this[GdmName name]
    //    {
    //        get
    //        {
    //            return _lookup[name.AsSpan()];
    //        }
    //    }

    //    IOGraphGdmType IOGraphGdmTypeCollection.this[GdmName name] => this[name];

    //    bool IOGraphGdmTypeCollection.IsReadOnly => throw new NotImplementedException();

    //    void IOGraphGdmTypeCollection.Add(IOGraphGdmType type)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    IEnumerator<IOGraphGdmType> IEnumerable<IOGraphGdmType>.GetEnumerator()
    //    {
    //        return GetEnumerator();
    //    }

    //    void IOGraphGdmTypeCollection.Remove(IOGraphGdmType type)
    //    {
    //        throw new NotImplementedException();
    //    }

    //#else
    //#endif
}