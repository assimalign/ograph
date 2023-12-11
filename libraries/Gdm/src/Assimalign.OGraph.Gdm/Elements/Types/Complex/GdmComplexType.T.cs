using System;
using System.Xml;
using System.Text.Json;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.OGraph.Gdm;

using Assimalign.OGraph.Gdm.Internal;

[DebuggerDisplay("Gdm Type ({Kind}): {Label}")]
public class GdmComplexType<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] T> : GdmType<T>,
    IOGraphGdmComplexType
    where T : class, new()
{
    private readonly Action<IOGraphGdmComplexTypeDescriptor<T>> configure;

    public GdmComplexType() : this(descriptor => { }) { }

    GdmComplexType(Action<IOGraphGdmComplexTypeDescriptor<T>> configure)
    {
        if (configure is null)
        {
            GdmThrowHelper.ThrowArgumentNullException(nameof(configure));
        }
        this.configure = configure;
        Configure(new GdmComplexTypeDescriptor<T>(this));
    }

    /// <inheritdoc />
    public override GdmTypeKind Kind => GdmTypeKind.Complex;

    /// <inheritdoc />
    public IOGraphGdmPropertyCollection Properties { get; } = new GdmPropertyCollection();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="descriptor"></param>
    protected virtual void Configure(IOGraphGdmComplexTypeDescriptor<T> descriptor)
    {
        configure?.Invoke(descriptor);
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
                // TODO: throw invalid exception
            }
            if (this.TryGetProperty(name, out var property))
            {
                // TODO: throw invalid operation
            }
            if (property!.IsComputed)
            {
                // TODO: throw invalid operation. Cannot set computed value
            }
            if (!property.IsNullable)
            {
                // TODO:throw invalid operation. Property is required.
            }

            var type = property!.Type.Definition;
            var value = type.Read(ref reader);
            var setter = property!.Setter;

            setter(instance, value);
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

            if (this.TryGetProperty(propertyName, out var property))
            {

            }
        }



        return instance;
    }
    public override void Write(Utf8JsonWriter writer, T value)
    {
        writer.WriteStartObject();


        writer.WriteEndObject();
    }
    public override void Write(XmlWriter writer, T value)
    {
        writer.WriteStartElement(Label);



        writer.WriteEndElement();
    }

    #region Overloads

    /// <inheritdoc />
    public override string ToString()
    {
        return Label;
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(Label, typeof(IOGraphGdmComplexType));
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
    #endregion

    public static GdmComplexType<T> Create(Action<IOGraphGdmComplexTypeDescriptor<T>> configure)
    {
        return new GdmComplexType<T>(configure);
    }
}