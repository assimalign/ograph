using System;
using System.Xml;
using System.Text.Json;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.OGraph.Gdm.Elements;

using Internal;

public class GdmEntityType : GdmType, IOGraphGdmEntityType
{
    public GdmEntityType()
    {
        
    }
    public GdmEntityType(
        GdmName name,
        GdmEntityKey key,
        GdmGraph graph,
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type runtimeType)
    {
        Name = name;
        Key = ThrowHelper.ThrowIfNull(key, nameof(key));
        Graph = ThrowHelper.ThrowIfNull(graph, nameof(graph));
        RuntimeType = ThrowHelper.ThrowIfNull(runtimeType, nameof(runtimeType));
    }

    #region Properties

    public override GdmName Name { get; internal set; } = default!;
    public override GdmGraph Graph { get; internal set; } = default!;
    public GdmEntityKey Key { get; internal set; } = default!;
    public GdmMemberCollection Members { get; internal set; } = new GdmMemberCollection();
    public override Type RuntimeType { get; internal set; } = default!;
    public override GdmTypeKind Kind => GdmTypeKind.Entity;
    IOGraphGdmEntityKey IOGraphGdmEntityType.Key => Key;
    IOGraphGdmMemberCollection IOGraphGdmComplexType.Members => Members;
    IOGraphGdmGraph IOGraphGdmType.Graph => Graph;
    IOGraphGdmMetaCollection IOGraphGdmElement.Meta => Meta;

    #endregion

    #region Methods
    public new object Read(ref Utf8JsonReader reader)
    {
        return base.Read(ref reader);
    }
    public new object Read(XmlReader reader)
    {
        return base.Read(reader);
    }
    public new void Write(Utf8JsonWriter writer, object value)
    {
        base.Write(writer, value);
    }
    public new void Write(XmlWriter writer, object value)
    {
        base.Write(writer, value);
    }
    #endregion
}


//public class GdmEntityTypeOld : IOGraphGdmEntityType
//{
//    private readonly Action<IOGraphGdmEntityTypeDescriptor> configure;

//    [DynamicallyAccessedMembers(
//        DynamicallyAccessedMemberTypes.PublicParameterlessConstructor |
//        DynamicallyAccessedMemberTypes.PublicProperties)]
//    internal Type runtimeType;
//    internal Label label;
//    internal IOGraphGdmEntityKey key;
//    internal IOGraphGdmGraph graph;

//    /// <summary>
//    /// 
//    /// </summary>
//    /// <param name="type"></param>
//    public GdmEntityType([DynamicallyAccessedMembers(
//        DynamicallyAccessedMemberTypes.PublicParameterlessConstructor |
//        DynamicallyAccessedMemberTypes.PublicProperties)]Type type) 
//        : this(type, descriptor => { }) { }

//    GdmEntityType([DynamicallyAccessedMembers(
//        DynamicallyAccessedMemberTypes.PublicParameterlessConstructor |
//        DynamicallyAccessedMemberTypes.PublicProperties)]Type type, Action<IOGraphGdmEntityTypeDescriptor> configure)
//    {
//        if (configure is null)
//        {
//            ThrowHelper.ThrowArgumentNullException(nameof(configure));
//        }
//        if (type is null)
//        {
//            ThrowHelper.ThrowArgumentNullException(nameof(type));
//        }
//        this.configure = configure;
//        this.runtimeType = type;
//        this.key = default!;
//        this.Configure(new GdmEntityTypeDescriptor(this));
//    }

//    /// <inheritdoc />
//    public Label Label => label;

//    /// <inheritdoc />
//    public Type RuntimeType => runtimeType;

//    /// <inheritdoc />
//    public GdmTypeKind Kind => GdmTypeKind.Complex;

//    /// <inheritdoc />
//    public GdmElementKind ElementKind => GdmElementKind.Type;

//    /// <inheritdoc />
//    public IOGraphGdmMemberCollection Members { get; } = new GdmMemberCollection();

