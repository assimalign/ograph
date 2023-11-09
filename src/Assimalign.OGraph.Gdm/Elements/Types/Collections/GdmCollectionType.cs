using System;
using System.Xml;
using System.Text.Json;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Assimalign.OGraph.Gdm;

using Assimalign.OGraph.Gdm.Internal;

[DebuggerDisplay("Gdm Type ({Kind}): {Label}")]
public class GdmCollectionType<TGdmType> : IOGraphGdmCollectionType
    where TGdmType : IOGraphGdmType, new()
{
    internal Label label;

    public GdmCollectionType()
    {
        ItemType = new TGdmType();
        label = $"{ItemType.Label}Collection";
    }

    public GdmCollectionType(TGdmType itemType)
    {
        if (itemType is null)
        {
            throw new ArgumentNullException(nameof(itemType));
        }
        ItemType = itemType; 
        label = $"{ItemType.Label}Collection";
    }

    public virtual Label Label => label;
    public TGdmType ItemType { get; }
    IOGraphGdmType IOGraphGdmCollectionType.ItemType => ItemType;
    public GdmTypeKind Kind => GdmTypeKind.Collection;
    public Type RuntimeType => typeof(IEnumerable<>).MakeGenericType(ItemType.RuntimeType!);
    public GdmElementType ElementType => GdmElementType.Type;
    public virtual void Write(Utf8JsonWriter writer, object value)
    {
        if (value is null)
        {
            GdmThrowHelper.ThrowInvalidContentException(nameof(value));
        }
        if (value is not IEnumerable enumerable)
        {
            GdmThrowHelper.ThrowInvalidContentException("");
        }
        else
        {
            writer.WriteStartArray();

            foreach (var item in enumerable)
            {
                ItemType.Write(writer, item);
            }

            writer.WriteEndArray();
        }
    }
    public virtual void Write(XmlWriter writer, object value)
    {
        if (value is null)
        {
            GdmThrowHelper.ThrowInvalidContentException(nameof(value));
        }
        if (value is not IEnumerable enumerable)
        {
            GdmThrowHelper.ThrowInvalidContentException("");
        }
        else
        {
            writer.WriteStartElement(Label);

            foreach (var item in enumerable)
            {
                ItemType.Write(writer, item);
            }

            writer.WriteEndElement();
        }
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