using System;
using System.Xml;
using System.Text.Json;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.OGraph.Gdm.Elements;

using Internal;

/// <summary>
/// 
/// </summary>
public abstract class GdmType : GdmNamedElement, IOGraphGdmType
{
    private GdmGraph _graph = default!;

    #region Constructors

    /*
        Notes:
            - Closing off the constructors to require Generic types so that the IOGraphGdmType.RuntimeType is known
              at compile time. This will support AOT compiling
     */
    internal GdmType() { }
    internal GdmType(GdmName name, GdmGraph graph) : base(name)
    {
        SetGraph(ThrowHelper.ThrowIfNull(graph));
    }

    #endregion

    #region Properties

    public GdmGraph Graph => _graph;
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
    public abstract Type RuntimeType { get; }
    public abstract GdmTypeKind Kind { get; }
    public bool IsPrimitive => this is GdmStringType or GdmBooleanType or GdmFloatType or GdmInt32Type;
    public sealed override GdmElementKind ElementKind { get; } = GdmElementKind.Type;
    IOGraphGdmMetaCollection IOGraphGdmElement.Meta => Meta;
    IOGraphGdmGraph IOGraphGdmType.Graph => Graph;
    // public bool IsPrimitive => this is GdmBooleanType or GdmStringType or GdmInt32Type or GdmDoubleType or GdmUuidType;

    #endregion

    #region Methods - Public

    public virtual object Read(ref Utf8JsonReader reader)
    {
        throw new NotImplementedException();
    }
    public virtual object Read(XmlReader reader)
    {
        throw new NotImplementedException();
    }
    public virtual void Write(Utf8JsonWriter writer, object value)
    {
        throw new NotImplementedException();
    }
    public virtual void Write(XmlWriter writer, object value)
    {
        throw new NotImplementedException();
    }
    public override IEnumerable<TElement> GetElements<TElement>()
    {
        if (this is TElement element)
        {
            yield return element;
        }
    }

    #endregion

    #region Methods - Internal

    internal virtual bool IsOfType(Type type)
    {
        return RuntimeType == type;
    }
    internal void SetGraph(GdmGraph graph)
    {
        if (_graph is null || !ReferenceEquals(_graph, graph))
        {
            _graph = graph;
        }
    }
    internal virtual void Configure() { }

    #endregion
}