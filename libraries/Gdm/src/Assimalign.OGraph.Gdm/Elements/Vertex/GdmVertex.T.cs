using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.OGraph.Gdm.Elements;

using Internal;

[DebuggerDisplay("{Label} [Vertex]")]
public abstract class GdmVertex<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] T> : GdmVertex
    where T : class, new()
{
    private readonly Action<GdmVertexDescriptor<T>> _configure;

    #region Constructors

    public GdmVertex(GdmGraph graph) : base(typeof(T).Name, new GdmEntityType(typeof(T), graph), graph)
    {
        _configure = Configure;
    }

    public GdmVertex(GdmLabel label, GdmEntityType<T> type, GdmGraph graph) 
        : base(label, type, graph)
    {
        _configure = Configure;
    }

    #endregion



    //#region Properties

    //public GdmLabel Label { get; }
    //public GdmGraph Graph { get; }
    //public GdmEntityType<T> Type { get; }
    //public GdmEdgeCollection Edges { get; } = new GdmEdgeCollection();


    //#endregion

    #region Methods

    /// <summary>
    /// 
    /// </summary>
    /// <param name="descriptor"></param>
    protected virtual void Configure(GdmVertexDescriptor<T> descriptor) { }

    #endregion

    internal override void Configure()
    {
        _configure.Invoke(new GdmVertexDescriptor<T>(this));
    }

}