using System;
using System.Xml;
using System.Text.Json;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;
using System.Reflection;

namespace Assimalign.OGraph.Gdm.Elements;

using Internal;

public class GdmComplexType : GdmType, IOGraphGdmComplexType
{
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
    private readonly Type _runtimeType = default!;

    #region Constructors

    public GdmComplexType([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type runtimeType, GdmGraph graph) 
        : this(runtimeType.Name, runtimeType, graph)
    {

    }
    public GdmComplexType(GdmName name, GdmGraph graph) 
        : this(name, typeof(Dictionary<string, object>), graph)
    {

    }
    public GdmComplexType(GdmName name, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type runtimeType, GdmGraph graph) 
        : base(name, graph)
    {
#pragma warning disable IL2074
        _runtimeType = AssertRuntimeType(runtimeType);
#pragma warning restore IL2074
    }

    #endregion

    #region Properties

    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
    public sealed override Type RuntimeType => _runtimeType;
    public sealed override GdmTypeKind Kind { get; } = GdmTypeKind.Complex;
    public GdmMemberCollection Members { get; } = new GdmMemberCollection();
    IOGraphGdmMemberCollection IOGraphGdmComplexType.Members => Members;

    #endregion

    #region Methods - Public

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
        if (reader.TokenType != JsonTokenType.StartObject)
        {
            // TODO: throw invalid operation
        }

        // capture the depth
        var startDepth = reader.CurrentDepth;
        var instance = Activator.CreateInstance(_runtimeType);

        if (instance is null)
        {
            throw new Exception();
        }

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

            var property = Members.GetProperty(name);

            if (!property!.IsNullable && reader.TokenType == JsonTokenType.Null)
            {
                // TODO:throw invalid operation. Property is required.
            }

            var value = property.Type.Read(ref reader);
            var setter = property!.Setter;

            setter.Invoke(instance, value);
        }

        return instance;
    }
    public override void Write(Utf8JsonWriter writer, object value)
    {
        base.Write(writer, value);
    }
    public override void Write(XmlWriter writer, object value)
    {
        base.Write(writer, value);
    }
    public static GdmComplexType Create(GdmName name, GdmGraph graph)
    {
        return new GdmComplexType(name, typeof(Dictionary<GdmName, object>), graph);
    }

    #endregion

    #region Methods - Private

    private static Type AssertRuntimeType([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type runtimeType)
    {
        ThrowHelper.ThrowIfNull(runtimeType);

        //// Check for a Public Parameterless Constructor
        //if (runtimeType.GetConstructor(BindingFlags.Public | BindingFlags.Instance, Type.EmptyTypes) is null)
        //{
        //    ThrowHelper.ThrowArgumentException("The runtimeType must have a parameterless constructor.");
        //}
        //if (runtimeType.IsAbstract)
        //{
        //    ThrowHelper.ThrowArgumentException("The runtimeType cannot be an abstract class.");
        //}

        return runtimeType;
    }

    #endregion
}
