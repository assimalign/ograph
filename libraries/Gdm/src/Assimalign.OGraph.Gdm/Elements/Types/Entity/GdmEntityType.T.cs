using System;
using System.Linq;
using System.Xml;
using System.Text.Json;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.OGraph.Gdm;

using Assimalign.OGraph.Gdm.Internal;

public class GdmEntityType<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties)] T> : GdmType<T>, IOGraphGdmEntityType
    where T : class, new()
{
    internal IOGraphGdmEntityKey key = default!;
    private readonly Action<IOGraphGdmEntityTypeDescriptor<T>> configure;

    /// <summary>
    /// Default constructure.
    /// </summary>
    public GdmEntityType() : this(descriptor => { }) { }
    GdmEntityType(Action<IOGraphGdmEntityTypeDescriptor<T>> configure)
    {
        if (configure is null)
        {
            GdmThrowHelper.ThrowArgumentNullException(nameof(configure));
        }
        this.configure = configure;
        Configure(new GdmEntityTypeDescriptor<T>(this));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="descriptor"></param>
    protected virtual void Configure(IOGraphGdmEntityTypeDescriptor<T> descriptor)
    {
        configure.Invoke(descriptor);
    }

    /// <inheritdoc />
    public override GdmTypeKind Kind => GdmTypeKind.Entity;

    /// <inheritdoc />
    public IOGraphGdmPropertyCollection Properties { get; } = new GdmPropertyCollection();

    /// <inheritdoc />
    public IOGraphGdmEntityKey Key => key;

    #region Overloads
    /// <inheritdoc />
    public override string ToString()
    {
        return Label;
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(Label, typeof(IOGraphGdmEntityType));
    }

    /// <inheritdoc />
    public override bool Equals(object? instance)
    {
        if (instance is not null)
        {
            return GetHashCode() == instance.GetHashCode();
        }
        return false;
    }

    public override T Read(ref Utf8JsonReader reader)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
        {
            // TODO: throw invalid operation
        }

        // capture the depth
        var startDepth = reader.CurrentDepth;
        var instance = Activator.CreateInstance<T>();

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
    public override T Read(XmlReader reader)
    {
        if (reader.NodeType != XmlNodeType.Element)
        {
            // TODO: Throw invalid operation exception
        }
        if (reader.LocalName != Label)
        {
            // TODO: Throw invalid operation exception
        }
        var instance = Activator.CreateInstance<T>();
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
    public override void Write(Utf8JsonWriter writer, T value)
    {
        if (value is null)
        {
            return;
        }

        writer.WriteStartObject();

        foreach (var property in Properties)
        {
            var label = property.Label;
            var type = property!.Type.Definition;
            var getter = property!.Getter;
            var propertyValue = getter.Invoke(value);

            writer.WritePropertyName(Label);

            if (!property.IsNullable && propertyValue is null)
            {
                // TODO: throw invalid operation exception
            }
            else if (propertyValue is null)
            {
                writer.WriteNullValue();
            }
            else
            {
                type.Write(writer, propertyValue);
            }
        }

        writer.WriteEndObject();
    }
    public override void Write(XmlWriter writer, T value)
    {
        if (value is null)
        {
            return;
        }

        writer.WriteStartElement(Label);

        foreach (var property in Properties)
        {
            var label = property.Label;
            var type = property!.Type.Definition;
            var getter = property!.Getter;
            var propertyValue = getter.Invoke(value);

            writer.WriteStartElement(label);

            if (!property.IsNullable && propertyValue is null)
            {
                // TODO: throw invalid operation exception
            }
            else if (propertyValue is not null)
            {
                type.Write(writer, propertyValue);
            }

            writer.WriteEndElement();
        }

        writer.WriteEndElement();
    }
    #endregion

    #region Static Members/Methods
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="configure"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static GdmEntityType<T> Create(Action<IOGraphGdmEntityTypeDescriptor<T>> configure) 
    {
        return new GdmEntityType<T>(configure);
    }
    #endregion
}