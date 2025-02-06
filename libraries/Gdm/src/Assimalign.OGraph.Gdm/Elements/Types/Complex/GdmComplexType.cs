using System;
using System.Xml;
using System.Linq;
using System.Text.Json;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.OGraph.Gdm.Elements;

using Assimalign.OGraph.Gdm.Internal;

public class GdmComplexType : IOGraphGdmComplexType
{
    private readonly Action<IOGraphGdmComplexTypeDescriptor> configure;

    [DynamicallyAccessedMembers(
        DynamicallyAccessedMemberTypes.PublicParameterlessConstructor |
        DynamicallyAccessedMemberTypes.PublicProperties)]
    internal Type runtimeType;
    internal Label label;
    internal IOGraphGdmGraph grpah;

    public GdmComplexType([DynamicallyAccessedMembers(
        DynamicallyAccessedMemberTypes.PublicParameterlessConstructor |
        DynamicallyAccessedMemberTypes.PublicProperties)] Type type) : this(type, descriptor => { }) { }

    GdmComplexType([DynamicallyAccessedMembers(
        DynamicallyAccessedMemberTypes.PublicParameterlessConstructor |
        DynamicallyAccessedMemberTypes.PublicProperties)] Type type, Action<IOGraphGdmComplexTypeDescriptor> configure)
    {
        if (configure is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(configure));
        }
        if (type is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(type));
        }
        this.configure = configure;
        this.runtimeType = type;
        this.Configure(new GdmComplexTypeDescriptor(this));
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="descriptor"></param>
    protected virtual void Configure(IOGraphGdmComplexTypeDescriptor descriptor)
    {
        configure.Invoke(descriptor);
    }

    public Label Label => label;
    public Type RuntimeType => runtimeType;
    public GdmTypeKind Kind => GdmTypeKind.Complex;
    public GdmElementKind ElementKind => GdmElementKind.Type;
    public IOGraphGdmMemberCollection Members { get; } = new GdmMemberCollection();
    public IOGraphGdmGraph Graph => Graph!;
    public IOGraphGdmMetadata Meta { get; } = new GdmMetadata();

    public virtual object Read(ref Utf8JsonReader reader)
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
            if (!property!.IsNullable && reader.TokenType == JsonTokenType.Null)
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
    public virtual object Read(XmlReader reader)
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
            if (!property!.IsNullable && reader.NodeType == XmlNodeType.Text)
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

            foreach (var property in this.Members.OfType<IOGraphGdmProperty>())
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

    #region Overloads
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
    #endregion

    #region Static Members

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static GdmComplexType Create([DynamicallyAccessedMembers(
        DynamicallyAccessedMemberTypes.PublicParameterlessConstructor |
        DynamicallyAccessedMemberTypes.PublicProperties)]Type type, Action<IOGraphGdmComplexTypeDescriptor> configure)
    {
        return new GdmComplexType(type, configure);
    }

    #endregion
}