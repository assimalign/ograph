using System;
using System.Xml;
using System.Text.Json;
using System.Collections;
using System.Diagnostics;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.OGraph.Gdm;

using Assimalign.OGraph.Gdm.Internal;

[DebuggerDisplay("Gdm Type ({Kind}): {Label}")]
public class GdmListType<T> : GdmCollectionType<List<T>, T>
    where T : new()
{
    public GdmListType(GdmType<T> itemType)
    {
        if (itemType is null)
        {
            GdmThrowHelper.ThrowArgumentNullException(nameof(itemType));
        }

        ItemType = itemType;
    }

    public override Label Label => throw new NotImplementedException();
    public override GdmType<T> ItemType { get; }

    public override List<T> Read(ref Utf8JsonReader reader)
    {
        var list = new List<T>();
        var startDepth = reader.CurrentDepth;

        while (reader.Read() || reader.CurrentDepth <= startDepth)
        {
            list.Add(ItemType.Read(ref reader));
        }

        return list;
    }

    public override List<T> Read(XmlReader reader)
    {
        var list = new List<T>();


        return list;
    }

    public override void Write(Utf8JsonWriter writer, List<T> value)
    {
        writer.WriteStartArray();

        foreach (var item in value)
        {
            ItemType.Write(writer, item);
        }

        writer.WriteEndArray();
    }

    public override void Write(XmlWriter writer, List<T> value)
    {
        writer.WriteStartElement("");

        foreach (var item in value)
        {
            ItemType.Write(writer, item);
        }

        writer.WriteEndElement();
    }
}