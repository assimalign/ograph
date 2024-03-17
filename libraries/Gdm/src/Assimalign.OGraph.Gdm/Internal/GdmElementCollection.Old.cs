using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm.Internal;

[DebuggerDisplay("Count = {Count}")]
internal class GdmElementCollectionT : IOGraphGdmElementCollection
{
    private readonly HashSet<IOGraphGdmElement> elements;

    public GdmElementCollectionT()
    {
        elements = new HashSet<IOGraphGdmElement>();
    }

    public int Count => elements.Count;
    public bool IsReadOnly { get; internal set; }
    public void Add(IOGraphGdmElement item)
    {
        AssertReadOnly();
        AssertNotNull(item);

        switch (item)
        {
            case IOGraphGdmProperty property:
                {
                    Add(property);
                    break;
                }
            case IOGraphGdmType type:
                {
                    Add(type);
                    break;
                }
            case IOGraphGdmVertex vertex:
                {
                    Add(vertex);
                    break;
                }
        }
    }
    
    
    
    
    private void Add(IOGraphGdmProperty property)
    {
        elements.Add(property);

        if (property.Type is not null)
        {
            Add(property.Type.Definition);
        }
    }
    private void Add(IOGraphGdmVertex vertex)
    {
        elements.Add(vertex);

        Add(vertex.Type.Definition);
    }
    private void Add(IOGraphGdmType type)
    {
        switch (type)
        {
            case IOGraphGdmPrimitiveType pt:
                {
                    Add(pt);
                    break;
                }
            case IOGraphGdmEnumType et:
                {
                    Add(et);
                    break;
                }
            case IOGraphGdmComplexType cpt:
                {
                    Add(cpt);
                    break;
                }
            case IOGraphGdmCollectionType ct:
                {
                    Add(ct);
                    break;
                }
        }
    }
    private void Add(IOGraphGdmComplexType type)
    {
        elements.Add(type);

        foreach (var property in type.Properties)
        {
            Add(property);
        }
    }
    private void Add(IOGraphGdmCollectionType type)
    {
        elements.Add(type);

        Add(type.ItemType);
    }
    private void Add(IOGraphGdmEnumType type)
    {
        elements.Add(type);
    }
    private void Add(IOGraphGdmPrimitiveType type)
    {
        elements.Add(type);
    }

    public bool Remove(IOGraphGdmElement item)
    {
        AssertReadOnly();
        AssertNotNull(item);
        throw new NotImplementedException();
    }
    public void Clear()
    {
        AssertReadOnly();
        elements.Clear();
    }
    public bool Contains(IOGraphGdmElement item)
    {
        AssertNotNull(item);
        throw new NotImplementedException();
    }

    public void CopyTo(IOGraphGdmElement[] array, int arrayIndex)
    {
        if (array is null) throw new ArgumentNullException("array");
        elements.CopyTo(array, arrayIndex);
    }

    public IEnumerator<IOGraphGdmElement> GetEnumerator()
    {
        return elements.GetEnumerator();
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }


    private void AssertReadOnly()
    {
        if (IsReadOnly)
        {
            throw new InvalidOperationException("The collection is readonly.");
        }
    }
    private void AssertNotNull(object item)
    {
        if (item is null)
        {
            throw new ArgumentNullException(nameof(item));
        }
    }
}
