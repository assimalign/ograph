using System;
using System.Linq;
using System.Xml;
using System.Text.Json;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.OGraph.Gdm;

using Assimalign.OGraph.Gdm.Internal;

public class GdmEntityType : IOGraphGdmEntityType
{
    private readonly Action<IOGraphGdmEntityTypeDescriptor> configure;

    [DynamicallyAccessedMembers(
        DynamicallyAccessedMemberTypes.PublicParameterlessConstructor |
        DynamicallyAccessedMemberTypes.PublicProperties)]
    internal Type runtimeType;
    internal Label label;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    public GdmEntityType([DynamicallyAccessedMembers(
        DynamicallyAccessedMemberTypes.PublicParameterlessConstructor |
        DynamicallyAccessedMemberTypes.PublicProperties)]Type type) 
        : this(type, descriptor => { }) { }

    GdmEntityType([DynamicallyAccessedMembers(
        DynamicallyAccessedMemberTypes.PublicParameterlessConstructor |
        DynamicallyAccessedMemberTypes.PublicProperties)]Type type, Action<IOGraphGdmEntityTypeDescriptor> configure)
    {
        if (configure is null)
        {
            GdmThrowHelper.ThrowArgumentNullException(nameof(configure));
        }
        if (type is null)
        {
            GdmThrowHelper.ThrowArgumentNullException(nameof(type));
        }
        this.configure = configure;
        this.runtimeType = type;
        this.Configure(new GdmEntityTypeDescriptor(this));
    }

    /// <inheritdoc />
    public Label Label => label;

    /// <inheritdoc />
    public Type RuntimeType => runtimeType;

    /// <inheritdoc />
    public GdmTypeKind Kind => GdmTypeKind.Complex;

    /// <inheritdoc />
    public GdmElementKind ElementKind => GdmElementKind.Type;

    /// <inheritdoc />
    public IOGraphGdmPropertyCollection Properties { get; } = new GdmPropertyCollection();

    /// <inheritdoc />
    public GdmEntityKeyResolver KeyResolver => instance =>
    {
        return Properties.First(p => p.IsKey).Getter.Invoke(instance)!;
    };

    /// <summary>
    /// 
    /// </summary>
    /// <param name="descriptor"></param>
    protected virtual void Configure(IOGraphGdmEntityTypeDescriptor descriptor)
    {
        configure.Invoke(descriptor);
    }

    /// <inheritdoc />
    public object Read(ref Utf8JsonReader reader)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
        {
            // TODO: throw invalid operation
        }

        // capture the depth
        var startDepth = reader.CurrentDepth;
        var instance = Activator.CreateInstance(runtimeType)!;

        while (reader.Read() || reader.TokenType == JsonTokenType.EndObject || startDepth <= reader.CurrentDepth)
        {
            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                // TODO: throw invalid exception
            }

            var name = reader.GetString()!;

            if (!reader.Read())
            {
                // TODO: throw invalid operation exception
            }
            if (this.TryGetProperty(name, out var property))
            {
                // TODO: throw invalid operation exception
            }
            if (property!.IsComputed)
            {
                // TODO: throw invalid operation. Cannot set computed value
            }
            if (!property.IsNullable && reader.TokenType == JsonTokenType.Null)
            {
                // TODO:throw invalid operation. Property is required.
            }

            var type = property!.Type.Definition;
            var value = type.Read(ref reader);
            var setter = property!.Setter;

            setter.Invoke(instance, value);
        }

        return instance;
    }

    /// <inheritdoc />
    public object Read(XmlReader reader)
    {
        if (reader.NodeType != XmlNodeType.Element)
        {
            // TODO: Throw invalid operation exception
        }
        if (reader.LocalName != Label)
        {
            // TODO: Throw invalid operation exception
        }
        var instance = Activator.CreateInstance(runtimeType)!;
        var startDepth = reader.Depth;

        while (reader.Read() || startDepth <= reader.Depth)
        {
            if (reader.NodeType != XmlNodeType.Element)
            {
                // TODO: Throw invalid operation exception
            }
            var propertyName = reader.LocalName;

            if (!reader.Read())
            {
                // TODO: throw invalid operation exception
            }
            if (!this.TryGetProperty(propertyName, out var property))
            {
                // TODO: throw invalid operation exception
            }
            if (property!.IsComputed)
            {
                // TODO: throw invalid operation. Cannot set computed value
            }
            if (!property.IsNullable && reader.NodeType == XmlNodeType.Text)
            {
                // TODO:throw invalid operation. Property is required.
            }

            var type = property!.Type.Definition;
            var value = type.Read(reader);
            var setter = property!.Setter;

            setter.Invoke(instance, value);
        }

        return instance;
    }

    /// <inheritdoc />
    public void Write(Utf8JsonWriter writer, object value)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void Write(XmlWriter writer, object value)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="descriptor"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static GdmEntityType Create([DynamicallyAccessedMembers(
        DynamicallyAccessedMemberTypes.PublicParameterlessConstructor |
        DynamicallyAccessedMemberTypes.PublicProperties)] Type type, Action<IOGraphGdmEntityTypeDescriptor> descriptor)
    {
        return new GdmEntityType(type, descriptor);
    }
}
