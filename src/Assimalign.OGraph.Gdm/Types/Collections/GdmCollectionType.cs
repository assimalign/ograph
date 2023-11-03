using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Xml;

namespace Assimalign.OGraph.Gdm;

public class GdmCollectionType<TType> : IOGraphGdmCollectionType
    where TType : IOGraphGdmType, new()
{
    public GdmCollectionType()
    {
        ItemType = new TType();
    }
    public virtual Label Label
    {
        get
        {
            return $"{ItemType.Label}Collection";
        }
    }

    public TType ItemType { get; }
    IOGraphGdmType IOGraphGdmCollectionType.ItemType => ItemType;
    public GdmTypeKind Kind => GdmTypeKind.Collection;
    public Type RuntimeType => typeof(IEnumerable<>).MakeGenericType(ItemType.RuntimeType!);
    public virtual bool IsAssignableTo(IOGraphGdmType type)
    {
        return RuntimeType!.IsAssignableFrom(type.RuntimeType);
    }

    public void Write(Utf8JsonWriter writer, object value)
    {
        throw new NotImplementedException();
    }

    public void Write(XmlWriter writer, object value)
    {
        throw new NotImplementedException();
    }

    public object Read(ref Utf8JsonReader reader)
    {
        throw new NotImplementedException();
    }

    public object Read(XmlReader reader)
    {
        throw new NotImplementedException();
    }
}