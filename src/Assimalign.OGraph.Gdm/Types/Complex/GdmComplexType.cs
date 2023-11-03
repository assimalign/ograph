using System;
using System.Text.Json;
using System.Xml;

namespace Assimalign.OGraph.Gdm;

using Assimalign.OGraph.Gdm.Internal;

public class GdmComplexType : IOGraphGdmComplexType
{
    public GdmComplexType()
    {
        Properties = new GdmPropertyCollection();
    }

    public Label Label { get; init; }
    public GdmTypeKind Kind => GdmTypeKind.Complex;
    public IOGraphGdmPropertyCollection Properties { get; init; }
    public Type? RuntimeType { get; init; }
    public virtual bool IsAssignableTo(IOGraphGdmType type)
    {
        if (type is not IOGraphGdmComplexType complexType)
        {
            return false;
        }
        if (complexType.Properties.Count != Properties.Count)
        {
            return false;
        }
        if (!RuntimeType!.IsAssignableFrom(type.RuntimeType))
        {
            return false;
        }
        foreach (var property in Properties)
        {
            if (!complexType.Properties.Contains(property))
            {
                return false;
            }
        }
        return true;
    }
    
    
    public virtual object Read(ref Utf8JsonReader reader)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException();
        }

        var instance = Activator.CreateInstance(RuntimeType!);

        string propertyName;

        while ((reader.Read() && reader.TokenType != JsonTokenType.EndObject))
        {
            if (reader.TokenType == JsonTokenType.PropertyName)
            {
                throw new JsonException();
            }

            propertyName = reader.GetString()!;

            var property = Properties[propertyName];
            var propertyType = property.Type.Definition;

            reader.Read();

            var propertyValue = propertyType.Read(ref reader);



        }

        return instance;
    }
    public virtual object Read(XmlReader reader)
    {
        throw new NotImplementedException();
    }
    public virtual void Write(Utf8JsonWriter writer, object value)
    {
        throw new NotImplementedException();
    }
    public virtual void Write(XmlWriter writer, object value)
    {
        throw new NotImplementedException();
    }
}