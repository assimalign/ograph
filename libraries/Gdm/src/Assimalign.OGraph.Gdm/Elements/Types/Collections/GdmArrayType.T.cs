using System;
using System.Xml;
using System.Text.Json;

namespace Assimalign.OGraph.Gdm;

using Assimalign.OGraph.Gdm.Internal;

public class GdmArrayType<T> : GdmCollectionType<T[], T>
{
    public GdmArrayType(IOGraphGdmType itemType)
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
            label = ItemType!.RuntimeType!.Name + "Array";
        }
    }

    public override IOGraphGdmType ItemType { get; }

    public override T[] Read(ref Utf8JsonReader reader)
    {
        var items = new T[4]; // use 4 as buffer
        var count = 0;



        Array.Resize<T>(ref items, count);
        return items;
    }

    public override T[] Read(XmlReader reader)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, T[] value)
    {
        throw new NotImplementedException();
    }

    public override void Write(XmlWriter writer, T[] value)
    {
        throw new NotImplementedException();
    }
}
