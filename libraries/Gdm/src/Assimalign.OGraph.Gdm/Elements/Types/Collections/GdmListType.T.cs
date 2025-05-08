using System;
using System.Xml;
using System.Text.Json;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm.Elements;

using Assimalign.OGraph.Gdm.Internal;

public class GdmListType<T> : GdmCollectionType<List<T>, T>
{
    private static GdmName CreateName<TType>()
    {
        return typeof(TType).Name + "List";
    }

    public GdmListType(GdmType itemType, GdmGraph graph)  : base(CreateName<T>(), itemType, graph)
    {
        ////Graph = ThrowHelper.ThrowIfNull(graph, nameof(graph));
        //ItemType = ThrowHelper.ThrowIfNull(itemType, nameof(itemType));
 
        //if (GdmLabel.IsValid(ItemType!.RuntimeType!.Name))
        //{
        //    Name = ItemType!.RuntimeType!.Name + "List";
        //}
    }

    public GdmListType(GdmName name, GdmType itemType, GdmGraph graph) : base(name, itemType, graph)
    {

    }

    #region Overloads

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

    #region Static Members
    //public static GdmListType<T> Create<TGdmType>() where TGdmType : GdmScalarType<T>, new()
    //{
    //    return new GdmListType<T>(new TGdmType());
    //}
    //public static GdmListType<T> Create(IOGraphGdmType type)
    //{
    //    return new(type);
    //}
    #endregion
}