using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace Assimalign.OGraph.Gdm.Elements;

public sealed class GdmDictionaryType<TKey, TValue> : GdmCollectionType<Dictionary<TKey, TValue>, KeyValuePair<TKey, TValue>>
    where TKey : notnull
{
    private static GdmName CreateName<TK, TV>()
    {
        var keyName = typeof(TK).Name;
        var valueName = typeof(TV).Name;

        return keyName + valueName + "Dictionary";
    }

    public GdmDictionaryType(GdmType keyType, GdmType valueType, GdmGraph graph) 
        : base(CreateName<TKey, TValue>(), new GdmKeyValuePairType<TKey, TValue>(keyType, valueType, graph), graph)
    {

    }


    public override Dictionary<TKey, TValue> Read(ref Utf8JsonReader reader)
    {
        IDictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();

        var value = ((GdmKeyValuePairType<TKey, TValue>)ItemType).Read(ref reader);

        dictionary.Add(value);

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