//    /// <inheritdoc />
//    public IOGraphGdmEntityKey Key => key;

//    /// <inheritdoc />
//    public IOGraphGdmGraph Graph => graph!;

//    /// <inheritdoc />
//    public IOGraphGdmMetadata Meta { get; } = new GdmMetadata();

//    /// <summary>
//    /// 
//    /// </summary>
//    /// <param name="descriptor"></param>
//    protected virtual void Configure(IOGraphGdmEntityTypeDescriptor descriptor)
//    {
//        configure.Invoke(descriptor);
//    }

//    /// <inheritdoc />
//    public object Read(ref Utf8JsonReader reader)
//    {
//        if (reader.TokenType != JsonTokenType.StartObject)
//        {
//            // TODO: throw invalid operation
//        }

//        // capture the depth
//        var startDepth = reader.CurrentDepth;
//        var instance = Activator.CreateInstance(runtimeType)!;

//        while (reader.Read() || reader.TokenType == JsonTokenType.EndObject || startDepth <= reader.CurrentDepth)
//        {
//            if (reader.TokenType != JsonTokenType.PropertyName)
//            {
//                // TODO: throw invalid exception
//            }

//            var name = reader.GetString()!;

//            if (!reader.Read())
//            {
//                // TODO: throw invalid operation exception
//            }
//            if (this.TryGetProperty(name, out var property))
//            {
//                // TODO: throw invalid operation exception
//            }
//            if (!property!.IsNullable && reader.TokenType == JsonTokenType.Null)
//            {
//                // TODO:throw invalid operation. Property is required.
//            }

//            var type = property!.Type.Definition;
//            var value = type.Read(ref reader);
//            var setter = property!.Setter;

//            setter.Invoke(instance, value);
//        }

//        return instance;
//    }

//    /// <inheritdoc />
//    public object Read(XmlReader reader)
//    {
//        if (reader.NodeType != XmlNodeType.Element)
//        {
//            // TODO: Throw invalid operation exception
//        }
//        if (reader.LocalName != Label)
//        {
//            // TODO: Throw invalid operation exception
//        }
//        var instance = Activator.CreateInstance(runtimeType)!;
//        var startDepth = reader.Depth;

//        while (reader.Read() || startDepth <= reader.Depth)
//        {
//            if (reader.NodeType != XmlNodeType.Element)
//            {
//                // TODO: Throw invalid operation exception
//            }
//            var propertyName = reader.LocalName;

//            if (!reader.Read())
//            {
//                // TODO: throw invalid operation exception
//            }
//            if (!this.TryGetProperty(propertyName, out var property))
//            {
//                // TODO: throw invalid operation exception
//            }
//            if (!property!.IsNullable && reader.NodeType == XmlNodeType.Text)
//            {
//                // TODO:throw invalid operation. Property is required.
//            }

//            var type = property!.Type.Definition;
//            var value = type.Read(reader);
//            var setter = property!.Setter;

//            setter.Invoke(instance, value);
//        }

//        return instance;
//    }

//    /// <inheritdoc />
//    public void Write(Utf8JsonWriter writer, object value)
//    {
//        throw new NotImplementedException();
//    }

//    /// <inheritdoc />
//    public void Write(XmlWriter writer, object value)
//    {
//        throw new NotImplementedException();
//    }

//    /// <summary>
//    /// 
//    /// </summary>
//    /// <param name="type"></param>
//    /// <param name="descriptor"></param>
//    /// <returns></returns>
//    /// <exception cref="ArgumentNullException"></exception>
//    public static GdmEntityType Create([DynamicallyAccessedMembers(
//        DynamicallyAccessedMemberTypes.PublicParameterlessConstructor |
//        DynamicallyAccessedMemberTypes.PublicProperties)] Type type, Action<IOGraphGdmEntityTypeDescriptor> descriptor)
//    {
//        return new GdmEntityType(type, descriptor);
//    }
//}
