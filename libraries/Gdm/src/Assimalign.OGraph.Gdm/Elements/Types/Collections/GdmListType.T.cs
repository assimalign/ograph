using System;
using System.Xml;
using System.Text.Json;
using System.Diagnostics;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm.Elements;

using Assimalign.OGraph.Gdm.Internal;

public class GdmListType<T> : GdmCollectionType<List<T>, T>
{
    public GdmListType(IOGraphGdmType itemType)
    {
        if (itemType is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(itemType));
        }
        if (!itemType!.RuntimeType!.IsAssignableTo(typeof(T)))
        {
            ThrowHelper.ThrowArgumentException("");
        }

        ItemType = itemType;

        if (Label.IsValid(ItemType!.RuntimeType!.Name))
        {
            label = ItemType!.RuntimeType!.Name + "List";
        }
    }

    #region Overloads
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
    #endregion

    #region Static Members
    public static GdmListType<T> Create<TGdmType>() where TGdmType : GdmScalarType<T>, new()
    {
        return new GdmListType<T>(new TGdmType());
    }
    public static GdmListType<T> Create(IOGraphGdmType type)
    {
        return new(type);
    }
    #endregion
}