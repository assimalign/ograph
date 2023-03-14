using System;
using System.Xml;
using System.Text.Json;
using System.Collections;

namespace Assimalign.OGraph;

public abstract class CollectionType<TCollection> : IOGraphCollectionType
    where TCollection : IEnumerable
{
    public virtual Name TypeName => nameof(TCollection);
    public abstract IOGraphType ItemType { get; }
    public OGraphTypeIdentifier TypeIdentifier => OGraphTypeIdentifier.Collection;
    public Type RuntimeType => typeof(TCollection);

    public abstract bool TryReadJson(Utf8JsonReader reader, out OGraphCollection collection);
    public abstract bool TryReadXml(XmlReader reader, out OGraphCollection collection);
    public abstract bool TryWriteJson(Utf8JsonWriter writer, OGraphCollection collection);
    public abstract bool TryWriteXml(XmlWriter writer, OGraphCollection collection);

    //public bool TryReadJson(Utf8JsonReader reader, out OGraphCollection collection)
    //{
    //    throw new NotImplementedException();
    //}

    //public bool TryReadXml(XmlReader reader, out OGraphCollection collection)
    //{
    //    throw new NotImplementedException();
    //}

    //public bool TryWriteJson(Utf8JsonWriter writer, OGraphCollection collection)
    //{
    //    if (ItemType is IOGraphComplexType complexType)
    //    {
    //        foreach (var item in collection.Items)
    //        {
    //            if (!item.IsComplexType(out var value))
    //            {
    //                return false;
    //            }
    //            if (!complexType.TryWriteJson(writer, value))
    //            {
    //                return false;
    //            }
    //        }

    //        return true;
    //    }
    //    if (ItemType is IOGraphPrimitiveType primitiveType)
    //    {
    //        foreach (var item in collection.Items)
    //        {
    //            if (!item.IsPrimitiveType(out var value))
    //            {
    //                return false;
    //            }
    //            if (!primitiveType.TryWriteJson(writer, value))
    //            {
    //                return false;
    //            }
    //        }

    //        return true;
    //    }
    //    if (ItemType is IOGraphCollectionType collectionType)
    //    {
    //        foreach (var item in collection.Items)
    //        {
    //            if (!item.IsCollectionType(out var value))
    //            {
    //                return false;
    //            }
    //            if (!collectionType.TryWriteJson(writer, value))
    //            {
    //                return false;
    //            }
    //        }

    //        return true;
    //    }

    //    return false;
    //}

    //public bool TryWriteXml(XmlWriter writer, OGraphCollection collection)
    //{
    //    throw new NotImplementedException();
    //}
}
