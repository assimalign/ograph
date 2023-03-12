using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace Assimalign.OGraph;

public class ComplexType : IOGraphComplexType
{
    public Name TypeName { get; init; }
    public OGraphTypeIdentifier TypeIdentifier => OGraphTypeIdentifier.Complex;
    public IOGraphPropertyCollection Properties { get; init; }
    public Type? RuntimeType { get; init; }

    public bool TryReadJson(Utf8JsonReader reader, out OGraphObject item)
    {
        item = default;

        var items = new List<OGraphObjectItem>();

        var currentDepth = reader.CurrentDepth;

        while (reader.Read())
        {
            if (reader.CurrentDepth == currentDepth)
            {
                break;
            }
            if (reader.TokenType == JsonTokenType.PropertyName)
            {
                var propertyName = reader.GetString();

                if (!Properties.TryGet(propertyName, out var property))
                {
                    //throw new JsonException($"Property '{propertyName}' does not exist on type {TypeName}");
                    return false;
                }
                if (!reader.Read())
                {
                    //throw new JsonException("Unexpected end of File in JSON document.");
                    return false;
                }
                switch (property.Type)
                {
                    case IOGraphComplexType complexType:
                        {
                            if (!complexType.TryReadJson(reader, out var value))
                            {
                                return false;
                            }
                            items.Add(new(propertyName, value));
                            break;
                        }
                    case IOGraphCollectionType collectionType:
                        {
                            if (!collectionType.TryReadJson(reader, out var value))
                            {
                                return false;
                            }
                            items.Add(new(propertyName, value));
                            break;
                        }
                    case IOGraphPrimitiveType primitiveType:
                        {
                            if (!primitiveType.TryReadJson(reader, out var value))
                            {
                                return false;
                            }
                            items.Add(new(propertyName, value));
                            break;
                        }
                }
            }
        }


        return true;
    }
    public bool TryWriteJson(Utf8JsonWriter writer, OGraphObject item)
    {
        writer.WriteStartObject();

        foreach (var value in item.Items)
        {
            if (!Properties.TryGet(value.PropertyName, out var property))
            {
                return false;
            }

            writer.WritePropertyName(value.PropertyName);

            switch (property.Type)
            {
                case IOGraphComplexType complexType:
                    {
                        if (!value.IsComplexType(out var complexValue))
                        {
                            return false;
                        }
                        if (!complexType.TryWriteJson(writer, complexValue))
                        {
                            return false;
                        }
                        break;
                    }
                case IOGraphCollectionType collectionType:
                    {
                        if (!value.IsCollectionType(out var collectionValue))
                        {
                            return false;
                        }
                        if (!collectionType.TryWriteJson(writer, collectionValue))
                        {
                            return false;
                        }
                        break;
                    }
                case IOGraphPrimitiveType primitiveType:
                    {
                        if (!value.IsPrimitiveType(out var primitiveValue))
                        {
                            return false;
                        }
                        if (!primitiveType.TryWriteJson(writer, primitiveValue))
                        {
                            return false;
                        }
                        break;
                    }
            }
        }

        writer.WriteEndObject();

        return true;
    }
    public bool TryWriteXml(XmlWriter writer, OGraphObject item)
    {
        throw new NotImplementedException();
    }
    public bool TryReadXml(XmlReader reader, out OGraphObject item)
    {
        throw new NotImplementedException();
    }
}
