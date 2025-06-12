using System;
using System.Xml;
using System.Text.Json;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.OGraph.Gdm.Elements;

using Internal;

public abstract class GdmCollectionType<TCollection> : GdmCollectionType
    where TCollection : GdmType
{
    internal GdmCollectionType(GdmName name, GdmGraph graph) : base(name, graph)
    {
        
    }

    public override GdmType ItemType => throw new NotImplementedException();
}

public abstract class GdmCollectionType<TCollection, T> : GdmCollectionType 
    where TCollection : class, IEnumerable<T>
{
    public GdmCollectionType(GdmName name, GdmType itemType, GdmGraph graph) : base(name, graph)
    {
        ItemType = ThrowHelper.ThrowIfNull(itemType);
    }
    public sealed override GdmType ItemType { get; }
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
    public sealed override Type RuntimeType { get; } = typeof(TCollection);
    public abstract new TCollection Read(ref Utf8JsonReader reader);
    public abstract new TCollection Read(XmlReader reader);
    public abstract void Write(Utf8JsonWriter writer, TCollection value);
    public abstract void Write(XmlWriter writer, TCollection value);
    public sealed override void Write(Utf8JsonWriter writer, object value)
    {
        Write(writer, ThrowHelper.ThrowIfNotType<TCollection>(value));
    }
    public sealed override void Write(XmlWriter writer, object value)
    {
        Write(writer, ThrowHelper.ThrowIfNotType<TCollection>(value));
    }
}
