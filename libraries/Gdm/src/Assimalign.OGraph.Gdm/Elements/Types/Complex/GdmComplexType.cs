using System;
using System.Xml;
using System.Text.Json;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm.Elements;

using Internal;
using System.Reflection;

public class GdmComplexType : GdmType, IOGraphGdmComplexType
{
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
    private readonly Type _runtimeType = default!;

    #region Constructors

    public GdmComplexType([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type runtimeType, GdmGraph graph) : this(runtimeType.Name, runtimeType, graph)
    {

    }
    public GdmComplexType(GdmName name, GdmGraph graph) : this(name, typeof(Dictionary<string, object>), graph)
    {

    }
    public GdmComplexType(GdmName name, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type runtimeType, GdmGraph graph) : base(name, graph)
    {
#pragma warning disable IL2074
        _runtimeType = AssertRuntimeType(runtimeType);
#pragma warning restore IL2074
    }

    #endregion

    #region Properties

    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
    public sealed override Type RuntimeType => _runtimeType;
    public sealed override GdmTypeKind Kind { get; } = GdmTypeKind.Entity;
    public GdmMemberCollection Members { get; } = new GdmMemberCollection();
    IOGraphGdmMemberCollection IOGraphGdmComplexType.Members => Members;

    #endregion

    #region Methods - Public

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
    public static GdmComplexType Create(GdmName name, GdmGraph graph)
    {
        return new GdmComplexType(name, typeof(Dictionary<GdmName, object>), graph);
    }

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
}
