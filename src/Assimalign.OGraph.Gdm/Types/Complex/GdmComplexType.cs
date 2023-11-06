using System;
using System.Xml;
using System.Text.Json;

namespace Assimalign.OGraph.Gdm;

using Assimalign.OGraph.Gdm.Internal;
using System.Diagnostics;

[DebuggerDisplay("Gdm Type ({Kind}): {Label}")]
public class GdmComplexType : IOGraphGdmComplexType
{
    internal Label label;

    public GdmComplexType() { }
    public GdmComplexType(Type type)
    {
        if (type is null)
        {
            throw new ArgumentNullException(nameof(type));
        }
        RuntimeType = type;
        Initialize();
        Configure(new GdmComplexTypeDescriptor(this));
    }

    private void Initialize()
    {
        foreach (var property in RuntimeType.GetGdmComplexTypeProperties())
        {
            Properties.Add(property);
        }
    }

    protected virtual void Configure(IOGraphGdmComplexTypeDescriptor descriptor) { }

    public Label Label
    {
        get => label;
        init => label = value;
    }
    public GdmTypeKind Kind => GdmTypeKind.Complex;
    public IOGraphGdmPropertyCollection Properties { get; init; } = new GdmPropertyCollection();
    public Type RuntimeType { get; } = default!;
    public virtual object Read(ref Utf8JsonReader reader)
    {
        if (!reader.IsStartOfObjectToken())
        {
            //ThrowE
        }
        string propertyName;

        var instance = Activator.CreateInstance(RuntimeType!);

        if (instance is null)
        {
            throw new Exception();
        }

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

            property.Setter.Invoke(instance, propertyValue);
        }

        return instance;
    }
    public virtual object Read(XmlReader reader)
    {
        throw new NotImplementedException();
    }
    public virtual void Write(Utf8JsonWriter writer, object value)
    {
        if (value is null)
        {
            writer.WriteNullValue();
        }
        else
        {
            var type = value.GetType();

            if (!type.IsAssignableTo(RuntimeType))
            {
                throw new InvalidOperationException("");
            }

            writer.WriteStartObject();

            foreach (var property in Properties)
            {
                var propertyName = property.Name;
                var propertyType = property.Type.Definition;
                var propertyValue = property.Getter.Invoke(value)!;

                writer.WritePropertyName(propertyName);

                propertyType.Write(writer, propertyValue);
            }

            writer.WriteEndObject();
        }
    }
    public virtual void Write(XmlWriter writer, object value)
    {
        var type = value.GetType();

        if (!type.IsAssignableTo(RuntimeType))
        {

        }

        throw new NotImplementedException();
    }
}