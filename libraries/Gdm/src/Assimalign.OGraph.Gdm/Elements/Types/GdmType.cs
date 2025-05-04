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
public abstract class GdmType : GdmElement, IOGraphGdmType
{
    private GdmGraph _graph = default!;

    #region Constructors

    /*
        Notes:
            - Closing off the constructors to require Generic types so that the IOGraphGdmType.RuntimeType is known
              at compile time. This will support AOT compiling
     */
    internal GdmType() { }
    internal GdmType(GdmGraph graph)
    {
        _graph = ThrowHelper.ThrowIfNull(graph);
    }

    #endregion

    #region Properties
    public abstract GdmName Name { get; }
    public GdmGraph Graph => _graph;
    public abstract Type RuntimeType { get;  }
    public abstract GdmTypeKind Kind { get; }
    public sealed override GdmElementKind ElementKind { get; } = GdmElementKind.Type;
    public bool IsEntityType => Kind.Equals(GdmTypeKind.Entity);
    public bool IsComplexType => Kind.Equals(GdmTypeKind.Complex);
    public bool IsCollectionType => Kind.Equals(GdmTypeKind.Collection);
    public bool IsScalarType => Kind.Equals(GdmTypeKind.Scalar);
    public bool IsEnumType => Kind.Equals(GdmTypeKind.Enum);
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

    internal virtual void Initialize(GdmGraph graph)
    {
        _graph = graph;
        _graph.Types.Add(this);
    }
    internal virtual bool IsOfType(Type runtimeType)
    {
        return RuntimeType == runtimeType;
    }

    #endregion

}