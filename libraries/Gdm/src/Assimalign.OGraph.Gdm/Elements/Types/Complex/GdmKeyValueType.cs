using System;
using System.Xml;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;

namespace Assimalign.OGraph.Gdm;

using Assimalign.OGraph.Gdm.Internal;

public sealed class GdmKeyValueType<TKey, TValue> : GdmComplexType<GdmKeyValuePair<TKey, TValue>>
    where TKey : notnull
{
    public GdmKeyValueType(IOGraphGdmType keyType, IOGraphGdmType valueType) 
    {
        foreach (var property in Properties.Cast<GdmProperty>())
        {
            if (property.Label == "key")
            {
                property.Type = new GdmTypeReference()
                {
                    Definition = keyType
                };
            }
            if (property.Label == "value")
            {
                property.Type = new GdmTypeReference()
                {
                    Definition = valueType
                };
            }
        }
    }

    protected override void Configure(IOGraphGdmComplexTypeDescriptor<GdmKeyValuePair<TKey, TValue>> descriptor)
    {
        descriptor.HasProperty(p => p.Key).UsePropertyName("key");
        descriptor.HasProperty(p => p.Value).UsePropertyName("value");
    }

    public override GdmKeyValuePair<TKey, TValue> Read(ref Utf8JsonReader reader)
    {
        var key = (TKey)Properties["key"].Type.Definition.Read(ref reader);
        var value = (TValue)Properties["value"].Type.Definition.Read(ref reader);

        return new GdmKeyValuePair<TKey, TValue> 
        { 
            Key = key, 
            Value = value 
        };
    }

    public override GdmKeyValuePair<TKey, TValue> Read(XmlReader reader)
    {
        return base.Read(reader);
    }

    public override void Write(Utf8JsonWriter writer, GdmKeyValuePair<TKey, TValue> value)
    {
        base.Write(writer, value);
    }

    public override void Write(XmlWriter writer, GdmKeyValuePair<TKey, TValue> value)
    {
        base.Write(writer, value);
    }
}

public class GdmKeyValuePair<TKey, TValue>
{
    public TKey? Key { get; set; }
    public TValue? Value { get; set; }

    public static implicit operator KeyValuePair<TKey, TValue>(GdmKeyValuePair<TKey, TValue> valueType)
    {
        return new KeyValuePair<TKey, TValue>(valueType.Key!, valueType.Value!);
    }
}