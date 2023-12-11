using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace Assimalign.OGraph.Gdm;

public sealed class GdmDictionaryType<TKey, TValue> : GdmCollectionType<Dictionary<TKey, TValue>, KeyValuePair<TKey, TValue>>
{



    public GdmDictionaryType(GdmType<TKey> key, GdmType<TValue> value)
    {
        
    }

    public override GdmType<KeyValuePair<TKey, TValue>> ItemType => throw new NotImplementedException();

    public override Dictionary<TKey, TValue> Read(ref Utf8JsonReader reader)
    {
        throw new NotImplementedException();
    }

    public override Dictionary<TKey, TValue> Read(XmlReader reader)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, Dictionary<TKey, TValue> value)
    {
        throw new NotImplementedException();
    }

    public override void Write(XmlWriter writer, Dictionary<TKey, TValue> value)
    {
        throw new NotImplementedException();
    }
}
