using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace Assimalign.OGraph.Gdm.Elements;

public sealed class GdmKeyValuePairType<TKey, TValue> : GdmComplexType<KeyValuePair<TKey, TValue>>, IOGraphGdmComplexType
{
    public GdmKeyValuePairType(GdmGraph graph) : base(graph)
    {

    }

    public GdmKeyValuePairType(GdmType keyType, GdmType valueType, GdmGraph graph) : base(graph)
    {
        Members.Add(new GdmProperty("Key", keyType, this, true, false));
        Members.Add(new GdmProperty("Value", valueType, this, true, false));
    }

    public override KeyValuePair<TKey, TValue> Read(ref Utf8JsonReader reader)
    {
        var key = (TKey)((GdmProperty)Members["Key"]).Type.Read(ref reader);
        var value = (TValue)((GdmProperty)Members["Value"]).Type.Read(ref reader);

        return new KeyValuePair<TKey, TValue>(key, value);
    }

    public override KeyValuePair<TKey, TValue> Read(XmlReader reader)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, KeyValuePair<TKey, TValue> value)
    {
        throw new NotImplementedException();
    }

    public override void Write(XmlWriter writer, KeyValuePair<TKey, TValue> value)
    {
        throw new NotImplementedException();
    }
}
