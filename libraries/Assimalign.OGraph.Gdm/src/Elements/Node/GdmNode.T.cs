using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.OGraph.Gdm.Elements;

using Internal;

[DebuggerDisplay("{Label} [Node]")]
public abstract class GdmNode<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] T> : GdmNode
{
    private readonly Action<GdmVertexDescriptor<T>> _configure;

    #region Constructors

    public GdmNode(GdmGraph graph) : base(typeof(T).Name, new GdmEntityType(typeof(T), graph), graph)
    {
        _configure = Configure;
    }

    public GdmNode(GdmName name, GdmEntityType<T> type, GdmGraph graph) 
        : base(name, type, graph)
    {
        _configure = Configure;
    }

    #endregion

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