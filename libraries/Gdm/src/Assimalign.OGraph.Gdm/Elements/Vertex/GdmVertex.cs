using System;
using System.Diagnostics;

namespace Assimalign.OGraph.Gdm.Elements;

using Internal;

[DebuggerDisplay("{Label} [Vertex]")]
public class GdmVertex : GdmElement, IOGraphGdmVertex
{
    #region Constructors
    internal GdmVertex()
    {
        //var lazy = new Lazy<GdmType>(() =>
        //{
        //    return default;
        //});
    }

    public GdmVertex(GdmLabel label, GdmEntityType type, GdmGraph graph) 
    {
        Label = label;
        Type = ThrowHelper.ThrowIfNull(type);
        Graph = ThrowHelper.ThrowIfNull(graph);
    }

    #endregion

    #region Properties

    public GdmLabel Label { get;  }
    public GdmEntityType Type { get; } 
    public GdmGraph Graph { get; } 
    public GdmEdgeCollection Edges { get; } = new GdmEdgeCollection();
    public sealed override GdmElementKind ElementKind { get; } = GdmElementKind.Vertex;
    IOGraphGdmType IOGraphGdmVertex.Type => Type;
    IOGraphGdmMetaCollection IOGraphGdmElement.Meta => Meta;
    IOGraphGdmEdgeCollection IOGraphGdmVertex.Edges => Edges;
    IOGraphGdmGraph IOGraphGdmVertex.Graph => Graph;
    IOGraphGdmPathCollection IOGraphGdmVertex.Paths => throw new NotImplementedException();

    #endregion

    #region Methods

    /// <summary>
    /// 
    /// </summary>
    /// <param name="descriptor"></param>
   // protected virtual void Configure(GdmVertexDescriptor descriptor) { }

    #endregion
}


//[DebuggerDisplay("Vertex = {Label}")]
//public class GdmVertex : IOGraphGdmVertex
//{
//    private readonly Action<IOGraphGdmVertexDescriptor> configure;

//    internal Label label;
//    internal IOGraphGdmType? type;
//    internal IOGraphGdmGraph? graph;

//    public GdmVertex() : this(descriptor => { }) { }
//    GdmVertex(Action<IOGraphGdmVertexDescriptor> configure)
//    {
//        if (configure is null)
//        {
//            ThrowHelper.ThrowArgumentNullException(nameof(configure));
//        }
//        this.configure = configure;
//        this.Configure(new GdmVertexDescriptor(this));
//    }

//    /// <summary>
//    /// 
//    /// </summary>
//    /// <param name="descriptor"></param>
//    protected virtual void Configure(IOGraphGdmVertexDescriptor descriptor)
//    {
//        configure.Invoke(descriptor);
//    }

//    public Label Label => label;
//    public IOGraphGdmType Type => type!;
//    public IOGraphGdmEdgeCollection Edges { get; } = new GdmEdgeCollection();
//    public IOGraphGdmOperationCollection Operations { get; } = new GdmVertexOperationCollection();
//    public IOGraphGdmMetadata Meta { get; } = new GdmMetadata();
//    public IOGraphGdmGraph Graph => graph!;
//    public GdmElementKind ElementKind => GdmElementKind.Vertex;

//    #region Static Members
//    /// <summary>
//    /// Creates a Vertex from a configurable method.
//    /// </summary>
//    /// <param name="configure"></param>
//    /// <returns></returns>
//    /// <exception cref="ArgumentNullException"></exception>
//    public static GdmVertex Create(Action<IOGraphGdmVertexDescriptor> configure)
//    {
//        return new GdmVertex(configure);
//    }
//    #endregion

//    #region Overload Members

//    /// <inheritdoc />
//    public override string ToString()
//    {
//        return Label;
//    }

//    /// <inheritdoc />
//    public override int GetHashCode()
//    {
//        return HashCode.Combine(Label, typeof(IOGraphGdmVertex));
//    }

//    /// <inheritdoc />
//    public override bool Equals(object? instance)
//    {
//        if (instance is not null)
//        {
//            return GetHashCode() == instance.GetHashCode();
//        }
//        return false;
//    }

//    #endregion
//}
