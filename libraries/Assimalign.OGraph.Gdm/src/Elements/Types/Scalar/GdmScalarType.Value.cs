using System;

namespace Assimalign.OGraph.Gdm.Elements;

public abstract class GdmValueScalarType<T> : GdmScalarType<T> where T : struct
{
    public GdmValueScalarType(GdmGraph graph) : base(typeof(T).Name, graph) { }
    public GdmValueScalarType(GdmName name, GdmGraph graph) : base(name, graph) { }
    internal override bool IsOfType(Type type)
    {
        return typeof(T) == type || typeof(T?) == type;
    }
}
