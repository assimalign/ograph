using System;
using System.Xml;
using System.Text.Json;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Elements;

using Internal;


/// <summary>
/// 
/// </summary>
public class GdmEntityType : GdmType,
    IOGraphGdmEntityType
{
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
    private readonly Type _runtimeType = default!;
    private readonly GdmName _name;
    private readonly GdmEntityKey _key = default!;


    internal GdmEntityType() { }

    public GdmEntityType(GdmName key, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type runtimeType, GdmGraph graph) 
        : this(runtimeType.Name, key, runtimeType, graph)
    {
    }

    public GdmEntityType(GdmName name, GdmName key, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type runtimeType, GdmGraph graph) 
        : base(graph)
    {
        _name = name;
        _runtimeType = runtimeType;
        _key = GenerateKey(key);
    }

    public sealed override GdmName Name => _name;
    public GdmEntityKey Key => _key;
    public sealed override Type RuntimeType => _runtimeType;
    public GdmMemberCollection Members { get; } = new GdmMemberCollection();
    public sealed override GdmTypeKind Kind { get; } = GdmTypeKind.Entity;
    IOGraphGdmEntityKey IOGraphGdmEntityType.Key => Key;
    IOGraphGdmMemberCollection IOGraphGdmComplexType.Members => Members;


    public override object Read(XmlReader reader)
    {
        return base.Read(reader);
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

    private GdmEntityKey GenerateKey(GdmName name)
    {
        var propertyInfo = _runtimeType.GetProperty(name, BindingFlags.Instance | BindingFlags.Public);

        if (propertyInfo is null)
        {
            ThrowHelper.ThrowArgumentException($"The key '{name}' does not exist.");
        }

        if (!propertyInfo.CanRead || !propertyInfo.CanWrite)
        {
            ThrowHelper.ThrowArgumentException($"The key '{name}' must be readable and writable.");
        }

        if (!propertyInfo.PropertyType.IsValueType)
        {
            ThrowHelper.ThrowArgumentException("");
        }

        GdmType? propertyType = default;

        foreach (var gdmType in (Graph.Types as IEnumerable<GdmType>).Where(p => p is IOGraphGdmScalarType))
        {
            if (gdmType.IsOfType(propertyInfo.PropertyType))
            {
                propertyType = gdmType;
            }
        }

        if (propertyType is null)
        {
            throw new Exception();
        }

        var property = new GdmProperty(name, propertyType, this, propertyInfo.GetValue, propertyInfo.SetValue);

        Members.Add(property);

        return new GdmEntityKey(property);
    }

    public static GdmEntityType Create<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] T>(GdmName key, GdmGraph graph) where T : class, new()
    {
        return new GdmEntityType(key, typeof(T), graph);
    }

    public static GdmEntityType Create<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] T>(GdmName name, GdmName key, GdmGraph graph) where T : class, new()
    {
        return new GdmEntityType(name, key, typeof(T), graph);
    }
}
