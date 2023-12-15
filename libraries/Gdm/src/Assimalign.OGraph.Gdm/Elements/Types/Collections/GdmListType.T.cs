using System;
using System.Xml;
using System.Text.Json;
using System.Diagnostics;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

using Assimalign.OGraph.Gdm.Internal;


[DebuggerDisplay("Gdm Type ({Kind}): {Label}")]
public class GdmListType<T> : GdmCollectionType<List<T>, T>
    where T : new()
{
    public static GdmListType<T> Create<TGdmType>() where TGdmType : GdmPrimitiveType<T>, new()
    {
        return new GdmListType<T>(new TGdmType());
    }
    public static GdmListType<T> Create(IOGraphGdmType type)
    {
        return new(type);
    }

    public GdmListType(IOGraphGdmType itemType)
    {
        if (itemType is null)
        {
            GdmThrowHelper.ThrowArgumentNullException(nameof(itemType));
        }
        if (!ItemType!.RuntimeType!.IsAssignableTo(typeof(T)))
        {
            GdmThrowHelper.ThrowArgumentException("");
        }

        ItemType = itemType;

        if (Label.IsValid(ItemType!.RuntimeType!.Name))
        {
            label = ItemType!.RuntimeType!.Name + "List";
        }
    }

    public override IOGraphGdmType ItemType { get; }
    public override List<T> Read(ref Utf8JsonReader reader)
    {
        var list = new List<T>();
        var startDepth = reader.CurrentDepth;

        while (reader.Read() && reader.CurrentDepth > startDepth)
        {
            if (ItemType.Read(ref reader) is not T item) 
            {
                throw new Exception();
            }
            list.Add(item);
        }

        return list;
    }

    public override List<T> Read(XmlReader reader)
    {
        if (reader.NodeType != XmlNodeType.Element)
        {
            // TODO: throw invalid exception
        }
        if (reader.LocalName != Label)
        {
            // TODO: throw invalid exception
        }

        var list = new List<T>();
        var startDepth = reader.Depth;

        while (reader.Read() && reader.Depth > startDepth)
        {
            if (ItemType.Read(reader) is not T item)
            {
                throw new Exception();
            }
            list.Add(item);

            if (reader.NodeType == XmlNodeType.EndElement && reader.LocalName == Label)
            {
                break;
            }
        }

        return list;
    }

    public override void Write(Utf8JsonWriter writer, List<T> value)
    {
        writer.WriteStartArray();

        foreach (var item in value)
        {
            ItemType.Write(writer, item!);
        }

        writer.WriteEndArray();
    }

    public override void Write(XmlWriter writer, List<T> value)
    {
        writer.WriteStartElement(Label);

        foreach (var item in value)
        {
            ItemType.Write(writer, item!);
        }

        writer.WriteEndElement();
    }
}