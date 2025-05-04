using System;
using System.Xml;
using System.Linq;
using System.Text.Json;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.OGraph.Gdm.Elements;

using Internal;

public abstract class GdmComplexType<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] T> : GdmComplexType
    where T : class, new()
{
    private readonly Action<GdmComplexTypeDescriptor<T>>? _configure;

    #region Constructors

    protected GdmComplexType()
    {
        _configure = Configure;
    }

    public GdmComplexType(GdmGraph graph) : base(typeof(T), graph)
    {
        _configure = Configure;
    }
    public GdmComplexType(GdmName name, GdmGraph graph) : base(name, typeof(T), graph)
    {
        _configure = Configure;
    }

    #endregion


    #region Methods

    protected abstract void Configure(GdmComplexTypeDescriptor<T> descriptor);

    public virtual new T Read(ref Utf8JsonReader reader)
    {
        return ThrowHelper.ThrowIfNotType<T>(base.Read(ref reader));
    }
    public virtual new T Read(XmlReader reader)
    {
        return ThrowHelper.ThrowIfNotType<T>(base.Read(reader));
    }
    public virtual void Write(Utf8JsonWriter writer, T value)
    {
        base.Write(writer, value);
    }
    public virtual void Write(XmlWriter writer, T value)
    {
        base.Write(writer, value);
    }
    public sealed override void Write(Utf8JsonWriter writer, object value)
    {
        Write(writer, ThrowHelper.ThrowIfNotType<T>(value));
    }
    public sealed override void Write(XmlWriter writer, object value)
    {
        Write(writer, ThrowHelper.ThrowIfNotType<T>(value));
    }


    //public static GdmComplexType<T> Create()

    #endregion

    //internal override void Initialize()
    //{
    //    if (_configure is null)
    //    {
    //        return;
    //    }

    //    var descriptor = new GdmComplexTypeDescriptor<T>(this);

    //    _configure.Invoke(descriptor);
    //}


    //private readonly Action<IOGraphGdmComplexTypeDescriptor<T>> configure;

    //public GdmComplexType() : this(descriptor => { }) { }

    //internal GdmComplexType(Action<IOGraphGdmComplexTypeDescriptor<T>> configure)
    //{
    //    if (configure is null)
    //    {
    //        ThrowHelper.ThrowArgumentNullException(nameof(configure));
    //    }
    //    this.configure = configure;
    //    Configure(new GdmComplexTypeDescriptor<T>(this));
    //}

    //#region Properties

    //public IOGraphGdmMemberCollection Members { get; } = new GdmMemberCollection();
    //public override GdmTypeKind Kind => GdmTypeKind.Complex;

    //#endregion

    //#region Methods

    //protected virtual void Configure(IOGraphGdmComplexTypeDescriptor<T> descriptor)
    //{
    //    configure?.Invoke(descriptor);
    //}
    //public override T Read(ref Utf8JsonReader reader)
    //{
    //    if (reader.TokenType != JsonTokenType.StartObject)
    //    {
    //        // TODO: throw invalid operation
    //    }

    //    // capture the depth
    //    var startDepth = reader.CurrentDepth;
    //    var instance = Activator.CreateInstance<T>();

    //    while (reader.Read() || reader.TokenType == JsonTokenType.EndObject || startDepth <= reader.CurrentDepth)
    //    {
    //        if (reader.TokenType != JsonTokenType.PropertyName)
    //        {
    //            // TODO: throw invalid exception
    //        }

    //        var name = reader.GetString()!;

    //        if (!reader.Read())
    //        {
    //            // TODO: throw invalid operation exception
    //        }
    //        if (this.TryGetProperty(name, out var property))
    //        {
    //            // TODO: throw invalid operation exception
    //        }
    //        if (!property!.IsNullable && reader.TokenType == JsonTokenType.Null)
    //        {
    //            // TODO:throw invalid operation. Property is required.
    //        }

    //        var type = property!.Type.Definition;
    //        var value = type.Read(ref reader);
    //        var setter = property!.Setter;

    //        setter.Invoke(instance, value);
    //    }

    //    return instance;
    //}
    //public override T Read(XmlReader reader)
    //{
    //    if (reader.NodeType != XmlNodeType.Element)
    //    {
    //        // TODO: Throw invalid operation exception
    //    }
    //    if (reader.LocalName != Label)
    //    {
    //        // TODO: Throw invalid operation exception
    //    }
    //    var instance = Activator.CreateInstance<T>();
    //    var startDepth = reader.Depth;

    //    while (reader.Read() || startDepth <= reader.Depth)
    //    {
    //        if (reader.NodeType != XmlNodeType.Element)
    //        {
    //            // TODO: Throw invalid operation exception
    //        }
    //        var propertyName = reader.LocalName;

    //        if (!reader.Read())
    //        {
    //            // TODO: throw invalid operation exception
    //        }
    //        if (!this.TryGetProperty(propertyName, out var property))
    //        {
    //            // TODO: throw invalid operation exception
    //        }
    //        if (!property!.IsNullable && reader.NodeType == XmlNodeType.Text)
    //        {
    //            // TODO:throw invalid operation. Property is required.
    //        }

    //        var type = property!.Type.Definition;
    //        var value = type.Read(reader);
    //        var setter = property!.Setter;

    //        setter.Invoke(instance, value);
    //    }

    //    return instance;
    //}
    //public override void Write(Utf8JsonWriter writer, T value)
    //{
    //    if (value is null)
    //    {
    //        return;
    //    }

    //    writer.WriteStartObject();

    //    foreach (var property in Members.OfType<IOGraphGdmProperty>())
    //    {
    //        var label = property.Label;
    //        var type = property!.Type;
    //        var getter = property!.Getter;
    //        var propertyValue = getter.Invoke(value);

    //        writer.WritePropertyName(Label);

    //        if (!property.IsNullable && propertyValue is null)
    //        {
    //            // TODO: throw invalid operation exception
    //        }
    //        else if (propertyValue is null)
    //        {
    //            writer.WriteNullValue();
    //        }
    //        else
    //        {
    //            type.Write(writer, propertyValue);
    //        }
    //    }

    //    writer.WriteEndObject();
    //}
    //public override void Write(XmlWriter writer, T value)
    //{
    //    if (value is null)
    //    {
    //        return;
    //    }

    //    writer.WriteStartElement(Label);

    //    foreach (var property in Members.OfType<IOGraphGdmProperty>())
    //    {
    //        var label = property.Label;
    //        var type = property!.Type;
    //        var getter = property!.Getter;
    //        var propertyValue = getter.Invoke(value);

    //        writer.WriteStartElement(label);

    //        if (!property.IsNullable && propertyValue is null)
    //        {
    //            // TODO: throw invalid operation exception
    //        }
    //        else if (propertyValue is not null)
    //        { 
    //            type.Write(writer, propertyValue);
    //        }

    //        writer.WriteEndElement();
    //    }

    //    writer.WriteEndElement();
    //}

    //#endregion

    //#region Overloads

    ///// <inheritdoc />
    //public override string ToString()
    //{
    //    return Label;
    //}

    ///// <inheritdoc />
    //public override int GetHashCode()
    //{
    //    return HashCode.Combine(Label, typeof(IOGraphGdmComplexType));
    //}

    ///// <inheritdoc />
    //public override bool Equals(object? instance)
    //{
    //    if (instance is not null)
    //    {
    //        return GetHashCode() == instance.GetHashCode();
    //    }

    //    return false;
    //}
    //#endregion

    ///// <summary>
    ///// 
    ///// </summary>
    ///// <param name="configure"></param>
    ///// <returns></returns>
    ///// <exception cref="ArgumentNullException"></exception>
    //public static GdmComplexType<T> Create(Action<IOGraphGdmComplexTypeDescriptor<T>> configure)
    //{
    //    return new GdmComplexType<T>(configure);
    //}
}