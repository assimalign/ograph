using System;
using System.Linq;
using System.Xml;
using System.Text.Json;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm.Elements;

using Assimalign.OGraph.Gdm.Internal;

public class GdmListType<T> : GdmCollectionType<List<T>, T>
{
    public GdmListType(GdmGraph graph) : this (CreateName<T>(), Find<T>(graph), graph)
    {
    }

    public GdmListType(GdmType itemType, GdmGraph graph)  : base(CreateName<T>(), itemType, graph)
    {
    }

    public GdmListType(GdmName name, GdmType itemType, GdmGraph graph) : base(name, itemType, graph)
    {
    }

    #region Methods

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
        if (reader.LocalName != Name)
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

            if (reader.NodeType == XmlNodeType.EndElement && reader.LocalName == Name)
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
        writer.WriteStartElement(Name);

        foreach (var item in value)
        {
            ItemType.Write(writer, item!);
        }

        writer.WriteEndElement();
    }

    #endregion

    private static GdmName CreateName<TType>()
    {
        return typeof(TType).Name + "List";
    }

    private static GdmType Find<TType>(GdmGraph graph)
    {
        var types = (graph.Types as IEnumerable<GdmType>).Where(t => t.RuntimeType == typeof(TType));

        if (types.Count() == 0)
        {
            throw new InvalidOperationException($"Type '{typeof(TType).Name}' not found in graph types.");
        }

        if (types.Count() > 1)
        {
            throw new InvalidOperationException($"Multiple types found for '{typeof(TType).Name}' in graph. Expected a single type.");
        }

        return types.First();
    }
}