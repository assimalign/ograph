using System;
using System.Xml;
using System.Text.Json;
using System.Linq;
using System.Reflection;

namespace Assimalign.OGraph.Gdm;

using Assimalign.OGraph.Gdm.Internal;

public class GdmComplexType : IOGraphGdmComplexType
{
    public GdmComplexType(Type? type)
    {
        RuntimeType = AssertType(type);
        Properties = new GdmPropertyCollection();
    }

    public Label Label { get; init; }
    public GdmTypeKind Kind => GdmTypeKind.Complex;
    public IOGraphGdmPropertyCollection Properties { get; init; }
    public Type RuntimeType { get; }

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
        var type = value.GetType();

        if (!type.IsAssignableTo(RuntimeType))
        {
            throw new InvalidOperationException("");
        }




        throw new NotImplementedException();
    }
    public virtual void Write(XmlWriter writer, object value)
    {
        var type = value.GetType();

        if (!type.IsAssignableTo(RuntimeType))
        {

        }

        throw new NotImplementedException();
    }


    private Type AssertType(Type? type)
    {
        // 1. Check that Type is not null
        if (type is null)
        {
            throw new Exception();
        }

        if (!type.IsClass)
        {
            throw new Exception();
        }
        // Since a delegate are actually compiled into a class. Let's check that 
        if (type.IsSubclassOf(typeof(Delegate)))
        {
            throw new Exception("Delegates are not allowed as complex types.");
        }
        // 3. Check if type has default constructor
        if (type.GetConstructor(Type.EmptyTypes) is null)
        {
            throw new InvalidOperationException($"The type {type.Name} does not have a default constructor. {type.Name}.ctor()");
        }
        return type;
    }

    private void PopulateProperties(Type type)
    {
        var properties = type.GetProperties(BindingFlags.Public)
            .Where(p => p.CanWrite && p.CanRead);

        foreach (var property in properties)
        {

        }
    }
}