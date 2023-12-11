using System;
using System.Xml;
using System.Text.Json;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.OGraph.Gdm;

using Assimalign.OGraph.Gdm.Internal;

[DebuggerDisplay("Gdm Type ({Kind}): {Label}")]
public class GdmComplexType : IOGraphGdmComplexType
{
    private readonly Action<IOGraphGdmComplexTypeDescriptor> configure;

    public GdmComplexType() : this(descriptor => { }) { }
    public GdmComplexType(Type type) : this(type, descriptor => { }) { }

    GdmComplexType(Action<IOGraphGdmComplexTypeDescriptor> configure)
    {
        if (configure is null)
        {
            GdmThrowHelper.ThrowArgumentNullException(nameof(configure));
        }
        this.configure = configure;
        this.Configure(new GdmComplexTypeDescriptor(this));
    }

    GdmComplexType(Type type, Action<IOGraphGdmComplexTypeDescriptor> configure)
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
        this.RuntimeType = type;
        this.Configure(new GdmComplexTypeDescriptor(this));
    }

    protected virtual void Configure(IOGraphGdmComplexTypeDescriptor descriptor)
    {
        configure.Invoke(descriptor);
    }

    #region Explicit Implementations
    Label IOGraphGdmElement.Label => Label;
    Type IOGraphGdmType.RuntimeType => RuntimeType;
    GdmTypeKind IOGraphGdmType.Kind => GdmTypeKind.Complex;
    GdmElementType IOGraphGdmElement.ElementType => GdmElementType.Type;
    IOGraphGdmPropertyCollection IOGraphGdmComplexType.Properties { get; } = new GdmPropertyCollection();
    #endregion

    internal Label Label { get; set; }

    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
    internal Type RuntimeType { get; set; } = default!;
    
    
    public virtual object Read(ref Utf8JsonReader reader)
    {
        if (!reader.IsStartOfObjectToken())
        {
            //ThrowE
        }
        string propertyName;

        var instance = Activator.CreateInstance(RuntimeType);

        if (instance is null)
        {
            throw new Exception();
        }

        var startDepth = reader.CurrentDepth;

        while (reader.Read() || reader.TokenType != JsonTokenType.EndObject)
        {
            if (reader.TokenType == JsonTokenType.PropertyName)
            {
                throw new JsonException();
            }

            propertyName = reader.GetString()!;

            var property = (this as IOGraphGdmComplexType).Properties[propertyName];
            var propertyType = property.Type.Definition;

            reader.Read();

            var propertyValue = propertyType.Read(ref reader);

            property.Setter.Invoke(instance, propertyValue);

            if (reader.CurrentDepth >= startDepth) break;
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

            foreach (var property in (this as IOGraphGdmComplexType).Properties)
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
    /// <param name="configure"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static GdmComplexType Create(Action<IOGraphGdmComplexTypeDescriptor> configure)
    {
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
        return new GdmComplexType(type, configure);
    }

    #endregion
}