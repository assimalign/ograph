using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace Assimalign.OGraph;

internal class ComplexType : IOGraphComplexType
{
    public IOGraphPropertyCollection Properties => throw new NotImplementedException();

    public Name TypeName => throw new NotImplementedException();

    public OGraphTypeIdentifier TypeIdentifier => throw new NotImplementedException();

    public Type? RuntimeType => throw new NotImplementedException();

    public bool IsRuntimeType => throw new NotImplementedException();

    public bool TryReadJson(Utf8JsonReader reader, out OGraphValue[] values)
    {
        values = Array.Empty<OGraphValue>();

        var list = new List<OGraphValue>();

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.PropertyName)
            {
                var propertyName = reader.GetString();

                if (Properties.TryGet(propertyName, out var property))
                {
                    if (property is IOGraphPrimitiveType primitiveType)
                    {
                        if (primitiveType.TryReadJson(reader, out var value))
                        {
                            list.Add(value);
                        }
                    }
                    if (property is IOGraphComplexType complexType)
                    {
                        if (complexType.TryReadJson(reader, ))
                    }
                }
            }
        }


        values = list.ToArray();

        return true;
    }

    public bool TryReadXml(XmlReader reader, out OGraphValue[] values)
    {
        throw new NotImplementedException();
    }

    public bool TryWriteJson(Utf8JsonWriter writer, OGraphValue[] values)
    {
        throw new NotImplementedException();
    }

    public bool TryWriteXml(XmlWriter writer, OGraphValue[] values)
    {
        throw new NotImplementedException();
    }
}
