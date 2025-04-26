using System;
using System.Xml;
using System.Text.Json;

namespace Assimalign.OGraph.Gdm.Elements;

public abstract class GdmType : IOGraphGdmType
{
    public abstract GdmName Name { get; internal set; }
    public abstract GdmTypeKind Kind { get; }
    public virtual GdmGraph Graph { get; internal set; } = default!;
    public abstract Type RuntimeType { get; internal set; }
    public GdmMetadata Meta { get; } = new GdmMetadata();
    public GdmElementKind ElementKind { get; } = GdmElementKind.Type;
    public bool IsPrimitive => this is 
        GdmBooleanType or
        GdmStringType or
        GdmInt32Type;

    IOGraphGdmMetaCollection IOGraphGdmElement.Meta => Meta;
    IOGraphGdmGraph IOGraphGdmType.Graph => Graph;

    public object Read(ref Utf8JsonReader reader)
    {
        return GetType().Read(ref reader);
    }
    public object Read(XmlReader reader)
    {
        return GetType().Read(reader);
    }
    public void Write(Utf8JsonWriter writer, object value)
    {
        GetType().Write(writer, value);
    }
    public void Write(XmlWriter writer, object value)
    {
        GetType().Write(writer, value);
    }
    
    private IOGraphGdmType GetType()
    {
        foreach (var type in Graph.Types)
        {
            if (type.RuntimeType == RuntimeType && type.Label == Label)
            {
                return type;
            }
        }
        throw new InvalidOperationException();
    }
}