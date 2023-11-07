using System;
using System.Xml;
using System.Text.Json;
using System.Collections.Generic;
using System.Diagnostics;

namespace Assimalign.OGraph.Gdm;

[DebuggerDisplay("Gdm Type ({Kind}): {Label}")]
public class GdmCollectionType<TGdmType> : IOGraphGdmCollectionType
    where TGdmType : IOGraphGdmType, new()
{
    internal Label label;

    public GdmCollectionType()
    {
        ItemType = new TGdmType();
        label = Label.AsPascalCase($"{ItemType.Label}CollectionType");
    }

    public virtual Label Label => label;
    public TGdmType ItemType { get; }
    IOGraphGdmType IOGraphGdmCollectionType.ItemType => ItemType;
    public GdmTypeKind Kind => GdmTypeKind.Collection;
    public Type RuntimeType => typeof(IEnumerable<>).MakeGenericType(ItemType.RuntimeType!);
    public GdmElementType ElementType => GdmElementType.Type;
    public virtual void Write(Utf8JsonWriter writer, object value)
    {
        writer.WriteStartArray();



        writer.WriteEndArray();
    }
    public virtual void Write(XmlWriter writer, object value)
    {
        throw new NotImplementedException();
    }

    public virtual object Read(ref Utf8JsonReader reader)
    {
        throw new NotImplementedException();
    }

    public virtual object Read(XmlReader reader)
    {
        throw new NotImplementedException();
    }
}