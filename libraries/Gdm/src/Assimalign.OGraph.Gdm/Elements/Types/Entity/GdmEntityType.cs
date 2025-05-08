using System;
using System.Xml;
using System.Text.Json;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace Assimalign.OGraph.Gdm.Elements;

using Internal;

/// <summary>
/// 
/// </summary>
public class GdmEntityType : GdmType, IOGraphGdmEntityType
{
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
    private Type _runtimeType = typeof(Dictionary<GdmName, object>);
    private GdmEntityKey _key = default!;

    #region Constructors

    public GdmEntityType([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type runtimeType, GdmGraph graph) : this(runtimeType.Name, runtimeType, graph)
    {

    }
    public GdmEntityType(GdmName name, GdmGraph graph) : this(name, typeof(Dictionary<string, object>), graph)
    {

    }
    public GdmEntityType(GdmName name, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type runtimeType, GdmGraph graph) : base(name, graph)
    {
#pragma warning disable IL2074 
        _runtimeType = AssertRuntimeType(runtimeType);
#pragma warning restore IL2074 
    }

    #endregion

    #region Properties

    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
    public sealed override Type RuntimeType => _runtimeType;
    public GdmEntityKey Key => _key;
    public GdmMemberCollection Members { get; } = new GdmMemberCollection();
    public sealed override GdmTypeKind Kind { get; } = GdmTypeKind.Entity;
    IOGraphGdmEntityKey IOGraphGdmEntityType.Key => Key;
    IOGraphGdmMemberCollection IOGraphGdmComplexType.Members => Members;

    #endregion

    #region Methods - Public

    //public void AddKey(GdmName name, GdmType type)
    //{

    //}

    //public void AddProperty(GdmName name)
    //{
    //    ThrowHelper.ThrowIfNameEmpty(name);

    //    var propertyInfo = _runtimeType.GetProperty(name);

    //    if (propertyInfo is null)
    //    {
    //        ThrowHelper.ThrowInvalidOperationException("");
    //    }


    //}

    //public GdmProperty AddProperty(GdmName name, GdmType type)
    //{
    //    ThrowHelper.ThrowIfNameEmpty(name);
    //    ThrowHelper.ThrowIfNull(type);

    //    var property = new GdmProperty(name, type, this);

    //    Members.Add(property);

    //    return property;
    //}


    //public GdmProperty AddProperty(GdmName name, GdmType type, bool isNullable = false, bool isReadOnly = false)
    //{
    //    ThrowHelper.ThrowIfNameEmpty(name);
    //    ThrowHelper.ThrowIfNull(type);

    //    var property = new GdmProperty(
    //        name,
    //        type,
    //        this,
    //        isNullable,
    //        isReadOnly);

    //    Members.Add(property);

    //    return property;
    //}

    public override object Read(XmlReader reader)
    {
        if (reader.NodeType != XmlNodeType.Element)
        {
            // TODO: Throw invalid operation exception
        }
        if (reader.LocalName != Name)
        {
            // TODO: Throw invalid operation exception
        }

        var instance = Activator.CreateInstance(_runtimeType);

        if (instance is null)
        {
            throw new Exception();
        }

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

            var property = Members.GetProperty(propertyName);

            if (property is null)
            {
                throw new InvalidOperationException();
            }

            if (!property!.IsNullable && reader.NodeType == XmlNodeType.Text)
            {
                // TODO:throw invalid operation. Property is required.
            }

            var value = property.Type.Read(reader);

            property.Setter.Invoke(instance, value);
        }

        return instance;
    }
    public override object Read(ref Utf8JsonReader reader)
    {
        return base.Read(ref reader);
    }
    public override void Write(Utf8JsonWriter writer, object value)
    {
        base.Write(writer, value);
    }
    public override void Write(XmlWriter writer, object value)
    {
        base.Write(writer, value);
    }

    #endregion

    #region Methods - Internal

    internal void SetKey(GdmEntityKey key) => _key = ThrowHelper.ThrowIfNull(key);
    internal void SetRuntimeType([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type runtimeType) => ThrowHelper.ThrowIfNull(runtimeType);

    #endregion


    #region Methods - Private

    private static Type AssertRuntimeType([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type runtimeType)
    {
        ThrowHelper.ThrowIfNull(runtimeType);

        // Check for a Public Parameterless Constructor
        if (runtimeType.GetConstructor(BindingFlags.Public | BindingFlags.Instance, Type.EmptyTypes) is null)
        {
            ThrowHelper.ThrowArgumentException("The runtimeType must have a parameterless constructor.");
        }
        if (runtimeType.IsAbstract)
        {
            ThrowHelper.ThrowArgumentException("The runtimeType cannot be an abstract class.");
        }

        return runtimeType;
    }

    #endregion


    //private GdmEntityKey GenerateKey(GdmName name)
    //{
    //    var propertyInfo = _runtimeType.GetProperty(name, BindingFlags.Instance | BindingFlags.Public);

    //    if (propertyInfo is null)
    //    {
    //        ThrowHelper.ThrowArgumentException($"The key '{name}' does not exist.");
    //    }

    //    if (!propertyInfo.CanRead || !propertyInfo.CanWrite)
    //    {
    //        ThrowHelper.ThrowArgumentException($"The key '{name}' must be readable and writable.");
    //    }

    //    if (!propertyInfo.PropertyType.IsValueType)
    //    {
    //        ThrowHelper.ThrowArgumentException("");
    //    }

    //    GdmType? propertyType = default;

    //    foreach (var gdmType in (Graph.Types as IEnumerable<GdmType>).Where(p => p is IOGraphGdmScalarType))
    //    {
    //        //if (gdmType.IsOfType(propertyInfo.PropertyType))
    //        //{
    //        //    propertyType = gdmType;
    //        //}
    //    }

    //    if (propertyType is null)
    //    {
    //        throw new Exception();
    //    }

    //    var property = new GdmProperty(
    //        name,
    //        propertyType,
    //        this,
    //        propertyInfo.GetValue,
    //        propertyInfo.SetValue);

    //    Members.Add(property);

    //    return new GdmEntityKey(property);
    //}


    //public static GdmEntityType Create<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] T>(GdmName key, GdmGraph graph) where T : class, new()
    //{
    //    return new GdmEntityType(key, typeof(T), graph);
    //}

    //public static GdmEntityType<T> Create<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] T>(GdmName name, GdmName key, GdmGraph graph) 
    //    where T : class, new()
    //{
    //    return default;// new GdmEntityType<T>(name, key, typeof(T), graph);
    //}
}
