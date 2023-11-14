using System;

namespace Assimalign.OGraph;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
public sealed class OGraphVertexAttribute<TVertex> : Attribute
    where TVertex : IOGraphVertex, new()
{
    public OGraphVertexAttribute()
    {
        Vertex = new TVertex();
    }
    /// <summary>
    /// 
    /// </summary>
    public IOGraphVertex Vertex { get; }
}