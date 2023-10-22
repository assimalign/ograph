namespace Assimalign.OGraph;

public abstract class OGraphVertex<T> : OGraphVertex
    where T : class, new()
{
    public OGraphVertex()
    {
        base.type = new ComplexType<T>();
    }


    protected virtual void Configure(IOGraphVertexDescriptor<T> descriptor) { }
}