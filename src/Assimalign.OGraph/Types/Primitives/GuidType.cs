using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Text.Json;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

internal class GuidType : IOGraphType
{
    public Name TypeName => "Guid";
    public OGraphTypeIdentifier TypeIdentifier => throw new NotImplementedException();

    public Type? RuntimeType => throw new NotImplementedException();

    public bool IsRuntimeType => throw new NotImplementedException();

    public bool TryReadJson(Utf8JsonReader reader, out object value)
    {
        value = default;

        if (reader.TokenType == JsonTokenType.String && reader.TryGetGuid(out var guid))
        {
            value = guid;
            return true;
        }

        return false;
    }

    public bool TryWriteJson(Utf8JsonWriter writer, object value)
    {
        if (value is Guid guid)
        {
            writer.WriteStringValue(guid);
            return true;
        }
        return false;
    }

    public bool TryReadXml(XmlReader reader, out object value)
    {
        value = default;


        throw new NotImplementedException();
    }

    

    public bool TryWriteXml(XmlWriter writer, object value)
    {
        if (value is Guid guid)
        {
            writer.WriteValue(guid);
            return true;
        }
        return false;
    }
}
