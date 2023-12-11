using System;

namespace Assimalign.OGraph.Gdm.Internal;

internal class Instance : IDisposable
{
    private readonly InstancePool pool;

    public Instance(InstancePool pool)
    {
        this.pool = pool;
    }
    public object Value { get; }
    public void Dispose()
    {
        pool.Return(this);
    }
}