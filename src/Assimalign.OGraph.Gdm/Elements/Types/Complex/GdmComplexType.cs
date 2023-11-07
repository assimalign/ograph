using System;
using System.Xml;
using System.Text.Json;
using System.Diagnostics;

namespace Assimalign.OGraph.Gdm;

using Assimalign.OGraph.Gdm.Internal;

[DebuggerDisplay("Gdm Type ({Kind}): {Label}")]
public class GdmComplexType : IOGraphGdmComplexType
{
    private readonly Action<IOGraphGdmComplexTypeDescriptor> configure;


    internal Label label;

    private GdmComplexType(Action<IOGraphGdmComplexTypeDescriptor> configure)
    {
        this.configure = configure;
        Configure(new GdmComplexTypeDescriptor(this));
    }
    private GdmComplexType(Type type, Action<IOGraphGdmComplexTypeDescriptor> configure)
    {
        this.configure = configure;
        RuntimeType = type;
        Initialize();
        Configure(new GdmComplexTypeDescriptor(this));
    }

    public GdmComplexType() 
        : this(descriptor => { }) 
    { 
    }

    public GdmComplexType(Type type) 
        : this(type, descriptor => { }) 
    {   
    }

    private void Initialize()
    {
        foreach (var property in RuntimeType.GetGdmComplexTypeProperties())
        {
            property.DeclaringType = new GdmTypeReference()
            {
                Definition = this
            };

            Properties.Add(property);
        }
    }

    protected virtual void Configure(IOGraphGdmComplexTypeDescriptor descriptor)
    {
        configure?.Invoke(descriptor);
    }

    public Label Label
    {
        get => label;
        init => label = value;
    }
    public GdmTypeKind Kind => GdmTypeKind.Complex;
    public IOGraphGdmPropertyCollection Properties { get; } = new GdmPropertyCollection();
    public Type RuntimeType { get; } = default!;
    public GdmElementType ElementType => GdmElementType.Type;
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
                var propertyName = property.Label;
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
    public override string ToString()
    {
        return Label;
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(Label, typeof(IOGraphGdmComplexType));
    }
    public override bool Equals(object? instance)
    {
        if (instance is not null)
        {
            return GetHashCode() == instance.GetHashCode();
        }

        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configure"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static GdmComplexType Create(Action<IOGraphGdmComplexTypeDescriptor> configure)
    {
        if (configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }
        return new GdmComplexType(configure);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static GdmComplexType Create(Type type, Action<IOGraphGdmComplexTypeDescriptor> configure)
    {
        if (configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }
        return new GdmComplexType(type, configure);
    }
}